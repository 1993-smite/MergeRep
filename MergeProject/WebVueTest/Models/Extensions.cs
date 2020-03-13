using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public static class Extensions
    {
        public static string DateTimeRusAppFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
