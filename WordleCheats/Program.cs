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

		static void Main(string[] args)
		{
			IList<string> availableWords = Words.probableWords.ToList();

			//foreach (var x in Words.words.Where(x => x[1] == 'i' && x[2] == 'n' && x[4] == 'a'))
			//	Console.WriteLine(x);

			//var results = new int[10];
			//uint j = 0;

			//foreach (var item in Words.words)
			//{
			//	j++;
			//	if (BitOperations.PopCount(j) == 2)
			//		Console.WriteLine(j);
			//	results[simulate(item)]++;
			//}

			//for (int i = 0; i < 10; i++)
			//{
			//	Console.WriteLine($"{i}: {results[i]}");
			//}

			//availableWords = availableWords.GroupBy(x => compareWords("olate", x)).First(x => translate("wggwg") == x.Key).ToList();

			while (availableWords.Count > 1)
			{
				var word = getBestWord(availableWords);
				Console.WriteLine(word.word);
				string response = Console.ReadLine();
				int r = translate(response);
				availableWords = word.partition.First(x => x.Key == r).ToList();
			}

			Console.WriteLine(availableWords[0]);
		}


	}
}