#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec59b11dd1cb0527e80a2a9dc4d101382df0b730"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_MedicalWorkerArea_Views_MedicalWorker_PatientDetails), @"mvc.1.0.view", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/PatientDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec59b11dd1cb0527e80a2a9dc4d101382df0b730", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/PatientDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_MedicalWorkerArea_Views_MedicalWorker_PatientDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Patient>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("control-label"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
  
    //SelectList medicalPackagesList = new SelectList(Model.MedicalPackages, "Id", "Name");
    //SelectList nfzUnits = new SelectList(Model.NFZUnits, "Id", "Description");
    //Model.Person.HasPolishCitizenship = true;

    //Asklepios.Web.Areas.AdministrativeArea.Models.ViewMode editViewMode = Asklepios.Web.Areas.AdministrativeArea.Models.ViewMode.Edit;
    //Asklepios.Web.Areas.AdministrativeArea.Models.ViewMode removeViewMode = Asklepios.Web.Areas.AdministrativeArea.Models.ViewMode.Remove;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"d-flex flex-column\">\r\n    <div class=\"d-flex flex-column align-content-around  m-2\">\r\n\r\n\r\n        <div class=\"d-flex flex-fill  justify-content-center mt-2\" style=\"text-align:center\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 764, "\"", 795, 1);
#nullable restore
#line 15 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
WriteAttributeValue("", 770, Model.Person.ImageSource, 770, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid\" style=\"display: block; max-height:300px;\r\n        border-radius: 6px; \">\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"form-group d-flex  flex-wrap\">\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b7305809", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 29 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.Name);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 30 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b7307899", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 34 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.Surname);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 35 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.Surname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b7309995", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 39 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.Gender);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 40 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.Gender.GetDescription());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73012106", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 44 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.DefaultAglomeration);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 45 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.DefaultAglomeration);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73014227", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 49 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.PhoneNumber);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 50 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73016332", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 54 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.BirthDate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 55 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.BirthDate.Value.Date.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73018464", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 59 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.PESEL);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 60 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.PESEL);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73020557", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 64 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.PassportNumber);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 65 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.PassportNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73022668", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 69 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.PassportCode);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 70 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.PassportCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73024775", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 74 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Person.BirthDate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 75 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.Person.BirthDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73026876", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 79 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.NFZUnit);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 80 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.NFZUnit.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n        <div class=\"d-flex flex-fill align-content-xl-around form-control border-info alert-info m-2\" style=\"width:400px\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ec59b11dd1cb0527e80a2a9dc4d101382df0b73028971", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 84 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.MedicalPackage);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"flex-grow-1 text-right\">");
#nullable restore
#line 85 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
                                            Write(Model.MedicalPackage.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n        </div>\r\n    </div>\r\n\r\n");
#nullable restore
#line 90 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
      
        if (Model.TestsResults != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                Wyniki badań\r\n            </div>\r\n");
#nullable restore
#line 96 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
            foreach (var item in Model.TestsResults)
            {

            }

        }
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 103 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
      
        if (Model.MedicalReferrals != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                Skierowania\r\n            </div>\r\n");
#nullable restore
#line 109 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
            foreach (var item in Model.MedicalReferrals)
            {

            }

        }
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 116 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
      
        if (Model.HistoricalVisits != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                Wizyty\r\n            </div>\r\n");
#nullable restore
#line 122 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\PatientDetails.cshtml"
            foreach (var item in Model.HistoricalVisits)
            {

            }

        }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n</div>\r\n<script>\r\n    $(\'#MessageSpan\').delay(5000).fadeOut(300);\r\n</script>\r\n");
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
