using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public partial class STATUS_TABLE
    {
        public static ObservableCollection<string> ListStatus;
        public static void loadListStatus()
        {
            using (var db = new mainEntities())
            {
                ListStatus = new ObservableCollection<string>(db.STATUS_TABLE.Select(i => i.Status).ToList());
            }
        }
    }
}
