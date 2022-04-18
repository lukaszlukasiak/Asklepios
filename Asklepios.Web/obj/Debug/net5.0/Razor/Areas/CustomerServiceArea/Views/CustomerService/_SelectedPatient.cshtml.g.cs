#pragma checksum "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d7e446db9e554937a512cc1e4d1a76b94c9a7f5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_CustomerServiceArea_Views_CustomerService__SelectedPatient), @"mvc.1.0.view", @"/Areas/CustomerServiceArea/Views/CustomerService/_SelectedPatient.cshtml")]
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
#line 1 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Core.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web.ServiceClasses;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d7e446db9e554937a512cc1e4d1a76b94c9a7f5", @"/Areas/CustomerServiceArea/Views/CustomerService/_SelectedPatient.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_CustomerServiceArea_Views_CustomerService__SelectedPatient : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Patient>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "CustomerServiceArea", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "CustomerService", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeselectPatient", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-icon-split "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<div class=""d-flex jumbotron flex-column"" style="" padding:20px;"">
    <div class=""d-flex mb-3 flex-wrap align-content-stretch"" style=""text-align:center"">
        <div class=""p-2  pt-3 align-content-center align-items-center align-self-center mx-auto flex-grow-1"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-fill  justify-content-center"" style=""text-align:center"">
");
            WriteLiteral("                <div class=\"fas fa-portrait .bg-gradient-dark\" style=\"font-size: 15em; color: #4e73df\" ></div>\r\n\r\n");
            WriteLiteral("            </div>\r\n        </div>\r\n        <div class=\"p-2 flex-grow-1 pt-3 m-auto flex-grow-1\" style=\"width:400px;text-align:center\">\r\n            <h3 style=\"font-weight:900;\">\r\n                ");
#nullable restore
#line 14 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
           Write(Model.Person.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h3>\r\n            <br>\r\n            <h3 style=\"font-weight:900;\">\r\n                Pakiet: ");
#nullable restore
#line 18 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
                   Write(Model.MedicalPackage.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h3>\r\n            <h3 style=\"font-weight:900;\">\r\n                ");
#nullable restore
#line 21 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
           Write(Model.NFZUnit.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h3>\r\n");
#nullable restore
#line 23 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
             if (Model.Person.HasPolishCitizenship)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h3 style=\"font-weight:900;\">\r\n                    PESEL: ");
#nullable restore
#line 26 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
                      Write(Model.Person.PESEL);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n");
#nullable restore
#line 28 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"

            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h3 style=\"font-weight:900;\">\r\n                    Nr paszportu ");
#nullable restore
#line 33 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
                            Write(Model.Person.PassportNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n");
#nullable restore
#line 35 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <h3 style=\"font-weight:900;\">\r\n                Data urodzin: ");
#nullable restore
#line 37 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_SelectedPatient.cshtml"
                         Write(Model.Person.BirthDate.Value.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h3>\r\n\r\n\r\n        </div>\r\n\r\n        <div class=\"d-flex p-2 pt-3 m-auto flex-grow-1 flex-fill\" style=\"width:400px;text-align:center\">\r\n            <div class=\"d-flex flex-column flex-grow-1\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9d7e446db9e554937a512cc1e4d1a76b94c9a7f59219", async() => {
                WriteLiteral(@"
                    <span class=""icon text-white-50 left"" style=""text-align:left;float:left"">
                        <i class=""fas fa-undo""></i>
                    </span>
                    <span class=""text"" style=""float: left"">Cofnij wybór</span>
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
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.Patient> Html { get; private set; }
    }
}
#pragma warning restore 1591
