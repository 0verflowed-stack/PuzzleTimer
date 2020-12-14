using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Resources;
using System.Text;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PuzzleTimer.ViewModels {
    class AboutViewModel : ViewModel {
        Settings settings; 
        private string _AboutText1;
        public string AboutText1 {
            get => _AboutText1;
            set => Set(ref _AboutText1, value);
        }
        private string _AboutText2;
        public string AboutText2 {
            get => _AboutText2;
            set => Set(ref _AboutText2, value);
        }
        private string _AboutText3;
        public string AboutText3 {
            get => _AboutText3;
            set => Set(ref _AboutText3, value);
        }
        #region Commands
        #region NavigateToUrlCommand
        public ICommand NavigateToUrlCommand { get; }
        #endregion

        #endregion
        public AboutViewModel(Settings settings) {
            this.settings = settings;
            NavigateToUrlCommand = new RelayCommand<RequestNavigateEventArgs>(OnNavigateToUrl, null);
            ChangeLanguage();
        }
        private void OnNavigateToUrl(RequestNavigateEventArgs e) {
            try {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            } catch {
            }
        }
        private void ChangeLanguage(bool addUpdate = false) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            AboutText1 = LocRM.GetString("AboutText1");
            AboutText2 = LocRM.GetString("AboutText2");
            AboutText3 = LocRM.GetString("AboutText3");
        }
    }
}
