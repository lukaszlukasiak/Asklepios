#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "75d44024714999fd092dbfb4cf3a0e8d11befb71"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdministrativeArea_Views_Administrative_MedicalWorkerDetails), @"mvc.1.0.view", @"/Areas/AdministrativeArea/Views/Administrative/MedicalWorkerDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75d44024714999fd092dbfb4cf3a0e8d11befb71", @"/Areas/AdministrativeArea/Views/Administrative/MedicalWorkerDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_AdministrativeArea_Views_Administrative_MedicalWorkerDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.MedicalWorker>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Stars", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_MedicalReview", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral(@"<div class=""d-flex  flex-column jumbotron w-100 h-auto align-content-between"" style="" padding:20px;"">
    <div class=""d-flex mb-3 flex-wrap  align-items-center"">
        <div class=""p-2  pt-3 align-content-center align-items-center align-self-center mx-auto"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-fill  justify-content-center"" style=""text-align:center"">
                <img");
            BeginWriteAttribute("alt", " alt=\"", 462, "\"", 495, 1);
#nullable restore
#line 7 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
WriteAttributeValue("", 468, Model.FullProffesionalName, 468, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 496, "\"", 527, 1);
#nullable restore
#line 7 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
WriteAttributeValue("", 502, Model.Person.ImageSource, 502, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid"" style=""display: block; max-height:300px;
        border-radius: 6px; "">
            </div>
        </div>
        <div class=""d-flex  p-2 flex-grow-1 pt-3  align-content-stretch align-items-center"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-column m-auto"">
                <h3 style=""font-weight:900;"">
                    ");
#nullable restore
#line 14 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
               Write(Model.FullProffesionalName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n                <br />\r\n                <h5>Region - ");
#nullable restore
#line 17 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                        Write(Model.Person.DefaultAglomeration);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <h5>Edukacja:</h5>\r\n");
            WriteLiteral("                    <p>");
#nullable restore
#line 21 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                  Write(Model.Education);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n");
            WriteLiteral("                <h5>Doświadczenie:</h5>\r\n                <div>");
#nullable restore
#line 24 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                Write(Model.Experience);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
            </div>
        </div>

        <div class=""p-2 flex-grow-1 pt-3 mx-auto flex-grow-1 "" style=""width:400px;text-align:center"">
            <h3 style=""font-weight:900;""> </h3>
            <h5>Zakres specjalizacji:</h5>
            <h5>
");
#nullable restore
#line 32 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                 for (int i = 0; i < Model.MedicalServices.Count; i++)
                {
                    MedicalService service = Model.MedicalServices[i];
                    //if (i % 2 == 1)
                    //{

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <br>\r\n");
#nullable restore
#line 39 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
               Write(service.Name);

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                                 
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h5>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "75d44024714999fd092dbfb4cf3a0e8d11befb718774", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 43 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.AverageRating;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 45 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
             if (Model.VisitReviews.Count > 0)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
           Write(Model.RatingDescription);

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                                        
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n    <div  class=\"d-flex flex-column\">\r\n        <h2>Opinie</h2>\r\n        <div class=\"d-flex flex-column\">\r\n");
#nullable restore
#line 54 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
             for (int i = 0; i < Model.VisitReviews.Count; i++)
            {
                VisitReview review = Model.VisitReviews[i];
                if (i % 2 == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"d-flex mt-2\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "75d44024714999fd092dbfb4cf3a0e8d11befb7111882", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 60 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = review;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 62 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                    
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"d-flex mt-4\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "75d44024714999fd092dbfb4cf3a0e8d11befb7113892", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 67 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = review;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 69 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\MedicalWorkerDetails.cshtml"
                    
                }


            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.MedicalWorker> Html { get; private set; }
    }
}
#pragma warning restore 1591