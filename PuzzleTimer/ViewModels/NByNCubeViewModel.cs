using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class NByNCubeViewModel : ViewModel {
        int size;
        NByNCubeSideViewModel _WhiteSide;
        public NByNCubeSideViewModel WhiteSide {
            get => _WhiteSide;
            set => Set(ref _WhiteSide, value);
        }
        NByNCubeSideViewModel _OrangeSide;
        public NByNCubeSideViewModel OrangeSide {
            get => _OrangeSide;
            set => Set(ref _OrangeSide, value);
        }
        NByNCubeSideViewModel _GreenSide;
        public NByNCubeSideViewModel GreenSide {
            get => _GreenSide;
            set => Set(ref _GreenSide, value);
        }
        NByNCubeSideViewModel _RedSide;
        public NByNCubeSideViewModel RedSide {
            get => _RedSide;
            set => Set(ref _RedSide, value);
        }
        NByNCubeSideViewModel _BlueSide;
        public NByNCubeSideViewModel BlueSide {
            get => _BlueSide;
            set => Set(ref _BlueSide, value);
        }
        NByNCubeSideViewModel _YellowSide;
        public NByNCubeSideViewModel YellowSide {
            get => _YellowSide;
            set => Set(ref _YellowSide, value);
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
        Thickness _Margin;
        public Thickness Margin {
            get => _Margin;
            set => Set(ref _Margin, value);
        }
        #region Commands
        #region SizeChangedCommand
        public ICommand SizeChangedCommand { get; }
        #endregion
        #endregion
        public NByNCubeViewModel(int size) {
            this.size = size;
            SetChangedSize(650, 630);
            Margin = new Thickness(size * 5, 0, 0, 0);
            WhiteSide = new NByNCubeSideViewModel(Brushes.White, size);
            OrangeSide = new NByNCubeSideViewModel(Brushes.Orange, size);
            GreenSide = new NByNCubeSideViewModel(Brushes.Green, size);
            RedSide = new NByNCubeSideViewModel(Brushes.Red, size);
            BlueSide = new NByNCubeSideViewModel(Brushes.Blue, size);
            YellowSide = new NByNCubeSideViewModel(Brushes.Yellow, size);
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
        }
        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            SetChangedSize(obj.NewSize.Width, obj.NewSize.Height);
        }
        private void SetChangedSize(double width, double height) {
            ScaleX = width / (size * 5 * 4);
            ScaleY = height / (size * 5 * 3);
            ControlWidth = width;
            ControlHeight = height;
        }
        private void ResetColors() {
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j) {
                    WhiteSide.Rows[i].Colors[j] = Brushes.White;
                    OrangeSide.Rows[i].Colors[j] = Brushes.Orange;
                    GreenSide.Rows[i].Colors[j] = Brushes.Green;
                    RedSide.Rows[i].Colors[j] = Brushes.Red;
                    BlueSide.Rows[i].Colors[j] = Brushes.Blue;
                    YellowSide.Rows[i].Colors[j] = Brushes.Yellow;
                }
            }
        }
        public void SetUnfold(string letters) {
            ResetColors();
            string[] letter = letters.Split();
            for (int t = 0; t < letter.Length; ++t) {
                if (letter[t] == "" || letter[t] == " ")
                    continue;
                int offset = 0;
                int loops = 1;
                if (letter[t].Length == 2) {
                    if (letter[t][1] == '2') {
                        loops = 2;
                    }
                    else if (letter[t][1] == '\'') {
                        loops = 3;
                    }
                    else if (letter[t][1] == 'w') {
                        offset = 1;
                    }
                }
                else if (letter[t].Length == 3) {
                    if (Char.IsLetter(letter[t][0])) {
                        if (letter[t][2] == '\'') {
                            offset = 1;
                            loops = 3;
                        }
                        else if (letter[t][2] == '2') {
                            offset = 1;
                            loops = 2;
                        }
                    }
                    else {
                        offset = Int32.Parse(letter[t][0].ToString()) - 1;
                    }
                }
                else if (letter[t].Length == 4) {
                    if (letter[t][3] == '2') {
                        loops = 2;
                    }
                    else if (letter[t][3] == '\'') {
                        loops = 3;
                    }
                    offset = Int32.Parse(letter[t][0].ToString()) - 1;
                }
                if (letter[t][0] == 'R' || (letter[t].Length > 1 && letter[t][1] == 'R')) {
                    for (int j = 0; j < loops; ++j) {
                        for (int k = 0; k < offset + 1; ++k) {
                            for (int i = 0; i < size; ++i) {
                                Brush temp = WhiteSide.Rows[i].Colors[size - 1 - k];
                                WhiteSide.Rows[i].Colors[size - 1 - k] = GreenSide.Rows[i].Colors[size - 1 - k];
                                GreenSide.Rows[i].Colors[size - 1 - k] = YellowSide.Rows[i].Colors[size - 1 - k];
                                YellowSide.Rows[i].Colors[size - 1 - k] = BlueSide.Rows[size - 1 - i].Colors[k];
                                BlueSide.Rows[size - 1 - i].Colors[k] = temp;
                            }
                        }
                        RedSide.Rotate90DegClockwise();
                    }
                }
                else if (letter[t][0] == 'L' || (letter[t].Length > 1 && letter[t][1] == 'L')) {
                    for (int j = 0; j < loops; ++j) {
                        for (int k = 0; k < offset + 1; ++k) {
                            for (int i = 0; i < size; ++i) {
                                Brush temp = WhiteSide.Rows[i].Colors[k];
                                WhiteSide.Rows[i].Colors[k] = BlueSide.Rows[size - 1 - i].Colors[size - 1 - k];
                                BlueSide.Rows[size - 1 - i].Colors[size - 1 - k] = YellowSide.Rows[i].Colors[k];
                                YellowSide.Rows[i].Colors[k] = GreenSide.Rows[i].Colors[k];
                                GreenSide.Rows[i].Colors[k] = temp;
                            }
                        }
                        OrangeSide.Rotate90DegClockwise();
                    }
                }
                else if (letter[t][0] == 'U' || (letter[t].Length > 1 && letter[t][1] == 'U')) {
                    for (int j = 0; j < loops; ++j) {
                        for (int k = 0; k < offset + 1; ++k) {
                            for (int i = 0; i < size; ++i) {
                                Brush temp = OrangeSide.Rows[k].Colors[i];
                                OrangeSide.Rows[k].Colors[i] = GreenSide.Rows[k].Colors[i];
                                GreenSide.Rows[k].Colors[i] = RedSide.Rows[k].Colors[i];
                                RedSide.Rows[k].Colors[i] = BlueSide.Rows[k].Colors[i];
                                BlueSide.Rows[k].Colors[i] = temp;
                            }
                        }
                        WhiteSide.Rotate90DegClockwise();
                    }
                }
                else if (letter[t][0] == 'D' || (letter[t].Length > 1 && letter[t][1] == 'D')) {
                    for (int j = 0; j < loops; ++j) {
                        for (int k = 0; k < offset + 1; ++k) {
                            for (int i = 0; i < size; ++i) {
                                Brush temp = OrangeSide.Rows[size - 1 - k].Colors[i];
                                OrangeSide.Rows[size - 1 - k].Colors[i] = BlueSide.Rows[size - 1 - k].Colors[i];
                                BlueSide.Rows[size - 1 - k].Colors[i] = RedSide.Rows[size - 1 - k].Colors[i];
                                RedSide.Rows[size - 1 - k].Colors[i] = GreenSide.Rows[size - 1 - k].Colors[i];
                                GreenSide.Rows[size - 1 - k].Colors[i] = temp;
                            }
                        }
                        YellowSide.Rotate90DegClockwise();
                    }
                }
                else if (letter[t][0] == 'F' || (letter[t].Length > 1 && letter[t][1] == 'F')) {
                    for (int j = 0; j < loops; ++j) {
                        for (int k = 0; k < offset + 1; ++k) {
                            for (int i = 0; i < size; ++i) {
                                Brush temp = OrangeSide.Rows[i].Colors[size - 1 - k];
                                OrangeSide.Rows[i].Colors[size - 1 - k] = YellowSide.Rows[k].Colors[i];
                                YellowSide.Rows[k].Colors[i] = RedSide.Rows[size - 1 - i].Colors[k];
                                RedSide.Rows[size - 1 - i].Colors[k] = WhiteSide.Rows[size - 1 - k].Colors[size - 1 - i];
                                WhiteSide.Rows[size - 1 - k].Colors[size - 1 - i] = temp;
                            }
                        }
                        GreenSide.Rotate90DegClockwise();
                    }
                }
                else if (letter[t][0] == 'B' || (letter[t].Length > 1 && letter[t][1] == 'B')) {
                    for (int j = 0; j < loops; ++j) {
                        for (int k = 0; k < offset + 1; ++k) {
                            for (int i = 0; i < size; ++i) {
                                Brush temp = OrangeSide.Rows[i].Colors[k];
                                OrangeSide.Rows[i].Colors[k] = WhiteSide.Rows[k].Colors[size - 1 - i];
                                WhiteSide.Rows[k].Colors[size - 1 - i] = RedSide.Rows[size - 1 - i].Colors[size - 1 - k];
                                RedSide.Rows[size - 1 - i].Colors[size - 1 - k] = YellowSide.Rows[size - 1 - k].Colors[i];
                                YellowSide.Rows[size - 1 - k].Colors[i] = temp;
                            }
                        }
                        BlueSide.Rotate90DegClockwise();
                    }
                }
            }
        }
    }
}
