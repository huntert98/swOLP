using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.sldworks;

namespace swOLP
{
    [Guid("eefe67dd-17c0-41a1-823e-691dae2166d0"), ComVisible(true)]
    [ProgId(swOLP.App_PROGID)]
    public class swOLP : SwAddin
    {

        #region Variables
        public static swOLP Addin;
        private TaskpaneView logPageView;
        private TPHost tpHost;
        public SldWorks swApp;
        private int swCookie;
        private wpfLogPage LogPage;
        public const string TP_PROGID = "SolidWorks.OpenLogPage.LogPageCtl";
        public const string App_PROGID = "SolidWorks.OpenLogPage";
        #endregion

        #region Connect/Disconnect
        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            swApp = (SldWorks)ThisSW;
            Addin = this;
            swCookie = Cookie;
            System.Diagnostics.Debug.Print("Successfully loaded Open Log Page");
            bool result = swApp.SetAddinCallbackInfo(0, this, Cookie);
            AddLogPage();
            LogPage.WriteLine("Connected to Solidworks", new wpfLogPageEntry.logEntryType { msgColor = System.Windows.Media.Colors.Green, msgPrefix = "[OLP]" });
            return true;
        }
        public bool DisconnectFromSW()
        {
            Addin = null;
            RemoveLogPage();
            GC.Collect();
            return true;
        }
        #endregion

        #region Create Log Page
       private void AddLogPage()
        {
            string[] img = { @"E:\ownCloud\Projects\Resources\20x.png", @"E:\ownCloud\Projects\Resources\32x.png",
                             @"E:\ownCloud\Projects\Resources\40x.png", @"E:\ownCloud\Projects\Resources\64x.png",
                             @"E:\ownCloud\Projects\Resources\96x.png", @"E:\ownCloud\Projects\Resources\128x.png"};

            logPageView = swApp.CreateTaskpaneView3(img, "Log Page");
            tpHost = logPageView.AddControl(TP_PROGID, string.Empty);
            LogPage = new wpfLogPage();
            tpHost.AddWpfControl(LogPage);
        }
        private void RemoveLogPage()
        {
            logPageView.DeleteView();
            Marshal.ReleaseComObject(logPageView);
            logPageView = null;
            tpHost = null;
        }
        #endregion

        #region Public Members
        public void Write(string text, string prefix = "[MSG]", byte[] argb = null)
        {
            if(argb ==null)
            {
                argb = new byte[] { 255, 0, 128, 0 };
            }
            LogPage.WriteLine(text, new wpfLogPageEntry.logEntryType { msgColor = System.Windows.Media.Color.FromArgb(argb[0],argb[1],argb[2],argb[3]), msgPrefix = prefix });
        }
        #endregion

        #region COM_REGISTER
        [ComRegisterFunction()]
        public static void RegisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

            string keyname = @"SOFTWARE\SolidWorks\Addins\{" + t.GUID.ToString() + "}";
            Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
            addinkey.SetValue(null, 0);
            addinkey.SetValue("Description", "Open Source Log Page for Solidworks");
            addinkey.SetValue("Title", "Open Log Page");

            keyname = @"Software\SolidWorks\AddInsStartup\{" + t.GUID.ToString() + "}";
            addinkey = hkcu.CreateSubKey(keyname);
            addinkey.SetValue(null, 1);
        }
        [ComUnregisterFunction()]
        public static void UnregisterFunction(Type t)
        {
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

            string keyname = @"SOFTWARE\SolidWorks\Addins\{" + t.GUID.ToString() + "}";
            hklm.DeleteSubKey(keyname);

            keyname = @"Software\SolidWorks\AddInsStartup\{" + t.GUID.ToString() + "}";
            hkcu.DeleteSubKey(keyname);
        }
        #endregion
    }
}
