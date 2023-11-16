using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
	public static class Class1
	{
		public const int WHITE = 0;
		public const int GREEN = 1;
		public const int YELLOW = 2;
		public const int WORD_LENGTH = 5;

		public static int simulate(string answer)
		{
			IList<string> availableWords = Words.probableWords.ToList();

			for (int i = 1; ; ++i)
			{
				var guess = i != 1 ? getBestWord(availableWords) : new BestWord("olate", availableWords.GroupBy(x => compareWords("olate", x)).ToList());

				if (guess.word == answer)
					return i;
				availableWords = guess.partition.First(x => x.Key == compareWords(guess.word, answer)).ToList();
			}
		}

		public static int translate(string response)
		{
			int x = 0;
			for (int i = 0; i < WORD_LENGTH; i++)
			{
				x *= 3;
				switch (response[i])
				{
					case 'g':
						x += GREEN;
						break;
					case 'y':
						x += YELLOW;
						break;
				}
			}
			return x;
		}

		public static int compareWords(string guess, string correct)
		{
			int[] chars = new int[26];
			for (int i = 0; i < WORD_LENGTH; i++)
			{
				chars[correct[i] - 'a']++;
			}
			int greens = 0;
			for (int i = 0; i < WORD_LENGTH; i++)
			{
				greens *= 3;
				if (guess[i] == correct[i])
				{
					greens += GREEN;
					--chars[guess[i] - 'a'];
				}
			}
			int yellows = 0;
			for (int i = 0; i < WORD_LENGTH; i++)
			{
				yellows *= 3;
				int idx = guess[i] - 'a';
				if (guess[i] != correct[i] && chars[idx] > 0)
				{
					yellows += YELLOW;
					--chars[idx];
				}
			}
			return greens + yellows;
		}


		public static BestWord getBestWord(IList<string> words)
		{
			string bestString = null;
			List<IGrouping<int, string>> bestResult = null;
			int points = int.MaxValue;
			for (int i = 0; i < Words.words.Length; i++)
			{
				var grouping = words.GroupBy(x => compareWords(Words.words[i], x)).ToList();
				int candidatePoints = grouping.Max(x => x.Count()) * 2 - Convert.ToInt32(words.Contains(Words.words[i]));
				if (candidatePoints < points)
				{
					points = candidatePoints;
					bestResult = grouping;
					bestString = Words.words[i];
				}
			}
			return (bestString, bestResult);
		}

	}

	public struct BestWord
	{
		public string word;
		public List<IGrouping<int, string>> partition;

		public BestWord(string word, List<IGrouping<int, string>> partition)
		{
			this.word = word;
			this.partition = partition;
		}

		public override bool Equals(object obj)
		{
			return obj is BestWord other &&
				   word == other.word &&
				   EqualityComparer<List<IGrouping<int, string>>>.Default.Equals(partition, other.partition);
		}

		public override int GetHashCode()
		{
			int hashCode = 1933102734;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(word);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<IGrouping<int, string>>>.Default.GetHashCode(partition);
			return hashCode;
		}

		public void Deconstruct(out string word, out List<IGrouping<int, string>> partition)
		{
			word = this.word;
			partition = this.partition;
		}

		public static implicit operator (string word, List<IGrouping<int, string>> partition)(BestWord value)
		{
			return (value.word, value.partition);
		}

		public static implicit operator BestWord((string word, List<IGrouping<int, string>> partition) value)
		{
			return new BestWord(value.word, value.partition);
		}
	}
}

