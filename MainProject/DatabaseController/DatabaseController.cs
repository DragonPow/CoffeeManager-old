using MainProject.Model;
using System.Collections.Generic;
using System.Linq;

namespace MainProject.DatabaseController
{
     public static class DataController
    {
        public static void DeleteTable(int Number, int Floors)
        {
            
            using (var db = new mainEntities())
            {
                TABLE table = db.TABLES.Where(d => (d.NUMBER == Number && d.FLOORS == Floors) && (d.DELETED == 0)).FirstOrDefault();

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

        public static void UpdateProduct(PRODUCT product)
        {
            using ( var db = new mainEntities())
            {
                product.DELETED = 0;
                PRODUCT pro = db.PRODUCTs.Where(p => (p.NAME == product.NAME) && (p.DELETED == 0)).FirstOrDefault();

                if (pro != null)
                {
                    pro.DELETED = 1;
                    db.PRODUCTs.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    db.PRODUCTs.Add(product);
                    db.SaveChanges();
                }                    
            }
        }

        public static void DeleteProduct(PRODUCT pro)
        {
            using (var db = new mainEntities())
            {
                PRODUCT product = db.PRODUCTs.Where(p => (p.NAME == pro.NAME) && (p.DELETED == 0)).FirstOrDefault();
                if (product != null)
                {
                    product.DELETED = 1;
                }
                db.SaveChanges();
            }

        }

        public static List<TABLE> LoadTable(int Floors)
        {
            
            using (var db = new mainEntities())
            {
                List<TABLE> listtab = db.TABLES.Where(t => (t.FLOORS == Floors ) && (t.DELETED == 0)).ToList();
                if (listtab != null)
                {
                    return listtab;
                }
            }
            return null;
        }

        public static List<PRODUCT> SearchProByType(string Type)
        {
            using (var db = new mainEntities())
            {
                List<PRODUCT> listpro = db.PRODUCTs.Where(p => (p.TYPE == Type) && (p.DELETED == 0)).ToList();
                if (listpro != null)
                {
                    return listpro;
                }
            }
            return null;
        }

    }
}
