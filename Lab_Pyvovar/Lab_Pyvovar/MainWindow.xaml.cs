using System.Windows;
using System.Windows.Controls;
using Lab_Pyvovar.Tools;
using Lab_Pyvovar.Tools.Managers;
using Lab_Pyvovar.Tools.Navigation;
using Lab_Pyvovar.View;

namespace Lab_Pyvovar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.EnterInfo);
        }
    }
}
