using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolidWorks.Interop.sldworks;

namespace swOLP
{
    /// <summary>
    /// Interaction logic for wpfLogPage.xaml
    /// </summary>
    public partial class wpfLogPage : UserControl
    {
        public wpfLogPage()
        {
            InitializeComponent();
        }
        public void WriteLine(string text, wpfLogPageEntry.logEntryType type, string InnerMsg = "")
        {
            wpfLogPageEntry newLine = new wpfLogPageEntry(text, InnerMsg, type);
            LogBoxList.Items.Add(newLine);
            LogBoxList.SelectedIndex = LogBoxList.Items.Count - 1;
            LogBoxList.ScrollIntoView(LogBoxList.SelectedItem);
        }
        public string GetAllLines()
        {
            string strBuilder = "";
            foreach (wpfLogPageEntry logLine in LogBoxList.Items)
                strBuilder += logLine.FullMessage + System.Environment.NewLine;
            return strBuilder.ToString();
        }
    }
}
