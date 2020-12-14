using Microsoft.EntityFrameworkCore.Internal;
using PuzzleTimer.Infrustructure.Commands;
using PuzzleTimer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PuzzleTimer.ViewModels {
    class ItemViewViewModel : ViewModel, ICloseWindow {
        public event NotifyMain Notify = () => { };
        public Action Close { get; set; }
        bool addUpdate;

        public Settings settings;
        public ObservableCollection<ObservableCollection<Solution>> listOfSolutions;
        public Solution CurrentSolutionGrid;
        public List<string> PuzzleNameCollection {
            get => MainWindowViewModel.PuzzleNameCollection;
        }
        #region Unfolds
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
        private ObservableCollection<Visibility> _UnfoldsVisibility;
        public ObservableCollection<Visibility> UnfoldsVisibility {
            get => _UnfoldsVisibility;
            set => Set(ref _UnfoldsVisibility, value);
        }
        double TopControlOffsetHeight = 250;
        #endregion
        #region Text
        private string _SolutionTimeTextBoxToolTipText;
        public string SolutionTimeTextBoxToolTipText {
            get => _SolutionTimeTextBoxToolTipText;
            set => Set(ref _SolutionTimeTextBoxToolTipText, value);
        }
        private string _DateToolTipText;
        public string DateToolTipText {
            get => _DateToolTipText;
            set => Set(ref _DateToolTipText, value);
        }
        private string _PuzzleTypeText;
        public string PuzzleTypeText {
            get => _PuzzleTypeText;
            set => Set(ref _PuzzleTypeText, value);
        }
        private string _SolutionDateText;
        public string SolutionDateText {
            get => _SolutionDateText;
            set => Set(ref _SolutionDateText, value);
        }
        private string _SolutionDateTextBox;
        public string SolutionDateTextBox {
            get => _SolutionDateTextBox;
            set => Set(ref _SolutionDateTextBox, value);
        }
        private string _SolutionTimeText;
        public string SolutionTimeText {
            get => _SolutionTimeText;
            set => Set(ref _SolutionTimeText, value);
        }
        private string _SolutionTimeTextBox;
        public string SolutionTimeTextBox {
            get => _SolutionTimeTextBox;
            set => Set(ref _SolutionTimeTextBox, value);
        }
        private string _ScrambleText;
        public string ScrambleText {
            get => _ScrambleText;
            set => Set(ref _ScrambleText, value);
        }
        private string _ScrambleTextBox;
        public string ScrambleTextBox {
            get => _ScrambleTextBox;
            set { 
                Set(ref _ScrambleTextBox, value);
                if (!CheckIfScrambleIsNotValid()) {
                    if (ComboBoxSelectedIndex >= 0 && ComboBoxSelectedIndex <= 5) {
                        UnfoldsVisibility[0] = Visibility.Visible;
                        NByNCubeViewModelProperty?.SetUnfold(ScrambleTextBox);
                    } else if (ComboBoxSelectedIndex == 6) {
                        UnfoldsVisibility[1] = Visibility.Visible;
                        SkewbViewModelProperty?.SetUnfold(ScrambleTextBox);
                    } else if (ComboBoxSelectedIndex == 7) {
                        UnfoldsVisibility[2] = Visibility.Visible;
                        PyraminxViewModelProperty?.SetUnfold(ScrambleTextBox);
                    } else if (ComboBoxSelectedIndex == 8) {
                        UnfoldsVisibility[3] = Visibility.Visible;
                        MegaminxViewModelProperty?.SetUnfold(ScrambleTextBox);
                    }
                } else {
                    for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                        UnfoldsVisibility[i] = Visibility.Collapsed;
                    }
                }
            }
        }
        private string _AddUpdateText;
        public string AddUpdateText {
            get => _AddUpdateText;
            set => Set(ref _AddUpdateText, value);
        }
        private string _WarningText;
        public string WarningText {
            get => _WarningText;
            set => Set(ref _WarningText, value);
        }
        private int _ComboBoxSelectedIndex;
        public int ComboBoxSelectedIndex {
            get => _ComboBoxSelectedIndex;
            set {
                Set(ref _ComboBoxSelectedIndex, value);
                for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                    UnfoldsVisibility[i] = Visibility.Collapsed;
                }
                if (!CheckIfScrambleIsNotValid()) {
                    if (value >= 0 && value <= 5) {
                        UnfoldsVisibility[0] = Visibility.Visible;
                        NByNCubeViewModelProperty = new NByNCubeViewModel(value + 2);
                        NByNCubeViewModelProperty?.SetUnfold(ScrambleTextBox);
                    } else if (value == 6) {
                        UnfoldsVisibility[1] = Visibility.Visible;
                        SkewbViewModelProperty?.SetUnfold(ScrambleTextBox);
                    } else if (value == 7) {
                        UnfoldsVisibility[2] = Visibility.Visible;
                        PyraminxViewModelProperty?.SetUnfold(ScrambleTextBox);
                    } else if (value == 8) {
                        UnfoldsVisibility[3] = Visibility.Visible;
                        MegaminxViewModelProperty?.SetUnfold(ScrambleTextBox);
                    }
                }
            }
        }
        #endregion
        #region Commands
        #region AddUpdateCommand
        public ICommand AddUpdateCommand { get; }
        private bool CanAddUpdateCommandExecute(object p) => true;
        private void OnAddUpdateCommandExecuted(object p) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            bool incorrectInput = false;
            WarningText = String.Empty;
            if (!DateTime.TryParse(SolutionDateTextBox, out _)) {
                WarningText += LocRM.GetString("incorrectDate");
                incorrectInput = true;
            }
            if (!SolutionTime.TryParse(SolutionTimeTextBox)) {

                if (incorrectInput) {
                    WarningText += LocRM.GetString("incorrectTimePlus");
                }
                else {
                    WarningText += LocRM.GetString("incorrectTime");
                    incorrectInput = true;
                }
            }
            if (ScrambleTextBox != null) {
                string[] tempScrambleLetters = ScrambleTextBox.Split();
                for (int i = 0; i < tempScrambleLetters.Length; ++i) {
                    if (tempScrambleLetters[i] != "" && tempScrambleLetters[i] != " ") {
                        if (!Regex.IsMatch(tempScrambleLetters[i], MainWindowViewModel.ScrambleLetters[ComboBoxSelectedIndex])) {
                            if (incorrectInput) {
                                WarningText += LocRM.GetString("incorrectScramblePlus");
                            } else {
                                WarningText += LocRM.GetString("incorrectScramble");
                                incorrectInput = true;
                            }
                            break;
                        }
                    }
                }
            } else {
                if (incorrectInput) {
                    WarningText += LocRM.GetString("incorrectScramblePlus");
                } else {
                    WarningText += LocRM.GetString("incorrectScramble");
                    incorrectInput = true;
                }
            }
            if (!incorrectInput) {
                if (!addUpdate) {
                    int index = 1;
                    if (listOfSolutions[ComboBoxSelectedIndex].Count != 0) {
                        index = listOfSolutions[ComboBoxSelectedIndex].Select(x => x.Id).Max() + 1;
                    }
                    using (SolutionsDbContext context = new SolutionsDbContext()) {
                        context.Solutions.Add(
                            new Solution(index, PuzzleNameCollection[ComboBoxSelectedIndex], SolutionTimeTextBox,
                                Convert.ToDateTime(SolutionDateTextBox), ScrambleTextBox));
                        context.SaveChangesAsync();
                    }
                    listOfSolutions[ComboBoxSelectedIndex].Add(
                        new Solution(index, PuzzleNameCollection[ComboBoxSelectedIndex], SolutionTimeTextBox,
                            Convert.ToDateTime(SolutionDateTextBox), ScrambleTextBox));
                    Notify();
                    Close();
                }
                else {
                    using (SolutionsDbContext context = new SolutionsDbContext()) {
                        if (ComboBoxSelectedIndex != settings.CurrentPuzzle) {
                            int index = CurrentSolutionGrid.Id;
                            context.Solutions.Remove(context.Solutions
                                .Where(x => x.Id == CurrentSolutionGrid.Id
                                    && x.PuzzleName == MainWindowViewModel.PuzzleNameCollection[settings.CurrentPuzzle])
                                .ToList()[0]
                            );
                            context.SaveChangesAsync();
                            listOfSolutions[settings.CurrentPuzzle]
                                .Remove(listOfSolutions[settings.CurrentPuzzle]
                                .Where(x => x.Id == CurrentSolutionGrid.Id)
                                .ToList()[0]);
                            int n = context.Solutions
                                .Where(x => x.PuzzleName == PuzzleNameCollection[settings.CurrentPuzzle])
                                .Count();
                            for (int i = 0; i < n; ++i) {
                                Solution solution = context.Solutions
                                    .Where(x => x.PuzzleName == PuzzleNameCollection[settings.CurrentPuzzle])
                                    .ToList()[i];
                                if (solution.Id > index) {
                                    Solution temp = solution;
                                    context.Solutions.Remove(temp);
                                    context.SaveChangesAsync();
                                    listOfSolutions[settings.CurrentPuzzle].RemoveAt(i);
                                    temp.Id--;
                                    context.Solutions.Add(temp);
                                    context.SaveChangesAsync();
                                    listOfSolutions[settings.CurrentPuzzle].Insert(i, temp);
                                }
                            }

                            index = 1;
                            if (listOfSolutions[ComboBoxSelectedIndex].Count != 0) {
                                index = listOfSolutions[ComboBoxSelectedIndex].Select(x => x.Id).Max() + 1;
                            }

                            context.Solutions.Add(
                            new Solution(index, PuzzleNameCollection[ComboBoxSelectedIndex], SolutionTimeTextBox,
                                Convert.ToDateTime(SolutionDateTextBox), ScrambleTextBox));
                            context.SaveChangesAsync();

                            listOfSolutions[ComboBoxSelectedIndex].Add(
                                new Solution(index, PuzzleNameCollection[ComboBoxSelectedIndex], SolutionTimeTextBox,
                                    Convert.ToDateTime(SolutionDateTextBox), ScrambleTextBox));
                            Notify();
                        } else {
                            context.Solutions.Remove(context.Solutions
                                    .Where(x => x.Id == CurrentSolutionGrid.Id
                                        && x.PuzzleName == MainWindowViewModel.PuzzleNameCollection[settings.CurrentPuzzle])
                                    .ToList()[0]);
                            context.Solutions.Add(
                                    new Solution(
                                        CurrentSolutionGrid.Id,
                                        PuzzleNameCollection[ComboBoxSelectedIndex],
                                        SolutionTimeTextBox,
                                        Convert.ToDateTime(SolutionDateTextBox),
                                        ScrambleTextBox)
                                );

                            context.SaveChangesAsync();
                            int index = CurrentSolutionGrid.Id;
                            int insertIndex = listOfSolutions[settings.CurrentPuzzle].IndexOf(
                                listOfSolutions[settings.CurrentPuzzle]
                                .Where(x => x.Id == index)
                                .ToList()[0]);
                            listOfSolutions[settings.CurrentPuzzle].Remove(
                                listOfSolutions[settings.CurrentPuzzle]
                                .Where(x => x.Id == index)
                                .ToList()[0]);

                            listOfSolutions[settings.CurrentPuzzle].Insert(insertIndex, new Solution(
                                        index,
                                        PuzzleNameCollection[ComboBoxSelectedIndex],
                                        SolutionTimeTextBox,
                                        Convert.ToDateTime(SolutionDateTextBox),
                                        ScrambleTextBox
                                    ));
                            Notify();
                        }
                    }
					Close();
                }
            }
        }
        #endregion
        #region PuzzleTypeSelectionChangedCommand
        public ICommand PuzzleTypeSelectionChangedCommand { get; }
        #endregion
        #endregion
        public ItemViewViewModel(
                ObservableCollection<ObservableCollection<Solution>> listOfSolutions, 
                Settings settings, 
                Solution CurrentSolutionGrid, bool addUpdate = false) {
            this.settings = settings;
            this.listOfSolutions = listOfSolutions;
            this.CurrentSolutionGrid = CurrentSolutionGrid;
            this.addUpdate = addUpdate;
            AddUpdateCommand = new LambdaCommand(OnAddUpdateCommandExecuted, CanAddUpdateCommandExecute);
            PuzzleTypeSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(OnPuzzleTypeSelectionChangedCommand, null);
            UnfoldsVisibility = new ObservableCollection<Visibility>();
            for (int i = 0; i < 4; ++i) {
                UnfoldsVisibility.Add(Visibility.Collapsed);
            }
            if (addUpdate) {
                ComboBoxSelectedIndex = settings.CurrentPuzzle;
                Solution obj = listOfSolutions[settings.CurrentPuzzle]
                    .Where(x => x.Id == CurrentSolutionGrid.Id)
                    .ToList()[0];
                ComboBoxSelectedIndex = settings.CurrentPuzzle;
                SolutionDateTextBox = obj.SolutionDate.ToString();
                SolutionTimeTextBox = obj.SolutionTime.ToString();
                ScrambleTextBox = obj.Scramble;
            }
            else {
                SolutionDateTextBox = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                ComboBoxSelectedIndex = settings.CurrentPuzzle;
            }

            ChangeLanguage(addUpdate);
            InitializeUnfoldsAsync();
        }
        private async void InitializeUnfoldsAsync() {
            await Task.Run(() => {
                NByNCubeViewModelProperty = new NByNCubeViewModel(7);
                SkewbViewModelProperty = new SkewbViewModel();
                PyraminxViewModelProperty = new PyraminxViewModel();
                MegaminxViewModelProperty = new MegaminxViewModel();
                ControlHeight = 600;
                ControlWidth = 800;
                SetUnfoldsSize(ControlHeight - TopControlOffsetHeight - 20);
                PuzzleTypeSelectionChanged();
            });
        }
        private void OnPuzzleTypeSelectionChangedCommand(SelectionChangedEventArgs obj) {
            PuzzleTypeSelectionChanged();
        }
        private bool CheckIfScrambleIsNotValid() {
            if (ScrambleTextBox == null) return true;
            string[] tempScrambleLetters = ScrambleTextBox.Split();
            for (int i = 0; i < tempScrambleLetters.Length; ++i) {
                if (tempScrambleLetters[i] != "" && tempScrambleLetters[i] != " ") {
                    if (!Regex.IsMatch(tempScrambleLetters[i], MainWindowViewModel.ScrambleLetters[ComboBoxSelectedIndex])) {
                        return true;
                    }
                }
            }
            return false;
        }
        private void PuzzleTypeSelectionChanged() {
            for (int i = 0; i < UnfoldsVisibility.Count; ++i) {
                UnfoldsVisibility[i] = Visibility.Collapsed;
            }
            if (ComboBoxSelectedIndex >= 0 && ComboBoxSelectedIndex <= 5) {
                NByNCubeViewModelProperty = new NByNCubeViewModel(ComboBoxSelectedIndex + 2);
                SetUnfoldsSizeAsync(ControlHeight - TopControlOffsetHeight);
                if (!CheckIfScrambleIsNotValid()) {
                    NByNCubeViewModelProperty.SetUnfold(ScrambleTextBox);
                    UnfoldsVisibility[0] = Visibility.Visible;
                }
            } else if (ComboBoxSelectedIndex == 6) {
                SetUnfoldsSizeAsync(ControlHeight - TopControlOffsetHeight);
                if (!CheckIfScrambleIsNotValid()) {
                    SkewbViewModelProperty.SetUnfold(ScrambleTextBox);
                    UnfoldsVisibility[1] = Visibility.Visible;
                }
            } else if (ComboBoxSelectedIndex == 7) {
                SetUnfoldsSizeAsync(ControlHeight - TopControlOffsetHeight);
                if (!CheckIfScrambleIsNotValid()) {
                    PyraminxViewModelProperty.SetUnfold(ScrambleTextBox);
                    UnfoldsVisibility[2] = Visibility.Visible;
                }
            } else if (ComboBoxSelectedIndex == 8) {
                SetUnfoldsSizeAsync(ControlHeight - TopControlOffsetHeight);
                if (!CheckIfScrambleIsNotValid()) {
                    MegaminxViewModelProperty.SetUnfold(ScrambleTextBox);
                    UnfoldsVisibility[3] = Visibility.Visible;
                }
            }
        }
        private async void SetUnfoldsSizeAsync(double Height, double Width = 0) {
            await Task.Run(() => SetUnfoldsSize(Height, Width));
        }
        private void SetUnfoldsSize(double Height, double Width = 0) {
            double controlHeight = Height - 5;
            double heightToWidth = 1.25;
            double widthToHeight = 1;
            double controlHeightMegaminx = Height - 5;
            double heightToWidthMegaminx = 2;
            double widthToHeightMegaminx = 1;
            if (ControlWidth == controlHeight * heightToWidth) {
                return;
            } else if (ControlWidth != 0 && ControlWidth < controlHeight * heightToWidth) {
                heightToWidth = 1;
                widthToHeight = 0.75;
                controlHeight = ControlWidth - 5;
            }
            if (ControlWidth != 0 && ControlWidth < controlHeight * heightToWidth * 1.1) {
                controlHeightMegaminx = ControlWidth * 1.45;
                heightToWidthMegaminx = 1;
                widthToHeightMegaminx = 0.5;
            }
            NByNCubeViewModelProperty.ScaleX = controlHeight * heightToWidth / 64.0;
            NByNCubeViewModelProperty.ScaleY = controlHeight * widthToHeight / 62.0;
            NByNCubeViewModelProperty.ControlHeight = controlHeight * widthToHeight * 0.95;
            NByNCubeViewModelProperty.ControlWidth = controlHeight * heightToWidth * 0.95;

            SkewbViewModelProperty.ScaleX = controlHeight * heightToWidth / 51.5;
            SkewbViewModelProperty.ScaleY = controlHeight * widthToHeight / 42.0;
            SkewbViewModelProperty.ControlHeight = controlHeight * widthToHeight * 0.95;
            SkewbViewModelProperty.ControlWidth = controlHeight * heightToWidth * 0.95;

            PyraminxViewModelProperty.ScaleX = controlHeight * heightToWidth / 64.0;
            PyraminxViewModelProperty.ScaleY = controlHeight * widthToHeight / 62.0;
            PyraminxViewModelProperty.ControlHeight = controlHeight * widthToHeight * 0.9;
            PyraminxViewModelProperty.ControlWidth = controlHeight * heightToWidth * 0.9;

            MegaminxViewModelProperty.ScaleX = controlHeightMegaminx * heightToWidthMegaminx / 120.0;
            MegaminxViewModelProperty.ScaleY = controlHeightMegaminx * widthToHeightMegaminx / 45.0;
            MegaminxViewModelProperty.ControlHeight = controlHeightMegaminx * widthToHeightMegaminx;
            MegaminxViewModelProperty.ControlWidth = controlHeightMegaminx * heightToWidthMegaminx*0.45;
        }
        private void ChangeLanguage(bool addUpdate = false) {
            string strLanguage = "PuzzleTimer.Languages." + SettingsViewModel.Languages[settings.Language];
            ResourceManager LocRM = new ResourceManager(strLanguage, typeof(MainWindow).Assembly);
            DateToolTipText = LocRM.GetString("DateToolTipText");
            SolutionTimeTextBoxToolTipText = LocRM.GetString("SolutionTimeTextBoxToolTipText");
            PuzzleTypeText = LocRM.GetString("PuzzleType");
            SolutionDateText = LocRM.GetString("SolutionDate");
            SolutionTimeText = LocRM.GetString("SolutionTime");
            ScrambleText = LocRM.GetString("Scramble");
            if (addUpdate) {
                AddUpdateText = LocRM.GetString("Update");
            } else {
                AddUpdateText = LocRM.GetString("Add");
            }
        }
    }
}
