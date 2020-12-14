using PuzzleTimer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PuzzleTimer {
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window {
        public ChartWindow(ObservableCollection<Solution> CurrentPuzzleList, Settings settings) {
            DataContext = new ChartViewModel(CurrentPuzzleList, settings);
            InitializeComponent();
        }
        
    }
}
