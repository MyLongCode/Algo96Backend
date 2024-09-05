using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Algo96.EF.DAL
{
    public enum DayOfWeek
    {
        [Description("Monday")]
        Monday,
        [Description("Tuesday")]
        Tuesday,
        [Description("Wednesday")]
        Wednesday,
        [Description("Thursday")]
        Thursday,
        [Description("Friday")]
        Friday,
        [Description("Saturday")]
        Saturday,
        [Description("Sunday")]
        Sunday,
    }
    public class Group
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public IEnumerable<User> Users { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime DateTime { get; set; }
        public Place Place { get; set; }
    }
}
