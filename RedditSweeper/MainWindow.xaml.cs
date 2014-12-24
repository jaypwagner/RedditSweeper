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
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace RedditSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ICommand _sweepCommand;

        public ICommand SweepCommand
        {
            get
            {
                if (_sweepCommand == null)
                {
                    _sweepCommand = new RelayCommand(
                        ParamArrayAttribute => this.SaveObject(),
                        ParamArrayAttribute => this.CanSave()
                        );
                }
                return _sweepCommand;
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private void SaveObject()
        {
            MessageBox.Show("Message with Help file and Help navigator.",
                                               "Help Caption");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient web = new WebClient();
            String html = web.DownloadString("http://www.reddit.com/r/programming/");
            string beforeTitle = @"tabindex=""1"" >";
            string afterTitle = @"</a>&#32;";
            string matchAddress = @"/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/";
            string otherMatchAddress = @"^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$";
            MatchCollection fullInfo = Regex.Matches(html, @"title may-blank\s*(.+?)\s*</a>", RegexOptions.None);
            //MatchCollection pClassTitle = Regex.Matches(html, @"k "" href=\s*(.+?)\s*</a>", RegexOptions.None);
            string x = "http://www.reddit.com";
            MatchCollection m1 = Regex.Matches(html, @"(https?:\/\/)[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)\/+[a-zA-Z0-9\-\.\/]*", RegexOptions.None);
           // MatchCollection m2 = Regex.Matches(html, @"http://www.", RegexOptions.None);
            //MatchCollection mOther = new MatchCollection();
            MatchCollection titleLink = null;
            StringBuilder sB = new StringBuilder();
            string someStuff = "";
            foreach (Match m in fullInfo)
            {
                var title = m.ToString();
                sB.Append(m.ToString());
                someStuff += title;
               
            }
            titleLink = Regex.Matches(sB.ToString(), @"(https?:\/\/)[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)", RegexOptions.None);
            string thisStuff = "";
            foreach (var m in titleLink)
            {
                thisStuff += m.ToString();
            }
            
            //SharpMap sm = new SharpMap();
            int i = 1;
            i++;
        }
    }

}
