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
        public ObservableCollection<string> ListPosition { get; private set; }
        public static void loadListStatus()
        {
            using (var db = new mainEntities())
            {
                Instance.ListPosition = new ObservableCollection<string>(db.POSITION_EMPLOYEE.Select(i => i.Position).ToList());
            }
        }
        private static POSITION_EMPLOYEE _instance;
        public static POSITION_EMPLOYEE Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new POSITION_EMPLOYEE();
                }
                return _instance;
            }
        }
    }
}
