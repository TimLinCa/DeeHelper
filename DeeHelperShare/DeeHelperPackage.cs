using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;
using DeeHelper.Options;

namespace DeeHelper
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(GeneralOptionPage), "DeeReferenceHelper", "General", 0, 0, true)]
    [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "DeeReferenceHelper", "General", 0, 0, true)]

    [ProvideOptionPage(typeof(CutomizedReferenceListPage), "DeeReferenceHelper", "CustomizedReferenceList", 0, 0, true, SupportsProfiles = true)]
    [ProvideProfile(typeof(OptionsProvider.CustomizedReferenceListOptions), "DeeReferenceHelper", "CustomizedReferenceList", 0, 0, true)]

    [ProvideOptionPage(typeof(BusinessTierPathPage), "DeeReferenceHelper", "BusinessTierPathList", 0, 0, true, SupportsProfiles = true)]
    [ProvideProfile(typeof(OptionsProvider.BusinessTierPathListOptions), "DeeReferenceHelper", "BusinessTierPathList", 0, 0, true)]

    [Guid(PackageGuids.DeeHelperString)]
    [ProvideToolWindow(typeof(GetActionGroupWindow.GetActionGroupWindow))]
    public sealed class DeeHelperPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
            await GetActionGroupWindow.GetActionGroupWindowCommand.InitializeAsync(this);
        }
    }


}