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
    /// Interaction logic for CutomizedReferenceLists.xaml
    /// </summary>
    public partial class CutomizedReferenceLists : UserControl
    {
        public ObservableCollection<ReferenceList> ReferenceListCollection { get; set; } = new ObservableCollection<ReferenceList>();
        public ReferenceList SeletedReference { get; set; }
        internal CutomizedReferenceListPage cutomizedReferenceListPage;
        public CutomizedReferenceLists()
        {
            InitializeComponent();
            DescriptionTB.Text = GetDescription(CustomizedReferenceList.Instance, "ReferencesList");
        }

        public void Initialize()
        {
            CustomizedReferenceList.Instance.ReferencesList = CustomizedReferenceList.Instance.ReferencesList.Where(rlc => rlc.NameSpace != string.Empty && rlc.Assembles != string.Empty).ToList();
            ReferenceListCollection = new ObservableCollection<ReferenceList>(CustomizedReferenceList.Instance.ReferencesList);
            DGView.ItemsSource = ReferenceListCollection;
            CustomizedReferenceList.Instance.Save();
        }
        private void AddReference(object sender, RoutedEventArgs e)
        {
            ReferenceListCollection.Add(new ReferenceList());
        }

        private void RemoveReference(object sender, RoutedEventArgs e)
        {
            ReferenceList seletedRL =  DGView.SelectedItem as ReferenceList;
            if (ReferenceListCollection.Contains(seletedRL)) ReferenceListCollection.Remove(seletedRL);
            CustomizedReferenceList.Instance.ReferencesList = ReferenceListCollection.ToList();
            CustomizedReferenceList.Instance.Save();
        }

        private void CellChange(object sender, EventArgs e)
        {
            CustomizedReferenceList.Instance.ReferencesList = ReferenceListCollection.ToList();
            CustomizedReferenceList.Instance.Save();
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
