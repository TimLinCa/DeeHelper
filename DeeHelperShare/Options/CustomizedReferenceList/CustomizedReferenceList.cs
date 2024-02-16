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
        [Description("Define the corresponding assembly of the namespace. NameSpace and assemblies can be multiple values separated by semicolons. 1. NameSpace > Assemblies THEN 2. Keyword > NameSpace > Assemblies")]
        public List<ReferenceList> ReferencesList { get; set; } = new List<ReferenceList>() {
            new ReferenceList() { NameSpace = "Z.Expressions", Assembles = "Z.Expressions.Eval.dll" },
            new ReferenceList() { KeyWord="%.AsEnumerable()%", Assembles = @"%MicrosoftNetPath%\\System.ComponentModel;" + @"%MicrosoftNetPath%\\System.ComponentModel.Primitives;" + @"%MicrosoftNetPath%\\System.Xml.ReaderWriter;" + @"%MicrosoftNetPath%\\System.private.Xml" },
            new ReferenceList() { NameSpace="%.Erp", Assembles = "Cmf.Custom.BusinessObjects.ErpCustomManagement.dll" }
        };
    }

    public class ReferenceList
    {
        public string KeyWord { get; set; }
        public string NameSpace { get; set; }
        public string Assembles { get; set; }
    }
}
