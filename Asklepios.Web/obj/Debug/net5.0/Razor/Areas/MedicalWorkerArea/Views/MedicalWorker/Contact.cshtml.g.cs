#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6ecf23666dc85487a351f64e7ce79cd3d64ae41"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_MedicalWorkerArea_Views_MedicalWorker_Contact), @"mvc.1.0.view", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/Contact.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6ecf23666dc85487a351f64e7ce79cd3d64ae41", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/Contact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_MedicalWorkerArea_Views_MedicalWorker_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Web.Areas.HomeArea.Models.ContactMessageViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Contact", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"d-flex flex-column .align-items-stretch p-5\">\r\n    <h2>Formularz kontaktowy - uwagi odnośnie portalu</h2>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6ecf23666dc85487a351f64e7ce79cd3d64ae415748", async() => {
                WriteLiteral("\r\n        <div class=\"form-group\">\r\n            <label for=\"inputState\">Temat</label>\r\n");
                WriteLiteral("            ");
#nullable restore
#line 13 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
       Write(Html.DropDownListFor(a => a.Subject, new[]
{
        new SelectListItem() {Text = "Zgłoszenie błędu na portalu pacjenta"},
        new SelectListItem() {Text = "Propozycja usprawnień portalu pacjenta" },
        new SelectListItem() {Text = "Opinia na temat usług" },
        new SelectListItem() {Text = "Zapytanie dotyczące usług medycznych" },
        new SelectListItem() {Text = "Inne..." },
        }, "Wybierz temat...", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6ecf23666dc85487a351f64e7ce79cd3d64ae416874", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 21 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Subject);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"inputAddress\">Wiadomość</label>\r\n");
                WriteLiteral("\r\n            ");
#nullable restore
#line 28 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
       Write(Html.TextAreaFor(m => m.Message, 10, 1, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6ecf23666dc85487a351f64e7ce79cd3d64ae419147", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 29 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Message);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n        </div>\r\n");
                WriteLiteral(@"        <button type=""submit"" class=""btn btn-primary btn-icon-split w-100"">
            <span class=""icon text-white-50 left"" style=""text-align:left;float:left"">
                <i class=""fas fa-forward""></i>
            </span>

            <span class=""text"" style=""float: left"">Wyślij!</span>
        </button>

");
#nullable restore
#line 41 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
         if (Model.HasInfoMessage)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
             if (Model.HasInfoMessage)
            {
                if (Model.AlertMessageType == Asklepios.Web.Enums.AlertMessageType.Info)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <div class=\"messages\" style=\"margin-top:10px;text-align:center\">\r\n                        <p class=\"alert alert-success\">\r\n                            ");
#nullable restore
#line 49 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
                       Write(Model.AlertMessage);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </p>\r\n                    </div>\r\n");
#nullable restore
#line 52 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"

                }
                else
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <div class=\"messages\" style=\"margin-top: 10px; text-align: center \">\r\n                        <p class=\"alert alert-danger\">\r\n                            ");
#nullable restore
#line 58 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
                       Write(Model.AlertMessage);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                        </p>\r\n                    </div>\r\n");
#nullable restore
#line 61 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"

                }
            }
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\Contact.cshtml"
                                              
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Web.Areas.HomeArea.Models.ContactMessageViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
