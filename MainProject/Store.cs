using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public static class Store
    {
        #region Field
        static string _name;
        static string _phone;
        static string _address;
        static string _nameInnkeeper;
        #endregion

        #region Property

        public static string Name { get => _name; set => _name = value; }
        public static string Phone { get => _phone; set => _phone = value; }
        public static string Address { get => _address; set => _address = value; }
        public static string NameInnkeeper { get => _nameInnkeeper; set => _nameInnkeeper = value; }

        #endregion
    }
}
