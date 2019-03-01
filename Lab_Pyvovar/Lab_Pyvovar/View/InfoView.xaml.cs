using System.Windows.Controls;
using Lab_Pyvovar.Tools.Navigation;

namespace Lab_Pyvovar.View
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl, INavigatable
    {
        private readonly InfoViewModel _infoViewModel = new InfoViewModel();

        public InfoView()
        {
            InitializeComponent();
            DataContext = _infoViewModel;
        }

        INavigatable INavigatable.RefreshInfo()
        {
            _infoViewModel.RefreshInfo();
            return this;
        }
    }
}
