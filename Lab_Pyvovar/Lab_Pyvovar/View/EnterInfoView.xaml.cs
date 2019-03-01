using System.Windows.Controls;
using Lab_Pyvovar.Tools.Navigation;

namespace Lab_Pyvovar.View
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class EnterInfoView : UserControl, INavigatable
    {
        private readonly EnterInfoViewModel _enterInfoViewModel = new EnterInfoViewModel();

        public EnterInfoView()
        {
            InitializeComponent();
            DataContext = _enterInfoViewModel;
        }

        INavigatable INavigatable.RefreshInfo()
        {
            _enterInfoViewModel.RefreshInfo();
            return this;
        }
    }
}
