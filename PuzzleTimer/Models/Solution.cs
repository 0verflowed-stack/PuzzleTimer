using PuzzleTimer.Models;
using PuzzleTimer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace PuzzleTimer {
    public class Solution : INotifyPropertyChanged {
        [Key]
        public int Id { get; set; }
        [Key]
        public string PuzzleName { get; set; }
        [NotMapped]
        private SolutionTime _SolutionTime;
        public string SolutionTime {
            get => _SolutionTime.Time;
            set {
                _SolutionTime = new SolutionTime(value);
            } 
        }
        public double TotalMilliseconds() => _SolutionTime.TotalMilliseconds;
        public double TotalSeconds() => _SolutionTime.TotalSeconds;
        public DateTime SolutionDate { get; set; }
        public string Scramble { get; set; }

        public Solution() {
            Id = 0;
            PuzzleName = MainWindowViewModel.PuzzleNameCollection[1];
            SolutionTime = "0.0";
            SolutionDate = DateTime.Now;
            Scramble = String.Empty;
        }
        public Solution(int Id, string PuzzleName, string SolutionTime, DateTime SolutionDate, string Scramble) {
            this.Id = Id;
            this.PuzzleName = PuzzleName;
            this.SolutionTime = SolutionTime;
            this.SolutionDate = SolutionDate;
            this.Scramble = Scramble;
        }
         public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public override string ToString() {
            return $"{Id} {PuzzleName} {SolutionTime} {SolutionDate.ToString("MM/dd/yyyy HH:mm:ss")} {Scramble}";
        }

    }
}
