using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
            SortUsingLine.IsChecked = General.Instance.IsSortandRemoveUsing;
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
            General.Instance.IsSortandRemoveUsing = SortUsingLine.IsChecked.Value;
            General.Instance.Save();
        }

        private void GotFocusEvent(Object sender, EventArgs e)
        {
            string Name = string.Empty;
            if (sender is TextBox)
            {
                TextBox textBox = sender as TextBox;
                Name = textBox.Name;
            }
            if(sender is CheckBox)
            {
                CheckBox checkBox = sender as CheckBox;
                Name = checkBox.Name;
            }

            switch (Name)
            {
                case "BusnissPathTextBox":
                    DescriptionTB.Text = GetDescription(General.Instance, "BusinessTierPath");
                    break;
                case "ActionCodeStartTextBox":
                    DescriptionTB.Text = GetDescription(General.Instance, "ActionCodeStartLine");
                    break;
                case "ActionCodeEndTextBox":
                    DescriptionTB.Text = GetDescription(General.Instance, "ActionCodeEndLine");
                    break;
                case "VaildationCodeStartTextBox":
                    DescriptionTB.Text = GetDescription(General.Instance, "ValidationCodeStartLine");
                    break;
                case "VaildationCodeEndTextBox":
                    DescriptionTB.Text = GetDescription(General.Instance, "ValidationCodeEndLine");
                    break;
                case "ScanBusinessObjectsCheckBox":
                    DescriptionTB.Text = GetDescription(General.Instance, "ScanBusinessObjects");
                    break;
                case "SortUsingLine":
                    DescriptionTB.Text = GetDescription(General.Instance, "IsSortandRemoveUsing");
                    break;
            }


        }

        private static string GetDescription<T>(T item, string propertyName) where T : class
        {
            PropertyInfo propertyInfo = typeof(T).GetProperties().FirstOrDefault(p => p.Name == propertyName);
            if (propertyInfo == null) return propertyName;
            else
            {
                return Attribute.IsDefined(propertyInfo, typeof(DescriptionAttribute)) ? (Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute)) as DescriptionAttribute).Description : propertyInfo.Name;
            }
        }
    }
}
