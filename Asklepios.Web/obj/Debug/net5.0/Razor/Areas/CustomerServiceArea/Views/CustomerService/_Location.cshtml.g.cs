#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19c8805cf432cb21d95f444323a3d8df47e68d6f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_CustomerServiceArea_Views_CustomerService__Location), @"mvc.1.0.view", @"/Areas/CustomerServiceArea/Views/CustomerService/_Location.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19c8805cf432cb21d95f444323a3d8df47e68d6f", @"/Areas/CustomerServiceArea/Views/CustomerService/_Location.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_CustomerServiceArea_Views_CustomerService__Location : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Location>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    <div class=""d-flex jumbotron w-100"" style=""padding:20px;display:flex;align-content:center;"">
        <div class=""d-flex flex-column w-100"">
            <div class=""d-flex justify-content-center center-block mb-2"">
                <h2 style=""text-align:center;"">");
#nullable restore
#line 5 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                                          Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n            </div>\r\n            <div class=\"d-flex flex-row justify-content-around  flex-wrap \">\r\n                <div class=\"d-flex flex-fill pt-2\" style=\"max-width:500px \">\r\n                    <img");
            BeginWriteAttribute("alt", " alt=\"", 525, "\"", 542, 1);
#nullable restore
#line 9 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
WriteAttributeValue("", 531, Model.Name, 531, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 543, "\"", 565, 1);
#nullable restore
#line 9 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
WriteAttributeValue("", 549, Model.ImagePath, 549, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid w-100\" style=\"display: block;\r\n                         border-radius: 6px;\">\r\n                </div>\r\n");
            WriteLiteral("                <div class=\"d-flex flex-column flex-fill  pt-2\">\r\n");
            WriteLiteral("                    <div class=\"d-flex ml-4 text-wrap\">\r\n                        <p class=\"text-nowrap\">");
#nullable restore
#line 18 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
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
#line 26 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                           Write(Model.City);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 26 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                                        Write(Model.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                            <div>\r\n                                ");
#nullable restore
#line 29 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                           Write(Model.StreetAndNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                            <div>\r\n                                ");
#nullable restore
#line 32 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                           Write(Model.VoivodeshipType);

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
#line 42 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                           Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
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
#line 51 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                              
                                int count = Model.Services.Count();

                                for (int i = 0; i < count; i++)
                                {
                                    if (i < count - 1)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"d-flex mr-1 mt-0 pt-0 pb-0 mb-0\">\r\n");
            WriteLiteral("                                                <p style=\"padding:0px;margin:0px;\">\r\n                                                    ");
#nullable restore
#line 61 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                                               Write(Model.Services.ElementAt(i));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                                                </p>\r\n");
            WriteLiteral("                                        </div>\r\n");
#nullable restore
#line 67 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"

                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"d-flex mr-1\">\r\n");
            WriteLiteral("                                                <p>\r\n                                                    ");
#nullable restore
#line 74 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"
                                               Write(Model.Services.ElementAt(i));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </p>\r\n");
            WriteLiteral("                                        </div>\r\n");
#nullable restore
#line 80 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\CustomerServiceArea\Views\CustomerService\_Location.cshtml"

                                    }
                                }
                            

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
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
