"""Prepare a cleaned mirror without rewriting the working repository or pushing.

Run with Python 3.11+ from the repository root. All private output goes under
artifacts/repo-cleanup. The bundle, manifests, and replacements contain private
data and must never be published. Re-running refuses to overwrite the mirror.
"""

from __future__ import annotations

import argparse
import hashlib
import json
import re
import subprocess
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parents[2]
OUTPUT = ROOT / "artifacts" / "repo-cleanup"


def git(*args: str, cwd: Path = ROOT, data: bytes | None = None) -> bytes:
    result = subprocess.run(
        ["git", *args], cwd=cwd, input=data, capture_output=True, check=False
    )
    if result.returncode:
        # Avoid printing stderr: tools can echo sensitive paths or content.
        raise RuntimeError(f"git {args[0]} failed with exit {result.returncode}")
    return result.stdout


def save_json(path: Path, value: object) -> None:
    path.write_text(json.dumps(value, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")


def inventory() -> None:
    OUTPUT.mkdir(parents=True, exist_ok=True)
    manifest_path = OUTPUT / "local-files.json"
    if manifest_path.exists():
        raise RuntimeError("Inventory already exists; preserve it instead of overwriting it")

    tracked = git("ls-files", "-z").split(b"\0")
    tracked = [path for path in tracked if path]
    excluded = git("ls-files", "-ci", "--exclude-standard", "-z").split(b"\0")
    excluded = [path for path in excluded if path]
    if not excluded:
        raise RuntimeError("No tracked ignored files found")

    files = []
    for encoded in excluded:
        relative = encoded.decode("utf-8")
        path = ROOT / relative
        if not path.is_file():
            raise RuntimeError("An excluded local file is missing; resolve before cleanup")
        with path.open("rb") as stream:
            checksum = hashlib.file_digest(stream, "sha256").hexdigest()
        files.append({"path": relative, "sha256": checksum, "size": path.stat().st_size})

    OUTPUT.joinpath("untrack-paths.nul").write_bytes(b"\0".join(excluded) + b"\0")
    save_json(manifest_path, files)

    historical = git("log", "--all", "--format=", "--name-only", "-z").split(b"\0")
    all_paths = set(tracked)
    all_paths.update(path.lstrip(b"\r\n") for path in historical if path.strip(b"\r\n"))
    check = subprocess.run(
        ["git", "check-ignore", "--no-index", "-z", "--stdin"],
        cwd=ROOT,
        input=b"\0".join(sorted(all_paths)) + b"\0",
        capture_output=True,
        check=False,
    )
    if check.returncode not in (0, 1):
        raise RuntimeError("Cannot evaluate historical paths against .gitignore")
    ignored = sorted(path for path in check.stdout.split(b"\0") if path)
    if any(b"\n" in path or b"\r" in path for path in ignored):
        raise RuntimeError("Cannot safely serialize a historical filename")
    OUTPUT.joinpath("history-exclusions.txt").write_bytes(
        b"\n".join(b"literal:" + path for path in ignored) + b"\n"
    )

    # Extract exact credentials privately, including previous config versions.
    # Only actual configuration keys are interpreted; source code is scanned
    # separately by Gitleaks after filtering out unwanted paths.
    secrets: set[str] = set()
    objects = git("rev-list", "--objects", "--all").splitlines()
    config_blobs = set()
    for entry in objects:
        object_id, _, name = entry.partition(b" ")
        if re.search(rb"(^|/)appsettings(?:\.[^/]*)?\.json$", name, re.I):
            config_blobs.add(object_id.decode("ascii"))
    for object_id in config_blobs:
        raw = git("cat-file", "blob", object_id)
        try:
            config = json.loads(raw.decode("utf-8-sig"))
        except (UnicodeDecodeError, json.JSONDecodeError):
            continue
        if not isinstance(config, dict):
            continue
        for connection in config.get("ConnectionStrings", {}).values():
            if not isinstance(connection, str):
                continue
            for match in re.finditer(
                r"(?i)(?:^|;)\s*(?:Password|Pwd)\s*=\s*(?:\"([^\"]*)\"|'([^']*)'|([^;]*))",
                connection,
            ):
                secret = next(value for value in match.groups() if value is not None).strip()
                if secret:
                    secrets.add(secret)
        mail = config.get("Email", {})
        if isinstance(mail, dict) and isinstance(mail.get("MAIL_PASS"), str) and mail["MAIL_PASS"]:
            secrets.add(mail["MAIL_PASS"])

    # An exact literal substitution is deliberately avoided for very short
    # credentials, which could corrupt ordinary source code. Their config
    # files are still removed from every commit.
    replacements = []
    for secret in sorted(secrets, key=lambda value: (-len(value), value)):
        if len(secret) >= 8 and not any(part in secret for part in ("\n", "\r", "==>")):
            replacements.append(b"literal:" + secret.encode("utf-8") + b"==>REMOVED_CREDENTIAL")
    OUTPUT.joinpath("private-replacements.txt").write_bytes(b"\n".join(replacements) + b"\n")
    save_json(OUTPUT / "credential-fingerprints.json", [
        {"sha256": hashlib.sha256(secret.encode("utf-8")).hexdigest(), "length": len(secret)}
        for secret in sorted(secrets)
    ])
    save_json(OUTPUT / "inventory-summary.json", {
        "local_files_to_untrack": len(excluded),
        "historical_paths_to_remove": len(ignored),
        "configuration_credentials_found": len(secrets),
        "literal_replacements": len(replacements),
        "original_head": git("rev-parse", "HEAD").decode().strip(),
    })
    print(f"Inventoried {len(excluded)} local files and {len(ignored)} historical paths; values not displayed")


def verify_local_files() -> None:
    files = json.loads((OUTPUT / "local-files.json").read_text(encoding="utf-8"))
    for item in files:
        with (ROOT / item["path"]).open("rb") as stream:
            checksum = hashlib.file_digest(stream, "sha256").hexdigest()
        if checksum != item["sha256"]:
            raise RuntimeError("A preserved local file changed after inventory")
    print(f"Verified: all {len(files)} local files remain byte-for-byte unchanged")


def mirror(filter_repo: Path) -> None:
    mirror_path = OUTPUT / "clean.git"
    if mirror_path.exists():
        raise RuntimeError("Clean mirror already exists; do not overwrite it")
    if not filter_repo.is_file():
        raise RuntimeError("Provide the official git-filter-repo Python script")
    git("clone", "--mirror", "--no-local", str(ROOT), str(mirror_path))
    # Tool checkpoints and remote-tracking refs are private implementation
    # details. Only actual local branches and tags belong in a public mirror.
    refs = git("for-each-ref", "--format=%(refname) %(objectname)", cwd=mirror_path)
    for line in refs.decode("utf-8").splitlines():
        ref, object_id = line.split(" ", 1)
        if not ref.startswith(("refs/heads/", "refs/tags/")):
            git("update-ref", "-d", ref, object_id, cwd=mirror_path)
    log_path = OUTPUT / "filter-repo.log"
    with log_path.open("wb") as log:
        result = subprocess.run([
            sys.executable, str(filter_repo.resolve()),
            "--sensitive-data-removal", "--no-fetch", "--invert-paths",
            "--paths-from-file", str(OUTPUT / "history-exclusions.txt"),
            "--replace-text", str(OUTPUT / "private-replacements.txt"),
            "--replace-message", str(OUTPUT / "private-replacements.txt"),
        ], cwd=mirror_path, stdout=log, stderr=subprocess.STDOUT, check=False)
    if result.returncode:
        raise RuntimeError("Filtering failed; review the private log locally")
    git("fsck", "--full", cwd=mirror_path)
    print("Prepared and checked artifacts/repo-cleanup/clean.git; no remote push performed")


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("action", choices=("inventory", "verify-local-files", "mirror"))
    parser.add_argument("--filter-repo", type=Path)
    args = parser.parse_args()
    if args.action == "inventory":
        inventory()
    elif args.action == "verify-local-files":
        verify_local_files()
    else:
        if not args.filter_repo:
            parser.error("mirror requires --filter-repo")
        mirror(args.filter_repo)


if __name__ == "__main__":
    main()
