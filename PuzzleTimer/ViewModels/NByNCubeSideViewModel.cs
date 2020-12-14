using PuzzleTimer.Infrustructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PuzzleTimer.ViewModels {
    class NByNCubeSideViewModel : ViewModel {
        ObservableCollection<NByNCubeSideRowViewModel> _Rows;
        public ObservableCollection<NByNCubeSideRowViewModel> Rows {
            get => _Rows;
            set => Set(ref _Rows, value);
        }

        ObservableCollection<Visibility> _RowsVisibility;
        public ObservableCollection<Visibility> RowsVisibility {
            get => _RowsVisibility;
            set => Set(ref _RowsVisibility, value);
        }

        #region Sizes
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
        public NByNCubeSideViewModel(Brush mainColor, int size) {
            this.Size = size;
            #region Commands
            SizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(OnSizeChangedCommand, null);
            #endregion
            ControlWidth = 5 * size ;
            ControlHeight = 5 * size;
            ScaleX = ControlWidth / (size * 5);
            ScaleY = ControlHeight / (size * 5);
            InitializeColors(mainColor, size);
        }
        private void OnSizeChangedCommand(SizeChangedEventArgs obj) {
            ScaleX = obj.NewSize.Width / (Size * 5);
            ScaleY = obj.NewSize.Height / (Size * 5);
            ControlWidth = obj.NewSize.Width;
            ControlHeight = obj.NewSize.Height;
        }
        
        private void InitializeColors(Brush mainColor, int size) {
            Rows = new ObservableCollection<NByNCubeSideRowViewModel>();
            for (int i = 0; i < 7; ++i) {
                Rows.Add(new NByNCubeSideRowViewModel(mainColor, size, ControlHeight/Size, ControlWidth));
            }

            RowsVisibility = new ObservableCollection<Visibility>();
            RowsVisibility.Add(Visibility.Visible);
            for (int i = 1; i < 7; ++i) {
                RowsVisibility.Add(Visibility.Collapsed);
            }

            if (size >= 2) {
                RowsVisibility[1] = Visibility.Visible;
            }
            if (size >= 3) {
                RowsVisibility[2] = Visibility.Visible;
            }
            if (size >= 4) {
                RowsVisibility[3] = Visibility.Visible;
            }
            if (size >= 5) {
                RowsVisibility[4] = Visibility.Visible;
            }
            if (size >= 6) {
                RowsVisibility[5] = Visibility.Visible;
            }
            if (size == 7) {
                RowsVisibility[6] = Visibility.Visible;
            }
        }
        public void Rotate90DegClockwise() {
            for (int j = 0; j < Math.Floor(Size / 2.0) + 1; ++j) {
                for (int i = j; i < Size - j - 1; ++i) {
                    Brush temp = Rows[j].Colors[i];
                    Rows[j].Colors[i] = Rows[Size - 1 - i].Colors[j];
                    Rows[Size - 1 - i].Colors[j] = Rows[Size - 1 - j].Colors[Size - 1 - i];
                    Rows[Size - 1 - j].Colors[Size - 1 - i] = Rows[i].Colors[Size - 1 - j];
                    Rows[i].Colors[Size - 1 - j] = temp;
                }
            }
        }
    }
}
