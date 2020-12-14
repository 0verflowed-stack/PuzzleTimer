using Accessibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PuzzleTimer.Models {
    class SolutionTimeComparer : ICustomSorter {
        int reverse;
        public ListSortDirection SortDirection { 
            get {
                if (reverse == -1) {
                    return ListSortDirection.Descending;
                }
                return ListSortDirection.Ascending;
            } set {
                reverse = value == ListSortDirection.Ascending ? 1 : -1;
            } 
        }

        public int Compare(object x, object y) {
            return this.reverse * SolutionTime.Compare(((Solution)x).SolutionTime, ((Solution)y).SolutionTime);
        }
        public SolutionTimeComparer() {
            reverse = 1;
        }
    }
}
