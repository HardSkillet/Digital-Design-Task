using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassHomework
{
    public class Homework
    {
        static private Dictionary<string, int> answer;
        static private Regex regex;
        static Homework() {
            answer = new Dictionary<string, int>();
            regex = new Regex(@"\w\w*[']?\w*|\w*[']?\w*\w");
        }
        static private void ParsingText(string str)
        {
            MatchCollection matches = regex.Matches(str);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (Regex.IsMatch(match.Value, @"\d\d*")) continue;
                    lock (answer)
                    {
                        if (answer.ContainsKey(match.Value.ToLower())) answer[match.Value.ToLower()]++;
                        else answer.Add(match.Value.ToLower(), 1);
                    }
                }
            }
        }
        public Homework(string path)
        {
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
        public static void GetAnswer()
        {
            File.WriteAllLines("answer.txt", answer
                .OrderByDescending(pair => pair.Value)
                .Select(pair => pair.Key + " " + pair.Value)
                .ToArray());
        }
    }
}