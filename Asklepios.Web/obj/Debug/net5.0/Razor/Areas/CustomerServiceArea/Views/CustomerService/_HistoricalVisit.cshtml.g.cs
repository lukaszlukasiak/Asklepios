#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca2990ec58d84d7f8f452b57032c2f81fa4459b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_CustomerServiceArea_Views_CustomerService__HistoricalVisit), @"mvc.1.0.view", @"/Areas/CustomerServiceArea/Views/CustomerService/_HistoricalVisit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca2990ec58d84d7f8f452b57032c2f81fa4459b2", @"/Areas/CustomerServiceArea/Views/CustomerService/_HistoricalVisit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_CustomerServiceArea_Views_CustomerService__HistoricalVisit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Web.Areas.CustomerServiceArea.Models.VisitViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "VisitDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-icon-split "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("visibility:collapse; height:0px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form d-flex flex-column mt-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BookVisitConditions", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""d-flex jumbotron  flex-column"" style="" padding:20px;align-content:center;position:relative"">
    <div class=""d-flex mb-3 flex-wrap  align-items-center"">
        <div class=""p-2   align-content-center align-items-center  mx-auto"" style=""width:400px; text-align:center"">
            <div class=""d-flex flex-fill flex-column justify-content-center"" style=""text-align:center"">
                <h3 style=""font-weight:900;"">
                    ");
#nullable restore
#line 8 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
               Write(Model.Visit.MedicalWorker.FullProffesionalName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n                <div class=\"d-flex align-content-center mr-auto pt-2\" style=\"text-align:center\">\r\n                    <img");
            BeginWriteAttribute("alt", " alt=\"", 722, "\"", 775, 1);
#nullable restore
#line 11 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
WriteAttributeValue("", 728, Model.Visit.MedicalWorker.FullProffesionalName, 728, 47, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 776, "\"", 827, 1);
#nullable restore
#line 11 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
WriteAttributeValue("", 782, Model.Visit.MedicalWorker.Person.ImageSource, 782, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""mx-auto d-block"" style=""display: block; max-height:50% ; max-width:50%;
        border-radius: 6px; "">
                </div>
            </div>
        </div>
        <div class=""p-2 flex-grow-1 pt-3 m-auto flex-grow-1"" style=""width:400px;text-align:center"">
            <h5 style=""font-weight:900;"">
                ");
#nullable restore
#line 18 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
           Write(Model.Visit.GetVisitDateDescription());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h5>\r\n            <br>\r\n            <h5>\r\n                ");
#nullable restore
#line 22 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
           Write(Model.Visit.Location.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h5>\r\n            <h5>\r\n                ");
#nullable restore
#line 25 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
           Write(Model.Visit.VisitCategory.CategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 25 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
                                                     Write(Model.Visit.PrimaryService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </h5>\r\n");
#nullable restore
#line 28 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
             if (Model.Visit.IsNotCompletedMedicalTest)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h5>\r\n                    \"W realizacji...\"\r\n                </h5>\r\n");
#nullable restore
#line 33 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"p-2 pt-3 m-auto flex-grow-1\" style=\"width:400px;; text-align:center\">\r\n            <div class=\"d-flex flex-column align-content-end justify-content-evenly m-auto\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca2990ec58d84d7f8f452b57032c2f81fa4459b210562", async() => {
                WriteLiteral(@"
                    <span class=""icon text-white-50 left"" style=""text-align:left;float:left"">
                        <i class=""fas fa-address-book""></i>
                    </span>
                    <span class=""text"" style=""float: left"">Szczeg????y wizyty</span>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 37 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
                                                 WriteLiteral(Model.Visit.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ca2990ec58d84d7f8f452b57032c2f81fa4459b213165", async() => {
                WriteLiteral(@"
                    <button type=""submit"" class=""btn btn-primary btn-icon-split d-flex"">
                        <span class=""icon text-white-50 left"" style=""text-align:left;float:left"">
                            <i class=""fas fa-plus-circle""></i>
                        </span>
                        <span class=""text"" style=""float: left"">Zarezerwuj ponownie</span>
                    </button>
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ca2990ec58d84d7f8f452b57032c2f81fa4459b213865", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 50 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.SelectedMedicalWorkerId);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 50 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
                                                                                                WriteLiteral(Model.Visit.MedicalWorker.Id.ToString());

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ca2990ec58d84d7f8f452b57032c2f81fa4459b216334", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 51 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.SelectedServiceId);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 51 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_HistoricalVisit.cshtml"
                                                                                          WriteLiteral(Model.Visit.PrimaryService.Id.ToString());

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Web.Areas.CustomerServiceArea.Models.VisitViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
