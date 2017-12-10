using System;
using System.Collections.Generic;
using Prism.Navigation;
using Xamarin.Forms;

namespace PopupPluginSample.Views
{
    public partial class MDPRoot : MasterDetailPage, IMasterDetailPageOptions
    {
        public MDPRoot()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}
