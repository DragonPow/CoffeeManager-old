using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public partial class TYPE_PRODUCT
    {
        public ObservableCollection<string> ListType { get; private set; }
        public static void loadListType()
        {
            using (var db = new mainEntities())
            {
                Instance.ListType = new ObservableCollection<string>(db.TYPE_PRODUCT.Select(i => i.Type).ToList());
            }
        }
        private static TYPE_PRODUCT _instance;
        public static TYPE_PRODUCT Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = new TYPE_PRODUCT();
                }
                return _instance;
            }
        }
    }
}
