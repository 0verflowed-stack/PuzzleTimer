using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PuzzleTimer.ViewModels;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using PuzzleTimer.Models;

namespace PuzzleTimer {
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window {
        public ItemWindow(NotifyMain notifyMain, ObservableCollection<ObservableCollection<Solution>> listOfSolutions, 
                Settings settings, Solution CurrentSolutionGrid, bool addUpdate = false) {
            InitializeComponent();
            DataContext = new ItemViewViewModel(
                listOfSolutions, settings, CurrentSolutionGrid, addUpdate);
            ((ItemViewViewModel)DataContext).Notify += notifyMain;
        }
    }
}
