#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6a60fdd1cd3a3eb09465a4ae1b2bcd4fca4d3f7b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdministrativeArea_Views_Administrative__Patient), @"mvc.1.0.view", @"/Areas/AdministrativeArea/Views/Administrative/_Patient.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6a60fdd1cd3a3eb09465a4ae1b2bcd4fca4d3f7b", @"/Areas/AdministrativeArea/Views/Administrative/_Patient.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_AdministrativeArea_Views_Administrative__Patient : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Web.Areas.AdministrativeArea.Models.PatientDetailsViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PatientDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Administrative", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
<div class=""d-flex jumbotron flex-column"" style="" padding:20px;"">
    <div class=""d-flex mb-3 flex-wrap align-content-around align-items-center"" style=""text-align:center"">
        <div class=""p-2  pt-3 align-content-center align-items-center align-self-center mx-auto"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-fill  justify-content-center"" style=""text-align:center"">
                <img");
            BeginWriteAttribute("alt", " alt=\"", 506, "\"", 549, 1);
#nullable restore
#line 7 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
WriteAttributeValue("", 512, Model.CurrentPatient.Person.FullName, 512, 37, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 550, "\"", 660, 1);
#nullable restore
#line 7 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
WriteAttributeValue("", 556, Model.CurrentPatient.Person.ReturnProperImageFilePath(Common.FEMALE_IMAGE_PATH, Common.MALE_IMAGE_PATH), 556, 104, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid"" style=""display: block; max-height:300px;
        border-radius: 6px; "">
            </div>
        </div>
        <div class=""p-2 d-flex pt-3 m-auto flex-grow-1 flex-column"" style=""width:400px;text-align:center"">
            <h3 style=""font-weight:900;"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6a60fdd1cd3a3eb09465a4ae1b2bcd4fca4d3f7b5940", async() => {
                WriteLiteral("\r\n\r\n                    ");
#nullable restore
#line 15 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.FullName);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                ");
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
#line 13 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
                                                                                 WriteLiteral(Model.CurrentPatient.Id);

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
            WriteLiteral("\r\n            </h3>\r\n            <h5>Id: ");
#nullable restore
#line 18 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 19 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.MedicalPackage.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 20 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.NFZUnit.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 21 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.EmailAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 22 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 23 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 24 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.PESEL);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 25 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.PassportNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 26 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.DefaultAglomeration);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n\r\n            <h5>Id: ");
#nullable restore
#line 28 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.BirthDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h5>Id: ");
#nullable restore
#line 29 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\_Patient.cshtml"
               Write(Model.CurrentPatient.Person.HasPolishCitizenship);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n\r\n            <br>\r\n\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Web.Areas.AdministrativeArea.Models.PatientDetailsViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
