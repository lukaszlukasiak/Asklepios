#pragma checksum "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e027c79bc95b140730de8e7b4cfdb3037943842b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_HomeArea_Views_Home__Location), @"mvc.1.0.view", @"/Areas/HomeArea/Views/Home/_Location.cshtml")]
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
#line 1 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Core.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\_ViewImports.cshtml"
using Asklepios.Web.ServiceClasses;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e027c79bc95b140730de8e7b4cfdb3037943842b", @"/Areas/HomeArea/Views/Home/_Location.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_HomeArea_Views_Home__Location : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.Location>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"jumbotron\" style=\"padding:20px\">\r\n");
            WriteLiteral("    <div class=\"row\">\r\n        <h2 style=\"text-align:center\">");
#nullable restore
#line 5 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                                 Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-xs-6\">\r\n\r\n");
            WriteLiteral("            <img");
            BeginWriteAttribute("alt", " alt=\"", 342, "\"", 359, 1);
#nullable restore
#line 12 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
WriteAttributeValue("", 348, Model.Name, 348, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 360, "\"", 382, 1);
#nullable restore
#line 12 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
WriteAttributeValue("", 366, Model.ImagePath, 366, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-responsive rounded\" style=\"        display: block;\r\n        border-radius: 6px; width:500px; height:auto\r\n\">\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"col\">\r\n            <h3 style=\"padding: 0px\">\r\n                ");
#nullable restore
#line 19 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
           Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </h3>\r\n");
            WriteLiteral("            <div class=\"row\" style=\"margin:0px; padding:0px\">\r\n                <div class=\"col-xs-2\" style=\"padding: 0px\">\r\n                    <h3>Adres</h3>\r\n                    ");
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
               Write(Model.City);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                            Write(Model.PostalCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <br />\r\n                    ");
#nullable restore
#line 29 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
               Write(Model.StreetAndNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <br />\r\n                    ");
#nullable restore
#line 31 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
               Write(Model.VoivodeshipType);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    <br />\r\n                    <h3>Telefon</h3>\r\n                    ");
#nullable restore
#line 34 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
               Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                </div>\r\n                <div class=\"col-xs-4\" style=\"margin:0px;padding:0px;\">\r\n                    <h3>Usługi</h3>\r\n");
#nullable restore
#line 39 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                      
                        int count = Model.Services.Count();

                        for (int i = 0; i < count; i += 2)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"row\" style=\"margin:0px;padding:0px\">\r\n");
#nullable restore
#line 45 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                                  
                                    if (i + 1 < count)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"col-xs-4\" style=\"margin:0px;padding:0px\">");
#nullable restore
#line 48 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                                                                                        Write(Model.Services.ElementAt(i));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                                        <div class=\"col-xs-4\" style=\"margin:0px;padding:0px\">");
#nullable restore
#line 49 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                                                                                        Write(Model.Services.ElementAt(i + 1));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 50 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"

                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <div class=\"col-xs-4\" style=\"margin:0px;padding:0px\">");
#nullable restore
#line 54 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                                                                                        Write(Model.Services.ElementAt(i));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 55 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                                    }
                                

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n");
#nullable restore
#line 59 "C:\Users\lukas\source\repos\Asklepios4\Asklepios.Web\Areas\HomeArea\Views\Home\_Location.cshtml"
                        }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
