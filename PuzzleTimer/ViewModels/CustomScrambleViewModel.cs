using PuzzleTimer.Infrustructure.Commands;
using PuzzleTimer.Models;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace PuzzleTimer.ViewModels {
    public delegate void ScrambleHandler(string s);
    internal class CustomScrambleViewModel : ViewModel, ICloseWindow {
        public event ScrambleHandler Notify = (string s) => { };
        public Action Close { get; set; }
        Settings settings;
        private string _TextBlockInputScramble;
        public string TextBlockInputScramble {
            get => _TextBlockInputScramble;
            set => Set(ref _TextBlockInputScramble, value);
        }
        private string _TextBoxInputScramble = "";
        public string TextBoxInputScramble {
            get => _TextBoxInputScramble;
            set => Set(ref _TextBoxInputScramble, value);
        }
        private string _ButtonCancelText;
        public string ButtonCancelText {
            get => _ButtonCancelText;
            set => Set(ref _ButtonCancelText, value);
        }
        private string _ButtonDoneText;
        public string ButtonDoneText {
            get => _ButtonDoneText;
            set => Set(ref _ButtonDoneText, value);
        }
        private string _WarningText;
        public string WarningText {
            get => _WarningText;
            set => Set(ref _WarningText, value);
        }
        #region Commands
        #region ButtonDoneCommand
        public ICommand ButtonDoneCommand { get; }

        private bool CanButtonDoneCommandExecute(object p) => true;
        private void OnButtonDoneCommandExecuted(object p) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            bool incorrectInput = false;
            string[] tempScrambleLetters = TextBoxInputScramble.Split();
            for (int i = 0; i < tempScrambleLetters.Length; ++i) {
                if (tempScrambleLetters[i] != "" && tempScrambleLetters[i] != " ") {
                    if (!Regex.IsMatch(tempScrambleLetters[i], MainWindowViewModel.ScrambleLetters[settings.CurrentPuzzle])) {
                        WarningText += LocRM.GetString("incorrectScramble");
                        incorrectInput = true;
                        break;
                    }
                }
            }
            if (!incorrectInput) {
                Notify(TextBoxInputScramble);
                Close();
            }
        }
        #endregion
        #region ButtonCancelCommand
        public ICommand ButtonCancelCommand { get; }

        private bool CanButtonCancelCommandExecute(object p) => true;
        private void OnButtonCancelCommandExecuted(object p) => Close();
        #endregion
        #endregion
        public CustomScrambleViewModel(Settings settings) {
            this.settings = settings;
            ChangeLanguage();
            #region Commands
            ButtonDoneCommand = new LambdaCommand(OnButtonDoneCommandExecuted, CanButtonDoneCommandExecute);
            ButtonCancelCommand = new LambdaCommand(OnButtonCancelCommandExecuted, CanButtonCancelCommandExecute);
            #endregion
        }
        private void ChangeLanguage() {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            TextBlockInputScramble = LocRM.GetString("CustomScrambleWindowText");
            ButtonCancelText = LocRM.GetString("CustomScrambleWindowButtonCancel");
            ButtonDoneText = LocRM.GetString("CustomScrambleWindowButtonDone");
        }
    }
}
