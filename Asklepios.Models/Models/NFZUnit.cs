using Asklepios.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asklepios.Core.Models
{
    public class NFZUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        public string Description { get; set; }
        
        public string Code { get; set; }
        public VoivodeshipType Voivodeship { get; set; }
    }
}
