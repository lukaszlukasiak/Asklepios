#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9f649ac8d75aec1d76bdd3fde2582dc67557f076"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_PatientArea_Views_Patient__PlannedVisit), @"mvc.1.0.view", @"/Areas/PatientArea/Views/Patient/_PlannedVisit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9f649ac8d75aec1d76bdd3fde2582dc67557f076", @"/Areas/PatientArea/Views/Patient/_PlannedVisit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_PatientArea_Views_Patient__PlannedVisit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Visit>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "PatientArea", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patient", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RescheduleVisit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-icon-split "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ResignFromVisit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-icon-split mt-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"
<div class=""d-flex jumbotron w-80 flex-column"" style="" padding:20px;display:flex;align-content:center;position:relative"">
    <div class=""d-flex mb-3 flex-wrap  align-items-center"">
        <div class=""p-2   align-content-center align-items-center align-self-center mx-auto"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-fill flex-column justify-content-center"" style=""text-align:center"">
                <h3 style=""font-weight:900;"">
                    ");
#nullable restore
#line 8 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
               Write(Model.MedicalWorker.FullProffesionalName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n                <div class=\"d-flex align-content-center mr-auto pt-2\" style=\"text-align:center\">\r\n                    <img");
            BeginWriteAttribute("alt", " alt=\"", 716, "\"", 763, 1);
#nullable restore
#line 11 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
WriteAttributeValue("", 722, Model.MedicalWorker.FullProffesionalName, 722, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 764, "\"", 800, 1);
#nullable restore
#line 11 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
WriteAttributeValue("", 770, Model.MedicalWorker.ImagePath, 770, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""mx-auto d-block""  style=""display: block; max-height:50% ; max-width:50%;
        border-radius: 6px; "">
                </div>
                
            </div>
        </div>
        <div class=""p-2 flex-grow-1 pt-3 m-auto flex-grow-1"" style=""width:400px;text-align:center"">
            <h5 style=""font-weight:900;"">
                ");
#nullable restore
#line 19 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
           Write(Model.GetVisitDateDescription());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h5>\r\n            <br>\r\n            <h5>\r\n                ");
#nullable restore
#line 23 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
           Write(Model.Location.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h5>\r\n\r\n            <h5>\r\n                ");
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
           Write(Model.VisitCategory.CategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
                                               Write(Model.PrimaryService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </h5>\r\n");
#nullable restore
#line 30 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
             if (Model.IsNotCompletedMedicalTest)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h5>\r\n                    \"W realizacji...\"\r\n                </h5>\r\n");
#nullable restore
#line 35 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"

            }

            

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"p-2 pt-3 m-auto flex-grow-1\" style=\"width:400px;\">\r\n            <div class=\"d-flex flex-column align-content-end justify-content-evenly m-auto\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9f649ac8d75aec1d76bdd3fde2582dc67557f0769801", async() => {
                WriteLiteral(@"
                    <span class=""icon text-white-50 left"" style=""text-align:left;float:left"">
                        <i class=""fas fa-address-book""></i>
                    </span>
                    <span class=""text"" style=""float: left"">Przełóż wizytę</span>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 56 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
                                                                                                  WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9f649ac8d75aec1d76bdd3fde2582dc67557f07612831", async() => {
                WriteLiteral(@"
                    <span class=""icon text-white-50 left"" style=""text-align:left;float:left"">
                        <i class=""fas fa-address-book""></i>
                    </span>
                    <span class=""text"" style=""float: left"">Zrezygnuj z wizyty</span>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 62 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_PlannedVisit.cshtml"
                                                                                                  WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.Visit> Html { get; private set; }
    }
}
#pragma warning restore 1591
