using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DeeHelper
{
    internal partial class OptionsProvider
    {
        // Register the options with this attribute on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.KeywordReferencesOptions), "DeeHelper", "KeywordReferences", 0, 0, true, SupportsProfiles = true)]
        [ComVisible(true)]
        public class KeywordReferencesOptions : BaseOptionPage<KeywordReferences> { }
    }

    public class KeywordReferences : BaseOptionModel<KeywordReferences>
    {
        [Category("My category")]
        [DisplayName("My Option")]
        [Description("An informative description.")]
        public bool MyOption { get; set; } = true;
    }
}
