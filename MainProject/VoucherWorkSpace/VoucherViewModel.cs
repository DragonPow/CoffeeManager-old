using MainProject.Model;
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

        private DateTime dateStart;
        public DateTime DateStart_Date { get => DateStart.Date;
            set
            {
                if (DateStart.Date != value)
                {
                    this.DateStart = new DateTime(DateStart.TimeOfDay.Ticks + value.Ticks);
                    OnPropertyChanged("DateStart_Date");
                }
            }
        }
        public TimeSpan DateStart_Time { get => DateStart.TimeOfDay;
            set
            {
                if (DateStart.TimeOfDay != value)
                {
                    this.DateStart = new DateTime(DateStart.Date.Ticks + value.Ticks);
                    OnPropertyChanged("DateStart_Time");
                }
            }
        }

        private DateTime dateEnd;
        public DateTime DateEnd { get => dateEnd;
            set
            {
                if (this.dateEnd != value)
                {
                    this.dateEnd = value;
                    OnPropertyChanged("DateEnd");
                }
            }
        }
        public DateTime DateEnd_Date
        {
            get => dateEnd.Date;
            set
            {
                if (this.dateEnd.Date != value)
                {
                    this.dateEnd = new DateTime(value.Ticks + dateEnd.TimeOfDay.Ticks);
                    OnPropertyChanged("DateEnd_Date");
                }
            }
        }
        public TimeSpan DateEnd_Time
        {
            get => dateEnd.TimeOfDay;
            set
            {
                if (this.dateEnd.TimeOfDay != value)
                {
                    this.dateEnd = new DateTime(value.Ticks + dateEnd.Date.Ticks);
                    OnPropertyChanged("DateEnd_Time");
                }
            }
        }

        private int _value = 20;
        public String Value
        {
            get => _value.ToString();
            set
            {
                int input = -1;
                try { input = int.Parse(value); }
                catch (Exception) { }

                if (input > 0 && input != _value)
                {
                    _value = input;
                    OnPropertyChanged("ValueString");
                    OnPropertyChanged("Value");
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

        public bool IsAuto { get => isAuto; set
            {
                if (isAuto != value)
                {
                    isAuto = value;
                    OnPropertyChanged("IsAuto");
                }
            }
        }

        public DateTime DateStart
        {
            get => dateStart;
            set
            {
                if (this.dateStart != value)
                {
                    dateStart = value;
                    OnPropertyChanged("DateStart");
                }
            }
        }

        private bool isAuto;

        public ValidationRules.LengthRangeValidationRule LengthRangeValidationRule = new ValidationRules.LengthRangeValidationRule();

        public VoucherViewModel()
        {
            this._code = getRandomCode();
            this.isAuto = true;
            this.Description = "Empty description";
            this.Value = "20";

            this.dateStart = DateTime.Now;
            this.dateEnd = DateTime.Now.AddDays(1);
        }

        public String getRandomCode() 
            {
            const String chars = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            Random random = new Random(DateTime.Now.TimeOfDay.Milliseconds);
            StringBuilder builder = new StringBuilder(16);
            for (int i = 0; i < builder.Capacity; i++)
            {
                builder.Append(chars.ElementAt(random.Next(chars.Length)));
            }
            return builder.ToString();
        }

        public void GetAvaiableCode()
        {
            this._code = getRandomCode();
            using (mainEntities db = new mainEntities())
            {
                while (db.VOUCHERs.Where((v) => v.ID == this._code).Any())
                {
                    this._code = getRandomCode();
                }
            }
            OnPropertyChanged("Code");
        }

        public VOUCHER toDB_Voucher()
        {
            VOUCHER voucher = new VOUCHER();
            voucher.ID = this.Code;
            voucher.BEGINTIME = this.DateStart;
            voucher.ENDTIME = this.dateEnd;
            voucher.PERCENT = this._value;
            voucher.DELETED = 0;
            voucher.DESCRIPTION = this.Description;
            return voucher;
        }

        public void SaveToDB(VoucherViewModel viewModel)
        {
            using (mainEntities db = new mainEntities())
            {
                db.VOUCHERs.Add(viewModel.toDB_Voucher());
                db.SaveChanges();
            }
        }

        public static bool HasExisted(String code)
        {
            bool rs = false;
            using (mainEntities db = new mainEntities())
            {
                rs = db.VOUCHERs.Where(v => v.ID == code).Any();
            }
            return rs;
        }
    }
}
