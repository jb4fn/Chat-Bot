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
using System.Speech.Synthesis;
using System.Speech.Recognition;
namespace penny_v1._2
{
    /// <summary>
    /// Interaction logic for Dictation.xaml
    /// </summary>
    public partial class Dictation : Window
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechSynthesizer penny = new SpeechSynthesizer();
        PromptBuilder pbuild = new PromptBuilder();
        GrammarBuilder gbuild = new GrammarBuilder();
        Choices stpdictation = new Choices(new string[] { "no listening" });
        
       // Logic dictaionobj = new Logic();
        public Dictation()
        {
            InitializeComponent();
            sre.LoadGrammar(new DictationGrammar());
            gbuild.Append(stpdictation);
            Grammar gm = new Grammar(gbuild);
            sre.LoadGrammarAsync(gm);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognized += sre_SpeechRecognized;
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "no listening")
            {
                penny.Speak("Sure!");
                this.Close();
            
            }
            foreach (RecognizedWordUnit wrd in e.Result.Words)
            {
                textbox.Text += " " + wrd.Text;
            }
        }
    }
}
