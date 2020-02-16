using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ViewTestProject.App_Code
{
    public static class ItemHelper
    {
        public static HtmlString CreateItem(this IHtmlHelper html, HtmlString innerEl)
        {
            string result = $"<div class='test'>{innerEl}</div>";
            return new HtmlString(result);
        }

        public static HtmlString CreateHtmlItem(this IHtmlHelper html, Func<object, object> content)
        {
            string result = $"<div class='test'>{content(null)}</div>";
            return new HtmlString(result);
        }

        public static HtmlString CreateTestItem(this IHtmlHelper html, string innerEl)
        {
            string result = $"<div class='test'>{innerEl}</div>";
            return new HtmlString(result);
        }
    }
}
