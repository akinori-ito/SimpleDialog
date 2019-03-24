using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NMeCab;

namespace SimpleDialog
{
    public class QADatabase
    {
        List<string[]> question;
        List<string> answer;
        public QADatabase(string filename, MeCabTagger tagger)
        {
            question = new List<string[]>();
            answer = new List<string>();
            using (var infile = new StreamReader(filename))
            {
                while (infile.Peek() != -1)
                {
                    var x = infile.ReadLine().Split(' ');
                    question.Add(Dialog.SplitWord(x[0], tagger));
                    answer.Add(x[1]);
                }
            }
        }
        static double Similarity(string[] x, string[] y)
        {
            var words = new HashSet<string>();
            foreach (string s in x)
            {
                words.Add(s);
            }
            int count = 0;
            foreach (string s in y)
            {
                if (words.Contains(s))
                {
                    count++;
                }
            }
            return (double)count / (Math.Sqrt((double)x.Length * y.Length));
        }

        public string NearestAnswer(string[] q)
        {
            double maxsim = -1;
            int maxind = -1;
            for (int i = 0; i < question.Count; i++)
            {
                double sim = Similarity(question[i], q);
                if (sim > maxsim)
                {
                    maxsim = sim;
                    maxind = i;
                }
            }
            return answer[maxind];
        }
    }
}
