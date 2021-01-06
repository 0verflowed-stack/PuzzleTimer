using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PuzzleTimer.Infrustructure.Commands;
using PuzzleTimer.Models;
using PuzzleTimer.Views.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;
using System.Windows.Threading;
using TNoodle.Puzzles;

namespace PuzzleTimer.ViewModels {
    internal class MainWindowViewModel : ViewModel {
        bool IsUnfold = false;

        bool delay;
        bool delay2;
        bool pressedUp = false;
        bool isActive = false;
        bool isDown = false;
        private bool isThreadStopWatchRunning = false;
        private bool isThreadTimeDelayRunning = false;
        DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
        DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
        Stopwatch sw;
        #region Some Properties with Fields

        private NByNCubeViewModel _NByNCubeViewModelProperty;
        public NByNCubeViewModel NByNCubeViewModelProperty {
            get => _NByNCubeViewModelProperty;
            set => Set(ref _NByNCubeViewModelProperty, value);
        }
        private SkewbViewModel _SkewbViewModel;
        public SkewbViewModel SkewbViewModelProperty {
            get => _SkewbViewModel;
            set => Set(ref _SkewbViewModel, value);
        }
        private PyraminxViewModel _PyraminxViewModel;
        public PyraminxViewModel PyraminxViewModelProperty {
            get => _PyraminxViewModel;
            set => Set(ref _PyraminxViewModel, value);
        }
        private MegaminxViewModel _MegaminxViewModelProperty;
        public MegaminxViewModel MegaminxViewModelProperty {
            get => _MegaminxViewModelProperty;
            set => Set(ref _MegaminxViewModelProperty, value);
        }
        bool _IsComboBoxFocused = true;
        public bool IsComboBoxFocused {
            get => _IsComboBoxFocused;
            set => Set(ref _IsComboBoxFocused, value);
        }
        double _ControlWidth;
        public double ControlWidth {
            get => _ControlWidth;
            set => Set(ref _ControlWidth, value);
        }
        double _ControlHeight;
        public double ControlHeight {
            get => _ControlHeight;
            set => Set(ref _ControlHeight, value);
        }
        double TextScrambleControlHeight = 100;
        private IList _GridSelectedList = new ObservableCollection<Solution>();

        public IList GridSelectedList {
            get => _GridSelectedList;
            set {
                Set(ref _GridSelectedList, value);
                AvgAnySelected = "";
            }
        }
        Settings settings;
        public ObservableCollection<ObservableCollection<Solution>> listOfSolutions;
        private ObservableCollection<Solution> _CurrentPuzzleList;
        public ObservableCollection<Solution> CurrentPuzzleList {
            get => _CurrentPuzzleList;
            set {
                Set(ref _CurrentPuzzleList, value);
                CurrentPuzzleListListCollectionView = new ListCollectionView(value);
            }
        }
        private ListCollectionView _CurrentPuzzleListListCollectionView;
        public ListCollectionView CurrentPuzzleListListCollectionView {
            get => _CurrentPuzzleListListCollectionView;
            set => Set(ref _CurrentPuzzleListListCollectionView, value);

        }
        public static List<string> ScrambleLetters {
            get {
                return new List<string> {
                    //2x2
                    @"^[RUFL][2']?$",
                    //3x3
                    @"^[RUFLDB][2']?$",
                    //4x4
                    @"^([RUFL][w]?[2']?|[DB][2']?)?$",
                    //5x5
                    @"^[RUFLDB][w]?[2']?$",
                    //6x6
                    @"^[3]?[RUFLDB][w]?[2']?$",
                    //7x7
                    @"^[3]?[RUFLDB][w]?[2']?$",
                    //Skewb
                    @"^[URLB][']?$",
                    //Pyraminx
                    @"^[RULBrulb][']?$",
                    //Megaminx
                    @"^(U'?|[RD](\+{2}|-{2}))$",
                };
            }
        }
        public static List<string> PuzzleNameCollection {
            get {
                return new List<string>{
                    "2x2",
                    "3x3",
                    "4x4",
                    "5x5",
                    "6x6",
                    "7x7",
                    "Skewb",
                    "Pyraminx",
                    "Megaminx"
                };
            }
        }
        public int CurrentPuzzle {
            get => settings.CurrentPuzzle;
            set {
                Set(ref settings.CurrentPuzzle, value);
                CurrentPuzzleList = listOfSolutions[value];
                UpdateAvg();
            }
        }
        private Solution _CurrentSolutionGrid;
        public Solution CurrentSolutionGrid {
            get => _CurrentSolutionGrid;
            set => Set(ref _CurrentSolutionGrid, value);
        }
        private SolutionTime _Avg3Last;
        public string Avg3Last {
            get => _Avg3Last?.Time ?? "-";
            set {
                if (listOfSolutions != null) {
                    TimeSpan temp = CalcAvgN(3);
                    if (temp != TimeSpan.Zero) {
                        Set(ref _Avg3Last, new SolutionTime(temp));
                    }
                    else {
                        Set(ref _Avg3Last, null);
                    }
                }
                else {
                    Set(ref _Avg3Last, null);
                }
            }
        }
        private SolutionTime _Avg5Last;
        public string Avg5Last {
            get => _Avg5Last?.Time ?? "-";
            set {
                if (listOfSolutions != null) {
                    TimeSpan temp = CalcAvgN(5);
                    if (temp != TimeSpan.Zero) {
                        Set(ref _Avg5Last, new SolutionTime(temp));
                    }
                    else {
                        Set(ref _Avg5Last, null);
                    }
                }
                else {
                    Set(ref _Avg5Last, null);
                }
            }
        }
        private SolutionTime _AvgAll;
        public string AvgAll {
            get => _AvgAll?.Time ?? "-";
            set {
                if (listOfSolutions != null) {
                    TimeSpan temp = CalcAvgN(listOfSolutions[CurrentPuzzle].Count);
                    if (temp != TimeSpan.Zero) {
                        Set(ref _AvgAll, new SolutionTime(temp));
                    }
                    else {
                        Set(ref _AvgAll, null);
                    }
                }
                else {
                    Set(ref _AvgAll, null);
                }
            }
        }
        private string _AvgAnytextBox;
        public string AvgAnyTextBox {
            get => _AvgAnytextBox;
            set {
                if (value != "" && new Regex("^[0-9]+$").IsMatch(value) && Int32.Parse(value) > 0 && Int32.Parse(value) <= listOfSolutions[CurrentPuzzle].Count) {
                    Set(ref _AvgAnytextBox, value);
                    AvgAny = value;
                } else {
                    Set(ref _AvgAnytextBox, "");
                    AvgAny = "-";
                }
            }
        }
        private SolutionTime _AvgAny;
        public string AvgAny {
            get {
                return _AvgAny?.Time ?? "-";
            }
            set {
                if (listOfSolutions != null) {
                    if (Int32.TryParse(value, out _) == true) {
                        TimeSpan temp = CalcAvgN(Int32.Parse(value));
                        if (temp != TimeSpan.Zero) {
                            Set(ref _AvgAny, new SolutionTime(temp));
                        }
                        else {
                            Set(ref _AvgAny, null);
                        }
                    }
                    else {
                        Set(ref _AvgAny, null);
                    }
                }
                else {
                    Set(ref _AvgAny, null);
                }
            }
        }
        private string _AvgAnySelectedText;
        public string AvgAnySelectedText {
            get => _AvgAnySelectedText;
            set => Set(ref _AvgAnySelectedText, value);
        }
        private SolutionTime _AvgAnySelected;
        public string AvgAnySelected {
            get {
                return _AvgAnySelected?.Time ?? "-";
            }
            set {
                if (GridSelectedList != null) {
                    if (GridSelectedList.Count != 0) {
                        TimeSpan temp = TimeSpan.FromMilliseconds(GridSelectedList
                            .OfType<Solution>()
                            .Select(x => x.TotalMilliseconds())
                            .Average());
                        if (temp != TimeSpan.Zero) {
                            Set(ref _AvgAnySelected, new SolutionTime(temp));
                        } else {
                            Set(ref _AvgAnySelected, null);
                        }
                    } else {
                        Set(ref _AvgAnySelected, null);
                    }
                } else {
                    Set(ref _AvgAnySelected, null);
                }
            }
        }
        private string _StopWatchText = "0:00:00";
        public string StopWatchText {
            get => _StopWatchText;
            set => Set(ref _StopWatchText, value);
        }
        private SolidColorBrush _MainColorStopWatchForeground;
        private SolidColorBrush _StopWatchForeground;
        public SolidColorBrush StopWatchForeground {
            get => _StopWatchForeground;
            set => Set(ref _StopWatchForeground, value);
        }
        private bool _ShowStopWatchCheckBox = true;
        public bool ShowStopWatchCheckBox {
            get => _ShowStopWatchCheckBox;
            set => Set(ref _ShowStopWatchCheckBox, value);
        }
        private Visibility _StopWatchVisibility = Visibility.Visible;
        public Visibility StopWatchVisibility {
            get => _StopWatchVisibility;
            set => Set(ref _StopWatchVisibility, value);
        }
        private string _ScrambleText;
        public string ScrambleText {
            get => _ScrambleText;
            set => Set(ref _ScrambleText, value);
        }
        private ObservableCollection<Visibility> _UnfoldsVisibility;
        public ObservableCollection<Visibility> UnfoldsVisibility {
            get => _UnfoldsVisibility;
            set => Set(ref _UnfoldsVisibility, value);
        }
        private Visibility _GridVisibility;
        public Visibility GridVisibility {
            get => _GridVisibility;
            set => Set(ref _GridVisibility, value);
        }
        #endregion
        #region Text
        private string _DateToolTipText;
        public string DateToolTipText {
            get => _DateToolTipText;
            set => Set(ref _DateToolTipText, value);
        }
        private string _GridHeaderIdText;
        public string GridHeaderIdText {
            get => _GridHeaderIdText;
            set => Set(ref _GridHeaderIdText, value);
        }
        private string _GridHeaderPuzzleNameText;
        public string GridHeaderPuzzleNameText {
            get => _GridHeaderPuzzleNameText;
            set => Set(ref _GridHeaderPuzzleNameText, value);
        }
        private string _GridHeaderSolutionTimeText;
        public string GridHeaderSolutionTimeText {
            get => _GridHeaderSolutionTimeText;
            set => Set(ref _GridHeaderSolutionTimeText, value);
        }
        private string _GridHeaderSolutionDateText;
        public string GridHeaderSolutionDateText {
            get => _GridHeaderSolutionDateText;
            set => Set(ref _GridHeaderSolutionDateText, value);
        }
        private string _GridHeaderScrambleText;
        public string GridHeaderScrambleText {
            get => _GridHeaderScrambleText;
            set => Set(ref _GridHeaderScrambleText, value);
        }
        private string _EditText;
        public string EditText {
            get => _EditText;
            set => Set(ref _EditText, value);
        }
        private string _AddText;
        public string AddText {
            get => _AddText;
            set => Set(ref _AddText, value);
        }
        private string _UpdateText;
        public string UpdateText {
            get => _UpdateText;
            set => Set(ref _UpdateText, value);
        }
        private string _DeleteText;
        public string DeleteText {
            get => _DeleteText;
            set => Set(ref _DeleteText, value);
        }
        private string _ClearAllText;
        public string ClearAllText {
            get => _ClearAllText;
            set => Set(ref _ClearAllText, value);
        }
        private string _CopyToClipboardText;
        public string CopyToClipboardText {
            get => _CopyToClipboardText;
            set => Set(ref _CopyToClipboardText, value);
        }
        private string _ChartText;
        public string ChartText {
            get => _ChartText;
            set => Set(ref _ChartText, value);
        }
        private string _HelpText;
        public string HelpText {
            get => _HelpText;
            set => Set(ref _HelpText, value);
        }
        private string _AboutText;
        public string AboutText {
            get => _AboutText;
            set => Set(ref _AboutText, value);
        }
        private string _SettingsText;
        public string SettingsText {
            get => _SettingsText;
            set => Set(ref _SettingsText, value);
        }
        private string _SelectPuzzleText;
        public string SelectPuzzleText {
            get => _SelectPuzzleText;
            set => Set(ref _SelectPuzzleText, value);
        }
        private string _Avg3LastText;
        public string Avg3LastText {
            get => _Avg3LastText;
            set => Set(ref _Avg3LastText, value);
        }
        private string _Avg5LastText;
        public string Avg5LastText {
            get => _Avg5LastText;
            set => Set(ref _Avg5LastText, value);
        }
        private string _AvgAllText;
        public string AvgAllText {
            get => _AvgAllText;
            set => Set(ref _AvgAllText, value);
        }
        private string _AvgAnyText;
        public string AvgAnyText {
            get => _AvgAnyText;
            set => Set(ref _AvgAnyText, value);
        }
        private string _AvgText;
        public string AvgText {
            get => _AvgText;
            set => Set(ref _AvgText, value);
        }
        private string _AvgLastWordText;
        public string AvgLastWordText {
            get => _AvgLastWordText;
            set => Set(ref _AvgLastWordText, value);
        }
        private string _NewScrambleText;
        public string NewScrambleText {
            get => _NewScrambleText;
            set => Set(ref _NewScrambleText, value);
        }
        private string _UnfoldText;
        public string UnfoldText {
            get => _UnfoldText;
            set => Set(ref _UnfoldText, value);
        }
        private string _ShowTimeText;
        public string ShowTimeText {
            get => _ShowTimeText;
            set => Set(ref _ShowTimeText, value);
        }
        private string _ZeroItemsChartText;
        public string ZeroItemsChartText {
            get => _ZeroItemsChartText;
            set => Set(ref _ZeroItemsChartText, value);
        }

        #endregion
        #region Commands
        #region AddItemCommand
        public ICommand AddItemCommand { get; }

        private bool CanAddItemCommandExecute(object p) => true;
        private void OnAddItemCommandExecuted(object p) {
            ItemWindow itemWindow = new ItemWindow(UpdateAvg, listOfSolutions, 
                settings, CurrentSolutionGrid);
            itemWindow.Owner = Application.Current.MainWindow;
            itemWindow.ShowDialog();
        }
        #endregion
        #region UpdateItemCommand
        public ICommand UpdateItemCommand { get; }

        private bool CanUpdateItemCommandExecute(object p) {
            bool a = CurrentSolutionGrid?.Id != null && CurrentSolutionGrid?.Id >= 0;
            return a;
        }
        private void OnUpdateItemCommandExecuted(object p) {
            if (CurrentSolutionGrid?.Id != null) {
                ItemWindow itemWindow = new ItemWindow(UpdateAvg, listOfSolutions,
                settings, CurrentSolutionGrid, true);
                itemWindow.Owner = Application.Current.MainWindow;
                itemWindow.ShowDialog();
            }
        }
        #endregion
        #region DeleteItemCommand
        public ICommand DeleteItemCommand { get; }

        private bool CanDeleteItemCommandExecute(object p) => CurrentSolutionGrid?.Id != null && CurrentSolutionGrid?.Id >= 0;
        private void OnDeleteItemCommandExecuted(object p) {
            DeleteItemHandle();
        }
		private void DeleteItemHandle(){
			using (SolutionsDbContext context = new SolutionsDbContext()) {
                int indexSingle = CurrentSolutionGrid.Id;
                context.Solutions.RemoveRange(GridSelectedList.OfType<Solution>());
                context.SaveChanges();
                List<Solution> tempList = new List<Solution>(GridSelectedList.Cast<Solution>());
                int index = tempList[0].Id;
                for (int j = 0; j < tempList.Count; ++j) {
                    listOfSolutions[CurrentPuzzle].Remove(tempList[j]);
                }
                int n = context.Solutions
                    .Where(x => x.PuzzleName == PuzzleNameCollection[CurrentPuzzle])
                    .Count();
                int newIndex = 1;
                tempList = context.Solutions
                    .Where(x => x.PuzzleName == PuzzleNameCollection[CurrentPuzzle])
                    .OrderBy(x => x.Id)
                    .ToList();
                for (int i = 0; i < n; ++i) {
                    Solution temp = tempList[i];
                    context.Solutions.Remove(temp);
                    context.SaveChanges();
                    listOfSolutions[CurrentPuzzle].RemoveAt(i);
                    temp.Id = newIndex++;
                    context.Solutions.Add(temp);
                    context.SaveChanges();
                    listOfSolutions[CurrentPuzzle].Insert(i, temp);
                }
            }
            UpdateAvg();
            IsComboBoxFocused = true;
            IsComboBoxFocused = false;
            IsComboBoxFocused = true;
		}
        #endregion
        #region ClearAllCommand
        public ICommand ClearAllCommand { get; }

        private bool CanClearAllCommandExecute(object p) => true;
        private void OnClearAllCommandExecuted(object p) {
            using (SolutionsDbContext context = new SolutionsDbContext()) {
                var ItemsToRemove = context.Solutions.Where(x => x.PuzzleName == PuzzleNameCollection[CurrentPuzzle]);
                foreach (var itemToRemove in ItemsToRemove) context.Solutions.Remove(itemToRemove);
                context.SaveChanges();
                listOfSolutions[CurrentPuzzle].Clear();
            }
            UpdateAvg();
            IsComboBoxFocused = true;
            IsComboBoxFocused = false;
            IsComboBoxFocused = true;
        }
        #endregion
        #region CopyToClipboardCommand
        public ICommand CopyToClipboardCommand { get; }

        private bool CanCopyToClipboardCommandExecute(object p) => CurrentSolutionGrid?.Id != null && CurrentSolutionGrid?.Id >= 0;
        private void OnCopyToClipboardCommandExecuted(object p) {
            Clipboard.SetText($"{CurrentSolutionGrid.SolutionTime} {CurrentSolutionGrid.Scramble}");
        }
        #endregion
        #region ChartCommand
        public ICommand ChartCommand { get; }

        private bool CanChartCommandExecute(object p) => true;
        private void OnChartCommandExecuted(object p) {
            if (listOfSolutions[CurrentPuzzle].Count != 0) {
                ChartWindow chartWindow = new ChartWindow(CurrentPuzzleList, settings);
                chartWindow.Owner = Application.Current.MainWindow;
                chartWindow.ShowDialog();
            }
            else {
                string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
                ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
                new MyMessageBoxWindow(LocRM.GetString("ErrorText"), ZeroItemsChartText, settings).ShowDialog();
            }
        }
        #endregion
        #region HelpCommand
        public ICommand HelpCommand { get; }

        private bool CanHelpCommandExecute(object p) => true;
        private void OnHelpCommandExecuted(object p) {
            HelpWindow helpWindow = new HelpWindow(settings);
            helpWindow.Owner = Application.Current.MainWindow;
            helpWindow.ShowDialog();
        }
        #endregion
        #region AboutCommand
        public ICommand AboutCommand { get; }

        private bool CanAboutCommandExecute(object p) => true;
        private void OnAboutCommandExecuted(object p) {
            AboutWindow aboutWindow = new AboutWindow(settings);
            aboutWindow.Owner = Application.Current.MainWindow;
            aboutWindow.ShowDialog();
        }
        #endregion
        #region SettingsCommand
        public ICommand SettingsCommand { get; }
        private bool CanSettingsCommandExecute(object p) => true;
        private void OnSettingsCommandExecuted(object p) {
            SettingsWindow settingsWindow = new SettingsWindow(settings, SettingsWindow_Notify, StopWatchTextColor, SaveSettings);
            settingsWindow.Owner = Application.Current.MainWindow;
            settingsWindow.ShowDialog();
        }
        private void SettingsWindow_Notify() {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            CultureInfo ci = new CultureInfo(LocRM.GetString("CultureString"));
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            EditText = LocRM.GetString("Edit");
            AddText = LocRM.GetString("Add");
            UpdateText = LocRM.GetString("Update");
            DeleteText = LocRM.GetString("Delete");
            ClearAllText = LocRM.GetString("ClearAll");
            CopyToClipboardText = LocRM.GetString("CopyToClipboard");
            ChartText = LocRM.GetString("Chart");
            ZeroItemsChartText = LocRM.GetString("ZeroItemsCantDrawChart");
            HelpText = LocRM.GetString("Help");
            AboutText = LocRM.GetString("AboutAuthor");
            SettingsText = LocRM.GetString("Settings");
            SelectPuzzleText = LocRM.GetString("PuzzleType");
            Avg3LastText = LocRM.GetString("Avg3Last");
            Avg5LastText = LocRM.GetString("Avg5Last");
            AvgAllText = LocRM.GetString("AvgAll");
            AvgAnySelectedText = LocRM.GetString("AvgAnySelected");
            AvgText = LocRM.GetString("Avg");
            AvgLastWordText = LocRM.GetString("Last");
            NewScrambleText = LocRM.GetString("NewScramble");
            UnfoldText = LocRM.GetString("Unfold");
            ShowTimeText = LocRM.GetString("ShowTime");
            GridHeaderIdText = LocRM.GetString("GridHeaderIdText");
            GridHeaderPuzzleNameText = LocRM.GetString("GridHeaderPuzzleNameText");
            GridHeaderSolutionTimeText = LocRM.GetString("GridHeaderSolutionTimeText");
            GridHeaderSolutionDateText = LocRM.GetString("GridHeaderSolutionDateText");
            GridHeaderScrambleText = LocRM.GetString("GridHeaderScrambleText");
            DateToolTipText = LocRM.GetString("DateToolTipText");
        }
        private void StopWatchTextColor(int i) {
            if (i == 0 || i == 2) {
                _MainColorStopWatchForeground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else {
                _MainColorStopWatchForeground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            StopWatchForeground = _MainColorStopWatchForeground;
        }
        #endregion
        #region KeyDownCommand
        public ICommand KeyDownCommand { get; }
        #endregion
        #region KeyReleaseCommand
        public ICommand KeyUpCommand { get; }
        #endregion
        #region EditScrambleCommand
        public ICommand EditScrambleCommand { get; }

        private bool CanEditScrambleCommandExecute(object p) => true;
        private void OnEditScrambleCommandExecuted(object p) {
            CustomScrambleWindow customScrambleWindow = new CustomScrambleWindow(settings, SetCustomScramble);
            customScrambleWindow.Owner = Application.Current.MainWindow;
            customScrambleWindow.ShowDialog();
        }
        #endregion
        #region NewScrambleCommand
        public ICommand NewScrambleCommand { get; }

        private bool CanNewScrambleCommandExecute(object p) => true;
        private void OnNewScrambleCommandExecuted(object p) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            ScrambleText = LocRM.GetString("Generating");
            GenerateScrambleAsync();
        }
        #endregion
        #region SizeChangedCommand
        public ICommand SizeChangedCommand { get; }
        #endregion
        #region SizeTextScrambleChangedCommand
        public ICommand SizeTextScrambleChangedCommand { get; }
        #endregion
        #region PuzzleTypeSelectionChangedCommand
        public ICommand PuzzleTypeSelectionChangedCommand { get; }
        #endregion
        #region GridLostFocusCommand
        public ICommand GridLostFocusCommand { get; }
        #endregion
        #region GridDeleteKeyUpCommand
        public ICommand GridDeleteKeyUpCommand { get; }
        #endregion
        #region UnfoldGridCommand
        public ICommand UnfoldGridCommand { get; }

        private bool CanUnfoldGridCommandExecute(object p) => true;
        private void OnUnfoldGridCommandExecuted(object p) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            if (!IsUnfold) {
                UnfoldText = LocRM.GetString("Table");
                IsUnfold = true;
                GridVisibility = Visibility.Collapsed;
                if (CurrentPuzzle >= 0 && CurrentPuzzle <= 5) {
                    UnfoldsVisibility[0] = Visibility.Visible;
                }
                else {
                    UnfoldsVisibility[CurrentPuzzle - 5] = Visibility.Visible;
                }
            }
            else {
                UnfoldText = LocRM.GetString("Unfold");
                IsUnfold = false;
                for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                    UnfoldsVisibility[i] = Visibility.Collapsed;
                }
                GridVisibility = Visibility.Visible;
            }
        }
        #endregion
        #endregion

        public MainWindowViewModel() {
            
            #region Commands
            AddItemCommand = new LambdaCommand(OnAddItemCommandExecuted, CanAddItemCommandExecute);
            UpdateItemCommand = new LambdaCommand(OnUpdateItemCommandExecuted, CanUpdateItemCommandExecute);
            DeleteItemCommand = new LambdaCommand(OnDeleteItemCommandExecuted, CanDeleteItemCommandExecute);
            ClearAllCommand = new LambdaCommand(OnClearAllCommandExecuted, CanClearAllCommandExecute);
            CopyToClipboardCommand = new LambdaCommand(OnCopyToClipboardCommandExecuted, CanCopyToClipboardCommandExecute);
            ChartCommand = new LambdaCommand(OnChartCommandExecuted, CanChartCommandExecute);
            HelpCommand = new LambdaCommand(OnHelpCommandExecuted, CanHelpCommandExecute);
            AboutCommand = new LambdaCommand(OnAboutCommandExecuted, CanAboutCommandExecute);
            SettingsCommand = new LambdaCommand(OnSettingsCommandExecuted, CanSettingsCommandExecute);
            EditScrambleCommand = new LambdaCommand(OnEditScrambleCommandExecuted, CanEditScrambleCommandExecute);
            NewScrambleCommand = new LambdaCommand(OnNewScrambleCommandExecuted, CanNewScrambleCommandExecute);
            KeyDownCommand = new RelayCommand<KeyEventArgs>(OnKeyDown, null);
            KeyUpCommand = new RelayCommand<KeyEventArgs>(OnKeyUp, null);
            GridLostFocusCommand = new RelayCommand<RoutedEventArgs>(OnGridLostFocus, null);
            GridDeleteKeyUpCommand = new RelayCommand<KeyEventArgs>(OnGridDeleteKeyUp, null);
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            SizeTextScrambleChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeTextScrambleChangedCommand, null);
            PuzzleTypeSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(OnPuzzleTypeSelectionChangedCommand, null);
            UnfoldGridCommand = new LambdaCommand(OnUnfoldGridCommandExecuted, CanUnfoldGridCommandExecute);
            #endregion
            GridVisibility = Visibility.Visible;

            try {
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Settings.json"));
            } catch {
                settings = new Settings();
            }
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            ScrambleText = LocRM.GetString("Generating");
            UnfoldsVisibility = new ObservableCollection<Visibility>();
            for (int i = 0; i < PuzzleNameCollection.Count; ++i) {
                UnfoldsVisibility.Add(Visibility.Collapsed);
            }
            NByNCubeViewModelProperty = new NByNCubeViewModel(CurrentPuzzle + 2);
            SkewbViewModelProperty = new SkewbViewModel();
            PyraminxViewModelProperty = new PyraminxViewModel();
            MegaminxViewModelProperty = new MegaminxViewModel();
            ControlHeight = 580;
            ControlWidth = 840;
            GenerateScrambleAsync();
            ReadFromDb();
            CurrentPuzzleList = listOfSolutions[settings.CurrentPuzzle];
            CurrentPuzzleListListCollectionView = new ListCollectionView(listOfSolutions[settings.CurrentPuzzle]);
            SettingsWindow_Notify();
            LoadTheme();
            UpdateAvg();
            dispatcherTimer1.Tick += new EventHandler(HandleTimeDelay);
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer2.Tick += new EventHandler(HandleTime);
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 1);

            sw = new Stopwatch();
            delay = false;
            delay2 = false;
            CheckForUpdate();
        }

        private void OnGridDeleteKeyUp(KeyEventArgs obj) {
            if(obj.Key == Key.Delete)
                DeleteItemHandle();
        }

        private void OnGridLostFocus(RoutedEventArgs obj) {
            obj.Handled = true;
        }

        private void OnPuzzleTypeSelectionChangedCommand(SelectionChangedEventArgs obj) {
            new Thread(PuzzleTypeSelectionChanged).Start();
        }
        private void PuzzleTypeSelectionChanged() {
            SaveSettings();
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            ScrambleText = LocRM.GetString("Generating");
            if (CurrentPuzzle >= 0 && CurrentPuzzle <= 5) {
                NByNCubeViewModelProperty = new NByNCubeViewModel(CurrentPuzzle + 2);
                GenerateScrambleAsync();
                if (IsUnfold) {
                    for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                        UnfoldsVisibility[i] = Visibility.Collapsed;
                    }
                    UnfoldsVisibility[0] = Visibility.Visible;
                }
            } else if (CurrentPuzzle == 6) {
                GenerateScrambleAsync();
                if (IsUnfold) {
                    for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                        UnfoldsVisibility[i] = Visibility.Collapsed;
                    }
                    UnfoldsVisibility[1] = Visibility.Visible;
                }
            } else if (CurrentPuzzle == 7) {
                GenerateScrambleAsync();
                if (IsUnfold) {
                    for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                        UnfoldsVisibility[i] = Visibility.Collapsed;
                    }
                    UnfoldsVisibility[2] = Visibility.Visible;
                }
            } else if (CurrentPuzzle == 8) {
                GenerateScrambleAsync();
                if (IsUnfold) {
                    for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                        UnfoldsVisibility[i] = Visibility.Collapsed;
                    }
                    UnfoldsVisibility[3] = Visibility.Visible;
                }
            }
            SetUnfoldsSizeAsync(ControlHeight - 50 - TextScrambleControlHeight);
        }
        private void SaveSettings() {
            File.WriteAllTextAsync("settings.json", JsonConvert.SerializeObject(settings));
        }
        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ControlHeight = obj.NewSize.Height;
            ControlWidth = obj.NewSize.Width;
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            if (ScrambleText != LocRM.GetString("Generating"))
                SetUnfoldsSizeAsync(ControlHeight - 50 - TextScrambleControlHeight);
                
        }
        private void OnSizeTextScrambleChangedCommand(SizeChangedEventArgs obj) {
            TextScrambleControlHeight = obj.NewSize.Height;
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            if (ScrambleText != LocRM.GetString("Generating"))
                SetUnfoldsSizeAsync(ControlHeight - 50 - TextScrambleControlHeight);
        }
        private async void SetUnfoldsSizeAsync(double Height, double Width = 0) {
            await Task.Run(() => SetUnfoldsSize(Height, Width));
        }
        private void SetUnfoldsSize(double Height, double Width = 0) {
            double controlHeight = Height - 5;
            double heightToWidth = 1.25;
            double widthToHeight = 1;
            double controlHeightPyraminx = Height - 5;
            double heightToWidthPyraminx = 1.35;
            double widthToHeightPyraminx = 1;
            double controlHeightMegaminx = Height - 5;
            double heightToWidthMegaminx = 2;
            double widthToHeightMegaminx = 1;
            if (ControlWidth != 0 && ControlWidth <= controlHeight * heightToWidth*1.1) {
                controlHeight = ControlWidth*0.9;
                heightToWidth = 0.95;
                widthToHeight = 0.75;
            }
            if (ControlWidth != 0 && ControlWidth <= controlHeightPyraminx * heightToWidthPyraminx*1.1) {
                controlHeightPyraminx = ControlWidth-5;
                heightToWidthPyraminx = 0.8;
                widthToHeightPyraminx = 2.0/3;
            }
            if (ControlWidth != 0 && ControlWidth < controlHeightMegaminx * heightToWidthMegaminx * 1.1) {
                controlHeightMegaminx = ControlWidth * 1.1;//1.45
                heightToWidthMegaminx = 1;
                widthToHeightMegaminx = 0.5;
            }
            NByNCubeViewModelProperty.ScaleX = controlHeight * heightToWidth / 64.0;
            NByNCubeViewModelProperty.ScaleY = controlHeight * widthToHeight / 62.0;
            NByNCubeViewModelProperty.ControlHeight = controlHeight * widthToHeight*0.95;
            NByNCubeViewModelProperty.ControlWidth = controlHeight * heightToWidth*0.95;

            SkewbViewModelProperty.ScaleX = controlHeight * heightToWidth / 51.5;
            SkewbViewModelProperty.ScaleY = controlHeight * widthToHeight / 42.0;
            SkewbViewModelProperty.ControlHeight = controlHeight * widthToHeight*0.95;
            SkewbViewModelProperty.ControlWidth = controlHeight * heightToWidth*0.95;

            PyraminxViewModelProperty.ScaleX = (controlHeightPyraminx - 15) * heightToWidthPyraminx / 64.0;
            PyraminxViewModelProperty.ScaleY = (controlHeightPyraminx - 15) * widthToHeightPyraminx / 62.0;
            PyraminxViewModelProperty.ControlHeight = (controlHeightPyraminx - 15) * widthToHeightPyraminx * 0.9;
            PyraminxViewModelProperty.ControlWidth = (controlHeightPyraminx - 15) * heightToWidthPyraminx * 0.9;

            MegaminxViewModelProperty.ScaleX = controlHeightMegaminx * heightToWidthMegaminx / 120.0;
            MegaminxViewModelProperty.ScaleY = controlHeightMegaminx * widthToHeightMegaminx / 45.0;
            MegaminxViewModelProperty.ControlHeight = controlHeightMegaminx * widthToHeightMegaminx;
            MegaminxViewModelProperty.ControlWidth = controlHeightMegaminx * heightToWidthMegaminx;

        }
        private void SetCustomScramble(string s) {
            ScrambleText = s;
            UpdateUnfold();
        }
        private void UpdateAvg() {
            Avg3Last = "";
            Avg5Last = "";
            AvgAll = "";
            AvgAny = AvgAnyTextBox;
        }
        private void LoadTheme() {
            var uri = new Uri("Themes/" + SettingsViewModel.ThemesNames[settings.CurrentTheme] + ".xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            StopWatchTextColor(settings.CurrentTheme);
        }
        private void ReadFromDb() {
            listOfSolutions = new ObservableCollection<ObservableCollection<Solution>>();
            for (int i = 0; i < PuzzleNameCollection.Count; i++) {
                listOfSolutions.Add(new ObservableCollection<Solution>());
            }
            using (SolutionsDbContext context = new SolutionsDbContext()) {
                
                List<Solution> tempListOfSolutions = context.Solutions.ToList();
                for (int i = 0; i < PuzzleNameCollection.Count; ++i) {
                    tempListOfSolutions
                        .Where(x => x.PuzzleName == PuzzleNameCollection[i])
                        .ToList()
                        .ForEach(x => listOfSolutions[i].Add(x));
                }
            }
        }
        private TimeSpan CalcAvgN(int n) {
            TimeSpan temp = TimeSpan.Zero;
            if (n == 0) {
                return temp;
            }
            if (listOfSolutions[CurrentPuzzle].Count == 0) {
            }
            else if (listOfSolutions[CurrentPuzzle].Count < n) {
            }
            else if (listOfSolutions[CurrentPuzzle].Count >= n) {
                temp = TimeSpan.FromMilliseconds(listOfSolutions[CurrentPuzzle].
                    ToList()
                    .OfType<Solution>()
                    .OrderByDescending(x => x.SolutionDate)
                    .Take(n)
                    .Select(x => x.TotalMilliseconds())
                    .Average());
            }
            return temp;
        }
        public int TimeInMSec(string solutionTime) {
            int num = 0;
            int res = 0;
            for (int i = 0; i < solutionTime.Length; ++i) {
                if (Char.IsDigit(solutionTime[i])) {
                    num *= 10;
                    num += Int32.Parse(solutionTime[i].ToString());
                }
                if (solutionTime[i] == ':') {
                    res += num * 6000;
                    num = 0;
                }
                else if (solutionTime[i] == '.') {
                    res += num * 100;
                    num = 0;
                }
                else if (i == solutionTime.Length - 1) {
                    res += num * Convert.ToInt32(Math.Pow(10, 2 - num.ToString().Length));
                }
            }
            return res * 10;
        }
        private async void GenerateScrambleAsync() => await Task.Run(() => GenerateScramble());
        private void GenerateScramble() {
            int currentPuzzle = CurrentPuzzle;
            Puzzle puzzle = currentPuzzle switch {
                0 => new TwoByTwoCubePuzzle(),
                1 => new ThreeByThreeCubePuzzle(),
                2 => new FourByFourCubePuzzle(),
                3 => new NbyNCubePuzzle(5),
                4 => new NbyNCubePuzzle(6),
                5 => new NbyNCubePuzzle(7),
                6 => new SkewbPuzzle(),
                7 => new PyraminxPuzzle(),
                8 => new MegaminxPuzzle(),
                _ => null
            };
            var r = new Random(Convert.ToInt32(DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString()) + Convert.ToInt32(DateTime.Now.DayOfYear.ToString() + DateTime.Now.Year.ToString()));
            ScrambleText = puzzle.GenerateWcaScramble(r);
            if (currentPuzzle == 4) {
                TextScrambleControlHeight = 156;
            } else if (currentPuzzle == 5) {
                TextScrambleControlHeight = 179;
            } else if (currentPuzzle == 8) {
                TextScrambleControlHeight = 173;
            }
            SetUnfoldsSizeAsync(ControlHeight - 50 - TextScrambleControlHeight - 20);
            UpdateUnfold();
        }
        private void UpdateUnfold() {
            if (CurrentPuzzle >= 0 && CurrentPuzzle <= 5) {
                NByNCubeViewModelProperty.SetUnfold(ScrambleText);
            }
            else if (CurrentPuzzle == 6) {
                SkewbViewModelProperty.SetUnfold(ScrambleText);
            }
            else if (CurrentPuzzle == 7) {
                PyraminxViewModelProperty.SetUnfold(ScrambleText);
            } 
            else if (CurrentPuzzle == 8) {
                MegaminxViewModelProperty.SetUnfold(ScrambleText);
            }
        }
        
        private void OnKeyDown(KeyEventArgs e) {
            if (e.Key == Key.Space) {
                e.Handled = true;
                Dispatcher.CurrentDispatcher.BeginInvoke(() => {
                    if (!delay && !delay2) {
                        isThreadTimeDelayRunning = true;
                        //create new thread to run stopwatch in
                        sw = new Stopwatch();
                        dispatcherTimer1.Start();
                        sw.Start();
                        isActive = true;

                        StopWatchText = "00:00.00";
                        StopWatchForeground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        delay = true;
                        delay2 = true;
                        isDown = true;
                    }
                    else if (!delay2) {
                        //cause the thread to end itself
                        dispatcherTimer2.Stop();
                        sw.Stop();
                        isThreadStopWatchRunning = false;
                        if (StopWatchVisibility == Visibility.Hidden)
                            StopWatchVisibility = Visibility.Visible;
                        delay = false;
                        delay2 = false;

                        //Add Solution
                        using (SolutionsDbContext context = new SolutionsDbContext()) {
                            int index = 1;
                            List<Solution> tempCollection = context.Solutions
                                        .Where(x => x.PuzzleName == PuzzleNameCollection[settings.CurrentPuzzle].ToString())
                                        .ToList();
                            if (tempCollection.Count != 0) {
                                index = tempCollection.Select(x => x.Id).Max() + 1;
                            }
                            Solution temp = new Solution(
                                    index,
                                    PuzzleNameCollection[settings.CurrentPuzzle].ToString(),
                                    StopWatchText,
                                    DateTime.Now,
                                    ScrambleText
                                );
                            context.Solutions.Add(
                                temp
                            );
                            context.SaveChanges();
                            listOfSolutions[settings.CurrentPuzzle].Add(
                                temp
                            );
                        }
                        UpdateAvg();

                        GenerateScrambleAsync();
                    }
                });
            }
        }
        private void OnKeyUp(KeyEventArgs e) {
            if (e.Key == Key.Space && isDown) {
                sw.Stop();
                double ts = sw.Elapsed.TotalSeconds;
                sw = new Stopwatch();
                isDown = false;
                isThreadTimeDelayRunning = false;
                dispatcherTimer1.Stop();
                pressedUp = true;
                delay2 = false;
                if (delay) {
                    if (pressedUp && isActive) {
                        isActive = false;
                        if (ts >= 1) {
                            if (ShowStopWatchCheckBox == false)
                                StopWatchVisibility = Visibility.Hidden;

                            this.isThreadStopWatchRunning = true;
                            //create new thread to run stopwatch in
                            dispatcherTimer2.Start();
                            sw.Start();
                        }
                        else {
                            pressedUp = false;
                            delay = delay2 = false;
                            isThreadStopWatchRunning = isThreadTimeDelayRunning = false;

                            GenerateScrambleAsync();
                            StopWatchForeground = _MainColorStopWatchForeground;
                        }
                    }
                }
            }
        }
        private void HandleTimeDelay(object sender, EventArgs e) {
            Dispatcher.CurrentDispatcher.BeginInvoke(() => {
                if (isThreadTimeDelayRunning && sw.Elapsed.TotalSeconds >= 1) {
                    isThreadTimeDelayRunning = false;
                    StopWatchForeground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                }
            });
        }
        private void HandleTime(object sender, EventArgs e) {
            Dispatcher.CurrentDispatcher.BeginInvoke(() => {
                if (isThreadStopWatchRunning) {
                    TimeSpan timespan = sw.Elapsed;
                    if (isThreadStopWatchRunning) {
                        StopWatchText = $"{timespan.Minutes}:{timespan.Seconds:d2}.{Convert.ToInt32(Math.Ceiling(timespan.Milliseconds / 10.0)):d2}";
                        StopWatchForeground = _MainColorStopWatchForeground;
                    }
                }
            });
        }
        private void CheckForUpdate() {
            try {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://raw.githubusercontent.com/overflowed-stack/PuzzleTimer/main/PuzzleTimer/CurrentVersion.txt");
                StreamReader reader = new StreamReader(stream);
                String version = reader.ReadLine().Trim();
                Version CurrentVersion = typeof(MainWindow).Assembly.GetName().Version;
                if (CurrentVersion.ToString() != version) {
                    string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
                    ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
                    new MyMessageBoxWindow(LocRM.GetString("NewVersionAvailableText"), $"{LocRM.GetString("NewVersionText1")} {version} {LocRM.GetString("NewVersionText2")}", settings, OpenDownloadSite).ShowDialog();
                }
            } catch { }
        }
        private void OpenDownloadSite() {
            try {
                Process.Start(new ProcessStartInfo("https://drive.google.com/drive/folders/17S_kqKifoVJle5IRiYd74ZSmVS_Fk54S?usp=sharing") { UseShellExecute = true });
            } catch { }
        }
       
    }
}
