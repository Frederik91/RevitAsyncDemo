using API;
using System.Windows;

namespace MyRevitAddinCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IController m_controller;

        public MainWindow(IController controller)
        {
            m_controller = controller;
            
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var document = await m_controller.DocumentController.Get(string.Empty);
            DocumentTitle.Content = document.Title;
        }
    }
}
