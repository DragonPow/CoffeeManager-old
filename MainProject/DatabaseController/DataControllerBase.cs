using MainProject.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MainProject.DatabaseController
{
    public static class DataControllerBase
    {

        internal static ObservableCollection<TABLECUSTOM> LoadTable(int Floors)
        {
            ObservableCollection<TABLECUSTOM> ObserTab = new ObservableCollection<TABLECUSTOM>();
            List<TABLE> listtab;

            using (var db = new mainEntities())
            {
                listtab = db.TABLES.Where(t => (t.FLOORS == Floors && t.DELETED == 0)).ToList();

                foreach (TABLE t in listtab)
                {
                    //Nhực làm bị lỗi, làm lại
                    //ObserTab.Add(new TABLECUSTOM() { table = t, ListPro = null });
                }
            }

            return ObserTab;
        }
    }
}