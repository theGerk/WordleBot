using static ClassLibrary1.Class1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Xml.Schema;
using ClassLibrary1;

namespace WordleCheats
{
	public class Program
	{

		static string getWord()
		{
			Console.WriteLine("Type in answer");
			return Console.ReadLine();
		}

		static void Main(string[] args)
		{
			//var answer = getWord();

			//IList<string> availableWords = Words.words.Union(new string[] { answer }).ToList();

			//foreach (var x in Words.words.Where(x => x[1] == 'i' && x[2] == 'n' && x[4] == 'a'))
			//	Console.WriteLine(x);

			IList<string> availableWords = Words.probableWords.ToList();

			while (availableWords.Count > 1)
			{
				var word = getBestWord(availableWords);
				Console.WriteLine(word.word + "    " + availableWords.Count);
				string response = Console.ReadLine();
				int r = translate(response);
				//int r = compareWords(word.word, answer);
				//int r1 = r;
				//string sb = "";
				//for(int i = 0; i < 5; i++)
				//{
				//	switch (r1 % 3)
				//	{
				//		case GREEN:
				//			sb = "g" + sb;
				//			break;
				//		case YELLOW:
				//			sb = "y" + sb;
				//			break;
				//		default:
				//			sb = "w" + sb;
				//			break;
				//	}
				//	r1 /= 3;
				//}
				//Console.WriteLine(sb);
				availableWords = word.partition.First(x => x.Key == r).ToList();
			}

			Console.WriteLine(availableWords[0]);
		}


	}
}