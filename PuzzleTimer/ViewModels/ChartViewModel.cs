using LiveCharts;
using PuzzleTimer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Resources;
using System.Text;

namespace PuzzleTimer.ViewModels {
    class ChartViewModel : ViewModel {
        ObservableCollection<Solution> _CurrentPuzzleList;
        public ObservableCollection<Solution> CurrentPuzzleList {
            get => _CurrentPuzzleList;
            set => Set(ref _CurrentPuzzleList, value);
        }
        string _Title;
        public string Title {
            get => _Title;
            set => Set(ref _Title, value);
        }
        ChartValues<double> _SolutionTimeValues;
        public ChartValues<double> SolutionTimeValues {
            get => _SolutionTimeValues;
            set => Set(ref _SolutionTimeValues, value);
        }
        string[] _Id;
        public string[] Id {
            get => _Id;
            set => Set(ref _Id, value);
        }
        string _SolutionTimeLabel;
        public string SolutionTimeLabel {
            get => _SolutionTimeLabel;
            set => Set(ref _SolutionTimeLabel, value);
        }
        public ChartViewModel(ObservableCollection<Solution> CurrentPuzzleList, Settings settings) {
            this.CurrentPuzzleList = CurrentPuzzleList;
            LoadLineChartData(settings);
        }
        private void LoadLineChartData(Settings settings) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            Title = LocRM.GetString("ChartWindowText");
            SolutionTimeLabel = LocRM.GetString("GridHeaderSolutionTimeText");
            List<string> xValues = new List<string>();
            SolutionTimeValues = new ChartValues<double>();
            List<string> StringTime = new List<string>();
            for (int i = 0; i < CurrentPuzzleList.Count; ++i) {
                xValues.Add(CurrentPuzzleList[i].Id.ToString());
                SolutionTimeValues.Add(CurrentPuzzleList[i].TotalSeconds());
            }
            Id = xValues.ToArray();
        }
    }
}
