using API.Controllers;
using System.Windows;

namespace MyRevitAddinCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DocumentController m_controller;

        public MainWindow(DocumentController controller)
        {
            m_controller = controller;
            DocumentTitle.Content = m_controller.Get(string.Empty);
            InitializeComponent();
        }
    }
}
