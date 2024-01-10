using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using DisplayNameAttribute = System.ComponentModel.DisplayNameAttribute;

namespace DeeHelper
{
    internal partial class OptionsProvider
    {
        // Register the options with these attributes on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "MyExtension", "General", 0, 0, true)]
        // [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "MyExtension", "General", 0, 0, true)]
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("General")]
        [DisplayName("CMF BusinessTier Folder Path ")]
        [Description("The using assemblies to be used will be determined by the DLL file located in this folder. Please select the ‘businessTier’ folder of the project")]
        [DefaultValue(@"C:\Program Files\CriticalManufacturing\BusinessTier")]
        public string BusinessTierPath { get; set; } = @"C:\Program Files\CriticalManufacturing\BusinessTier";

        [Category("Dee Detected Options")]
        [Description("Use this string as a marker to identify the starting line of the action code. This string can represent multiple values, which are separated by semicolons.")]
        [DisplayName("ActionCodeStartLine")]

        public string ActionCodeStartLine { get; set; } = "START OF USER-DEFINED CODE;Start DEE Code";
        [Category("Dee Detected Options")]
        [Description("Use this string as a marker to identify the ending line of the action code. This string can represent multiple values, which are separated by semicolons.")]
        [DisplayName("ActionCodeEndLine")]
       
        public string ActionCodeEndLine { get; set; } = "END OF USER-DEFINED CODE;End DEE Code";

        [Category("Dee Detected Options")]
        [Description("Use this string as a marker to identify the starting line of the validation code. This string can represent multiple values, which are separated by semicolons.")]
        [DisplayName("ValidationCodeStartLine")]

        public string ValidationCodeStartLine { get; set; } = "START OF USER-DEFINED VALIDATION CODE;Start DEE Condition Code";
        [Category("Dee Detected Options")]
        [Description("Use this string as a marker to identify the ending line of the validation code. This string can represent multiple values, which are separated by semicolons.")]
        [DisplayName("ValidationCodeEndLine")]
        
        public string ValidationCodeEndLine { get; set; } = "END OF USER-DEFINED VALIDATION CODE;End DEE Condition Code";

        [Category("Advanced Options")]
        [Description("Enable this option to scan used custom assemblies. Disable this option to utilize all custom assemblies.")]
        public bool ScanBusinessObjects { get; set; } = true;

        [Category("Advanced Options")]
        [Description("Enable this option to sort and remove unused assemblies.")]
        public bool IsSortandRemoveUsing { get; set; } = true;

    }
}
