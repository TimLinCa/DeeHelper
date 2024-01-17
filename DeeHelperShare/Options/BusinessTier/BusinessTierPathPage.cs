using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.VisualStudio.Shell;

namespace DeeHelper.Options
{
    [ComVisible(true)]
    [Guid("37848a16-18a5-42ec-8d3e-a43fd7d28d34")]

    public class BusinessTierPathPage : UIElementDialogPage
    {
        protected override UIElement Child
        {
            get
            {
                BusinessTierPath page = new BusinessTierPath
                {
                    businessTierPathPage = this
                };
                page.Initialize();
                return page;
            }
        }
    }
}
