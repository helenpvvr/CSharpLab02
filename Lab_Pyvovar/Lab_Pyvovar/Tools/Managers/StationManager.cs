using System;
using System.Windows.Forms;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools.DataStorage;

namespace Lab_Pyvovar.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static IDataStorage _dataStorage;

        internal static Person CurrentPerson { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            //MessageBox.Show("Close Window");
            //StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }

}