#pragma checksum "/Users/jan-jaaptencate/Documents/GitHub/Project-Corona/myWebApp/Pages/SignUp.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a229501fc8c98e65f7d98ae43087db896188024a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(myWebApp.Pages.Pages_SignUp), @"mvc.1.0.razor-page", @"/Pages/SignUp.cshtml")]
namespace myWebApp.Pages
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
#line 1 "/Users/jan-jaaptencate/Documents/GitHub/Project-Corona/myWebApp/Pages/_ViewImports.cshtml"
using myWebApp;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a229501fc8c98e65f7d98ae43087db896188024a", @"/Pages/SignUp.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"721989a388ce7a3f056d1e9ef37c2e43a92d4382", @"/Pages/_ViewImports.cshtml")]
    public class Pages_SignUp : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 4 "/Users/jan-jaaptencate/Documents/GitHub/Project-Corona/myWebApp/Pages/SignUp.cshtml"
  
    ViewData["Title"] = "SignUp";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>Sign up</h2>\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<myWebApp.Models.EmployeeModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<myWebApp.Models.EmployeeModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<myWebApp.Models.EmployeeModel>)PageContext?.ViewData;
        public myWebApp.Models.EmployeeModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
