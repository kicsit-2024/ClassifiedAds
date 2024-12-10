using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassifiedAds.Helpers
{
    public static class ExtensionMethods
    {
        public static int WordCounter(this string value)
        {
            return value.Split(' ').Count();
        }

        public static SelectList GetEnumDropDownList<T>(this IHtmlHelper _)
        {
            var data = new SelectList(Enum.GetValues(typeof(T)));
            return data;
        }
    }
}
