using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TimeOfDayTests
    {
        private string GetTimeOfDay(DateTime dateTime)
        {
            var intTime = dateTime.Hour;

            if (intTime >= 5 && intTime < 12)
            {
                return "Good morning";
            }
            else if (intTime >= 12 && intTime < 18)
            {
                return "Good afternoon";
            }
            else
            {
                return "Good evening";
            }

        }
        

        [Fact]
        public void GetTimeOfDay_Morning_ShouldReturnGoodMorning()
        {
            var morningTime = new DateTime(2024, 5, 28, 6, 0, 0); // 06:00
            var result = GetTimeOfDay(morningTime);
            Assert.Equal("Good morning", result);
        }

        [Fact]
        public void GetTimeOfDay_Afternoon_ShouldReturnGoodAfternoon()
        {
            var afternoonTime = new DateTime(2024, 5, 28, 13, 0, 0); //13:00
            var result = GetTimeOfDay(afternoonTime);
            Assert.Equal("Good afternoon", result);
        }

        [Fact]
        public void GetTimeOfDay_Evening_ShouldReturnGoodEvening()
        {
            var eveningTime = new DateTime(2024, 5, 28, 20, 0, 0); // 20:00 
            var result = GetTimeOfDay(eveningTime);
            Assert.Equal("Good evening", result);
        }

        [Fact]
        public void GetTimeOfDay_Night_ShouldReturnGoodEvening()
        {
            var nightTime = new DateTime(2024, 5, 28, 3, 0, 0); // 03:00
            var result = GetTimeOfDay(nightTime);
            Assert.Equal("Good evening", result);
        }
    }

}
