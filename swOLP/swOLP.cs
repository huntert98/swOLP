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
    public class swOLP : SwAddin
    {

        #region Variables
        public static swOLP Addin;
        SldWorks swApp;
        int swCookie;
        #endregion

        #region Connect/Disconnect
        public bool ConnectToSW(object ThisSW, int Cookie)
        {
            swApp = (SldWorks)ThisSW;
            Addin = this;
            swCookie = Cookie;
            System.Diagnostics.Debug.Print("Successfully loaded Open Log Page");
            return true;
        }
        public bool DisconnectFromSW()
        {
            Addin = null;
            GC.Collect();
            return true;
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
