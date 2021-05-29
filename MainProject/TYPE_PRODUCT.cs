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
        public static ObservableCollection<string> ListType;
        public static void loadListType()
        {
            using (var db = new mainEntities())
            {
                ListType = new ObservableCollection<string>(db.TYPE_PRODUCT.Select(i => i.Type).ToList());
            }
        }
    }
}
