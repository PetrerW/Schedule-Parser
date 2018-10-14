using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleParser
{
    /// <summary>
    /// An object that represents the night/day shift
    /// </summary>
    public class Shift
    {
        /// <summary>
        /// A constructor. Automatically sets beggining and end fields
        /// </summary>
        /// <param name="operator1">First operator shortcut</param>
        /// <param name="operator2">Second operator shortcut</param>
        /// <param name="isNight">For night shift it's true</param>
        /// <param name="day">Day with hour 00:00:00</param>
        public Shift(string operator1, string operator2, bool isNight, DateTime ShiftDate)
        {
            this.employee1 = operator1;
            this.employee2 = operator2;
            this.isNight = isNight;
            //Night shift
            if (isNight)
            {
                this.beggining = new DateTime(ShiftDate.Year, ShiftDate.Month, ShiftDate.Day, 19, 0, 0);
                //Add a day to the shift day as it ends a day later
                this.end = new DateTime(ShiftDate.Year, ShiftDate.Month, ShiftDate.AddDays(1).Day, 7, 00, 0);
            }
            //Day shift
            else
            {
                this.beggining = new DateTime(ShiftDate.Year, ShiftDate.Month, ShiftDate.Day, 7, 0, 0);
                this.beggining = new DateTime(ShiftDate.Year, ShiftDate.Month, ShiftDate.Day, 19, 0, 0);
            }
                
        }

        /// <summary>
        /// When the file with the schedule was updated
        /// </summary>
        private DateTime updateTime;



        /// <summary>
        /// Time of the beggining of the shift
        /// </summary>
        private DateTime beggining;

        /// <summary>
        /// Time of the end of the shift
        /// </summary>
        private DateTime end;

        /// <summary>
        /// True: the shift is in the night
        /// </summary>
        private bool isNight;

        /// <summary>
        /// First person in the shift
        /// </summary>
        private string employee1;

        /// <summary>
        /// Second person in the shift
        /// </summary>
        private string employee2;

        //Getters & setters
        public DateTime UpDateTime
        {
            get { return updateTime; }
            set { beggining = value; }
        }

        public DateTime Beggining
        {
            get { return beggining; }
            set { beggining = value; }
        }

        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        public string Employee1
        {
            get { return employee1; }
            set { employee1 = value; }
        }

        public string Employee2
        {
            get { return employee2; }
            set { employee2 = value; }
        }
    }
}
