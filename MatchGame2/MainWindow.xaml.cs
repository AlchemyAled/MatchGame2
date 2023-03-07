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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            if (moves == 1)
            {
                timeTextBlock.Text = moves + " move.";
            }
            else
            {
                timeTextBlock.Text = moves + " moves.";
            }
            
            if (matchesFound == 8)
            {
                timeTextBlock.Text = timeTextBlock.Text + " Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐙","🐙",
                "🐡","🐡",
                "🐘","🐘",
                "🐳","🐳",
                "🐑","🐑",
                "🦆","🦆",
                "🦘","🦘",
                "🦔","🦔",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    textBlock.Text = "?";
                    int index = random.Next(animalEmoji.Count);
                    moves = 0;
                    string nextEmoji = animalEmoji[index];
                    textBlock.Tag = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }

            }

            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;

        }

        TextBlock lastTextBlock;
        TextBlock lastTextBlock2;
        bool findingMatch = false;
        int incorrect = 0;
        int moves = 0;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            object tagObject = textBlock.Tag;
            if (incorrect == 1)
            {
                lastTextBlock.Text = "?";
                lastTextBlock2.Text = "?";
                lastTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                lastTextBlock2.Foreground = new SolidColorBrush(Colors.Black);
            }
            incorrect = 0;

                if (tagObject.ToString() != "")
                {
                    if (findingMatch == false)
                    {
                        textBlock.Text = tagObject.ToString();

                        lastTextBlock = textBlock;
                        findingMatch = true;
                    }
                    else if (textBlock.Tag == lastTextBlock.Tag)
                    {
                        if (textBlock != lastTextBlock)
                    {
                        matchesFound++;
                        textBlock.Text = tagObject.ToString();
                        findingMatch = false;
                        textBlock.Tag = "";
                        lastTextBlock.Tag = "";
                        lastTextBlock2 = textBlock;
                        moves += 1;
                    }

                    }
                    else
                    {
                        int wrongTimer = tenthOfSecondsElapsed;
                        textBlock.Text = tagObject.ToString();
                        textBlock.Foreground = new SolidColorBrush(Colors.Red);
                        lastTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                        incorrect = 1;
                        findingMatch = false;
                        lastTextBlock2 = textBlock;
                        moves += 1;
                    }
                }

           
           
            
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
