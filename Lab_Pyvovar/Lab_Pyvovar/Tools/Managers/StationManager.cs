using System;
using Lab_Pyvovar.Models;

namespace Lab_Pyvovar.Tools.Managers
{
    internal static class StationManager
    {
        internal static Person CurrentPerson { get; set; }

        internal static void CloseApp()
        {
            Environment.Exit(1);
        }
    }

}