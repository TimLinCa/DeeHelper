using Community.VisualStudio.Toolkit;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DeeHelper
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.CustomizedReferenceListOptions), "DeeHelper", "CustomizedReferenceList", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class CustomizedReferenceListOptions : BaseOptionPage<CustomizedReferenceList> { }
    }

    public class CustomizedReferenceList : BaseOptionModel<CustomizedReferenceList>
    {
        [Category("CustomizedReferenceList")]
        [DisplayName("ReferenceList")]
        [Description("Define the corresponding assembly of the namespace. Assemblies can be multiple values separated by semicolons.")]
        public List<ReferenceList> ReferencesList { get; set; } = new List<ReferenceList>() { new ReferenceList() { NameSpace = "Z.Expressions", Assembles = "Z.Expressions.Eval.dll" } };
    }

    public class ReferenceList
    {
        public string NameSpace { get; set; }
        public string Assembles { get; set; }
    }
}
