using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class SkwebViewModel : ViewModel {
        ObservableCollection<Brush> _ColorFirst;
        public ObservableCollection<Brush> ColorFirst {
            get => _ColorFirst;
            set => Set(ref _ColorFirst, value);
        }
        ObservableCollection<Brush> _ColorSecond;
        public ObservableCollection<Brush> ColorSecond {
            get => _ColorSecond;
            set => Set(ref _ColorSecond, value);
        }
        ObservableCollection<Brush> _ColorThird;
        public ObservableCollection<Brush> ColorThird {
            get => _ColorThird;
            set => Set(ref _ColorThird, value);
        }
        ObservableCollection<Brush> _ColorFourth;
        public ObservableCollection<Brush> ColorFourth {
            get => _ColorFourth;
            set => Set(ref _ColorFourth, value);
        }
        ObservableCollection<Brush> _ColorFifth;
        public ObservableCollection<Brush> ColorFifth {
            get => _ColorFifth;
            set => Set(ref _ColorFifth, value);
        }
        ObservableCollection<Brush> _ColorSixth;
        public ObservableCollection<Brush> ColorSixth {
            get => _ColorSixth;
            set => Set(ref _ColorSixth, value);
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
        #region Commands
        #region SizeChangedCommand
        public ICommand SizeChangedCommand { get; }
        #endregion
        #endregion
        public SkwebViewModel() {
            InitializeColors();
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            ControlWidth = 685;
            ControlHeight = 587;
            ScaleX = ControlWidth / 51.5;
            ScaleY = ControlHeight / 42;
        }
        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ScaleX = obj.NewSize.Width / 51.5;
            ScaleY = obj.NewSize.Height / 42;
            ControlWidth = obj.NewSize.Width;
            ControlHeight = obj.NewSize.Height;
        }
        private void InitializeColors() {
            ColorFirst = new ObservableCollection<Brush>();
            ColorSecond = new ObservableCollection<Brush>();
            ColorThird = new ObservableCollection<Brush>();
            ColorFourth = new ObservableCollection<Brush>();
            ColorFifth = new ObservableCollection<Brush>();
            ColorSixth = new ObservableCollection<Brush>();
            for (int i = 0; i < 5; ++i) {
                ColorFirst.Add(Brushes.Orange);
                ColorSecond.Add(Brushes.Green);
                ColorThird.Add(Brushes.Yellow);
                ColorFourth.Add(Brushes.Red);
                ColorFifth.Add(Brushes.Blue);
                ColorSixth.Add(Brushes.White);
            }
        }
        public void SetUnfold(string s) {
            ResetColors();
            string[] temp = s.Split();
            for (int i = 0; i < temp.Length; ++i) {
                Rotate(temp[i]);
            }
        }
        private void ResetColors() {
            for (int i = 0; i < 5; ++i) {
                ColorFirst[i] = Brushes.Orange;
                ColorSecond[i] = Brushes.Green;
                ColorThird[i] = Brushes.Yellow;
                ColorFourth[i] = Brushes.Red;
                ColorFifth[i] = Brushes.Blue;
                ColorSixth[i] = Brushes.White;
            }
        }
        private void Rotate(string letter) {
            if (letter == "" || letter == " ")
                return;
            int loop = 1;
            if (letter.Length == 2) {
                loop = 2;
            }
            if (letter[0] == 'R') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorFourth[0];
                    ColorFourth[0] = ColorThird[0];
                    ColorThird[0] = ColorFifth[1];
                    ColorFifth[1] = temp;
                    temp = ColorFourth[2];
                    ColorFourth[2] = ColorThird[2];
                    ColorThird[2] = ColorFifth[3];
                    ColorFifth[3] = temp;
                    temp = ColorFourth[3];
                    ColorFourth[3] = ColorThird[3];
                    ColorThird[3] = ColorFifth[0];
                    ColorFifth[0] = temp;
                    temp = ColorFourth[4];
                    ColorFourth[4] = ColorThird[4];
                    ColorThird[4] = ColorFifth[4];
                    ColorFifth[4] = temp;
                    temp = ColorSecond[3];
                    ColorSecond[3] = ColorFirst[0];
                    ColorFirst[0] = ColorSixth[3];
                    ColorSixth[3] = temp;
                }
            } else if (letter[0] == 'L') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorFirst[0];
                    ColorFirst[0] = ColorThird[2];
                    ColorThird[2] = ColorSecond[1];
                    ColorSecond[1] = temp;
                    temp = ColorFirst[2];
                    ColorFirst[2] = ColorThird[0];
                    ColorThird[0] = ColorSecond[3];
                    ColorSecond[3] = temp;
                    temp = ColorFirst[3];
                    ColorFirst[3] = ColorThird[1];
                    ColorThird[1] = ColorSecond[0];
                    ColorSecond[0] = temp;
                    temp = ColorFirst[4];
                    ColorFirst[4] = ColorThird[4];
                    ColorThird[4] = ColorSecond[4];
                    ColorSecond[4] = temp;
                    temp = ColorSixth[1];
                    ColorSixth[1] = ColorFifth[3];
                    ColorFifth[3] = ColorFourth[0];
                    ColorFourth[0] = temp;

                }
            } else if (letter[0] == 'U') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorSixth[1];
                    ColorSixth[1] = ColorFifth[1];
                    ColorFifth[1] = ColorFirst[0];
                    ColorFirst[0] = temp;
                    temp = ColorSixth[2];
                    ColorSixth[2] = ColorFifth[2];
                    ColorFifth[2] = ColorFirst[1];
                    ColorFirst[1] = temp;
                    temp = ColorSixth[3];
                    ColorSixth[3] = ColorFifth[3];
                    ColorFifth[3] = ColorFirst[2];
                    ColorFirst[2] = temp;
                    temp = ColorSixth[4];
                    ColorSixth[4] = ColorFifth[4];
                    ColorFifth[4] = ColorFirst[4];
                    ColorFirst[4] = temp;
                    temp = ColorSecond[1];
                    ColorSecond[1] = ColorFourth[2];
                    ColorFourth[2] = ColorThird[0];
                    ColorThird[0] = temp;

                }
            } else if (letter[0] == 'B') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorFirst[0];
                    ColorFirst[0] = ColorFifth[3];
                    ColorFifth[3] = ColorThird[0];
                    ColorThird[0] = temp;
                    temp = ColorFirst[1];
                    ColorFirst[1] = ColorFifth[0];
                    ColorFifth[0] = ColorThird[1];
                    ColorThird[1] = temp;
                    temp = ColorFirst[3];
                    ColorFirst[3] = ColorFifth[2];
                    ColorFifth[2] = ColorThird[3];
                    ColorThird[3] = temp;
                    temp = ColorFirst[4];
                    ColorFirst[4] = ColorFifth[4];
                    ColorFifth[4] = ColorThird[4];
                    ColorThird[4] = temp;
                    temp = ColorSecond[0];
                    ColorSecond[0] = ColorSixth[2];
                    ColorSixth[2] = ColorFourth[3];
                    ColorFourth[3] = temp;

                }
            }
        }
    }
}
