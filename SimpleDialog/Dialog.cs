using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using NMeCab;
using System.Speech.Synthesis;
using System.Globalization;

namespace SimpleDialog
{
    public class Dialog
    {
        SpeechRecognizer recognizer;
        SpeechSynthesizer synth;
        MeCabTagger Tagger { get; }
        QADatabase db;

        public Dialog(string dbfile)
        {
            var subscriptionkey = "YOUR-MICROSOFT-COGNITIVESERVICE-SPEECH-KEY";
            var config = SpeechConfig.FromSubscription(subscriptionkey, "japaneast");
            config.SpeechRecognitionLanguage = "ja-JP";

            Tagger = NMeCab.MeCabTagger.Create();
            recognizer = new SpeechRecognizer(config);
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 0, new CultureInfo("ja-JP"));
            db = new QADatabase(dbfile,Tagger);
        }

        public static string[] SplitWord(string result, MeCabTagger tagger)
        {
            var rlist = new List<string>();
            var tagresult = tagger.ParseToNode(result);
            while (tagresult != null)
            {
                    if (tagresult.Stat != MeCabNodeStat.Bos &&
                        tagresult.Stat != MeCabNodeStat.Eos)
                    {
                        rlist.Add(tagresult.Surface);
                    }
                    tagresult = tagresult.Next;
            }
            return rlist.ToArray();
        }

        async Task RecognizeSpeechAsync(Action<SpeechRecognitionResult> callback)
        {
            Console.WriteLine("Say something...");

            var result = await recognizer.RecognizeOnceAsync();
            callback(result);
        }

        void Output(SpeechRecognitionResult result)
        {
            // Checks result.
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                var words = SplitWord(result.Text,Tagger);
                for (int i = 0; i < words.Length; i++) {
                    Console.WriteLine(words[i]);
                }
                string answer = db.NearestAnswer(words);
                Console.WriteLine("=> " + answer);
                if (answer == "exit")
                {
                    Environment.Exit(0);
                }
                synth.Speak(answer);
            }

        }

        public void DoDialog()
        {
            while (true)
            {
                RecognizeSpeechAsync(Output).Wait();
            }
        }

    }
}
