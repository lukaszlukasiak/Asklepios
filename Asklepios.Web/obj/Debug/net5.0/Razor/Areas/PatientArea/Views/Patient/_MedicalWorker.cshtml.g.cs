#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "20fb181b0d3eb927b2b087357dc7a47d5fb93f06"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_PatientArea_Views_Patient__MedicalWorker), @"mvc.1.0.view", @"/Areas/PatientArea/Views/Patient/_MedicalWorker.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20fb181b0d3eb927b2b087357dc7a47d5fb93f06", @"/Areas/PatientArea/Views/Patient/_MedicalWorker.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_PatientArea_Views_Patient__MedicalWorker : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.MedicalWorker>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""jumbotron"" style=""padding:20px"">
    <div class=""jumbotron w-100"" style=""margin: 0px; padding: 0px; "">

        <div class=""d-flex mb-3 flex-wrap"">
            <div class=""p-2  pt-3"" style=""width:400px;"">
                <div class=""d-flex flex-fill  justify-content-center"">
                    <img");
            BeginWriteAttribute("alt", " alt=\"", 362, "\"", 395, 1);
#nullable restore
#line 8 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
WriteAttributeValue("", 368, Model.FullProffesionalName, 368, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 396, "\"", 418, 1);
#nullable restore
#line 8 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
WriteAttributeValue("", 402, Model.ImagePath, 402, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid"" style=""display: block; max-height:300px;
        border-radius: 6px; "">
                </div>
            </div>
            <div class=""p-2 flex-grow-1 pt-3"" style=""min-width:300px;"">
                <h3 style=""font-weight:900;"">
                    ");
#nullable restore
#line 14 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
               Write(Model.FullProffesionalName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h3>\r\n                <br>\r\n\r\n                <h4>Zakres specjalizacji:</h4>\r\n                <h5>\r\n");
#nullable restore
#line 20 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                     for (int i = 0; i < Model.MedicalServices.Count; i++)
                    {
                    MedicalService service=Model.MedicalServices[i];
                    if  (i%2==1)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <br>\r\n");
#nullable restore
#line 26 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                    }
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
               Write(service.Name);

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                                 
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h5>\r\n\r\n");
#nullable restore
#line 32 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                 if (Model.AverageRating > 4.75)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
");
#nullable restore
#line 39 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                }
                else if (Model.AverageRating >= 4.25)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star-half.svg"" class=""Star"" />
");
#nullable restore
#line 47 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"

                }
                else if (Model.AverageRating >= 3.75)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
");
#nullable restore
#line 55 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                }
                else if (Model.AverageRating >= 3.25)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star-half.svg"" class=""Star"" />
");
#nullable restore
#line 62 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"

                }
                else if (Model.AverageRating >= 2.75)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
");
#nullable restore
#line 69 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"

                }
                else if (Model.AverageRating >= 2.25)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star.svg"" class=""Star"" />
                <img src=""\vendor\fontawesome-free\svgs\solid\star-half.svg"" class=""Star"" />
");
#nullable restore
#line 76 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"

                }
                else if (Model.AverageRating >= 1.75)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <img src=\"\\vendor\\fontawesome-free\\svgs\\solid\\star.svg\" class=\"Star\" />\r\n                <img src=\"\\vendor\\fontawesome-free\\svgs\\solid\\star.svg\" class=\"Star\" />\r\n");
#nullable restore
#line 82 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"

                }
                else if (Model.AverageRating >= 1.25)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <img src=\"\\vendor\\fontawesome-free\\svgs\\solid\\star.svg\" class=\"Star\" />\r\n                <img src=\"\\vendor\\fontawesome-free\\svgs\\solid\\star-half.svg\" class=\"Star\" />\r\n");
#nullable restore
#line 88 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                }
                else if (Model.AverageRating >= 1)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <img src=\"\\vendor\\fontawesome-free\\svgs\\solid\\star.svg\" class=\"Star\" />\r\n");
#nullable restore
#line 92 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 95 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                 if   (Model.VisitRatings.Count >0)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
               Write(Model.RatingDescription);

#line default
#line hidden
#nullable disable
#nullable restore
#line 97 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\PatientArea\Views\Patient\_MedicalWorker.cshtml"
                                            
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n\r\n            <div class=\"p-2 flex-grow-1 pt-3\" style=\"min-width:300px;\">\r\n                Geeks 21111111111111\r\n            </div>\r\n        </div>\r\n\r\n\r\n    </div>\r\n</div>");
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
