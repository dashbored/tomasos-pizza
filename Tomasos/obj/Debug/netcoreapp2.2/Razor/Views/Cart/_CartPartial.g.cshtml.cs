#pragma checksum "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Cart\_CartPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "418c47c57f69862f0647d544f426b09224545701"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart__CartPartial), @"mvc.1.0.view", @"/Views/Cart/_CartPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Cart/_CartPartial.cshtml", typeof(AspNetCore.Views_Cart__CartPartial))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"418c47c57f69862f0647d544f426b09224545701", @"/Views/Cart/_CartPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"70f7e34b1df4767ab73d519f371120aa3bab0798", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart__CartPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Tomasos.Models.Identity.Matratt>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(46, 81, true);
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h3>Your cart</h3>\r\n</div>\r\n<div class=\"my-2\">\r\n");
            EndContext();
#line 7 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Cart\_CartPartial.cshtml"
     foreach (var dish in Model)
    {
        

#line default
#line hidden
            BeginContext(177, 16, false);
#line 9 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Cart\_CartPartial.cshtml"
   Write(dish.MatrattNamn);

#line default
#line hidden
            EndContext();
            BeginContext(195, 15, true);
            WriteLiteral("        <br/>\r\n");
            EndContext();
#line 11 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Cart\_CartPartial.cshtml"
    }

#line default
#line hidden
            BeginContext(217, 75, true);
            WriteLiteral("</div>\r\n<div class=\"cart-total\">\r\n    Total:\r\n    <div class=\"float-right\">");
            EndContext();
            BeginContext(293, 22, false);
#line 15 "C:\Users\richa\Desktop\C#\Inlamning\Tomasos\Tomasos\Tomasos\Views\Cart\_CartPartial.cshtml"
                        Write(Model.Sum(e => e.Pris));

#line default
#line hidden
            EndContext();
            BeginContext(315, 141, true);
            WriteLiteral(" kr</div>\r\n</div>\r\n<div class=\"text-center\">\r\n    <button type=\"button\" class=\"btn btn-lg btn-primary btn-block\">Buy</button>\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Tomasos.Models.Identity.Matratt>> Html { get; private set; }
    }
}
#pragma warning restore 1591