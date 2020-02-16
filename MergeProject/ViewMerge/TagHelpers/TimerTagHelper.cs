using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace TagHelpersApp.TagHelpers
{
    public class TimerTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";    // заменяет тег <timer> тегом <div>
                                       // устанавливаем содержимое элемента
            output.Content.SetContent($"Текущее время: {DateTime.Now.ToString("HH:mm:ss")}");
        }
    }
}
