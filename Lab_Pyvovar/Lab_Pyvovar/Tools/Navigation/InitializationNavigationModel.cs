using System;
using Lab_Pyvovar.Tools.Navigation;
using Lab_Pyvovar.View;

namespace Lab_Pyvovar.Tools
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.EnterInfo:
                    ViewsDictionary.Add(viewType, new EnterInfoView());
                    break;
                case ViewType.Info:
                    ViewsDictionary.Add(viewType, new InfoView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}