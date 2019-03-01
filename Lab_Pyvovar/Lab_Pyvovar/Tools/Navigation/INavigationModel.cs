namespace Lab_Pyvovar.Tools.Navigation
{
    internal enum ViewType
    {
        EnterInfo,
        Info
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
