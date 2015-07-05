using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Website.Utils
{
    public static class MvcHtmlStringCustom
    {
        public static MvcHtmlString IsDisabled(this MvcHtmlString htmlString, bool disabled)
        {
            string rawstring = htmlString.ToString();
            if (disabled)
            {
                rawstring = rawstring.Insert(rawstring.Length - 2, "disabled=\"disabled\"");
            }
            return new MvcHtmlString(rawstring);
        }

        public static MvcHtmlString IsEnabled(this MvcHtmlString htmlString, bool enabled)
        {
            return IsDisabled(htmlString, !enabled);
        }

        public static MvcHtmlString IsReadonly(this MvcHtmlString htmlString, bool @readonly)
        {
            string rawstring = htmlString.ToString();
            if (@readonly)
            {
                rawstring = rawstring.Insert(rawstring.Length - 2, "readonly=\"readonly\"");
            }
            return new MvcHtmlString(rawstring);
        }

        public static RouteValueDictionary ConditionalDisable(bool disabled, object htmlAttributes = null)
        {
            var dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (disabled)
                dictionary.Add("disabled", "disabled");

            return dictionary;
        }
    }
}