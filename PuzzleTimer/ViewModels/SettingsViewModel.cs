using PuzzleTimer.Infrustructure.Commands;
using PuzzleTimer.Models;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PuzzleTimer.ViewModels {
    class SettingsViewModel : ViewModel {
        public event NotifyMain Notify = () => {};
        public event NotifyMain SaveSettings = () => { };
        public event TextHandler NotifyTextColor = (int i) => { };
        public Settings settings;
        #region Text
        private string _LanguageText;
        public string LanguageText {
            get => _LanguageText;
            set => Set(ref _LanguageText, value);
        }
        private string _ThemeText;
        public string ThemeText {
            get => _ThemeText;
            set => Set(ref _ThemeText, value);
        }
        #endregion
        public int CurrentLanguage {
            get => settings.Language;
            set {
                Set(ref settings.Language, value);
                ChangeLanguage();
            }
        }
        public static List<string> Languages {
            get => new List<string> {
                "English",
                "Russian",
                "Ukrainian"
            };
        }
        public static List<LangComboBoxItem> LanguagesOriginalNames {
            get => new List<LangComboBoxItem> {
                new LangComboBoxItem("English", "/Images/English.png"),
                new LangComboBoxItem("Русский", "/Images/Russian.png"),
                new LangComboBoxItem("Українська", "/Images/Ukrainian.png")
            };
        }
        public static List<string> ThemesNames {
            get => new List<string> {
                "Dark",
                "Light",
                "Black"
            };
        }
        #region Commands
        #region DarkThemeCommand
        public ICommand DarkThemeCommand { get; }

        private bool CanDarkThemeCommandExecute(object p) => true;
        private void OnDarkThemeCommandExecuted(object p) => SetTheme(0, 0);
        #endregion
        #region LightThemeCommand
        public ICommand LightThemeCommand { get; }

        private bool CanLightThemeCommandExecute(object p) => true;
        private void OnLightThemeCommandExecuted(object p) => SetTheme(1, 1);
        #endregion
        #region BlackThemeCommand
        public ICommand BlackThemeCommand { get; }

        private bool CanBlackThemeCommandExecute(object p) => true;
        private void OnBlackThemeCommandExecuted(object p) => SetTheme(0, 2);
        #endregion
        #endregion
        public SettingsViewModel(Settings settings) {
            this.settings = settings;
            ChangeLanguage();
            #region Commands
            DarkThemeCommand = new LambdaCommand(OnDarkThemeCommandExecuted, CanDarkThemeCommandExecute);
            LightThemeCommand = new LambdaCommand(OnLightThemeCommandExecuted, CanLightThemeCommandExecute);
            BlackThemeCommand = new LambdaCommand(OnBlackThemeCommandExecuted, CanBlackThemeCommandExecute);
            #endregion
        }
        private void ChangeLanguage() {
            string strLanguage = "PuzzleTimer.Languages." + Languages[CurrentLanguage];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            LanguageText = LocRM.GetString("Language");
            ThemeText = LocRM.GetString("Theme");
            Notify();
            SaveSettings();
        }
        private void SetTheme(int themeType, int themeIndex) {
            settings.CurrentTheme = themeIndex;
            var uri = new Uri("Themes/" + ThemesNames[themeIndex] + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            NotifyTextColor(themeType);
            SaveSettings();
        }
    }
}
