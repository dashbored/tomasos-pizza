#pragma checksum "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "55aa205ace2bf3278a1a12a07bf0006979bd748e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Components_ManageOrders_Default), @"mvc.1.0.view", @"/Views/Admin/Components/ManageOrders/Default.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Components/ManageOrders/Default.cshtml", typeof(AspNetCore.Views_Admin_Components_ManageOrders_Default))]
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
#line 1 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\_ViewImports.cshtml"
using Tomasos;

#line default
#line hidden
#line 2 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\_ViewImports.cshtml"
using Tomasos.Models;

#line default
#line hidden
#line 3 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\_ViewImports.cshtml"
using Tomasos.Models.AccountViewModels;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55aa205ace2bf3278a1a12a07bf0006979bd748e", @"/Views/Admin/Components/ManageOrders/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"70f7e34b1df4767ab73d519f371120aa3bab0798", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Components_ManageOrders_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tomasos.Models.AdminViewModels.OrdersViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(55, 326, true);
            WriteLiteral(@"
<div id=""ManageOrders"" class=""panel panel-default todo-panel toggle"">

    <table class=""table table-hover"">
        <thead>
            <tr>
                <td>User</td>
                <td>Order date</td>
                <td>Amount</td>
                <td>Delivered?</td>
            </tr>
        </thead>

");
            EndContext();
#line 15 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml"
         foreach (var order in @Model.Orders)
        {

#line default
#line hidden
            BeginContext(439, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(478, 20, false);
#line 18 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml"
               Write(order.Customer.Email);

#line default
#line hidden
            EndContext();
            BeginContext(498, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(526, 36, false);
#line 19 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml"
               Write(order.CustomerOrder.BestallningDatum);

#line default
#line hidden
            EndContext();
            BeginContext(562, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(590, 31, false);
#line 20 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml"
               Write(order.CustomerOrder.Totalbelopp);

#line default
#line hidden
            EndContext();
            BeginContext(621, 49, true);
            WriteLiteral("</td>\r\n                <td>\r\n                    ");
            EndContext();
            BeginContext(671, 43, false);
#line 22 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml"
               Write(await Component.InvokeAsync("Order", order));

#line default
#line hidden
            EndContext();
            BeginContext(714, 44, true);
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
            EndContext();
#line 25 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Admin\Components\ManageOrders\Default.cshtml"

        }

#line default
#line hidden
            BeginContext(771, 22, true);
            WriteLiteral("\r\n    </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tomasos.Models.AdminViewModels.OrdersViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
