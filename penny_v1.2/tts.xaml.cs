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
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
namespace penny_v1._2
{
    /// <summary>
    /// Interaction logic for tts.xaml
    /// </summary>
    public partial class tts : Window
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechSynthesizer penny = new SpeechSynthesizer();
        PromptBuilder pmbuild = new PromptBuilder();
        GrammarBuilder gbuld = new GrammarBuilder();
        public tts()
        {
            InitializeComponent();
            Choices close = new Choices(new string[] { "finish this","speek this" });
            gbuld.Append(close);
            Grammar gm = new Grammar(gbuld);
            sre.LoadGrammar(gm);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += sre_SpeechRecognized;
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "speek this")
            {
                pmbuild.AppendText(contentbox.Text);
                penny.Speak(pmbuild);
                pmbuild.ClearContent();
            }
            if (e.Result.Text == "finish this")
            {
                penny.Speak("Ok");
                this.Close();
            }
            
        }

       
    }
}
