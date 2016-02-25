using System.Web.Mvc;

namespace TravelPlain.Web
{
    public static class Helpers
    {
        public static MvcHtmlString Rating(this HtmlHelper htmlHelper,
            int rating)
        {
            var builder = new TagBuilder("span");
            string innerText = string.Empty;

            for (int i = 0; i < 5; i++)
            {
                if (i < rating)
                {
                    innerText += "★";
                }
                else
                {
                    innerText += "☆";
                }
            }

            builder.SetInnerText(innerText);
            var result = MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
            return result;
        }
    }
}