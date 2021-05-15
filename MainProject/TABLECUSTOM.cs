using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    class TABLECUSTOM
    {
        public TABLE table { get; set; }
        public int Total;

        public virtual  ObservableCollection<DetailPro> ListPro
        {
            get => ListPro;
            set
            {
                if (value != ListPro)
                {
                    ListPro = value;
                }
            }
        }
    }
}
