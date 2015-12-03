using System.Windows;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
       
        public MainWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - Width;
        }
    }
}
