using Newtonsoft.Json;
using PuzzleTimer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
        public delegate void NotifyMain();
        public delegate void TextHandler(int i);
        public partial class SettingsWindow : Window {
            public SettingsWindow(Settings settings, NotifyMain notifyMain, TextHandler textHandler, NotifyMain saveSettings) {
                DataContext = new SettingsViewModel(settings);
                ((SettingsViewModel)DataContext).Notify += notifyMain;
                ((SettingsViewModel)DataContext).NotifyTextColor += textHandler;
                ((SettingsViewModel)DataContext).SaveSettings += saveSettings;
            InitializeComponent();
            }
    }
}
