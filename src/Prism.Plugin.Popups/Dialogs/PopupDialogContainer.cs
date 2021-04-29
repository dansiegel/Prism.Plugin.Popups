using System;
using Rg.Plugins.Popup.Pages;

namespace Prism.Plugin.Popups.Dialogs
{
    internal class PopupDialogContainer : PopupPage
    {
        public Action RequestClose { get; set; }
    }
}
