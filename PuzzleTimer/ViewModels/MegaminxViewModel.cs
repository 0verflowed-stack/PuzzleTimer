using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class MegaminxViewModel : ViewModel {
        #region DataContexts

        MegaminxSideViewModel _Side1;
        public MegaminxSideViewModel Side1 {
            get => _Side1;
            set => Set(ref _Side1, value);
        }
        MegaminxSideUpViewModel _Side2;
        public MegaminxSideUpViewModel Side2 {
            get => _Side2;
            set => Set(ref _Side2, value);
        }
        MegaminxSideUpViewModel _Side3;
        public MegaminxSideUpViewModel Side3 {
            get => _Side3;
            set => Set(ref _Side3, value);
        }
        MegaminxSideUpViewModel _Side4;
        public MegaminxSideUpViewModel Side4 {
            get => _Side4;
            set => Set(ref _Side4, value);
        }
        MegaminxSideUpViewModel _Side5;
        public MegaminxSideUpViewModel Side5 {
            get => _Side5;
            set => Set(ref _Side5, value);
        }
        MegaminxSideUpViewModel _Side6;
        public MegaminxSideUpViewModel Side6 {
            get => _Side6;
            set => Set(ref _Side6, value);
        }
        MegaminxSideViewModel _Side7;
        public MegaminxSideViewModel Side7 {
            get => _Side7;
            set => Set(ref _Side7, value);
        }
        MegaminxSideViewModel _Side8;
        public MegaminxSideViewModel Side8 {
            get => _Side8;
            set => Set(ref _Side8, value);
        }
        MegaminxSideViewModel _Side9;
        public MegaminxSideViewModel Side9 {
            get => _Side9;
            set => Set(ref _Side9, value);
        }
        MegaminxSideViewModel _Side10;
        public MegaminxSideViewModel Side10 {
            get => _Side10;
            set => Set(ref _Side10, value);
        }
        MegaminxSideViewModel _Side11;
        public MegaminxSideViewModel Side11 {
            get => _Side11;
            set => Set(ref _Side11, value);
        }
        MegaminxSideUpViewModel _Side12;
        public MegaminxSideUpViewModel Side12 {
            get => _Side12;
            set => Set(ref _Side12, value);
        }
        #endregion

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
        double _ScaleX;
        public double ScaleX {
            get => _ScaleX;
            set => Set(ref _ScaleX, value);
        }
        double _ScaleY;
        public double ScaleY {
            get => _ScaleY;
            set => Set(ref _ScaleY, value);
        }
        Thickness _MarginGrid;
        public Thickness MarginGrid {
            get => _MarginGrid;
            set => Set(ref _MarginGrid, value);
        }
        #region Commands
        #region SizeChangedCommand
        public ICommand SizeChangedCommand { get; }
        #endregion
        #endregion
        public MegaminxViewModel() {
            InitializeColor();
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            ControlWidth = 685;
            ControlHeight = 587;
            ScaleX = ControlWidth / 120;
            ScaleY = ControlHeight / 45;
            MarginGrid = new Thickness(21 * ScaleX, 16 * ScaleY, 0, 0);
        }

        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ScaleX = obj.NewSize.Width / 120;
            ScaleY = obj.NewSize.Height / 45;
            ControlWidth = obj.NewSize.Width;
            ControlHeight = obj.NewSize.Height;
            MarginGrid = new Thickness(21 * ScaleX, 16 * ScaleY, 0, 0);
        }

        private void InitializeColor() {
            Side1 = new MegaminxSideViewModel(Brushes.White);
            Side2 = new MegaminxSideUpViewModel(Brushes.DarkBlue);
            Side3 = new MegaminxSideUpViewModel(Brushes.Yellow);
            Side4 = new MegaminxSideUpViewModel(Brushes.Purple);
            Side5 = new MegaminxSideUpViewModel(Brushes.DarkGreen);
            Side6 = new MegaminxSideUpViewModel(Brushes.Red);
            Side7 = new MegaminxSideViewModel(Brushes.Pink);
            Side8 = new MegaminxSideViewModel(Brushes.LightYellow);
            Side9 = new MegaminxSideViewModel(Brushes.LightBlue);
            Side10 = new MegaminxSideViewModel(Brushes.Orange);
            Side11 = new MegaminxSideViewModel(Brushes.LightGreen);
            Side12 = new MegaminxSideUpViewModel(Brushes.Gray);
        }
        public void SetUnfold(string s) {
            ResetColors();
            string[] letters = s.Split();
            for (int i = 0; i < letters.Length; ++i) {
                Rotate(letters[i]);
            }
        }
        private void ResetColors() {
            Side1.Color.Clear();
            Side2.Color.Clear();
            Side3.Color.Clear();
            Side4.Color.Clear();
            Side5.Color.Clear();
            Side6.Color.Clear();
            Side7.Color.Clear();
            Side8.Color.Clear();
            Side9.Color.Clear();
            Side10.Color.Clear();
            Side11.Color.Clear();
            Side12.Color.Clear();
            for (int i = 0; i < 11; ++i) {
                Side1.Color.Add(Brushes.White);
                Side2.Color.Add(Brushes.DarkBlue);
                Side3.Color.Add(Brushes.Yellow);
                Side4.Color.Add(Brushes.Purple);
                Side5.Color.Add(Brushes.DarkGreen);
                Side6.Color.Add(Brushes.Red);
                Side7.Color.Add(Brushes.Pink);
                Side8.Color.Add(Brushes.LightYellow);
                Side9.Color.Add(Brushes.LightBlue);
                Side10.Color.Add(Brushes.Orange);
                Side11.Color.Add(Brushes.LightGreen);
                Side12.Color.Add(Brushes.Gray);
            }
        }
        private void Rotate(string letter) {
            if (letter == "" || letter == " ")
                return;
            int loop = 1;
            if (letter.Length == 2 && letter[1] == '\'') {
                loop = 4;
            }
            if (letter.Length == 3 && letter[1] == '+') {
                loop = 2;
            }
            if (letter.Length == 3 && letter[1] == '-') {
                loop = 3;
            }
            if (letter[0] == 'U') {
                for (int j = 0; j < loop; ++j) {
                    for (int i = 0; i < 3; ++i) {
                        Brush temp = Side2.Color[0 + i];
                        Side2.Color[0 + i] = Side3.Color[2 + i];
                        Side3.Color[2 + i] = Side4.Color[4 + i];
                        Side4.Color[4 + i] = Side5.Color[6 + i];
                        if (i != 2) {
                            Side5.Color[6 + i] = Side6.Color[8 + i];
                            Side6.Color[8 + i] = temp;
                        } else {
                            Side5.Color[6 + i] = Side6.Color[0];
                            Side6.Color[0] = temp;
                        }
                    }
                    Side1.RotateSideClockWise();
                }
            } else if (letter[0] == 'R') {
                Brush temp;
                for (int j = 0; j < loop; ++j) {
                    for (int i = 0; i < 10; ++i) {
                        temp = Side2.Color[i];
                        Side2.Color[i] = Side6.Color[i];
                        if (i > 6) {
                            Side6.Color[i] = Side8.Color[9 - (i - 7)];
                            Side8.Color[9 - (i - 7)] = Side12.Color[i - 6];
                            Side12.Color[i - 6] = Side11.Color[9 - (i - 5)];
                            Side11.Color[9 - (i - 5)] = temp;
                        } else {
                            Side6.Color[i] = Side8.Color[6 - i];
                            if (i > 5) {
                                Side8.Color[6 - i] = Side12.Color[i - 6];
                                Side12.Color[i - 6] = Side11.Color[9 - (i - 5)];
                                Side11.Color[9 - (i - 5)] = temp;
                            } else {
                                Side8.Color[6 - i] = Side12.Color[4 + i];
                                if (i > 4) {
                                    Side12.Color[4 + i] = Side11.Color[9 - (i - 5)];
                                    Side11.Color[9 - (i - 5)] = temp;
                                } else {
                                    Side12.Color[4 + i] = Side11.Color[4 - i];
                                    Side11.Color[4 - i] = temp;
                                }
                            }
                        }
                    }
                    for (int i = 0; i < 7; ++i) {
                        temp = Side3.Color[i + 3];
                        Side3.Color[i + 3] = Side1.Color[7 - i];
                        Side1.Color[7 - i] = Side5.Color[1 + i];
                        if (i > 3) {
                            Side5.Color[1 + i] = Side9.Color[9 + 4 - i];
                            Side9.Color[9 + 4 - i] = Side10.Color[9 + 4 - i];
                            Side10.Color[9 + 4 - i] = temp;
                        } else {
                            Side5.Color[1 + i] = Side9.Color[3 - i];
                            Side9.Color[3 - i] = Side10.Color[3 - i];
                            Side10.Color[3 - i] = temp;
                        }
                    }
                    temp = Side3.Color[10];
                    Side3.Color[10] = Side1.Color[10];
                    Side1.Color[10] = Side5.Color[10];
                    Side5.Color[10] = Side9.Color[10];
                    Side9.Color[10] = Side10.Color[10];
                    Side10.Color[10] = temp;

                    Side7.RotateSideClockWise();
                    temp = Side2.Color[10];
                    Side2.Color[10] = Side6.Color[10];
                    Side6.Color[10] = Side8.Color[10];
                    Side8.Color[10] = Side12.Color[10];
                    Side12.Color[10] = Side11.Color[10];
                    Side11.Color[10] = temp;
                }
            } else if (letter[0] == 'D') {
                Brush temp;
                for (int j = 0; j < loop; ++j) {
                    for (int i = 0; i < 10; ++i) {
                        temp = Side7.Color[i];
                        if (i > 1) {
                            Side7.Color[i] = Side8.Color[i - 2];
                            if (i > 3) {
                                Side8.Color[i - 2] = Side9.Color[i - 4];
                                if (i > 5) {
                                    Side9.Color[i - 4] = Side10.Color[i - 6];
                                    if (i > 7) {
                                        Side10.Color[i - 6] = Side11.Color[i - 8];
                                        Side11.Color[i - 8] = temp;
                                    } else {
                                        Side10.Color[i - 6] = Side11.Color[i + 2];
                                        Side11.Color[i + 2] = temp;
                                    }
                                } else {
                                    Side9.Color[i - 4] = Side10.Color[i + 4];
                                    Side10.Color[i + 4] = Side11.Color[i + 2];
                                    Side11.Color[i + 2] = temp;
                                }
                            } else {
                                Side8.Color[i - 2] = Side9.Color[6 + i];
                                Side9.Color[6 + i] = Side10.Color[4 + i];
                                Side10.Color[4 + i] = Side11.Color[2 + i];
                                Side11.Color[2 + i] = temp;
                            }
                        } else {
                            Side7.Color[i] = Side8.Color[8 + i];
                            Side8.Color[8 + i] = Side9.Color[6 + i];
                            Side9.Color[6 + i] = Side10.Color[4 + i];
                            Side10.Color[4 + i] = Side11.Color[2 + i];
                            Side11.Color[2 + i] = temp;
                        }
                    }
                    temp = Side7.Color[10];
                    Side7.Color[10] = Side8.Color[10];
                    Side8.Color[10] = Side9.Color[10];
                    Side9.Color[10] = Side10.Color[10];
                    Side10.Color[10] = Side11.Color[10];
                    Side11.Color[10] = temp;

                    for (int i = 0; i < 7; ++i) {
                        temp = Side2.Color[9 - i];
                        Side2.Color[9 - i] = Side6.Color[7 - i];
                        if (i > 5) {
                            Side6.Color[7 - i] = Side5.Color[9 - (i - 6)];
                            Side5.Color[9 - (i - 6)] = Side4.Color[9 - (i - 4)];
                            Side4.Color[9 - (i - 4)] = Side3.Color[7 - (i - 4)];
                            Side3.Color[7 - (i - 4)] = temp;
                        } else {
                            Side6.Color[7 - i] = Side5.Color[5 - i];
                            if (i > 3) {
                                Side5.Color[5 - i] = Side4.Color[9 - (i - 4)];
                                if (i > 4) {
                                    Side4.Color[9 - (i - 4)] = Side3.Color[7 - (i - 4)];
                                    Side3.Color[7 - (i - 4)] = temp;
                                } else {
                                    Side4.Color[9 - (i - 4)] = Side3.Color[7 - (i - 4)];
                                    Side3.Color[7 - (i - 4)] = temp;
                                }
                            } else {
                                Side5.Color[5 - i] = Side4.Color[3 - i];
                                if (i > 1) {
                                    Side4.Color[3 - i] = Side3.Color[9 - (i - 2)];
                                    Side3.Color[9 - (i - 2)] = temp;
                                } else {
                                    Side4.Color[3 - i] = Side3.Color[1 - i];
                                    Side3.Color[1 - i] = temp;
                                }
                            }
                        }
                    }
                    temp = Side2.Color[10];
                    Side2.Color[10] = Side6.Color[10];
                    Side6.Color[10] = Side5.Color[10];
                    Side5.Color[10] = Side4.Color[10];
                    Side4.Color[10] = Side3.Color[10];
                    Side3.Color[10] = temp;
                    Side12.RotateSideClockWise();
                }
            }
        }

    }
}
