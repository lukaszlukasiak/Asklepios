#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_Medicine.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6120763a1b8424e0e46c39f526f38a28d0b12011"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_MedicalWorkerArea_Views_MedicalWorker__Medicine), @"mvc.1.0.view", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/_Medicine.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6120763a1b8424e0e46c39f526f38a28d0b12011", @"/Areas/MedicalWorkerArea/Views/MedicalWorker/_Medicine.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_MedicalWorkerArea_Views_MedicalWorker__Medicine : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Core.Models.IssuedMedicine>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"d-flex flex-column\">\r\n    <div>Nazwa: ");
#nullable restore
#line 4 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_Medicine.cshtml"
           Write(Model.MedicineName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    <div>Opakowanie: ");
#nullable restore
#line 5 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_Medicine.cshtml"
                Write(Model.PackageSize);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n    <div>Odpłatność: ");
#nullable restore
#line 6 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\MedicalWorkerArea\Views\MedicalWorker\_Medicine.cshtml"
                Write(Model.PaymentDiscount);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Core.Models.IssuedMedicine> Html { get; private set; }
    }
}
#pragma warning restore 1591
