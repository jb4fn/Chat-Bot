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
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace penny_v1._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine sreng = new SpeechRecognitionEngine();
        SpeechSynthesizer penny = new SpeechSynthesizer();
        PromptBuilder pbuild = new PromptBuilder();
        int count = 1;
        DateTime _date = DateTime.Now;
        Process myprocess;
        static int flag = 0;
        Random ran = new Random();
        
        /// <summary>
        /// import user32.dll to intract with user environment
        /// FindWindow method find whether requested window is running or not.
        /// SetForegroundWindow set (if running) window to foreground.
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        //always contain goodbye penny command to exit application any time.
        public MainWindow()
        {
            InitializeComponent();
            Choices Close = new Choices();
            Close.Add(new string[] { "goodbye penny","Dictation","dictate","convert text",
            "switch window","open browser","open notepad","close notepad","penny","what time is it","what date is it","what day is it" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(Close);
            Grammar g = new Grammar(gb);
            sreng.SetInputToDefaultAudioDevice();
            sreng.UnloadAllGrammars();
            sreng.LoadGrammar(g);
            sreng.RecognizeAsync(RecognizeMode.Multiple);
            penny.Rate = -1;
            sreng.SpeechRecognized += sreng_SpeechRecognized;

        }

        void sreng_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string time = _date.GetDateTimeFormats('t')[0];
            int randno;
            string res = e.Result.Text;
            switch (res)
            {
                case "penny":
                    randno = ran.Next(1, 4);
                    if (randno == 1)
                        penny.Speak("Yes sir");
                    else if (randno == 2) {  penny.Speak("at your service sir!"); }
                    else if (randno == 3) { penny.Speak("I am here sir"); }
                    break;

                case "goodbye penny":
                    {
                        sreng.RecognizeAsyncStop();
                        penny.Speak("It was a great! Pleasure working with you.");
                        this.Close();
                    }
                    break;
                case "what time is it":
                    penny.Speak(time);
                    break;
                case "what date is it":
                    penny.Speak(DateTime.Today.ToString("dd-MM-yyyy"));
                    break;
                case "what day is it":
                    penny.Speak(DateTime.Today.ToString("dddd"));
                    break;
                case "Dictation":
                case "dictate":
                    {
                        penny.Speak("here we are");
                        var dictationwin = new Dictation();
                        dictationwin.Show();

                    }
                    break;

                case "convert text":
                    {
                        penny.Speak("let's do it");
                        var ttswin = new tts();
                        ttswin.Show();
                    }
                    break;
                case "open browser":
                    {
                        var browserwin = new Browser();
                        browserwin.Show();
                    }
                    break;

                case "open notepad":
                    {
                        if (flag == 0)
                        {
                            penny.Speak("opening application");
                            myprocess = System.Diagnostics.Process.Start("notepad");
                            flag = 1;

                        }
                        else
                            penny.Speak("notepad already open");
                    }
                    break;
                case "close notepad":
                    {
                        if (flag == 1)
                        {
                            penny.Speak("Closing application");
                            myprocess.Kill();
                            flag = 0;
                        }
                    }
                    break;
                case "switch window":

                    SendKeys.SendWait("%{TAB " + count + "}");


                    count += 1;
                    break;
            }
        }


        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{

        //    if (Login_name.Text == "nain" && Login_pass.Password == "nain")
        //        first_tab.Visibility = System.Windows.Visibility.Hidden;
        //    else
        //    {
        //        penny.Speak("Please try again");

        //    }


        //}


    }
}
