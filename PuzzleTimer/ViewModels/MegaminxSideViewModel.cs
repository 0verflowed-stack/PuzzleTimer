using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class MegaminxSideViewModel : ViewModel {
        #region Brushes
        ObservableCollection<Brush> _Color;
        public ObservableCollection<Brush> Color {
            get => _Color;
            set => Set(ref _Color, value);
        }
        #endregion
        #region TranslateTransform Points
        Point _Point1;
        public Point Point1 {
            get => _Point1;
            set => Set(ref _Point1, value);
        }
        Point _Point2;
        public Point Point2 {
            get => _Point2;
            set => Set(ref _Point2, value);
        }
        Point _Point3;
        public Point Point3 {
            get => _Point3;
            set => Set(ref _Point3, value);
        }
        Point _Point4;
        public Point Point4 {
            get => _Point4;
            set => Set(ref _Point4, value);
        }
        Point _Point5;
        public Point Point5 {
            get => _Point5;
            set => Set(ref _Point5, value);
        }
        Point _Point6;
        public Point Point6 {
            get => _Point6;
            set => Set(ref _Point6, value);
        }
        Point _Point7;
        public Point Point7 {
            get => _Point7;
            set => Set(ref _Point7, value);
        }
        Point _Point8;
        public Point Point8 {
            get => _Point8;
            set => Set(ref _Point8, value);
        }
        Point _Point9;
        public Point Point9 {
            get => _Point9;
            set => Set(ref _Point9, value);
        }
        Point _Point10;
        public Point Point10 {
            get => _Point10;
            set => Set(ref _Point10, value);
        }
        Point _Point11;
        public Point Point11 {
            get => _Point11;
            set => Set(ref _Point11, value);
        }
        Point _Point12;
        public Point Point12 {
            get => _Point12;
            set => Set(ref _Point12, value);
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
        public MegaminxSideViewModel(Brush brush) {
            Color = new ObservableCollection<Brush>();
            for (int i = 0; i < 11; ++i) {
                Color.Add(brush);
            }
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            ScaleX = 5.85;
            ScaleY = 6.3;
            TranslateTransformPointsInitialize();
        }
        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ScaleX = obj.NewSize.Width / 120;
            ScaleY = obj.NewSize.Height / 45;
            ControlWidth = obj.NewSize.Width;
            ControlHeight = obj.NewSize.Height;
            TranslateTransformPointsInitialize();
        }
        private void TranslateTransformPointsInitialize() {

            Point12 = new Point(41.4 * ScaleX, 14.5 * ScaleY);
            Point1 = new Point(0.0 * ScaleX, 0.0 * ScaleY);
            Point2 = new Point(4.8 * ScaleX, -3.5 * ScaleY);
            Point3 = new Point(-14.2 * ScaleX, -3.9 * ScaleY);
            Point4 = new Point(-6.2 * ScaleX, 0.1 * ScaleY);
            Point5 = new Point(-5 * ScaleX, 25.8 * ScaleY);
            Point6 = new Point(10.5 * ScaleX, 14.5 * ScaleY);
            Point7 = new Point(30.8 * ScaleX, 0 * ScaleY);
            Point8 = new Point(36.5 * ScaleX, 18 * ScaleY);
            Point9 = new Point(55.6 * ScaleX, 18.2 * ScaleY);
            Point10 = new Point(61.5 * ScaleX, 0 * ScaleY);
            Point11 = new Point(46.2 * ScaleX, -11 * ScaleY);
        }
        public void RotateSideClockWise() {
            for (int i = 0; i < 2; ++i) {
                Brush temp = Color[0 + i];
                Color[0 + i] = Color[8 + i];
                Color[8 + i] = Color[6 + i];
                Color[6 + i] = Color[4 + i];
                Color[4 + i] = Color[2 + i];
                Color[2 + i] = temp;
            }
        }
    }
}
