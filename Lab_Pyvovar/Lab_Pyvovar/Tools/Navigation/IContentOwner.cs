using System.Windows.Controls;

namespace Lab_Pyvovar.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}