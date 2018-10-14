using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleParser;

namespace ScheduleParser
{
    /// <summary>
    /// A class to test the TimeTableParser
    /// </summary>
    
    [TestClass]
    public class TimeTableParserTest
    {
        private TimetableParser timetableParser;

        public List<string> Read()
        {
            timetableParser = new TimetableParser();
            return timetableParser.read("10_Pazdziernik");
        }

        /// <summary>
        /// Print all lines from the read file
        /// </summary>
        [TestMethod]
        public void testRead()
        {
            var lines = Read();

            foreach(var line in lines)
                Console.WriteLine(line);
        }

        /// <summary>
        /// Test parse update time
        /// Case sensitive! 
        /// </summary>
        [TestMethod]
        public void testParseUpdateTime()
        {
            var lines = Read();

            foreach (var line in lines)
            {
                string updateTime = timetableParser.parseUpdateDate(line);
                if (updateTime != "")
                    Assert.AreEqual("09.10.2018", updateTime);
            }
        }

        /// <summary>
        /// Test the constructor
        /// </summary>
        [TestMethod]
        public void testShiftConstructor()
        {
            Shift shift = new Shift("mwj", "ksw", true, new DateTime(2018, 10, 14, 7, 0, 0));
            Console.WriteLine(shift.Beggining.Date);
        }

        /// <summary>
        /// Test if the names of the operators parse correctly
        /// </summary>
        [TestMethod]
        public void testParseShift()
        {
            var line = "poniedziałek 01.paź  mwj, pmi jkr, mfo    awy lku     bms bms     mwt asr";
            var year = 2018;
            //TODO
        }
        
    }
}
