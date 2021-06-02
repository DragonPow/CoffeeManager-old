using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public partial class PRODUCT
    {
        //private string _currentType;
        public string MainType
        {
            get
            {
                return TYPE_PRODUCT.Count==0 ? null : TYPE_PRODUCT[0].Type;
            }
            set
            {
                if (value != MainType)
                {
                    if (TYPE_PRODUCT == null)
                    {
                        throw new NullReferenceException("TYPE_PRODUCT of PRODUCT is null");
                        return;
                    }
                    if (TYPE_PRODUCT.Count==0)
                    {
                        TYPE_PRODUCT.Add(new TYPE_PRODUCT());
                    }
                    TYPE_PRODUCT[0].Type = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
