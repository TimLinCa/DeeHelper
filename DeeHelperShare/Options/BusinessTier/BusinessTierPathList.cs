using Community.VisualStudio.Toolkit;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DeeHelper
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.BusinessTierPathListOptions), "DeeHelper", "BusinessTierPathList", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class BusinessTierPathListOptions : BaseOptionPage<BusinessTierPathList> { }
    }

    public class BusinessTierPathList : BaseOptionModel<BusinessTierPathList>
    {
        [Category("BusinessTierPathList")]
        [DisplayName("BusinessTierPathList")]
        [Description("Click ADD button to add a project name and businessTier folder path. *Project name is unique.")]
        public List<BusinessTierPathObj> BusinessTierPaths { get; set; }
    }

    public class BusinessTierPathObj
    {
        public string ProjectName { get; set; }
        public string BusinessTierFolderPath { get; set; }
    }
}
