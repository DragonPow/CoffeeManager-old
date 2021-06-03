using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public class TABLECUSTOM : BaseViewModel
    {
        private ObservableCollection<DetailPro> _listPro;
        public TABLE table { get; set; }
        public long Total { get => ListPro.Sum(item => item.TotalPrice); }

        public virtual ObservableCollection<DetailPro> ListPro
        {
            get => _listPro;
            set
            {
                if (_listPro != value)
                {
                    _listPro = value;
                    if (_listPro != null) _listPro.CollectionChanged += (s, e) => OnPropertyChanged("Total");
                    OnPropertyChanged();
                }
            }
        }

        #region Init

        public TABLECUSTOM()
        {

        }

        #endregion
    }
}
