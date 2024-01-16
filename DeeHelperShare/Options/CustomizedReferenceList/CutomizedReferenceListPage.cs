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
    [Guid("D8B47497-8AC9-4E2E-9D62-D8E8E7A47AA4")]
    public class CutomizedReferenceListPage : UIElementDialogPage
    {
        protected override UIElement Child
        {
            get
            {
                CutomizedReferenceLists page = new CutomizedReferenceLists
                {
                    cutomizedReferenceListPage = this
                };
                page.Initialize();
                return page;
            }
        }
    }
}
