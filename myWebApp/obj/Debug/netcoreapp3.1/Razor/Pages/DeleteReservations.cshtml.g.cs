#pragma checksum "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f975f4c148a55092ae02605d32b50c4420ec0ce5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(myWebApp.Pages.Pages_DeleteReservations), @"mvc.1.0.razor-page", @"/Pages/DeleteReservations.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/DeleteReservations.cshtml", typeof(myWebApp.Pages.Pages_DeleteReservations), null)]
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
#line 1 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\_ViewImports.cshtml"
using myWebApp;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f975f4c148a55092ae02605d32b50c4420ec0ce5", @"/Pages/DeleteReservations.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"721989a388ce7a3f056d1e9ef37c2e43a92d4382", @"/Pages/_ViewImports.cshtml")]
    public class Pages_DeleteReservations : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", new global::Microsoft.AspNetCore.Html.HtmlString("Submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page-handler", "Remove", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
  
    ViewData["Title"] = "Delete Reservation";

#line default
#line hidden
            BeginContext(146, 766, true);
            WriteLiteral(@"
<div class=""container-fluid"">
    <div class=""row h-100"">
        <div class=""col-3 bg-light"">
            <h4 class=""text-left font-weight-bolder mt-4"">Welcome Admin</h4>
            <div class=""row"">
                <div class=""mt-5 w-100""></div>
                <div class=""col-9"">
                    <h5 class=""font-italic text-left"">Notifications</h5>
                </div>

                <div class=""col"">
                    <div class=""circle"">1</div>
                </div>
            </div>
            <hr>
            <p class=""mt-2 mb-2"">Text Notifications</p>
            <hr>

            <div class=""mt-5 w-100""></div>
            <h5 class=""font-italic text-left"">Manage my reservations</h5>
            <hr>
            ");
            EndContext();
            BeginContext(912, 52, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f975f4c148a55092ae02605d32b50c4420ec0ce57112", async() => {
                BeginContext(954, 6, true);
                WriteLiteral("Logout");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(964, 981, true);
            WriteLiteral(@"
        </div>

        <div class=""col"">
            <div class=""row"">
                <div class=""pl-5 col-8"">
                    <div class=""card mt-2"">
                        <article class=""card-body"">
                            <h4 class=""card-title mb-4 mt-1"">Delete Reservations</h4>
                            <div><b>Employee info:</b></div>
                            <table class=""table table-bordered table-hover"">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>email</th>
                                        <th>date</th>
                                        <th>location</th>
                                        <th>room</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
");
            EndContext();
#line 51 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
                                     foreach(var reservations in @Model.ShowReservations())
                                    {

#line default
#line hidden
            BeginContext(2077, 40, true);
            WriteLiteral("                                        ");
            EndContext();
            BeginContext(2117, 2231, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f975f4c148a55092ae02605d32b50c4420ec0ce510174", async() => {
                BeginContext(2137, 455, true);
                WriteLiteral(@"
                                            <fieldset>
                                                <tr>
                                                    <td>
                                                        <img class=""img-fluid"" src=""/Images/Reservation.png""/>
                                                    </td>
                                                    <td>
                                                        ");
                EndContext();
                BeginContext(2593, 40, false);
#line 60 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
                                                   Write(Html.DisplayFor(m => reservations.Email));

#line default
#line hidden
                EndContext();
                BeginContext(2633, 91, true);
                WriteLiteral("\r\n                                                        <input type=\"hidden\" name=\"Email\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2724, "\"", 2773, 1);
#line 61 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
WriteAttributeValue("", 2732, Html.DisplayFor(m => reservations.Email), 2732, 41, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2774, 177, true);
                WriteLiteral("/>\r\n                                                    </td>\r\n                                                    <td>\r\n                                                        ");
                EndContext();
                BeginContext(2952, 39, false);
#line 64 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
                                                   Write(Html.DisplayFor(m => reservations.Date));

#line default
#line hidden
                EndContext();
                BeginContext(2991, 90, true);
                WriteLiteral("\r\n                                                        <input type=\"hidden\" name=\"Date\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3081, "\"", 3129, 1);
#line 65 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
WriteAttributeValue("", 3089, Html.DisplayFor(m => reservations.Date), 3089, 40, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3130, 177, true);
                WriteLiteral("/>\r\n                                                    </td>\r\n                                                    <td>\r\n                                                        ");
                EndContext();
                BeginContext(3308, 43, false);
#line 68 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
                                                   Write(Html.DisplayFor(m => reservations.Location));

#line default
#line hidden
                EndContext();
                BeginContext(3351, 94, true);
                WriteLiteral("\r\n                                                        <input type=\"hidden\" name=\"Location\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3445, "\"", 3497, 1);
#line 69 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
WriteAttributeValue("", 3453, Html.DisplayFor(m => reservations.Location), 3453, 44, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3498, 177, true);
                WriteLiteral("/>\r\n                                                    </td>\r\n                                                    <td>\r\n                                                        ");
                EndContext();
                BeginContext(3676, 39, false);
#line 72 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
                                                   Write(Html.DisplayFor(m => reservations.Room));

#line default
#line hidden
                EndContext();
                BeginContext(3715, 90, true);
                WriteLiteral("\r\n                                                        <input type=\"hidden\" name=\"Room\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3805, "\"", 3853, 1);
#line 73 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
WriteAttributeValue("", 3813, Html.DisplayFor(m => reservations.Room), 3813, 40, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3854, 173, true);
                WriteLiteral("/>\r\n                                                    </td>\r\n                                                    <td>\r\n                                                    ");
                EndContext();
                BeginContext(4027, 101, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f975f4c148a55092ae02605d32b50c4420ec0ce515740", async() => {
                    BeginContext(4118, 1, true);
                    WriteLiteral("X");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.PageHandler = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(4128, 213, true);
                WriteLiteral("\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </fieldset>\r\n                                        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4348, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 81 "C:\Users\dvide\OneDrive\Documenten\GitHub\Project-Coronaa\myWebApp\Pages\DeleteReservations.cshtml"
                                    }

#line default
#line hidden
            BeginContext(4389, 241, true);
            WriteLiteral("                                </tbody>\r\n                            </table>\r\n                        </article>\r\n                    </div> <!-- card.// -->\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DeleteReservationModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DeleteReservationModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DeleteReservationModel>)PageContext?.ViewData;
        public DeleteReservationModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
