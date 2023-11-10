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
    [Guid(PackageGuids.DeeHelperString)]

    public class GeneralOptionPage : UIElementDialogPage
    {
        protected override UIElement Child
        {
            get
            {
                GeneralOptions page = new GeneralOptions
                {
                    generalOptionsPage = this
                };
                page.Initialize();
                return page;
            }
        }
    }
}
