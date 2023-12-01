using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeeHelper.Options
{
    /// <summary>
    /// Interaction logic for GeneralOptions.xaml
    /// </summary>
    public partial class GeneralOptions : UserControl
    {
        internal GeneralOptionPage generalOptionsPage;
        public GeneralOptions()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            BusnissPathTextBox.Text = General.Instance.BusinessTierPath;
            ActionCodeStartTextBox.Text = General.Instance.ActionCodeStartLine;
            ActionCodeEndTextBox.Text = General.Instance.ActionCodeEndLine;
            VaildationCodeStartTextBox.Text = General.Instance.ValidationCodeStartLine;
            VaildationCodeEndTextBox.Text = General.Instance.ValidationCodeEndLine;
            ScanBusinessObjectsCheckBox.IsChecked = General.Instance.ScanBusinessObjects;
            SortUsingLine.IsChecked = General.Instance.IsRortandRemoveUsing;
            General.Instance.Save();
        }

        private void BusnissTierFolder(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    BusnissPathTextBox.Text = fbd.SelectedPath;
                    General.Instance.BusinessTierPath = fbd.SelectedPath;
                    General.Instance.Save();
                }
            }
        }

        private void ScanBusinessObjectClick(object sender, RoutedEventArgs e)
        {
            General.Instance.ScanBusinessObjects = ScanBusinessObjectsCheckBox.IsChecked.Value;
            General.Instance.Save();
        }

        private void ACStartChanged(object sender, TextChangedEventArgs e)
        {
            General.Instance.ActionCodeStartLine = ActionCodeStartTextBox.Text;
            General.Instance.Save();
        }

        private void ACEndChanged(object sender, TextChangedEventArgs e)
        {
            General.Instance.ActionCodeEndLine = ActionCodeEndTextBox.Text;
            General.Instance.Save();
        }

        private void VCStartChanged(object sender, TextChangedEventArgs e)
        {
            General.Instance.ValidationCodeStartLine = VaildationCodeStartTextBox.Text;
            General.Instance.Save();
        }

        private void VCEnbChanged(object sender, TextChangedEventArgs e)
        {
            General.Instance.ValidationCodeEndLine = VaildationCodeEndTextBox.Text;
            General.Instance.Save();
        }

        private void SortReferenceClick(object sender, RoutedEventArgs e)
        {
            General.Instance.IsRortandRemoveUsing = SortUsingLine.IsChecked.Value;
            General.Instance.Save();
        }
    }
}
