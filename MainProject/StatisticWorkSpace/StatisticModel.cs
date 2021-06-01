using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.StatisticWorkSpace
{
    class StatisticModel
    {
        public DateTime TimeMin;
        public DateTime TimeMax;
        public int Revenue;
        public String Title;
        public StatisticModel()
        {
            TimeMin = new DateTime(2001, 8, 30, 0, 0, 0);
            TimeMax = new DateTime(2001, 8, 30, 23, 59, 59);
            Title = "01/05/2017 - 07/05/2017";
            Revenue = 50000;
        }
    }
}
