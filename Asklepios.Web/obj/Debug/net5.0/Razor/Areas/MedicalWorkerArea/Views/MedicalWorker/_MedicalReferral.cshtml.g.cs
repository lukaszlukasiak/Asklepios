#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3bc4f33e486ebdb8ec2d340ce41e0005589a1050"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_MedicalWorkerArea_Views_MedicalWorker__MedicalReferral), @"mvc.1.0.view", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/_MedicalReferral.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Core.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web.ServiceClasses;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bc4f33e486ebdb8ec2d340ce41e0005589a1050", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/_MedicalReferral.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_MedicalWorkerArea_Views_MedicalWorker__MedicalReferral : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.MedicalReferral>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MedicalWorkerDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "MedicalWorker", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""d-flex jumbotron w-100 flex-column"" style=""padding:20px;align-content:center;"">
    <div class=""d-flex flex-row justify-content-start align-items-end  flex-wrap "" style=""text-align:center"">

        <div class=""d-flex p-2   align-content-center align-items-center align-self-center mx-auto"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-fill flex-column justify-content-center"" style=""text-align:center"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3bc4f33e486ebdb8ec2d340ce41e0005589a10504807", async() => {
                WriteLiteral("\r\n");
                WriteLiteral("                    <h3 style=\"font-weight:900;\">\r\n                        ");
#nullable restore
#line 10 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                   Write(Model.VisitWhenIssued.MedicalWorker.FullProffesionalName);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </h3>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 7 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                                                                                      WriteLiteral(Model.VisitWhenIssued.MedicalWorker.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <div class=\"d-flex align-content-center mr-auto pt-2\" style=\"text-align:center\">\r\n                    <img");
            BeginWriteAttribute("alt", " alt=\"", 992, "\"", 1055, 1);
#nullable restore
#line 14 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
WriteAttributeValue("", 998, Model.VisitWhenIssued.MedicalWorker.FullProffesionalName, 998, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 1056, "\"", 1117, 1);
#nullable restore
#line 14 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
WriteAttributeValue("", 1062, Model.VisitWhenIssued.MedicalWorker.Person.ImageSource, 1062, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"mx-auto d-block\" style=\"display: block; max-height:50% ; max-width:50%;\r\n        border-radius: 6px; \">\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"d-flex p-2  pt-3 m-auto flex-grow-1\" style=\"width:400px;text-align:center\">\r\n            <div class=\"d-flex flex-fill pt-2 flex-column\">\r\n                <h2>\r\n                    Us??uga: ");
#nullable restore
#line 25 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                       Write(Model.PrimaryMedicalService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 26 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                     if (Model.MinorMedicalService != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span>\r\n                            Podus??uga: ");
#nullable restore
#line 29 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                                  Write(Model.MinorMedicalService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </span>\r\n");
#nullable restore
#line 31 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"

                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h2>\r\n\r\n                <div>\r\n                    Wystawione: ");
#nullable restore
#line 37 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                           Write(Model.IssueDate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div>\r\n                    Wa??ne do: ");
#nullable restore
#line 40 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                         Write(Model.ExpireDate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 42 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                  
                    if (Model.HasBeenUsed)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>\r\n                        Skierowanie zosta??o wykorzystane\r\n                    </p>\r\n");
#nullable restore
#line 47 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                    }
                    else if (Model.HasExpired)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p>Wa??no???? skierowania up??yn????a</p>\r\n");
#nullable restore
#line 51 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_MedicalReferral.cshtml"
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n");
            WriteLiteral("\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.MedicalReferral> Html { get; private set; }
    }
}
#pragma warning restore 1591
