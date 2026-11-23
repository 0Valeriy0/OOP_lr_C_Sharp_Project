using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    // Функції розширення для системних типів
    public static class MyExtensions
    {
        // string: " мельник " → "Мельник"
        public static string ToTitleCase(this string text)
        {
            text = text.Trim();
            if (text.Length == 0) return text;
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }

        // DateTime: повний вік за датою народження
        public static int Age(this DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (DateTime.Today < birthDate.AddYears(age)) age--;
            return age;
        }
    }
}
