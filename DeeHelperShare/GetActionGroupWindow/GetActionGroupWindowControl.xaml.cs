using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace DeeHelper.GetActionGroupWindow
{
    /// <summary>
    /// Interaction logic for GetActionGroupWindowControl.
    /// </summary>
    public partial class GetActionGroupWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetActionGroupWindowControl"/> class.
        /// </summary>
        public GetActionGroupWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "GetActionGroupWindow");
        }

        private void GenerateURLCommand(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(URLTextBox.Text)) return;
            char[] splitstring = { '/' };
            string[] urlsplit = URLTextBox.Text.Split(splitstring);
            if (urlsplit.Length != 6)
            {
                MessageBox.Show("It's not a CMF URL");
                return;
            }
            string service = urlsplit[4];
            string operation = urlsplit[5];

            ActionGroupPreTextBox.Text = $"{service}Management.{service}ManagementOrchestration.{operation}.Pre";
            ActionGroupPostTextBox.Text = $"{service}Management.{service}ManagementOrchestration.{operation}.Post";
        }
    }
}