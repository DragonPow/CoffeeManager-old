using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MainProject.VoucherWorkSpace
{
    class VoucherViewModel : BaseViewModel, IMainWorkSpace
    {
        public string NameWorkSpace => "Voucher";
        private const PackIconKind _IconDisplay = PackIconKind.GiftOutline;
        public PackIcon IconDisplay
        {
            get
            {
                return new PackIcon() { Kind = _IconDisplay, Width = 30, Height = 30 };
            }
        }

        public DateTime DateStart = new DateTime(2001, 1,1, 0,0,0);
        public DateTime DateEnd = new DateTime(2001, 1, 1,23,59,59);

        private int _value = 20;
        public String Value
        {
            get => _value.ToString();
            set
            {
                if (int.TryParse(value, out _value))
                {
                    OnPropertyChanged("ValueString");
                }
            }
        }

        public String ValueString
        {
            get => _value + "%";
        }

        private String _description = "Empty description";
        public String Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public String _code = "Empty code";
        public String Code
        {
            get => _code;
            set
            {
                if (this._code != value)
                {
                    this._code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        
    }
}
