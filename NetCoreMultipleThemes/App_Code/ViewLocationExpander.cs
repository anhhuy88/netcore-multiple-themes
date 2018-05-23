using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace NetCoreMultipleThemes.App_Code
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        private const string KeyTheme = "themes";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue(KeyTheme, out string theme);
            if (!string.IsNullOrEmpty(theme))
            {
                return ExpandViewLocationsCore(viewLocations, theme);
            }
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var theme = (context.ActionContext.HttpContext.Request.Query[KeyTheme]).ToString() ?? "";
            context.Values[KeyTheme] = theme;
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, string theme)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Insert(7, $"Themes/{theme}/");
            }
        }
    }


}
