using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace PuzzleTimer.Models {
    class LangComboBoxItem {
        public string Name { get; set; }
        public BitmapImage ImgPath { get; set; }
        public LangComboBoxItem() {
            Name = String.Empty;
            ImgPath = new BitmapImage();
        }
        public LangComboBoxItem(string Name, string ImgPath) {
            this.Name = Name;
            this.ImgPath = new BitmapImage(new Uri(ImgPath, UriKind.Relative));
        }
    }
}
