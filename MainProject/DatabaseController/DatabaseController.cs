using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.DatabaseController
{
     public static class DataController
    {
        public static void DeleteTable(int Number, int Floors)
        {
            
            using (var db = new mainEntities())
            {
                TABLE table = db.TABLES.Where(d => (d.NUMBER == Number && d.FLOORS == Floors) && (d.DELETED == 0)).First();

                if (table != null)
                {
                    table.DELETED = 1;

                    var TABLES = db.TABLES.Where(t => t.NUMBER > Number && t.FLOORS == Floors);

                    foreach (TABLE tab in TABLES)
                    {
                        --tab.NUMBER;
                    }

                }

                db.SaveChanges();
            }    
        }

        public static void AddTable (int Floors, int Number)
        {
            TABLE tab = new TABLE { FLOORS = Floors, STATUS = 0, NUMBER = Number, DELETED = 0 };

            using (var db = new mainEntities())
            {
                db.TABLES.Add(tab);
                db.SaveChanges();
            }
        }
    }
}
