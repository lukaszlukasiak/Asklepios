#pragma checksum "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5d1fd2a8c3aaa54eac916cf5a6866b58de4b35f7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_PatientArea_Views_Patient__Location), @"mvc.1.0.view", @"/Areas/PatientArea/Views/Patient/_Location.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d1fd2a8c3aaa54eac916cf5a6866b58de4b35f7", @"/Areas/PatientArea/Views/Patient/_Location.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_PatientArea_Views_Patient__Location : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Location>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"d-flex jumbotron w-100 flex-column\" style=\"padding:20px;align-content:stretch;\">\r\n    <div class=\"d-flex flex-column\">\r\n        <div class=\"d-flex justify-content-center\">\r\n            <h2>");
#nullable restore
#line 6 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
           Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
        </div>
        <div class=""d-flex flex-column flex-xl-row "">
            <div class=""mx-2 flex-column align-content-center align-items-center flex-grow-1"" style=""text-align:center;"">

                <div class=""d-flex flex-fill pt-2 align-content-center align-items-center"" style=""align-items: center; text-align: center"">
                    <img");
            BeginWriteAttribute("alt", " alt=\"", 621, "\"", 638, 1);
#nullable restore
#line 12 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
WriteAttributeValue("", 627, Model.Name, 627, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 639, "\"", 661, 1);
#nullable restore
#line 12 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
WriteAttributeValue("", 645, Model.ImagePath, 645, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid ItemImage m-auto"" style=""display: block; border-radius: 6px; text-align: center"">
                </div>
            </div>
            <div class=""mx-2 flex-grow-5"">
                <div class=""d-flex flex-column   pt-2"">
                    <div class=""d-flex flex-column"">
                        <div class=""d-flex ml-4 text-wrap"">
                            <p");
            BeginWriteAttribute("class", " class=\"", 1053, "\"", 1061, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 19 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                                   Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        </div>

                        <div class=""d-flex flex-row mt-4 ml-4"">
                            <div class=""d-flex  align-self-baseline w-25"">
                                <h6 style=""font-weight:bold"">Adres:</h6>
                            </div>
                            <div class=""d-flex flex-column align-self-baseline"">
                                <div>
                                    ");
#nullable restore
#line 28 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                               Write(Model.City);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 28 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                                            Write(Model.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </div>\r\n                                <div>\r\n                                    ");
#nullable restore
#line 31 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                               Write(Model.StreetAndNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </div>\r\n                                <div>\r\n                                    ");
#nullable restore
#line 34 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                               Write(Model.Aglomeration);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""d-flex flex-row mt-4 ml-4"">
                            <div class=""d-flex  align-self-baseline w-25"">
                                <h6 style=""font-weight:bold"">Telefon:</h6>
                            </div>
                            <div class=""d-flex flex-column align-self-baseline"">
                                <div>
                                    ");
#nullable restore
#line 44 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                               Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""d-flex flex-row mt-4 ml-4"">
                        <div class=""d-flex  align-self-baseline w-25"">
                            <h6 style=""font-weight:bold"">Usługi:</h6>
                        </div>
                        <div class=""d-flex flex-row flex-wrap w-75 align-self-baseline"">
");
#nullable restore
#line 54 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                              
                                int count = Model.Services.Count();

                                for (int i = 0; i < count; i++)
                                {
                                    if (i < count - 1)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"d-flex mr-1 mt-0 pt-0 pb-0 mb-0\">\r\n");
            WriteLiteral("                                                <span style=\"padding:0px;margin:0px;\">\r\n                                                    ");
#nullable restore
#line 64 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                                               Write(Model.Services.ElementAt(i).Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                                                </span>\r\n");
            WriteLiteral("                                        </div>\r\n");
#nullable restore
#line 70 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"

                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"d-flex mr-1\">\r\n");
            WriteLiteral("                                                <span>\r\n                                                    ");
#nullable restore
#line 77 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"
                                               Write(Model.Services.ElementAt(i).Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </span>\r\n");
            WriteLiteral("                                        </div>\r\n");
#nullable restore
#line 83 "C:\Users\Lukasz\source\repos\Asklepios2\Asklepios.Web\Areas\PatientArea\Views\Patient\_Location.cshtml"

                                    }
                                }
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.Location> Html { get; private set; }
    }
}
#pragma warning restore 1591
