using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleTimer.Models {
    interface ICloseWindow {
        Action Close { get; set; }
    }
}
