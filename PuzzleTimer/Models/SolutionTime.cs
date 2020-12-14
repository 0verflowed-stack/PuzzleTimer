using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PuzzleTimer.Models {
    public class SolutionTime {
        private TimeSpan _Time;
        public string Time {
            get {
                return Parse(_Time);
            }
            set {
                
                _Time = Parse(value);
            }
        }
        public double TotalMilliseconds {
            get => _Time.TotalMilliseconds;
        }
        public double TotalSeconds {
            get => _Time.TotalSeconds;
        }
        public static bool TryParse(string str) {
            if(str == null) return false;
            return new Regex("^([0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}.[0-9]{1,3}|[0-9]{1,2}:[0-9]{1,2}.[0-9]{1,3}|[0-9]{1,2}.[0-9]{1,3})$").IsMatch(str);
        }
        public static string Parse(TimeSpan timeSpan) {
            string shortForm = "";
            if (timeSpan.Hours > 0) {
                shortForm += string.Format("{0}:", timeSpan.Hours.ToString());
            }
            if (timeSpan.Minutes > 0 || timeSpan.Hours != 0) {
                shortForm += string.Format("{0}:", timeSpan.Minutes.ToString());
            }
            shortForm += string.Format("{0}.", timeSpan.Seconds.ToString());
            if (timeSpan.Milliseconds > 0) {
                if (Math.Ceiling(timeSpan.Milliseconds / 10.0).ToString().Length == 1) {
                    shortForm += String.Format("0{0}", Math.Ceiling(timeSpan.Milliseconds / 10.0).ToString());
                }
                else {
                    shortForm += String.Format("{0}", Math.Ceiling(timeSpan.Milliseconds / 10.0).ToString());
                }
            }
            else {
                shortForm += "0";
            }
            return shortForm;
        }
        public static TimeSpan Parse(string value) {
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int milliseconds = 0;
            List<string> timeNumbers = value.Split(new char[] { '.', ':' }).ToList();
            for (int i = timeNumbers.Count - 1; i >= 0; --i) {
                if (timeNumbers.Count - i == 1) {
                    milliseconds = Int32.Parse(timeNumbers[i]) * 10;
                }
                if (timeNumbers.Count - i == 2) {
                    seconds = Int32.Parse(timeNumbers[i]);
                }
                if (timeNumbers.Count - i == 3) {
                    minutes = Int32.Parse(timeNumbers[i]);
                }
                if (timeNumbers.Count - i == 4) {
                    hours = Int32.Parse(timeNumbers[i]);
                }
                if (timeNumbers.Count - i == 5) {
                    days = Int32.Parse(timeNumbers[i]);
                }
            }
            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }
        public static double ParseTotalMilliseconds(string s) {
            return Parse(s).TotalMilliseconds;
        }
        public static int Compare(string s1, string s2) {
            if (ParseTotalMilliseconds(s1) > ParseTotalMilliseconds(s2)) {
                return 1;
            }
            else if (ParseTotalMilliseconds(s1) < ParseTotalMilliseconds(s2)) {
                return -1;
            }
            return 0;
        }
        public SolutionTime() {
            _Time = TimeSpan.Zero;
        }
        public SolutionTime(string Time) {
            this.Time = Time;
        }
        public SolutionTime(TimeSpan Time) {
            this.Time = Parse(Time);
        }
        public SolutionTime(SolutionTime solutionTime) {
            Time = solutionTime.Time;
        }
        public override string ToString() {
            return Time;
        }
    }
}
