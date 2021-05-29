using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public partial class POSITION_EMPLOYEE
    {
        public static ObservableCollection<string> ListPosition;
        public static void loadListStatus()
        {
            using (var db = new mainEntities())
            {
                ListPosition = new ObservableCollection<string>(db.POSITION_EMPLOYEE.Select(i => i.Position).ToList());
            }
        }
    }
}
