using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace War_and_piece
{
    class Program
    {
        static Dictionary<string, int> answer = new Dictionary<string, int>();
        static Regex regex = new Regex(@"\w\w*[']?\w*|\w*[']?\w*\w");
        static public void ParsingText(string str) {
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (Regex.IsMatch(match.Value, @"\d\d*")) continue;
                    lock(answer) {
                        if (answer.ContainsKey(match.Value.ToLower())) answer[match.Value.ToLower()]++;
                        else answer.Add(match.Value.ToLower(), 1);
                    } 
                }
            }
        }
        static void ReadText(string path) {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        ParsingText(sr.ReadLine());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void Main()
        {
            var taskArray = new Task[4];
            for (int i = 1; i < 5; ++i) {
                {
                    var temp = i;
                    taskArray[temp - 1] = Task.Run(() => ReadText("Tom" + temp + ".txt"));
                }
            }
            Task.WaitAll(taskArray);
            File.WriteAllLines("answer.txt", answer
                .OrderByDescending(pair => pair.Value)
                .Select(pair => pair.Key + " " + pair.Value)
                .ToArray());
        }
    }
}
