#pragma checksum "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d088f4d9eb6769807addf5601880ca9cdf07c7ee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DownloadCertificate_Index), @"mvc.1.0.view", @"/Views/DownloadCertificate/Index.cshtml")]
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
#line 1 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\_ViewImports.cshtml"
using VMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\_ViewImports.cshtml"
using VMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d088f4d9eb6769807addf5601880ca9cdf07c7ee", @"/Views/DownloadCertificate/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab18737b16bcdfeed68f05e7a182822b7ee5280f", @"/Views/_ViewImports.cshtml")]
    public class Views_DownloadCertificate_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<VMS.Models.Appointment>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Download", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "DownloadCertificate", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 7 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.center.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 10 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.center.vname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 13 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.center.campaign.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 19 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d088f4d9eb6769807addf5601880ca9cdf07c7ee5359", async() => {
                WriteLiteral("\r\n            <td>\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" name=\"Name\" id=\"Name\"");
                BeginWriteAttribute("value", " value=\"", 742, "\"", 797, 1);
#nullable restore
#line 25 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 750, Html.DisplayFor(modelItem => item.center.Name), 750, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                </div>\r\n            </td>\r\n            <td>\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" name=\"Name\" id=\"Name\"");
                BeginWriteAttribute("value", " value=\"", 1005, "\"", 1061, 1);
#nullable restore
#line 30 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 1013, Html.DisplayFor(modelItem => item.center.vname), 1013, 48, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                </div>\r\n            </td>\r\n            <td>\r\n                <div class=\"form-group\">\r\n                    <input type=\"text\" name=\"Name\" id=\"Name\"");
                BeginWriteAttribute("value", " value=\"", 1269, "\"", 1333, 1);
#nullable restore
#line 35 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 1277, Html.DisplayFor(modelItem => item.center.campaign.Name), 1277, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                </div>\r\n\r\n            </td>\r\n            <td>\r\n                <input type=\"submit\" value=\"Select\" />\r\n            </td>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </tr>\r\n");
#nullable restore
#line 44 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 53 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.appointmentTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 56 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.center.vname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 59 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.center.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 62 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.center.campaign.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n\r\n\r\n    <tbody>\r\n");
#nullable restore
#line 70 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d088f4d9eb6769807addf5601880ca9cdf07c7ee11147", async() => {
                WriteLiteral("\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input type=\"text\" name=\"Name\" id=\"Name\"");
                BeginWriteAttribute("value", " value=\"", 2418, "\"", 2477, 1);
#nullable restore
#line 76 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 2426, Html.DisplayFor(modelItem => item.appointmentTime), 2426, 51, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input type=\"text\" name=\"vname\" id=\"vname\"");
                BeginWriteAttribute("value", " value=\"", 2707, "\"", 2763, 1);
#nullable restore
#line 81 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 2715, Html.DisplayFor(modelItem => item.center.vname), 2715, 48, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input type=\"text\" name=\"quantity\" id=\"quantity\"");
                BeginWriteAttribute("value", " value=\"", 2999, "\"", 3054, 1);
#nullable restore
#line 86 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 3007, Html.DisplayFor(modelItem => item.center.Name), 3007, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                    </div>\r\n                </td>\r\n                <td>\r\n                    <div class=\"form-group\">\r\n                        <input type=\"text\" name=\"startTime\" id=\"startTime\"");
                BeginWriteAttribute("value", " value=\"", 3292, "\"", 3356, 1);
#nullable restore
#line 91 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"
WriteAttributeValue("", 3300, Html.DisplayFor(modelItem => item.center.campaign.Name), 3300, 56, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"form-control\" readonly=\"readonly\">\r\n                    </div>\r\n                </td>                \r\n                <td>\r\n                    <input type=\"submit\" value=\"Download\" />\r\n                </td>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </tr>\r\n");
#nullable restore
#line 99 "C:\Users\student\Workspace\enpm809wproject-skumar26\VMS\Views\DownloadCertificate\Index.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<VMS.Models.Appointment>> Html { get; private set; }
    }
}
#pragma warning restore 1591
