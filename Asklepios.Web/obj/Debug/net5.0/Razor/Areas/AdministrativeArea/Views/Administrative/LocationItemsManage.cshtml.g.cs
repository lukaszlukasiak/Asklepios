#pragma checksum "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a3980120f181c576a0ef3fb6850dbe14e3a8e691"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdministrativeArea_Views_Administrative_LocationItemsManage), @"mvc.1.0.view", @"/Areas/AdministrativeArea/Views/Administrative/LocationItemsManage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a3980120f181c576a0ef3fb6850dbe14e3a8e691", @"/Areas/AdministrativeArea/Views/Administrative/LocationItemsManage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8363068bd805e1ea8857622c67a9d36b577d03f7", @"/Areas/_ViewImports.cshtml")]
    public class Areas_AdministrativeArea_Views_Administrative_LocationItemsManage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Asklepios.Web.Areas.AdministrativeArea.Models.LocationsManageViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Location", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 3 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
  
    ViewData["Title"] = "View";


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2 style=\"text-align:center\">Plac??wki</h2>\r\n");
#nullable restore
#line 9 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
  

    if (TempData.ContainsKey("successMessage"))

    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"messages\" id=\"MessageSpan\" style=\"margin-top: 10px; text-align: center \">\r\n            <p class=\"alert alert-success\">\r\n");
            WriteLiteral("                ");
#nullable restore
#line 17 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
           Write(TempData["successMessage"].ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </p>\r\n        </div>\r\n");
#nullable restore
#line 20 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"

    }
    //if (!string.IsNullOrWhiteSpace(Model.ErrorMessage))
    if (TempData.ContainsKey("errorMessage"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"messages\" id=\"MessageSpan\" style=\"margin-top: 10px; text-align: center \">\r\n            <p class=\"alert alert-success\">\r\n");
            WriteLiteral("                ");
#nullable restore
#line 28 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
           Write(TempData["errorMessage"].ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </p>\r\n        </div>\r\n");
#nullable restore
#line 32 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"

    }


#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
 foreach (var loc in Model.AllLocations)
{
    Model.SelectedLocation = loc;

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a3980120f181c576a0ef3fb6850dbe14e3a8e6916663", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 39 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = loc;

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
#line 40 "C:\Users\lukas\source\repos\Asklepios8\Asklepios.Web\Areas\AdministrativeArea\Views\Administrative\LocationItemsManage.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<script>\r\n    $(\'#MessageSpan\').delay(5000).fadeOut(300);\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Asklepios.Web.Areas.AdministrativeArea.Models.LocationsManageViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
