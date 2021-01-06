using PuzzleTimer.Infrustructure.Commands;
using PuzzleTimer.Models;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PuzzleTimer.ViewModels {
    class MyMessageBoxViewModel : ViewModel, ICloseWindow {
        public NotifyMain YesClicked;
        public Action Close { get; set; }
        string _MainText;
        public string MainText {
            get => _MainText;
            set => Set(ref _MainText, value);
        }
        string _TitleText;
        public string TitleText
        {
            get => _TitleText;
            set => Set(ref _TitleText, value);
        }
        string _YesText;
        public string YesText
        {
            get => _YesText;
            set => Set(ref _YesText, value);
        }
        string _NoOKText;
        public string NoOKText
        {
            get => _NoOKText;
            set => Set(ref _NoOKText, value);
        }
        Visibility _YesButtonVisibility;
        public Visibility YesButtonVisibility
        {
            get => _YesButtonVisibility;
            set => Set(ref _YesButtonVisibility, value);
        }
        #region Commands
        #region OKButtonCommand
        public ICommand OKButtonCommand { get; }

        private bool CanOKButtonCommandExecute(object p) => true;
        private void OnOKButtonCommandExecuted(object p) => Close();
        #endregion
        #region YesButtonCommand
        public ICommand YesButtonCommand { get; }

        private bool CanYesButtonCommandExecute(object p) => true;
        private void OnYesButtonCommandExecuted(object p) { 
            YesClicked();
            Close();
        }
        #endregion
        #endregion
        public MyMessageBoxViewModel(string TitleText, string Text, Settings settings, bool YesNo) {
            #region Commnads
            OKButtonCommand = new LambdaCommand(OnOKButtonCommandExecuted, CanOKButtonCommandExecute);
            YesButtonCommand = new LambdaCommand(OnYesButtonCommandExecuted, CanYesButtonCommandExecute);
            #endregion
            this.TitleText = TitleText;
            MainText = Text;
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            YesText = LocRM.GetString("YesText");
            if (YesNo) {
                YesButtonVisibility = Visibility.Visible;
                NoOKText = LocRM.GetString("NoText");
            } else {
                YesButtonVisibility = Visibility.Collapsed;
                NoOKText = "OK";
            }
        }
    }
}
