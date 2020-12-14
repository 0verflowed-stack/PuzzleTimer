using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace PuzzleTimer {
    public class Settings {
        [JsonProperty("CurrentPuzzle")]
        public int CurrentPuzzle;
        [JsonProperty("CurrentTheme")]
        public int CurrentTheme;
        [JsonProperty("Language")]
        public int Language;

        public Settings() {
            this.CurrentPuzzle = 1;
            this.CurrentTheme = 0;
            this.Language = 0;
        }
        public Settings(int CurrentPuzzle, int CurrentTheme, int Language) {
            this.CurrentPuzzle = CurrentPuzzle;
            this.CurrentTheme = CurrentTheme;
            this.Language = Language;
        }
        public override string ToString() {
            return $"{CurrentPuzzle} {CurrentTheme} {Language}";
        }
    }
}
