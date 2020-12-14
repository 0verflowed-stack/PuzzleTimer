using PuzzleTimer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PuzzleTimer.Views.Windows {
    /// <summary>
    /// Interaction logic for CustomScrambleWindow.xaml
    /// </summary>
    public partial class CustomScrambleWindow : Window {
        public CustomScrambleWindow(Settings settings, ScrambleHandler scrambleHandler) {
            DataContext = new CustomScrambleViewModel(settings);
            ((CustomScrambleViewModel)DataContext).Notify += scrambleHandler;
            InitializeComponent();
        }
    }
}
