using System;
using System.Collections.Generic;
using System.Text;

namespace SplashyApp.Models
{
    class Schedule
    {
        public string  StartDate { get; set; }
        public string EndDate { get; set; }
        public string Periodicity { get; set; }
        public string CronExoression { get; set; }


    }
    static class CronExpression
    {
      public  static Dictionary<string, string> cronExpressions = new Dictionary<string, string>()
      {
               { "Every second","* * * ? * *"},
               {"Every minute","0 * * ? * *" },
               {"Every even minute","0 */2 * ? * *" },
               {"Every uneven minute","0 1/2 * ? * *" },
               {"Every 2 minutes","0 */2 * ? * *" },
               {"Every 3 minutes","0 */3 * ? * *" },
               {"Every 4 minutes","0 */4 * ? * *" },
               {"Every 5 minutes","0 */5 * ? * *" },
               {"Every 10 minutes","0 */10 * ? * *" },
               {"Every 15 minutes","0 */15 * ? * *" },
               {"Every 30 minutes","0 */30 * ? * *" },
               {"Every hour at minutes 15, 30 and 45","0 15,30,45 * ? * *" },
               {"Every hour","0 0 * ? * *" },
               {"Every even hour","0 0 0/2 ? * *" },
               {"Every uneven hour","0 0 1/2 ? * *" },
               {"Every three hours","0 0 */3 ? * *" },
               {"Every four hours","0 0 */4 ? * *" },
               {"Every six hours","0 0 */6 ? * *" },
               {"Every eight hours","0 0 */8 ? * *" },
               {"Every twelve hours","0 0 */12 ? * *" },
               {"Every day at midnight - 12am","0 0 0 * * ?" },
               {"Every day at 1am","0 0 1 * * ?" },
               {"Every day at 6am","0 0 6 * * ?" },
               { "Every day at noon - 12pm","0 0 12 * * ?" },
               {"Every Sunday at noon","0 0 12 * * SUN" },
               {"Every Monday at noon","0 0 12 * * MON" },
               { "Every Weekday at noon","0 0 12 * * MON-FRI" },
               {"Every Saturday and Sunday at noon","0 0 12 * * SUN,SAT" },
               {"Every 7 days at noon","0 0 12 */7 * ?"},
               {"Every month on the 1st, at noon","0 0 12 1 * ?"},
               {"Every day at noon in January only","0 0 12 ? JAN *"},
               {"Every day at noon in June only","0 0 12 ? JUN *"},
               {"Every day at noon in January and June","0 0 12 ? JAN,JUN *"}
      };
    }
}
