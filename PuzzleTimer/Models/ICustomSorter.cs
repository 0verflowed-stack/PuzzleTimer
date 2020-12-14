using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PuzzleTimer.Models {
    public interface ICustomSorter : IComparer {
        ListSortDirection SortDirection { get; set; }
    }
}
