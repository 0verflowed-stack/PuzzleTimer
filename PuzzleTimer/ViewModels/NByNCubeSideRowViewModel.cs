using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class NByNCubeSideRowViewModel : ViewModel {
        ObservableCollection<Brush> _Colors;
        public ObservableCollection<Brush> Colors {
            get => _Colors;
            set => Set(ref _Colors, value);
        }

        ObservableCollection<Visibility> _RowsVisibility;
        public ObservableCollection<Visibility> RowsVisibility {
            get => _RowsVisibility;
            set => Set(ref _RowsVisibility, value);
        }
        #region Sizes
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
        int _Size;
        public int Size {
            get => _Size;
            set => Set(ref _Size, value);
        }
        #endregion
        #region Commands
        #region SizeChangedCommand
        public ICommand SizeChangedCommand { get; }
        #endregion
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
        public NByNCubeSideRowViewModel(Brush mainColor, int size, double h, double w) {
            Size = size;
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            Colors = new ObservableCollection<Brush>();
            for (int i = 0; i < 7; ++i) {
                Colors.Add(mainColor);
            }

            RowsVisibility = new ObservableCollection<Visibility>();
            RowsVisibility.Add(Visibility.Visible);
            for (int i = 1; i < 7; ++i) {
                RowsVisibility.Add(Visibility.Collapsed);
            }

            if (Size >= 2) {
                RowsVisibility[1] = Visibility.Visible;
            }
            if (Size >= 3) {
                RowsVisibility[2] = Visibility.Visible;
            }
            if (Size >= 4) {
                RowsVisibility[3] = Visibility.Visible;
            }
            if (Size >= 5) {
                RowsVisibility[4] = Visibility.Visible;
            }
            if (Size >= 6) {
                RowsVisibility[5] = Visibility.Visible;
            }
            if (Size == 7) {
                RowsVisibility[6] = Visibility.Visible;

            }
            ControlWidth = w;
            ControlHeight = h;
            ScaleX = (ControlWidth / (size * 5));
            ScaleY = (ControlHeight / 5.0);
        }

        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ScaleX = (obj.NewSize.Width / (Size * 5));
            ScaleY = (obj.NewSize.Height / 5);
            ControlWidth = obj.NewSize.Width;
            ControlHeight = obj.NewSize.Height;
        }
    }
}
