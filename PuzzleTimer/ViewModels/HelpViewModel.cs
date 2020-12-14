using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace PuzzleTimer.ViewModels {
    class HelpViewModel : ViewModel {
        Settings settings;
        private string _HelpText1;
        public string HelpText1 {
            get => _HelpText1;
            set => Set(ref _HelpText1, value);
        }
        private string _HelpText2;
        public string HelpText2 {
            get => _HelpText2;
            set => Set(ref _HelpText2, value);
        }
        private string _HelpText3;
        public string HelpText3 {
            get => _HelpText3;
            set => Set(ref _HelpText3, value);
        }
        private string _HelpText4;
        public string HelpText4 {
            get => _HelpText4;
            set => Set(ref _HelpText4, value);
        }
        private string _HelpText5;
        public string HelpText5 {
            get => _HelpText5;
            set => Set(ref _HelpText5, value);
        }
        private string _HelpText6;
        public string HelpText6 {
            get => _HelpText6;
            set => Set(ref _HelpText6, value);
        }
        private string _HelpText7;
        public string HelpText7 {
            get => _HelpText7;
            set => Set(ref _HelpText7, value);
        }
        private string _HelpText8;
        public string HelpText8 {
            get => _HelpText8;
            set => Set(ref _HelpText8, value);
        }
        private string _HelpText9;
        public string HelpText9 {
            get => _HelpText9;
            set => Set(ref _HelpText9, value);
        }
        private string _HelpText10;
        public string HelpText10 {
            get => _HelpText10;
            set => Set(ref _HelpText10, value);
        }
        private string _HelpText11;
        public string HelpText11 {
            get => _HelpText11;
            set => Set(ref _HelpText11, value);
        }
        private string _HelpText12;
        public string HelpText12 {
            get => _HelpText12;
            set => Set(ref _HelpText12, value);
        }
        public HelpViewModel(Settings settings) {
            this.settings = settings;
            ChangeLanguage();
        }
        private void ChangeLanguage(bool addUpdate = false) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            HelpText1 = LocRM.GetString("HelpText1");
            HelpText2 = LocRM.GetString("HelpText2");
            HelpText3 = LocRM.GetString("HelpText3");
            HelpText4 = LocRM.GetString("HelpText4");
            HelpText5 = LocRM.GetString("HelpText5");
            HelpText6 = LocRM.GetString("HelpText6");
            HelpText7 = LocRM.GetString("HelpText7");
            HelpText8 = LocRM.GetString("HelpText8");
            HelpText9 = LocRM.GetString("HelpText9");
            HelpText10 = LocRM.GetString("HelpText10");
            HelpText11 = LocRM.GetString("HelpText11");
            HelpText12 = LocRM.GetString("HelpText12");
        }
    }
}
