using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PuzzleTimer.Models;
using PuzzleTimer.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;
using System.Xml;
using TNoodle.Puzzles;

namespace PuzzleTimer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        List<Color> colorScheme = new List<Color> {
                    Color.FromRgb(255, 255, 255),
                    Color.FromRgb(255, 165, 0),
                    Color.FromRgb(0, 255, 0),
                    Color.FromRgb(255, 0, 0),
                    Color.FromRgb(0, 0, 255),
                    Color.FromRgb(255, 255, 0),
                };
        public MainWindow() {
            //DataContext = new MainWindowViewModel();
            InitializeComponent();
        }
        //private void GenerateScrambleAsync() {
        //    this.Dispatcher.Invoke(() => {
                
        //        int currentPuzzle = PuzzleType.SelectedIndex;
        //        Puzzle puzzle = currentPuzzle switch
        //        {
        //            0 => new TwoByTwoCubePuzzle(),
        //            1 => new ThreeByThreeCubePuzzle(),
        //            2 => new FourByFourCubePuzzle(),
        //            3 => new NbyNCubePuzzle(5),
        //            4 => new NbyNCubePuzzle(6),
        //            5 => new NbyNCubePuzzle(7),
        //            6 => new SkewbPuzzle(),
        //            7 => new PyraminxPuzzle(),
        //            8 => new MegaminxPuzzle(),
        //            9 => new SquareOnePuzzle(),
        //            _ => null
        //        };
        //        var r = new Random(Convert.ToInt32(DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString()) + Convert.ToInt32(DateTime.Now.DayOfYear.ToString() + DateTime.Now.Year.ToString()));
        //        ScrambleTextBlock.Text = puzzle.GenerateWcaScramble(r);
        //        if (currentPuzzle >= 0 && currentPuzzle <= 5) {
        //            var ListOfRectangles = (StackPanel)Unfold.Children[0];
        //            for (int n = 0; n < currentPuzzle + 2; ++n) {
        //                for (int m = 0; m < currentPuzzle + 2; ++m) {
        //                    ((Rectangle)((StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[0]).Children[0]).Children[n]).Children[m]).Fill = new SolidColorBrush(colorScheme[0]);
        //                }
        //            }
        //            for (int n = 0; n < currentPuzzle + 2; ++n) {
        //                for (int m = 0; m < currentPuzzle + 2; ++m) {
        //                    ((Rectangle)((StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[1]).Children[0]).Children[n]).Children[m]).Fill = new SolidColorBrush(colorScheme[1]);
        //                }
        //            }
        //            for (int n = 0; n < currentPuzzle + 2; ++n) {
        //                for (int m = 0; m < currentPuzzle + 2; ++m) {
        //                    ((Rectangle)((StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[1]).Children[1]).Children[n]).Children[m]).Fill = new SolidColorBrush(colorScheme[2]);
        //                }
        //            }
        //            for (int n = 0; n < currentPuzzle + 2; ++n) {
        //                for (int m = 0; m < currentPuzzle + 2; ++m) {
        //                    ((Rectangle)((StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[1]).Children[2]).Children[n]).Children[m]).Fill = new SolidColorBrush(colorScheme[3]);
        //                }
        //            }
        //            for (int n = 0; n < currentPuzzle + 2; ++n) {
        //                for (int m = 0; m < currentPuzzle + 2; ++m) {
        //                    ((Rectangle)((StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[1]).Children[3]).Children[n]).Children[m]).Fill = new SolidColorBrush(colorScheme[4]);
                 
        //                }
        //            }
        //            for (int n = 0; n < currentPuzzle + 2; ++n) {
        //                for (int m = 0; m < currentPuzzle + 2; ++m) {
        //                    ((Rectangle)((StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[2]).Children[0]).Children[n]).Children[m]).Fill = new SolidColorBrush(colorScheme[5]);
        //                }
        //            }
        //            string[] scrambleLettersSplited = ScrambleTextBlock.Text.Split();
        //            if (currentPuzzle >= 0 && currentPuzzle <= 5) {
        //                for (int i = 0; i < scrambleLettersSplited.Length; ++i) {
        //                    if (scrambleLettersSplited[i][0] == 'R' || (scrambleLettersSplited[i].Length > 1 && scrambleLettersSplited[i][1] == 'R')) {
        //                        UpdateUnfold(scrambleLettersSplited[i], new List<int> { 0, 1, 2, 1, 1 }, new List<int> { 0, 1, 0, 3, 2 },
        //                            new List<bool> { false, false, false, true }, new List<bool> { false, false, false, true });
        //                    }
        //                    else if (scrambleLettersSplited[i][0] == 'L' || (scrambleLettersSplited[i].Length > 1 && scrambleLettersSplited[i][1] == 'L')) {
        //                        UpdateUnfold(scrambleLettersSplited[i], new List<int> { 0, 1, 2, 1, 1 }, new List<int> { 0, 3, 0, 1, 0 },
        //                            new List<bool> { true, false, true, true }, new List<bool> { true, false, true, true });
        //                    }
        //                    else if (scrambleLettersSplited[i][0] == 'U' || (scrambleLettersSplited[i].Length > 1 && scrambleLettersSplited[i][1] == 'U')) {
        //                        UpdateUnfold(scrambleLettersSplited[i], new List<int> { 1, 1, 1, 1, 0 }, new List<int> { 3, 0, 1, 2, 0 },
        //                            new List<bool> { true, true, true, true }, new List<bool> { true, true, true, true });
        //                    }
        //                    else if (scrambleLettersSplited[i][0] == 'D' || (scrambleLettersSplited[i].Length > 1 && scrambleLettersSplited[i][1] == 'D')) {
        //                        UpdateUnfold(scrambleLettersSplited[i], new List<int> { 1, 1, 1, 1, 2 }, new List<int> { 1, 0, 3, 2, 0 },
        //                            new List<bool> { true, true, true, true }, new List<bool> { false, false, false, false });
        //                    }
        //                    else if (scrambleLettersSplited[i][0] == 'F' || (scrambleLettersSplited[i].Length > 1 && scrambleLettersSplited[i][1] == 'F')) {
        //                        UpdateUnfold(scrambleLettersSplited[i], new List<int> { 0, 1, 2, 1, 1 }, new List<int> { 0, 0, 0, 2, 1 },
        //                            new List<bool> { false, false, true, true }, new List<bool> { true, false, false, true });
        //                    }
        //                    else if (scrambleLettersSplited[i][0] == 'B' || (scrambleLettersSplited[i].Length > 1 && scrambleLettersSplited[i][1] == 'B')) {
        //                        UpdateUnfold(scrambleLettersSplited[i], new List<int> { 0, 1, 2, 1, 1 }, new List<int> { 0, 2, 0, 0, 3 },
        //                            new List<bool> { true, true, false, false }, new List<bool> { true, false, false, true });
        //                    }
        //                }
        //            }
        //        }
        //    });
        //}
        //private void UpdateUnfold(string letter, List<int> stackPanelNumber, List<int> sideNumber, List<bool> rowStart, List<bool> columnStart) {
        //    int currentPuzzle = PuzzleType.SelectedIndex;
        //    var ListOfRectangles = (StackPanel)Unfold.Children[0];
        //    int offset = 0;
        //    int loops = 1;
        //    if (letter.Length == 2) {
        //        if (letter[1] == '2') {
        //            loops = 2;
        //        }
        //        else if (letter[1] == '\'') {
        //            loops = 3;
        //        }
        //        else if (letter[1] == 'w') {
        //            offset = 1;
        //        }
        //    } else if (letter.Length == 3) {
        //        if (Char.IsLetter(letter[0])) {
        //            if (letter[2] == '\'') {
        //                offset = 1;
        //                loops = 3;
        //            }
        //            else if (letter[2] == '2') {
        //                offset = 1;
        //                loops = 2;
        //            }
        //        } else {
        //            offset = Int32.Parse(letter[0].ToString());
        //        }
        //    } else if (letter.Length == 4) {
        //        if (letter[3] == '2') {
        //            loops = 2;
        //        } else if (letter[3] == '\'') {
        //            loops = 3;
        //        }
        //        offset = Int32.Parse(letter[0].ToString());
        //    }
        //    for (int j = 0; j < loops; ++j) {
        //        for (int k = 0; k < offset + 1; ++k) {
        //            int columnFirst = columnStart[0] ? k : (currentPuzzle + 1 - k);
        //            int columnSecond = columnStart[1] ? k : (currentPuzzle + 1 - k);
        //            int columnThird = columnStart[2] ? k : (currentPuzzle + 1 - k);
        //            int columnFourth = columnStart[3] ? k : (currentPuzzle + 1 - k);
        //            for (int i = 0; i < currentPuzzle + 2; ++i) {
        //                int rowFirst = rowStart[0] ? i : (currentPuzzle + 1 - i);
        //                int rowSecond = rowStart[1] ? i : (currentPuzzle + 1 - i);
        //                int rowThird = rowStart[2] ? i : (currentPuzzle + 1 - i);
        //                int rowFourth = rowStart[3] ? i : (currentPuzzle + 1 - i);
        //                if ((letter[0] == 'U' || (letter.Length > 1 && letter[1] == 'U')) || (letter[0] == 'D' || (letter.Length > 1 && letter[1] == 'D'))) {
        //                    rowFirst = columnStart[0] ? k : (currentPuzzle + 1 - k);
        //                    rowSecond = columnStart[1] ? k : (currentPuzzle + 1 - k);
        //                    rowThird = columnStart[2] ? k : (currentPuzzle + 1 - k);
        //                    rowFourth = columnStart[3] ? k : (currentPuzzle + 1 - k);

        //                    columnFirst = rowStart[0] ? i : (currentPuzzle + 1 - i);
        //                    columnSecond = rowStart[1] ? i : (currentPuzzle + 1 - i);
        //                    columnThird = rowStart[2] ? i : (currentPuzzle + 1 - i);
        //                    columnFourth = rowStart[3] ? i : (currentPuzzle + 1 - i);
        //                }
        //                if ((letter[0] == 'F' || (letter.Length > 1 && letter[1] == 'F')) || (letter[0] == 'B' || (letter.Length > 1 && letter[1] == 'B'))) {
        //                    rowFirst = rowStart[0] ? k : (currentPuzzle + 1 - k);
        //                    rowSecond = rowStart[1] ? i : (currentPuzzle + 1 - i);
        //                    rowThird = rowStart[2] ? k : (currentPuzzle + 1 - k);
        //                    rowFourth = rowStart[3] ? i : (currentPuzzle + 1 - i);

        //                    columnFirst = columnStart[0] ? i : (currentPuzzle + 1 - i);
        //                    columnSecond = columnStart[1] ? k : (currentPuzzle + 1 - k);
        //                    columnThird = columnStart[2] ? i : (currentPuzzle + 1 - i);
        //                    columnFourth = columnStart[3] ? k : (currentPuzzle + 1 - k);
        //                }

        //                var lsOfRectanglesFirst = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[0]]).Children[sideNumber[0]]).Children[rowFirst];
        //                Brush temp = ((Rectangle)lsOfRectanglesFirst.Children[columnFirst]).Fill.CloneCurrentValue();
        //                //Up
        //                var lsOfRectanglesSecond = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[1]]).Children[sideNumber[1]]).Children[rowSecond];
        //                ((Rectangle)lsOfRectanglesFirst.Children[columnFirst]).Fill = ((Rectangle)lsOfRectanglesSecond.Children[columnSecond]).Fill.CloneCurrentValue();
        //                //Left
        //                var lsOfRectanglesThird = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[2]]).Children[sideNumber[2]]).Children[rowThird];
        //                ((Rectangle)lsOfRectanglesSecond.Children[columnSecond]).Fill = ((Rectangle)lsOfRectanglesThird.Children[columnThird]).Fill.CloneCurrentValue();
        //                //Bottom
        //                var lsOfRectanglesFourth = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[3]]).Children[sideNumber[3]]).Children[rowFourth];
        //                ((Rectangle)lsOfRectanglesThird.Children[columnThird]).Fill = ((Rectangle)lsOfRectanglesFourth.Children[columnFourth]).Fill.CloneCurrentValue();
        //                //Right
        //                ((Rectangle)lsOfRectanglesFourth.Children[columnFourth]).Fill = temp;
        //            }
        //        }
        //        //inner layers of main tile
        //        for (int k = 0; k < Math.Ceiling((currentPuzzle + 2) / 2 - 0.5); ++k) {
        //            for (int i = 0; i < currentPuzzle + 1 - 2 * k; ++i) {
        //                var tempInnerLayerBrushUp = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[4]]).Children[sideNumber[4]]).Children[k];
        //                Brush temp = ((Rectangle)tempInnerLayerBrushUp.Children[i + k]).Fill.CloneCurrentValue();
        //                //Up
        //                var tempInnerLayerBrushLeft = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[4]]).Children[sideNumber[4]]).Children[currentPuzzle + 1 - k - i];
        //                ((Rectangle)tempInnerLayerBrushUp.Children[i + k]).Fill = ((Rectangle)tempInnerLayerBrushLeft.Children[k]).Fill.CloneCurrentValue();
        //                //Left
        //                var tempInnerLayerBrushBottom = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[4]]).Children[sideNumber[4]]).Children[currentPuzzle + 1 - k];
        //                ((Rectangle)tempInnerLayerBrushLeft.Children[k]).Fill = ((Rectangle)tempInnerLayerBrushBottom.Children[currentPuzzle + 1 - k - i]).Fill.CloneCurrentValue();
        //                ////Bottom
        //                var tempInnerLayerBrushRight = (StackPanel)((StackPanel)((StackPanel)ListOfRectangles.Children[stackPanelNumber[4]]).Children[sideNumber[4]]).Children[i + k];
        //                ((Rectangle)tempInnerLayerBrushBottom.Children[currentPuzzle + 1 - k - i]).Fill = ((Rectangle)tempInnerLayerBrushRight.Children[currentPuzzle + 1 - k]).Fill.CloneCurrentValue();
        //                ////Right
        //                ((Rectangle)tempInnerLayerBrushRight.Children[currentPuzzle + 1 - k]).Fill = temp;
        //            }
        //        }
        //    }
        //}
        //private void PuzzleType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            
        //    if(ScrambleTextBlock != null)
        //        ScrambleTextBlock.Text = "Generating...";
        //    Thread thread = new Thread(InitializeUnfold);
        //    thread.Start();
        //    //InitializeUnfold();
        //}
        //private void InitializeUnfold() {
        //    this.Dispatcher.Invoke(() => {
        //        if (Unfold == null || PuzzleType.SelectedIndex == -1) return;
        //        //currentPuzzle = PuzzleType.SelectedIndex;
        //        Unfold.Children.Clear();
        //        StackPanel stackPanel = new StackPanel {
        //            HorizontalAlignment = HorizontalAlignment.Center,
        //        };
        //        if (PuzzleType.SelectedIndex <= 5) {
        //            StackPanel lineOfSides = new StackPanel { Orientation = Orientation.Horizontal };
        //            double stackPanelHeight = (!Double.IsNaN(ActualHeight) && ActualHeight != 0) ? ActualHeight * 0.23 : 590 * 0.23;
        //            double widthHeight = (stackPanelHeight - strokeThickness[PuzzleType.SelectedIndex] * 2 * (PuzzleType.SelectedIndex + 2)) / (PuzzleType.SelectedIndex + 2);
        //            for (int i = 0; i < 6; ++i) {
        //                StackPanel side = new StackPanel();
        //                if (i == 0 || i == 5) {
        //                    side.Margin = new Thickness(widthHeight * (PuzzleType.SelectedIndex + 2), 0, 0, 0);
        //                }
        //                for (int j = 0; j < PuzzleType.SelectedIndex + 2; ++j) {
        //                    StackPanel line = new StackPanel { Orientation = Orientation.Horizontal };
        //                    for (int k = 0; k < PuzzleType.SelectedIndex + 2; ++k) {
        //                        line.Children.Add(new Rectangle {
        //                            Width = widthHeight,
        //                            Height = widthHeight,
        //                            Fill = new SolidColorBrush(colorScheme[i]),
        //                            StrokeThickness = strokeThickness[PuzzleType.SelectedIndex],
        //                            Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
        //                            StrokeDashCap = PenLineCap.Round
        //                        });
        //                    }
        //                    side.Children.Add(line);
        //                }
        //                if (i == 0 || i == 5) {
        //                    StackPanel temp = new StackPanel();
        //                    temp.Children.Add(side);
        //                    stackPanel.Children.Add(temp);
        //                }
        //                else {
        //                    lineOfSides.Children.Add(side);
        //                    if (i == 4) {
        //                        stackPanel.Children.Add(lineOfSides);
        //                    }
        //                }
        //            }
        //        }
        //        else if (PuzzleType.SelectedIndex == 7) {
        //            StackPanel lineOfSides = new StackPanel { Orientation = Orientation.Horizontal };
        //            double stackPanelHeight = (!Double.IsNaN(ActualHeight) && ActualHeight != 0) ? ActualHeight * 0.23 : 590 * 0.23;
        //            double widthHeight = (stackPanelHeight / 7.0);
        //            for (int i = 0; i < 6; ++i) {
        //                StackPanel side = new StackPanel();
        //                if (i == 0 || i == 5) {
        //                    side.Margin = new Thickness(widthHeight * (PuzzleType.SelectedIndex + 2), 0, 0, 0);
        //                }
        //                for (int j = 0; j < PuzzleType.SelectedIndex + 2; ++j) {
        //                    StackPanel line = new StackPanel { Orientation = Orientation.Horizontal };
        //                    for (int k = 0; k < PuzzleType.SelectedIndex + 2; ++k) {
        //                        PointCollection points = new PointCollection();
        //                        points.Add(new Point(0, 0));
        //                        points.Add(new Point(5, 10));
        //                        points.Add(new Point(0, 10));
        //                        line.Children.Add(new Polygon {
        //                            Points = points,
        //                            Width = widthHeight,
        //                            Height = widthHeight,
        //                            Fill = new SolidColorBrush(colorScheme[i]),
        //                            StrokeThickness = strokeThickness[PuzzleType.SelectedIndex],
        //                            Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
        //                            StrokeDashCap = PenLineCap.Round
        //                        });
        //                    }
        //                    side.Children.Add(line);
        //                }
        //                if (i == 0 || i == 5) {
        //                    StackPanel temp = new StackPanel();
        //                    temp.Children.Add(side);
        //                    stackPanel.Children.Add(temp);
        //                }
        //                else {
        //                    lineOfSides.Children.Add(side);
        //                    if (i == 4) {
        //                        stackPanel.Children.Add(lineOfSides);
        //                    }
        //                }
        //            }
        //        }
        //        Unfold.Children.Add(stackPanel);
        //        //UpdateAvg();
        //        //dataGridSolutions.ItemsSource = listOfSolutions?.ElementAt(PuzzleType.SelectedIndex);
        //        ScrambleTextBlock.Text = "Generating...";
        //        GenerateScrambleAsync();
        //    });
        //}
        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        //    using (SolutionsDbContext context = new SolutionsDbContext()) {
        //        var changedEntries = context.ChangeTracker.Entries()
        //            .Where(x => x.State != EntityState.Unchanged)
        //            .ToList();
        //        if (changedEntries.Count != 0) {
        //            MessageBoxResult messageBoxResult = MessageBox.Show($"Do you want to save changes ?", "Puzzle Timer", MessageBoxButton.YesNoCancel);
        //            if (messageBoxResult != MessageBoxResult.No) {
        //                foreach (var entry in changedEntries) {
        //                    switch (entry.State) {
        //                        case EntityState.Modified:
        //                            entry.CurrentValues.SetValues(entry.OriginalValues);
        //                            entry.State = EntityState.Unchanged;
        //                            break;
        //                        case EntityState.Added:
        //                            entry.State = EntityState.Detached;
        //                            break;
        //                        case EntityState.Deleted:
        //                            entry.State = EntityState.Unchanged;
        //                            break;
        //                    }
        //                }
        //            }
        //            else if(messageBoxResult == MessageBoxResult.Yes) {
        //                context.SaveChanges();
        //            }
        //            //Add cancel handler
        //            else if (messageBoxResult == MessageBoxResult.Cancel) {
        //                //e.Cancel();
        //            }
        //        }
        //    }
        //}
        //private void ButtonUnfoldGrid_Click(object sender, RoutedEventArgs e) {
        //    //rewrite when added to view
        //    //!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //    if (UnfoldGridButton.Content.ToString() == "Unfold") {
        //        UnfoldGridButton.Content = "Table";
        //        Unfold.Visibility = Visibility.Visible;
        //        dataGridSolutions.Visibility = Visibility.Collapsed;
        //        return;
        //    }
        //    if (UnfoldGridButton.Content.ToString() == "Table") {
        //        UnfoldGridButton.Content = "Unfold";
        //        Unfold.Visibility = Visibility.Collapsed;
        //        dataGridSolutions.Visibility = Visibility.Visible;
        //        return;
        //    }
        //    //
        //    if (UnfoldGridButton.Content.ToString() == "Розгортка") {
        //        UnfoldGridButton.Content = "Таблиця";
        //        Unfold.Visibility = Visibility.Visible;
        //        dataGridSolutions.Visibility = Visibility.Collapsed;
        //        return;
        //    }
        //    if (UnfoldGridButton.Content.ToString() == "Таблиця") {
        //        UnfoldGridButton.Content = "Розгортка";
        //        Unfold.Visibility = Visibility.Collapsed;
        //        dataGridSolutions.Visibility = Visibility.Visible;
        //        return;
        //    }
        //    if (UnfoldGridButton.Content.ToString() == "Таблица") {
        //        UnfoldGridButton.Content = "Розгортка";
        //        Unfold.Visibility = Visibility.Collapsed;
        //        dataGridSolutions.Visibility = Visibility.Visible;
        //        return;
        //    }
        //    //
        //}
        //private void ButtonNewScramble_Click(object sender, RoutedEventArgs e) {
        //    ScrambleTextBlock.Text = "Generating...";
        //    new Thread(GenerateScrambleAsync).Start();
        //}
        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
        //    if (e.PreviousSize.Height != 0) {
        //        if (e.HeightChanged) {
        //            double stackPanelHeight = e.NewSize.Height * 0.23;
        //            double widthHeight = (stackPanelHeight - strokeThickness[PuzzleType.SelectedIndex] * 2 * (PuzzleType.SelectedIndex + 2)) / (PuzzleType.SelectedIndex + 2);
        //            Unfold.Children.OfType<Rectangle>().ToList().ForEach(
        //                x => {
        //                    x.Width = x.Height = widthHeight;
        //                    //To-do
        //                    //NotifyPropertyChanged("Width");
        //                }
        //            );
        //        }
        //    }
        //}
    }
}
