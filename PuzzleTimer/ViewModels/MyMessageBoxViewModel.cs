using PuzzleTimer.Infrustructure.Commands;
using PuzzleTimer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PuzzleTimer.ViewModels {
    class MyMessageBoxViewModel : ViewModel, ICloseWindow {
        string _MainText;
        public Action Close { get; set; }
        public string MainText {
            get => _MainText;
            set => Set(ref _MainText, value);
        }
        #region Commands
        #region OKButtonCommand
        public ICommand OKButtonCommand { get; }

        private bool CanOKButtonCommandExecute(object p) => true;
        private void OnOKButtonCommandExecuted(object p) => Close();
        #endregion
        #endregion
        public MyMessageBoxViewModel(string Text) {
            #region Commnads
            OKButtonCommand = new LambdaCommand(OnOKButtonCommandExecuted, CanOKButtonCommandExecute);
            #endregion
            MainText = Text;
        }
    }
}
