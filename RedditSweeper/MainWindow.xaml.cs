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
        public const string REDDIT_URL =  @"http://www.reddit.com";
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
            //string html = web.DownloadString("http://www.reddit.com/r/programming");
            //string html = web.DownloadString("http://www.reddit.com/r/dailyprogrammer");
            string html = web.DownloadString("http://www.reddit.com/r/learnprogramming/");
            List<string> urlList = GetTitleURL(html);
            List<string>  titleList = GetTitle(html);
            List<int> scoreList = GetScore(html);
        }

        private List<string> GetTitleURL(string html)
        {
            // Get url information from reddit sub for all the posts
            MatchCollection urlInfo = Regex.Matches(html, @"(?<=title may-blank "" href="")((https?:[-A-Za-z./0-9._~:/?#@!$&'()\[\]*+,;=]+)|(.*?))(?="")", RegexOptions.None);
            List<string> urlList = new List<string>();

            foreach (Match m in urlInfo)
            {
                if (!string.IsNullOrEmpty(m.ToString()) && m.ToString().Substring(0, 2).Equals("/r"))
                    urlList.Add(REDDIT_URL + m.ToString());
                else if (!string.IsNullOrEmpty(m.ToString()))
                    urlList.Add(m.ToString());
                else
                    urlList.Add(string.Empty);
            }
            return urlList;
        }

        private List<string> GetTitle(string html)
        {
            // Get title information from reddit sub for all the posts
            MatchCollection titleInfo = Regex.Matches(html, @"(?<=(tabindex=""1"" >)|(?<=tabindex=""1"" rel=""nofollow"" >))(.*?)(?=</a>)", RegexOptions.None);
            List<string> titleList = new List<string>();

            foreach (Match m in titleInfo)
            {
                if (!string.IsNullOrEmpty(m.ToString()))
                    titleList.Add(m.ToString());
                else
                    titleList.Add(string.Empty);
            }
            return titleList;
        }

        private List<int> GetScore(string html)
        {
            // Get score information from reddit sub for all the posts
            MatchCollection scoreInfo = Regex.Matches(html, @"(?<=class=""score unvoted"">)(.*?)(?=</div>)", RegexOptions.None);
            List<int> scoreList = new List<int>();

            foreach (Match m in scoreInfo)
            {
                if (!string.IsNullOrEmpty(m.ToString()) && m.ToString() != "&bull;")
                    scoreList.Add(Convert.ToInt32(m.ToString()));
                else
                    scoreList.Add(0);
            }
            return scoreList;
        }
    }

}
