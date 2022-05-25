using System;
using System.Reflection;
using Xamarin.Forms;

namespace Prism.Plugin.Popups.Extensions
{
    /// <summary>
    /// Helper extensions for working with <see cref="Page" />.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Returns <c>true</c> if the <see cref="Page"/> is of the given type.
        /// </summary>
        /// <typeparam name="T">Type of a <see cref="Page" />.</typeparam>
        /// <param name="page">Reference <see cref="Page" />.</param>
        /// <returns></returns>
        public static bool IsOrDerivesFrom<T>(this Page page) where T : Page =>
            page is T || page.GetType().GetTypeInfo().IsSubclassOf(typeof(T));

        /// <summary>
        /// Gets the Currently visible page.
        /// </summary>
        /// <param name="page"></param>
        /// <returns>The displayed page.</returns>
        public static Page GetDisplayedPage(this Page page)
        {
            while (page.IsOrDerivesFrom<FlyoutPage>() ||
                  page.IsOrDerivesFrom<TabbedPage>() ||
                  page.IsOrDerivesFrom<NavigationPage>())
            {
                switch (page)
                {
                    case FlyoutPage flp:
                        page = flp.Detail;
                        break;
                    case TabbedPage tabbed:
                        page = tabbed.CurrentPage;
                        break;
                    case NavigationPage nav:
                        page = nav.CurrentPage;
                        break;
                }
            }

            return page;
        }
    }
}