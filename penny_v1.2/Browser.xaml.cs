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
using System.Windows.Shapes;
using System.Web;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Net;
namespace penny_v1._2
{
    /// <summary>
    /// Interaction logic for Browser.xaml
    /// </summary>
    public partial class Browser : Window
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechSynthesizer penny = new SpeechSynthesizer();
        PromptBuilder pmbuild = new PromptBuilder();
        GrammarBuilder gbuild = new GrammarBuilder();
        public Browser()
        {
            InitializeComponent();
            //sre.LoadGrammar(new DictationGrammar());
            Choices serch = new Choices(new string[] { "google","facebook","shutdown","close browser" });
            gbuild.Append(serch);
            Grammar gm = new Grammar(gbuild);
            sre.LoadGrammar(gm);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += sre_SpeechRecognized;
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "shutdown")
            {
                this.Close();
            }
            if (e.Result.Text == "google")
            {
                wbrowser.Navigate("http://www.google.com");
            }
            if (e.Result.Text == "facebook")
            {
                wbrowser.Navigate("http://www.facebook.com");
            }
            else
            {
                wbrowser.Navigate("http://" + addbox.Text);
            }
            if (e.Result.Text == "close browser")
                this.Close();
        }

        private void addbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wbrowser.Navigate("http://" + addbox.Text);
        }
       
        
    }
}
