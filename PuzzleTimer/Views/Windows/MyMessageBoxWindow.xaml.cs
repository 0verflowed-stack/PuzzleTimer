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
    /// Interaction logic for MyMessageBoxWindow.xaml
    /// </summary>
    public partial class MyMessageBoxWindow : Window {
        public MyMessageBoxWindow(string titleText, string text, Settings settings, NotifyMain YesClicked = null) {
            DataContext = new MyMessageBoxViewModel(titleText, text, settings, YesClicked == null ? false : true);
            if(YesClicked != null)
                ((MyMessageBoxViewModel)DataContext).YesClicked += YesClicked;
            InitializeComponent();
        }
    }
}
