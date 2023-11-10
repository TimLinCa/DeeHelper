using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace DeeHelper
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.General1Options), "DeeHelper", "General1", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class General1Options : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("General")]
        [DisplayName("CMF BusinessTier Folder Path ")]
        [Description("BusinessTier FodlerPath.")]
        [DefaultValue(@"C:\Program Files\CriticalManufacturing\BusinessTier")]
        public string BusinessTierPath { get; set; } = @"C:\Program Files\CriticalManufacturing\BusinessTier";
        public string ActionCodeStartLine { get; set; } = "START OF USER-DEFINED CODE";
        public string ActionCodeEndLine { get; set; } = "END OF USER-DEFINED CODE";
        public string ValidationCodeStartLine { get; set; } = "START OF USER-DEFINED VALIDATION CODE";
        public string ValidationCodeEndLine { get; set; } = "END OF USER-DEFINED VALIDATION CODE";

        public bool ScanBusinessObjects { get; set; } = true;
        public bool IsRortandRemoveUsing { get; set; } = true;

    }
}
