using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Training.AppService.Common
{
    public static class RemoveSpecialCharacter
    {
        public static string Remove(this string name)
        {
            string except = " ";
            return Regex.Replace(name, @"[^a-zA-Z0-9" + except + "]+", string.Empty);
        }
    }
}
