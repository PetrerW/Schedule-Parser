using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleParser
{
    /// <summary>
    /// A class that parses the timetable from file to the list of Shift objects
    /// To parse the file do the following steps:
    /// 1. Download the site with the timetable as .txt file
    /// 2. Paste it to ScheduleFiles directory
    /// 3. Choose the file from the list
    /// 4. Click parse!
    /// </summary>
    public class TimetableParser
    {
        /// <summary>
        /// A list of short months' names
        /// </summary>
        public static List<string> months =
            new List<string>(new string[] {"sty", "lut", "mar", "kwi", "maj", "cze", "lip", "sie", "paź", "lis", "gru"});

        /// <summary>
        /// The list of the shifts in the file
        /// </summary>
        private List<Shift> shifts;

        /// <summary>
        /// A full path to the file
        /// </summary>
        private string textFilePath;

        /// <summary>
        /// A function that reads the file with the specified name (without .txt)
        /// </summary>
        public List<string> read(string fileName)
        {
            try
            {
                //Path to the file
                textFilePath = "./ScheduleFiles/" + fileName + ".txt";

                //Open a file
                var filestream = new System.IO.FileStream(textFilePath,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read,
                    System.IO.FileShare.ReadWrite);

                //Encoding.Default is default encoding of the OS
                var file = new System.IO.StreamReader(filestream, System.Text.Encoding.Default, true, 128);

                List<string> lines = new List<string>();

                var lineOfText = "";
                while ((lineOfText = file.ReadLine()) != null)
                {
                    lines.Add(lineOfText);
                }

                return lines;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error reading the file! The filepath: " + textFilePath);
                return new List<string>();
            }
        }

        /// <summary>
        /// Extracts and parses the Schedule file update
        /// </summary>
        /// <param name="line"></param>
        /// <returns>
        /// The update time of the Schedule file.
        /// </returns>
        public string parseUpdateDate(string line)
        {
                // Get a collection of matches (match the date of the schedule)
                //\d stands for a digit, \. matches exactly one dot.
                MatchCollection matches = Regex.Matches(line, @"\d\d\.\d\d\.\d\d\d\d");

                //The day when the Schedule file was updated.
                string fileDate;

                //If reading the date was succesful, fill the field.
                fileDate = matches.Count == 1 ? matches[0].ToString() : "";

                return fileDate;
        }

        /// <summary>
        /// A function that parses a single line to an Shift object
        /// </summary>
        /// <param name="line"></param>
        /// <param name="updateTime"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public Tuple<Shift, Shift> parseShift(string line, string updateTime, int year)
        {
            try
            {
                MatchCollection matches = Regex.Matches(line, @"\d\d\.[a-ż]+");
                string date;
                date = matches[0].Value;

                //Match the first operator on the shift ([0] - day, [1] - night
                MatchCollection operator1Match = Regex.Matches(line, @"\t[a-ź]+,");

                //The first operator on the day shift
                var operator1Day = operator1Match[0].Value.Substring(1, 3);

                //The first operator on the night shift
                var operator1Night = operator1Match[1].Value.Substring(1, 3);

                //Match the first operator on the shift ([0] - day, [1] - night
                MatchCollection operator2Match = Regex.Matches(line, @" [a-ż][a-ż][a-ż] \t[a-ż]");

                //The second operator on the day shift
                var operator2Day = operator2Match[0].Value.Substring(2, 3);

                //The second operator on the night shift
                var operator2Night = operator2Match[1].Value.Substring(2, 3);

                //todo

                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while parsing the line to the Shift object. " + e.Message);
                return null;
            }
            
        }

        /// <summary>
        /// Return a tuple <day,month>
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int> extractDayAndMonth(string date)
        {
            int day;
            int.TryParse(date.Substring(0, 2), out day);
            var month = date.Substring(3, 3);
            int monthIndex = months.FindIndex(x => x == month);
            var monthNumber = monthIndex++;
            return new Tuple<int, int>(day, monthNumber);
        }

        /// <summary>
        /// Getter/setter
        /// </summary>
        public string TextFilePath
        {
            get { return textFilePath; }
            set { textFilePath = value; }
        }
    }
}
