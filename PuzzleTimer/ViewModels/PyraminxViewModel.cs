using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class PyraminxViewModel : ViewModel {
        #region Brushes
        Brush _ColorFirst1;
        public Brush ColorFirst1 {
            get => _ColorFirst1;
            set => Set(ref _ColorFirst1, value);
        }
        Brush _ColorFirst2;
        public Brush ColorFirst2 {
            get => _ColorFirst2;
            set => Set(ref _ColorFirst2, value);
        }
        Brush _ColorFirst3;
        public Brush ColorFirst3 {
            get => _ColorFirst3;
            set => Set(ref _ColorFirst3, value);
        }
        Brush _ColorFirst4;
        public Brush ColorFirst4 {
            get => _ColorFirst4;
            set => Set(ref _ColorFirst4, value);
        }
        Brush _ColorFirst5;
        public Brush ColorFirst5 {
            get => _ColorFirst5;
            set => Set(ref _ColorFirst5, value);
        }
        Brush _ColorFirst6;
        public Brush ColorFirst6 {
            get => _ColorFirst6;
            set => Set(ref _ColorFirst6, value);
        }
        Brush _ColorFirst7;
        public Brush ColorFirst7 {
            get => _ColorFirst7;
            set => Set(ref _ColorFirst7, value);
        }
        Brush _ColorFirst8;
        public Brush ColorFirst8 {
            get => _ColorFirst8;
            set => Set(ref _ColorFirst8, value);
        }
        Brush _ColorFirst9;
        public Brush ColorFirst9 {
            get => _ColorFirst9;
            set => Set(ref _ColorFirst9, value);
        }
        //2
        Brush _ColorSecond1;
        public Brush ColorSecond1 {
            get => _ColorSecond1;
            set => Set(ref _ColorSecond1, value);
        }
        Brush _ColorSecond2;
        public Brush ColorSecond2 {
            get => _ColorSecond2;
            set => Set(ref _ColorSecond2, value);
        }
        Brush _ColorSecond3;
        public Brush ColorSecond3 {
            get => _ColorSecond3;
            set => Set(ref _ColorSecond3, value);
        }
        Brush _ColorSecond4;
        public Brush ColorSecond4 {
            get => _ColorSecond4;
            set => Set(ref _ColorSecond4, value);
        }
        Brush _ColorSecond5;
        public Brush ColorSecond5 {
            get => _ColorSecond5;
            set => Set(ref _ColorSecond5, value);
        }
        Brush _ColorSecond6;
        public Brush ColorSecond6 {
            get => _ColorSecond6;
            set => Set(ref _ColorSecond6, value);
        }
        Brush _ColorSecond7;
        public Brush ColorSecond7 {
            get => _ColorSecond7;
            set => Set(ref _ColorSecond7, value);
        }
        Brush _ColorSecond8;
        public Brush ColorSecond8 {
            get => _ColorSecond8;
            set => Set(ref _ColorSecond8, value);
        }
        Brush _ColorSecond9;
        public Brush ColorSecond9 {
            get => _ColorSecond9;
            set => Set(ref _ColorSecond9, value);
        }
        //3
        Brush _ColorThird1;
        public Brush ColorThird1 {
            get => _ColorThird1;
            set => Set(ref _ColorThird1, value);
        }
        Brush _ColorThird2;
        public Brush ColorThird2 {
            get => _ColorThird2;
            set => Set(ref _ColorThird2, value);
        }
        Brush _ColorThird3;
        public Brush ColorThird3 {
            get => _ColorThird3;
            set => Set(ref _ColorThird3, value);
        }
        Brush _ColorThird4;
        public Brush ColorThird4 {
            get => _ColorThird4;
            set => Set(ref _ColorThird4, value);
        }
        Brush _ColorThird5;
        public Brush ColorThird5 {
            get => _ColorThird5;
            set => Set(ref _ColorThird5, value);
        }
        Brush _ColorThird6;
        public Brush ColorThird6 {
            get => _ColorThird6;
            set => Set(ref _ColorThird6, value);
        }
        Brush _ColorThird7;
        public Brush ColorThird7 {
            get => _ColorThird7;
            set => Set(ref _ColorThird7, value);
        }
        Brush _ColorThird8;
        public Brush ColorThird8 {
            get => _ColorThird8;
            set => Set(ref _ColorThird8, value);
        }
        Brush _ColorThird9;
        public Brush ColorThird9 {
            get => _ColorThird9;
            set => Set(ref _ColorThird9, value);
        }
        //4
        Brush _ColorFourth1;
        public Brush ColorFourth1 {
            get => _ColorFourth1;
            set => Set(ref _ColorFourth1, value);
        }
        Brush _ColorFourth2;
        public Brush ColorFourth2 {
            get => _ColorFourth2;
            set => Set(ref _ColorFourth2, value);
        }
        Brush _ColorFourth3;
        public Brush ColorFourth3 {
            get => _ColorFourth3;
            set => Set(ref _ColorFourth3, value);
        }
        Brush _ColorFourth4;
        public Brush ColorFourth4 {
            get => _ColorFourth4;
            set => Set(ref _ColorFourth4, value);
        }
        Brush _ColorFourth5;
        public Brush ColorFourth5 {
            get => _ColorFourth5;
            set => Set(ref _ColorFourth5, value);
        }
        Brush _ColorFourth6;
        public Brush ColorFourth6 {
            get => _ColorFourth6;
            set => Set(ref _ColorFourth6, value);
        }
        Brush _ColorFourth7;
        public Brush ColorFourth7 {
            get => _ColorFourth7;
            set => Set(ref _ColorFourth7, value);
        }
        Brush _ColorFourth8;
        public Brush ColorFourth8 {
            get => _ColorFourth8;
            set => Set(ref _ColorFourth8, value);
        }
        Brush _ColorFourth9;
        public Brush ColorFourth9 {
            get => _ColorFourth9;
            set => Set(ref _ColorFourth9, value);
        }
        #endregion
        #region Points
        PointCollection _PointsFirst1;
        public PointCollection PointsFirst1 {
            get => _PointsFirst1;
            set => Set(ref _PointsFirst1, value);
        }
        PointCollection _PointsFirst2;
        public PointCollection PointsFirst2 {
            get => _PointsFirst2;
            set => Set(ref _PointsFirst2, value);
        }
        PointCollection _PointsFirst3;
        public PointCollection PointsFirst3 {
            get => _PointsFirst3;
            set => Set(ref _PointsFirst3, value);
        }
        PointCollection _PointsFirst4;
        public PointCollection PointsFirst4 {
            get => _PointsFirst4;
            set => Set(ref _PointsFirst4, value);
        }
        PointCollection _PointsFirst5;
        public PointCollection PointsFirst5 {
            get => _PointsFirst5;
            set => Set(ref _PointsFirst5, value);
        }
        PointCollection _PointsFirst6;
        public PointCollection PointsFirst6 {
            get => _PointsFirst6;
            set => Set(ref _PointsFirst6, value);
        }
        PointCollection _PointsFirst7;
        public PointCollection PointsFirst7 {
            get => _PointsFirst7;
            set => Set(ref _PointsFirst7, value);
        }
        PointCollection _PointsFirst8;
        public PointCollection PointsFirst8 {
            get => _PointsFirst8;
            set => Set(ref _PointsFirst8, value);
        }
        PointCollection _PointsFirst9;
        public PointCollection PointsFirst9 {
            get => _PointsFirst9;
            set => Set(ref _PointsFirst9, value);
        }
        //2
        PointCollection _PointsSecond1;
        public PointCollection PointsSecond1 {
            get => _PointsSecond1;
            set => Set(ref _PointsSecond1, value);
        }
        PointCollection _PointsSecond2;
        public PointCollection PointsSecond2 {
            get => _PointsSecond2;
            set => Set(ref _PointsSecond2, value);
        }
        PointCollection _PointsSecond3;
        public PointCollection PointsSecond3 {
            get => _PointsSecond3;
            set => Set(ref _PointsSecond3, value);
        }
        PointCollection _PointsSecond4;
        public PointCollection PointsSecond4 {
            get => _PointsSecond4;
            set => Set(ref _PointsSecond4, value);
        }
        PointCollection _PointsSecond5;
        public PointCollection PointsSecond5 {
            get => _PointsSecond5;
            set => Set(ref _PointsSecond5, value);
        }
        PointCollection _PointsSecond6;
        public PointCollection PointsSecond6 {
            get => _PointsSecond6;
            set => Set(ref _PointsSecond6, value);
        }
        PointCollection _PointsSecond7;
        public PointCollection PointsSecond7 {
            get => _PointsSecond7;
            set => Set(ref _PointsSecond7, value);
        }
        PointCollection _PointsSecond8;
        public PointCollection PointsSecond8 {
            get => _PointsSecond8;
            set => Set(ref _PointsSecond8, value);
        }
        PointCollection _PointsSecond9;
        public PointCollection PointsSecond9 {
            get => _PointsSecond9;
            set => Set(ref _PointsSecond9, value);
        }
        //3
        PointCollection _PointsThird1;
        public PointCollection PointsThird1 {
            get => _PointsThird1;
            set => Set(ref _PointsThird1, value);
        }
        PointCollection _PointsThird2;
        public PointCollection PointsThird2 {
            get => _PointsThird2;
            set => Set(ref _PointsThird2, value);
        }
        PointCollection _PointsThird3;
        public PointCollection PointsThird3 {
            get => _PointsThird3;
            set => Set(ref _PointsThird3, value);
        }
        PointCollection _PointsThird4;
        public PointCollection PointsThird4 {
            get => _PointsThird4;
            set => Set(ref _PointsThird4, value);
        }
        PointCollection _PointsThird5;
        public PointCollection PointsThird5 {
            get => _PointsThird5;
            set => Set(ref _PointsThird5, value);
        }
        PointCollection _PointsThird6;
        public PointCollection PointsThird6 {
            get => _PointsThird6;
            set => Set(ref _PointsThird6, value);
        }
        PointCollection _PointsThird7;
        public PointCollection PointsThird7 {
            get => _PointsThird7;
            set => Set(ref _PointsThird7, value);
        }
        PointCollection _PointsThird8;
        public PointCollection PointsThird8 {
            get => _PointsThird8;
            set => Set(ref _PointsThird8, value);
        }
        PointCollection _PointsThird9;
        public PointCollection PointsThird9 {
            get => _PointsThird9;
            set => Set(ref _PointsThird9, value);
        }
        //4
        PointCollection _PointsFourth1;
        public PointCollection PointsFourth1 {
            get => _PointsFourth1;
            set => Set(ref _PointsFourth1, value);
        }
        PointCollection _PointsFourth2;
        public PointCollection PointsFourth2 {
            get => _PointsFourth2;
            set => Set(ref _PointsFourth2, value);
        }
        PointCollection _PointsFourth3;
        public PointCollection PointsFourth3 {
            get => _PointsFourth3;
            set => Set(ref _PointsFourth3, value);
        }
        PointCollection _PointsFourth4;
        public PointCollection PointsFourth4 {
            get => _PointsFourth4;
            set => Set(ref _PointsFourth4, value);
        }
        PointCollection _PointsFourth5;
        public PointCollection PointsFourth5 {
            get => _PointsFourth5;
            set => Set(ref _PointsFourth5, value);
        }
        PointCollection _PointsFourth6;
        public PointCollection PointsFourth6 {
            get => _PointsFourth6;
            set => Set(ref _PointsFourth6, value);
        }
        PointCollection _PointsFourth7;
        public PointCollection PointsFourth7 {
            get => _PointsFourth7;
            set => Set(ref _PointsFourth7, value);
        }
        PointCollection _PointsFourth8;
        public PointCollection PointsFourth8 {
            get => _PointsFourth8;
            set => Set(ref _PointsFourth8, value);
        }
        PointCollection _PointsFourth9;
        public PointCollection PointsFourth9 {
            get => _PointsFourth9;
            set => Set(ref _PointsFourth9, value);
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
        #region Commands
        #region SizeChangedCommand
        public ICommand SizeChangedCommand { get; }
        #endregion
        #endregion
        public PyraminxViewModel() {
            InitializePoints();
            InitializeColors();
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            ControlWidth = 650;
            ControlHeight = 630;
            ScaleX = ControlWidth / 64.0;
            ScaleY = ControlHeight / 62.0;

        }

        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ScaleX = obj.NewSize.Width / 64.0;
            ScaleY = obj.NewSize.Height / 62.0;
            ControlWidth = obj.NewSize.Width;
            ControlHeight = obj.NewSize.Height;
        }

        private void InitializeColors() {
            ColorFirst1 = Brushes.Red;
            ColorFirst2 = Brushes.Red;
            ColorFirst3 = Brushes.Red;
            ColorFirst4 = Brushes.Red;
            ColorFirst5 = Brushes.Red;
            ColorFirst6 = Brushes.Red;
            ColorFirst7 = Brushes.Red;
            ColorFirst8 = Brushes.Red;
            ColorFirst9 = Brushes.Red;

            ColorSecond1 = Brushes.Green;
            ColorSecond2 = Brushes.Green;
            ColorSecond3 = Brushes.Green;
            ColorSecond4 = Brushes.Green;
            ColorSecond5 = Brushes.Green;
            ColorSecond6 = Brushes.Green;
            ColorSecond7 = Brushes.Green;
            ColorSecond8 = Brushes.Green;
            ColorSecond9 = Brushes.Green;

            ColorThird1 = Brushes.Blue;
            ColorThird2 = Brushes.Blue;
            ColorThird3 = Brushes.Blue;
            ColorThird4 = Brushes.Blue;
            ColorThird5 = Brushes.Blue;
            ColorThird6 = Brushes.Blue;
            ColorThird7 = Brushes.Blue;
            ColorThird8 = Brushes.Blue;
            ColorThird9 = Brushes.Blue;

            ColorFourth1 = Brushes.Yellow;
            ColorFourth2 = Brushes.Yellow;
            ColorFourth3 = Brushes.Yellow;
            ColorFourth4 = Brushes.Yellow;
            ColorFourth5 = Brushes.Yellow;
            ColorFourth6 = Brushes.Yellow;
            ColorFourth7 = Brushes.Yellow;
            ColorFourth8 = Brushes.Yellow;
            ColorFourth9 = Brushes.Yellow;
        }
        private void InitializePoints() {
            PointsFirst1 = new PointCollection(new[] { new Point(150, 300), new Point(200, 200), new Point(100, 200) });

            PointsFirst2 = new PointCollection(new[] { new Point(100, 200), new Point(150, 100), new Point(50, 100) });
            PointsFirst3 = new PointCollection(new[] { new Point(200, 200), new Point(150, 100), new Point(100, 200) });
            PointsFirst4 = new PointCollection(new[] { new Point(200, 200), new Point(250, 100), new Point(150, 100) });

            PointsFirst5 = new PointCollection(new[] { new Point(50, 100), new Point(100, 0), new Point(0, 0) });
            PointsFirst6 = new PointCollection(new[] { new Point(150, 100), new Point(100, 0), new Point(50, 100) });
            PointsFirst7 = new PointCollection(new[] { new Point(150, 100), new Point(200, 0), new Point(100, 0) });
            PointsFirst8 = new PointCollection(new[] { new Point(250, 100), new Point(200, 0), new Point(150, 100) });
            PointsFirst9 = new PointCollection(new[] { new Point(250, 100), new Point(300, 0), new Point(200, 0) });

            PointsSecond1 = new PointCollection(new[] { new Point(320, 0), new Point(370, 100), new Point(270, 100) });

            PointsSecond2 = new PointCollection(new[] { new Point(270, 100), new Point(320, 200), new Point(220, 200) });
            PointsSecond3 = new PointCollection(new[] { new Point(270, 100), new Point(320, 200), new Point(370, 100) });
            PointsSecond4 = new PointCollection(new[] { new Point(370, 100), new Point(420, 200), new Point(320, 200) });

            PointsSecond5 = new PointCollection(new[] { new Point(220, 200), new Point(270, 300), new Point(170, 300) });
            PointsSecond6 = new PointCollection(new[] { new Point(220, 200), new Point(270, 300), new Point(320, 200) });
            PointsSecond7 = new PointCollection(new[] { new Point(320, 200), new Point(370, 300), new Point(270, 300) });
            PointsSecond8 = new PointCollection(new[] { new Point(320, 200), new Point(370, 300), new Point(420, 200) });
            PointsSecond9 = new PointCollection(new[] { new Point(420, 200), new Point(470, 300), new Point(370, 300) });

            PointsThird1 = new PointCollection(new[] { new Point(490, 300), new Point(540, 200), new Point(440, 200) });

            PointsThird2 = new PointCollection(new[] { new Point(440, 200), new Point(490, 100), new Point(390, 100) });
            PointsThird3 = new PointCollection(new[] { new Point(540, 200), new Point(490, 100), new Point(440, 200) });
            PointsThird4 = new PointCollection(new[] { new Point(540, 200), new Point(590, 100), new Point(490, 100) });

            PointsThird5 = new PointCollection(new[] { new Point(390, 100), new Point(440, 0), new Point(340, 0) });
            PointsThird6 = new PointCollection(new[] { new Point(490, 100), new Point(440, 0), new Point(390, 100) });
            PointsThird7 = new PointCollection(new[] { new Point(490, 100), new Point(540, 0), new Point(440, 0) });
            PointsThird8 = new PointCollection(new[] { new Point(590, 100), new Point(540, 0), new Point(490, 100) });
            PointsThird9 = new PointCollection(new[] { new Point(590, 100), new Point(640, 0), new Point(540, 0) });

            PointsFourth1 = new PointCollection(new[] { new Point(320, 620), new Point(370, 520), new Point(270, 520) });

            PointsFourth2 = new PointCollection(new[] { new Point(270, 520), new Point(320, 420), new Point(220, 420) });
            PointsFourth3 = new PointCollection(new[] { new Point(370, 520), new Point(320, 420), new Point(270, 520) });
            PointsFourth4 = new PointCollection(new[] { new Point(370, 520), new Point(420, 420), new Point(320, 420) });

            PointsFourth5 = new PointCollection(new[] { new Point(220, 420), new Point(270, 320), new Point(170, 320) });
            PointsFourth6 = new PointCollection(new[] { new Point(320, 420), new Point(270, 320), new Point(220, 420) });
            PointsFourth7 = new PointCollection(new[] { new Point(320, 420), new Point(370, 320), new Point(270, 320) });
            PointsFourth8 = new PointCollection(new[] { new Point(420, 420), new Point(370, 320), new Point(320, 420) });
            PointsFourth9 = new PointCollection(new[] { new Point(420, 420), new Point(470, 320), new Point(370, 320) });
        }
        public void SetUnfold(string s) {
            InitializeColors();
            string[] temp = s.Split();
            for (int i = 0; i < temp.Length; ++i) {
                Rotate(temp[i]);
            }
        }
        void Rotate(string s) {
            if (s == "" || s == " ")
                return;
            int loop = 1;
            if (s.Length == 2) {
                loop = 2;
            }
            if (s.ToLower()[0] == 'r') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorThird1;
                    ColorThird1 = ColorSecond9;
                    ColorSecond9 = ColorFourth9;
                    ColorFourth9 = temp;
                    if (s[0] == 'R') {
                        Brush temp1 = ColorThird4;
                        Brush temp2 = ColorThird3;
                        Brush temp3 = ColorThird2;
                        ColorThird4 = ColorSecond4;
                        ColorThird3 = ColorSecond8;
                        ColorThird2 = ColorSecond7;

                        ColorSecond4 = ColorFourth7;
                        ColorSecond8 = ColorFourth8;
                        ColorSecond7 = ColorFourth4;

                        ColorFourth7 = temp1;
                        ColorFourth8 = temp2;
                        ColorFourth4 = temp3;
                    }
                }
            }
            else if (s.ToLower()[0] == 'l') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorFirst1;
                    ColorFirst1 = ColorFourth5;
                    ColorFourth5 = ColorSecond5;
                    ColorSecond5 = temp;
                    if (s[0] == 'L') {
                        Brush temp1 = ColorFirst4;
                        Brush temp2 = ColorFirst3;
                        Brush temp3 = ColorFirst2;
                        ColorFirst4 = ColorFourth2;
                        ColorFirst3 = ColorFourth6;
                        ColorFirst2 = ColorFourth7;

                        ColorFourth2 = ColorSecond7;
                        ColorFourth6 = ColorSecond6;
                        ColorFourth7 = ColorSecond2;

                        ColorSecond7 = temp1;
                        ColorSecond6 = temp2;
                        ColorSecond2 = temp3;
                    }
                }
            }
            else if (s.ToLower()[0] == 'u') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorFirst9;
                    ColorFirst9 = ColorSecond1;
                    ColorSecond1 = ColorThird5;
                    ColorThird5 = temp;
                    if (s[0] == 'U') {
                        Brush temp1 = ColorFirst7;
                        Brush temp2 = ColorFirst8;
                        Brush temp3 = ColorFirst4;
                        ColorFirst7 = ColorSecond2;
                        ColorFirst8 = ColorSecond3;
                        ColorFirst4 = ColorSecond4;

                        ColorSecond2 = ColorThird2;
                        ColorSecond3 = ColorThird6;
                        ColorSecond4 = ColorThird7;

                        ColorThird2 = temp1;
                        ColorThird6 = temp2;
                        ColorThird7 = temp3;
                    }
                }
            }
            else if (s.ToLower()[0] == 'b') {
                for (int i = 0; i < loop; ++i) {
                    Brush temp = ColorThird9;
                    ColorThird9 = ColorFourth1;
                    ColorFourth1 = ColorFirst5;
                    ColorFirst5 = temp;
                    if (s[0] == 'B') {
                        Brush temp1 = ColorFirst2;
                        Brush temp2 = ColorFirst6;
                        Brush temp3 = ColorFirst7;
                        ColorFirst2 = ColorThird7;
                        ColorFirst6 = ColorThird8;
                        ColorFirst7 = ColorThird4;

                        ColorThird7 = ColorFourth4;
                        ColorThird8 = ColorFourth3;
                        ColorThird4 = ColorFourth2;

                        ColorFourth4 = temp1;
                        ColorFourth3 = temp2;
                        ColorFourth2 = temp3;
                    }
                }
            }
        }
    }
}
