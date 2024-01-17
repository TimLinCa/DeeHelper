using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace DeeHelper.Options
{
    /// <summary>
    /// Interaction logic for BusinessTierPath.xaml
    /// </summary>
    public partial class BusinessTierPath : UserControl
    {
        internal BusinessTierPathPage businessTierPathPage;

        public BusinessTierPathObj SeletedPath { get; set; }
        public ObservableCollection<BusinessTierPathObj> BusinessTierPathCollection { get; set; } = new ObservableCollection<BusinessTierPathObj>();

        public BusinessTierPath()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            var test = BusinessTierPathList.Instance.BusinessTierPaths;
            if (BusinessTierPathList.Instance.BusinessTierPaths!= null)
            {
                BusinessTierPathCollection = new ObservableCollection<BusinessTierPathObj>(BusinessTierPathList.Instance.BusinessTierPaths);
            }
            DescriptionTB.Text = GetDescription(BusinessTierPathList.Instance, "BusinessTierPaths");
            PathLB.ItemsSource = BusinessTierPathCollection;
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            using (var form = new AddBusinessTierPathForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (BusinessTierPathCollection.Any(ob => ob.ProjectName == form.ProjectName)) System.Windows.MessageBox.Show($"{form.ProjectName} already exist on this list.");
                    else
                    {
                        BusinessTierPathCollection.Add(new BusinessTierPathObj() { ProjectName = form.ProjectName,BusinessTierFolderPath=form.BusinessTierFolderPath }) ;
                        BusinessTierPathList.Instance.BusinessTierPaths = BusinessTierPathCollection.ToList();
                        BusinessTierPathList.Instance.Save();
                    } 
                }
            }
        }

        private void RemoveProject(object sender, RoutedEventArgs e)
        {
            if (SeletedPath != null)
            {
                BusinessTierPathCollection.Remove(SeletedPath);
                BusinessTierPathList.Instance.BusinessTierPaths = BusinessTierPathCollection.ToList();
                BusinessTierPathList.Instance.Save();
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
