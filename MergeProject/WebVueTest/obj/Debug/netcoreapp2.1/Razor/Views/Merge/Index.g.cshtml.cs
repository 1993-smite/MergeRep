#pragma checksum "C:\Users\Касса\source\repos\Merge\MergeProject\WebVueTest\Views\Merge\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc5f5e786a6ca01f389dfa8b979c4fa29d7851b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Merge_Index), @"mvc.1.0.view", @"/Views/Merge/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Merge/Index.cshtml", typeof(AspNetCore.Views_Merge_Index))]
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
#line 1 "C:\Users\Касса\source\repos\Merge\MergeProject\WebVueTest\Views\_ViewImports.cshtml"
using WebVueTest;

#line default
#line hidden
#line 2 "C:\Users\Касса\source\repos\Merge\MergeProject\WebVueTest\Views\_ViewImports.cshtml"
using WebVueTest.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc5f5e786a6ca01f389dfa8b979c4fa29d7851b0", @"/Views/Merge/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10e1164072b0edcaa981f40bf0c98dc62fb8d459", @"/Views/_ViewImports.cshtml")]
    public class Views_Merge_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<UserViewValidate>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Касса\source\repos\Merge\MergeProject\WebVueTest\Views\Merge\Index.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";
    ViewData["Title"] = "List";

#line default
#line hidden
            BeginContext(118, 915, true);
            WriteLiteral(@"
<h2>Index</h2>


<div id=""users"">
    <div class=""row row-head"">
        <div class=""col-md-3""><h3>Id FIO</h3></div>
        <div class=""col-md-2""><h3>Birthday</h3></div>
        <div class=""col-md-2""><h3>WorkPlace</h3></div>
        <div class=""col-md-2""><h3>WorkPosition</h3></div>
        <div class=""col-md-2""><h3>HomeAddress</h3></div>
    </div>
    <div class=""row"" v-for=""item in userList"" v-bind:key=""item.id"">
        <div class=""col-md-3"">{{item.lastName}} {{item.firstName}} {{item.middleName}}</div>
        <div class=""col-md-2"">{{item.birthdayStr}}</div>
        <div class=""col-md-2"">{{item.workPlace}}</div>
        <div class=""col-md-2"">{{item.workPosition}}</div>
        <div class=""col-md-2"">{{item.homeAddress}}</div>
    </div>
</div>

<script src=""https://cdn.jsdelivr.net/npm/vue/dist/vue.js""></script>
<script type=""text/javascript"">
    var listData = JSON.parse('");
            EndContext();
            BeginContext(1034, 31, false);
#line 29 "C:\Users\Касса\source\repos\Merge\MergeProject\WebVueTest\Views\Merge\Index.cshtml"
                          Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
            EndContext();
            BeginContext(1065, 160, true);
            WriteLiteral("\');\r\n    console.log(listData);\r\n    var app = new Vue({\r\n        el: \'#users\',\r\n        data: {\r\n            userList: listData\r\n        }\r\n    })\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<UserViewValidate>> Html { get; private set; }
    }
}
#pragma warning restore 1591
