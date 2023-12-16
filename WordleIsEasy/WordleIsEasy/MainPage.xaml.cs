using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ClassLibrary1;

namespace WordleIsEasy
{
	public partial class MainPage : ContentPage
	{
		static Color NOT_EXIST => Color.White;
		static Color EXIST_NOT_MATCH => Color.Yellow;
		static Color CORRECT => Color.Green;

		//private BestWord bestWord = new BestWord("olate", Words.probableWords.GroupBy(x => Class1.compareWords("olate", x)).ToList());
		private BestWord bestWord = new BestWord("seria", Words.words.GroupBy(x => Class1.compareWords("seria", x)).ToList());


		private void setWords(int number)
		{
			remaining.Text = "Possible words remaining: " + number;
		}

		public MainPage()
		{
			InitializeComponent();
			initButtons();
			//setWords(Words.probableWords.Count());
			setWords(Words.words.Length);
        }

		private void renderWord(string word)
		{
			word = word.ToUpper();
			btn1.Text = word[0].ToString();
			btn2.Text = word[1].ToString();
			btn3.Text = word[2].ToString();
			btn4.Text = word[3].ToString();
			btn5.Text = word[4].ToString();
		}

		private void initButtons()
		{
			foreach (var item in wordleInput.Children)
			{
				item.BackgroundColor = NOT_EXIST;
			}
			renderWord(bestWord.word);
		}

		private int getResponse()
		{
			return colorToNumber(btn1.BackgroundColor) * (3 * 3 * 3 * 3) +
				colorToNumber(btn2.BackgroundColor) * (3 * 3 * 3) +
				colorToNumber(btn3.BackgroundColor) * (3 * 3) +
				colorToNumber(btn4.BackgroundColor) * (3) +
				colorToNumber(btn5.BackgroundColor);
		}

		private static int colorToNumber(Color color)
		{
			if (color == NOT_EXIST)
				return Class1.WHITE;
			else if (color == EXIST_NOT_MATCH)
				return Class1.YELLOW;
			else
				return Class1.GREEN;
		}

		private void btnClicked(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			if (btn.BackgroundColor == NOT_EXIST)
			{
				btn.BackgroundColor = EXIST_NOT_MATCH;
			}
			else if (btn.BackgroundColor == EXIST_NOT_MATCH)
			{
				btn.BackgroundColor = CORRECT;
			}
			else
			{
				btn.BackgroundColor = NOT_EXIST;
			}
		}

		private async void submit_Clicked(object sender, EventArgs e)
		{
			try
			{
				int answer = getResponse();
				var set = bestWord.partition.First(x => x.Key == answer).ToList();
				setWords(set.Count);
				bestWord = Class1.getBestWord(set);
				initButtons();
			}
			catch
			{
				await DisplayAlert("Something was input wrong", "This is quite literally impossible, try again.", "Click me or something");
				throw;
			}
		}
	}
}
