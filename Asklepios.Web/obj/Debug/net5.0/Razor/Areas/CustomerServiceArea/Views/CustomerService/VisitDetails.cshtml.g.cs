#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9b78a2d9c26a15da75fad7c68b159231698e036"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_CustomerServiceArea_Views_CustomerService_VisitDetails), @"mvc.1.0.view", @"/Areas/CustomerServiceArea/Views/CustomerService/VisitDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9b78a2d9c26a15da75fad7c68b159231698e036", @"/Areas/CustomerServiceArea/Views/CustomerService/VisitDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_CustomerServiceArea_Views_CustomerService_VisitDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Visit>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Stars", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Medicine", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "PatientArea", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patient", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BookVisit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-icon-split  flex-grow-1 m-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""d-flex jumbotron flex-column"" style="" padding:20px;display:flex;align-content:center;position:relative"">
    <div class=""d-flex mb-3 flex-wrap align-self-center align-items-center"">
        <div class=""p-2   align-content-center align-items-center align-self-center mx-auto "" style=""min-width:400px; text-align:center"">
            <div class=""d-flex flex-fill flex-column justify-content-center"" style=""text-align:center"">
                <h3 style=""font-weight:900;"">
                    ");
#nullable restore
#line 8 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
               Write(Model.MedicalWorker.FullProffesionalName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n                <div class=\"d-flex align-content-center mr-auto pt-2\" style=\"text-align:center\">\r\n                    <img");
            BeginWriteAttribute("alt", " alt=\"", 733, "\"", 780, 1);
#nullable restore
#line 11 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
WriteAttributeValue("", 739, Model.MedicalWorker.FullProffesionalName, 739, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 781, "\"", 826, 1);
#nullable restore
#line 11 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
WriteAttributeValue("", 787, Model.MedicalWorker.Person.ImageSource, 787, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""mx-auto d-block"" style=""display: block; max-height:50% ; max-width:50%;
        border-radius: 6px; "">
                </div>

            </div>
        </div>
        <div class=""p-2 flex-grow-1 pt-3 mx-auto flex-grow-1"" style=""min-width: 400px; text-align: center"">
            <h5>
                <p>
                    Data: ");
#nullable restore
#line 20 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                     Write(Model.GetVisitDateFullDescription());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n                <br>\r\n                <p>\r\n                    ");
#nullable restore
#line 24 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
               Write(Model.Location.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n                <p>\r\n                    Piętro: ");
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                       Write(Model.MedicalRoom.FloorNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral(", pokój: ");
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                                              Write(Model.MedicalRoom.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n            </h5>\r\n\r\n        </div>\r\n\r\n");
            WriteLiteral("        <div class=\"p-2 pt-3 m-auto flex-grow-1\" style=\"min-width: 400px; text-align: center\">\r\n            <h5>\r\n\r\n                <p>\r\n                    Kategoria: ");
#nullable restore
#line 38 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                          Write(Model.VisitCategory.CategoryName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n                <p>Usługi</p>\r\n                <p>\r\n                    ");
#nullable restore
#line 42 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
               Write(Model.PrimaryService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 42 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                            Write(Model.GetPrice(Model.PrimaryService));

#line default
#line hidden
#nullable disable
            WriteLiteral("; PLN\r\n                </p>\r\n\r\n");
#nullable restore
#line 45 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                 for (int i = 0; i < Model.MinorMedicalServices.Count; i++)
                {
                    MedicalService service = Model.MinorMedicalServices[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <p>\r\n                        ");
#nullable restore
#line 49 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                   Write(service.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 49 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                   Write(Model.GetPrice(service));

#line default
#line hidden
#nullable disable
            WriteLiteral("; PLN\r\n                    </p>\r\n");
#nullable restore
#line 51 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>Koszt całkowity: ");
#nullable restore
#line 52 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                               Write(Model.GetTotalPrice());

#line default
#line hidden
#nullable disable
            WriteLiteral(" PLN</p>\r\n            </h5>\r\n        </div>\r\n");
#nullable restore
#line 55 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
         if (Model.VisitReview != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"d-flex flex-column flex-fill flex-grow-1 m-auto\" style=\"min-width: 400px; text-align: center\">\r\n                <div class=\"d-flex flex-column\">\r\n                    <p>\r\n                        Atmosfera:\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b9b78a2d9c26a15da75fad7c68b159231698e03612850", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 61 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.VisitReview.AtmosphereRate;

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
            WriteLiteral("\r\n                    </p>\r\n                    <p>\r\n                        Kompetencje:\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b9b78a2d9c26a15da75fad7c68b159231698e03614605", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 65 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.VisitReview.CompetenceRate;

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
            WriteLiteral("\r\n                    </p>\r\n                    <p>\r\n                        Ocena ogólna: ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b9b78a2d9c26a15da75fad7c68b159231698e03616334", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 68 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model.VisitReview.GeneralRate;

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
            WriteLiteral("\r\n                    </p>\r\n                    <p>\r\n                        Opis:\r\n                        ");
#nullable restore
#line 72 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                   Write(Model.VisitReview.ShortDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </p>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 76 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 78 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
     if (Model.ExaminationReferrals != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"d-flex flex-column mx-2\">\r\n            <h5>Wystawione skierowania</h5>\r\n");
#nullable restore
#line 82 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
             for (int i = 0; i < Model.ExaminationReferrals.Count; i++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"border border-secondary rounded w-100 flex-grow-0 mb-2  p-3\">\r\n");
#nullable restore
#line 85 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                       
                        MedicalReferral referral = Model.ExaminationReferrals[i];


#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div>\r\n                                            Usługa: ");
#nullable restore
#line 89 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                               Write(referral.PrimaryMedicalService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 90 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                             if (referral.MinorMedicalService != null)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <span>\r\n                                                    Podusługa: ");
#nullable restore
#line 93 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                                          Write(referral.MinorMedicalService.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </span>\r\n");
#nullable restore
#line 95 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"

                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n");
            WriteLiteral("                    <div>\r\n                        Wystawione: ");
#nullable restore
#line 101 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                               Write(referral.IssueDate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                    <div>\r\n                        Ważne do: ");
#nullable restore
#line 104 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                             Write(referral.ExpireDate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n");
#nullable restore
#line 106 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                        if (referral.HasBeenUsed)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>\r\n                        Skierowanie zostało wykorzystane\r\n                    </p>\r\n");
#nullable restore
#line 110 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                        }
                        else if (referral.HasExpired)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <p>Ważność skierowania upłynęła</p>\r\n");
#nullable restore
#line 114 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 118 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 121 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 122 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                         if (Model.Prescription!=null)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"d-flex flex-column mx-2\">\r\n    <h5>Recepta</h5>\r\n\r\n    <div class=\"border border-secondary rounded w-100 flex-grow-0 mb-2  p-3\">\r\n\r\n        <div class=\"d-flex flex-fill pt-2 flex-column\">\r\n\r\n            <div>\r\n                Wystawione: ");
#nullable restore
#line 132 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                       Write(Model.Prescription.IssueDate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div>\r\n                Ważne do: ");
#nullable restore
#line 135 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                     Write(Model.Prescription.ExpirationDate.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div>\r\n                Numer id recepty:\r\n                <span style=\"font-weight:bold\"> ");
#nullable restore
#line 139 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                           Write(Model.Prescription.IdentificationCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            </div>\r\n            <p>Kod dostępu: <span style=\"font-weight:bold\">  ");
#nullable restore
#line 141 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                                                        Write(Model.Prescription.AccessCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> </p>\r\n        </div>\r\n        <div class=\"d-flex flex-fill  flex-column\">\r\n\r\n            <p>\r\n");
#nullable restore
#line 146 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                  
                    for (int i = 0; i < Model.Prescription.IssuedMedicines.Count; i++)
                    {
                        IssuedMedicine item = Model.Prescription.IssuedMedicines[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b9b78a2d9c26a15da75fad7c68b159231698e03626748", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 150 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = item;

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
            WriteLiteral("\r\n");
#nullable restore
#line 151 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                        if (i == Model.Prescription.IssuedMedicines.Count - 1)
                        {

                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <br />\r\n");
#nullable restore
#line 158 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
                        }
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </p>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
#nullable restore
#line 165 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\VisitDetails.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <div class=\"d-flex\">\r\n\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b9b78a2d9c26a15da75fad7c68b159231698e03629490", async() => {
                WriteLiteral("\r\n                <span class=\"icon text-white-50\">\r\n                    <i class=\"fas fa-book\"></i>\r\n                </span>\r\n                <span class=\"text\">Zarezerwuj ponownie</span>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.Visit> Html { get; private set; }
    }
}
#pragma warning restore 1591