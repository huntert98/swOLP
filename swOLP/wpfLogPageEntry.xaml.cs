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

namespace swOLP
{
    /// <summary>
    /// Interaction logic for wpfLogPageEntry.xaml
    /// </summary>
    public partial class wpfLogPageEntry : UserControl
    {
        public string msgText { get; }
        public string msgInner { get; }
        public DateTime msgTime { get;}
        public logEntryType msgType { get; }
        public string FullMessage
        {
            get
            {
                return "WIP";
                //return ((Run)LogBlock.Inlines.FirstInline).Text + ((Run)LogBlock.Inlines.LastInline).Text +
                //    LineText.Contains("Submitted exception as bug") ? "" : InsideMessage == "" ? "" : Constants.vbNewLine +
                //    Constants.vbTab + String.Join(msgInner.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None), Environment.NewLine + "\t");
            }
        }

        public wpfLogPageEntry(string text, string innerText, logEntryType entryType)
        {
            InitializeComponent();
            msgTime = DateTime.Now;
            msgText = text;
            msgInner = innerText;
            msgType = entryType;
            SetFullMessage();
        }
        private void SetFullMessage()
        {
            string dateStr = "[" + string.Format("{0:HH:mm:ss tt}", DateTime.Now) + "]";

            LogBlock.Inlines.Add(new Run(dateStr + msgType.msgPrefix + " ")  { Foreground = new SolidColorBrush(msgType.msgColor), Background = new SolidColorBrush(Colors.Transparent) });
            LogBlock.Inlines.Add(new Run(msgText) { Foreground = new SolidColorBrush(msgType.msgColor) });
            InnerMessage.Text = msgInner;
        }

        public class logEntryType
        {
            public string msgPrefix { get; set; }
            public Color msgColor { get; set; }

        }
    }
}
