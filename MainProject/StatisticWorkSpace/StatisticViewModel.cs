using MainProject.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.StatisticWorkSpace
{
    class StatisticViewModel : BaseViewModel, IMainWorkSpace
    {
        public string NameWorkSpace => "Thu ngân";
        private const PackIconKind _iconDisplay = PackIconKind.Finance;
        public PackIcon IconDisplay
        {
            get
            {
                return new PackIcon() { Kind = _iconDisplay, Width = 30, Height = 30 };
            }
        }

        ObservableCollection<StatisticModel> listModel;
        public ObservableCollection<StatisticModel> ListModel { get => listModel; }

        StatisticMode currentMode = StatisticMode.DayOfWeek;
        public StatisticMode CurrentMode => currentMode;
        public String CurrentMode_String => StatisticEnum.GetString(currentMode);
        public void SetCurrentMode(int index)
        {
            if (index < 0 && index >= Enum.GetValues(typeof(StatisticMode)).Length) return;
            currentMode = (StatisticMode)index;
        }

        public Func<double, string> formaterLabelAxisY { get; set; }
        private String getMoneyLabel(int money)
        {
            string[] prefix = new string[] { "k", "tr", "tỷ" };
            int i = 0;
            while (money / 1000000 != 0)
            {
                money /= 1000; 
                i++;
            }
            double rs = money / 1000d;

            return String.Format("{0:0.#}", rs) + prefix[i];
        }

        public void setTimeRange(DateTime minDate, DateTime maxDate)
        {
            listModel.Clear();

            List<StatisticModel> data = null;
            switch (CurrentMode)
            {
                case StatisticMode.DayOfWeek:
                    data = statisticByDay(minDate, maxDate);
                    break;
                case StatisticMode.WeekOfMonth:
                    data = statisticByWeek(minDate, maxDate);
                    break;
                case StatisticMode.MonthOfYear:
                    data = statisticByMonth(minDate, maxDate);
                    break;
            }

            foreach (var model in data)
            {
                ListModel.Add(model);
            }

            OnPropertyChanged(nameof(ListModel));
        }

        public List<StatisticModel> statisticByDay(DateTime minDate, DateTime maxDate)
        {
            using (mainEntities db = new mainEntities())
            {
                var data = db.BILLs.Where(b => b.CheckoutDay >= minDate && b.CheckoutDay <= maxDate)
                    .Join(db.DETAILBILLs, b => b.ID, dt => dt.ID_Bill,
                    (b, dt) => new
                    {
                        PD_ID = dt.ID_Product,
                        Date = b.CheckoutDay,
                        Amount = dt.Amount
                    }).Join(db.PRODUCTs, r => r.PD_ID, pd => pd.ID,
                    (r, pd) => new
                    {
                        pd.Name,
                        Revenue = pd.Price * r.Amount,
                        r.Date
                    });
                Dictionary<DateTime, StatisticModel> dictionary = new Dictionary<DateTime, StatisticModel>();
                foreach (var group in data)
                {
                    if (group.Date.HasValue)
                    {
                        DateTime date = new DateTime(group.Date.Value.Year, group.Date.Value.Month, group.Date.Value.Day, 0, 0, 0);
                        if (!dictionary.ContainsKey(date))
                        {
                            var model = new StatisticModel();
                            model.TimeMin = date;
                            model.TimeMax = date.AddDays(1).AddSeconds(-1);
                            model.Revenue = group.Revenue.Value;
                            model.Label = GetLabel(model);
                            model.Title = GetLabel(model);
                            dictionary.Add(date, model);
                        }
                        else
                        {
                            var model = dictionary[date];
                            model.Revenue += group.Revenue.Value;
                        }
                    }
                }
                return dictionary.Values.ToList();
            }
        }

        public List<StatisticModel> statisticByWeek(DateTime minDate, DateTime maxDate)
        {
            using (mainEntities db = new mainEntities())
            {
                var data = db.BILLs.Where(b => b.CheckoutDay >= minDate && b.CheckoutDay <= maxDate)
                    .Join(db.DETAILBILLs, b => b.ID, dt => dt.ID_Bill,
                    (b, dt) => new
                    {
                        PD_ID = dt.ID_Product,
                        Date = b.CheckoutDay,
                        Amount = dt.Amount
                    }).Join(db.PRODUCTs, r => r.PD_ID, pd => pd.ID,
                    (r, pd) => new
                    {
                        pd.Name,
                        Revenue = pd.Price * r.Amount,
                        r.Date
                    });
                Dictionary<DateTime, StatisticModel> dictionary = new Dictionary<DateTime, StatisticModel>();
                foreach (var group in data)
                {
                    if (group.Date.HasValue)
                    {
                        DateTime date = new DateTime(group.Date.Value.Year, group.Date.Value.Month, group.Date.Value.Day, 0, 0, 0);
                        while (date.DayOfWeek != DayOfWeek.Monday) { date = date.AddDays(-1); }
                        if (!dictionary.ContainsKey(date))
                        {
                            var model = new StatisticModel();
                            model.TimeMin = date;
                            model.TimeMax = date.AddDays(7).AddSeconds(-1);
                            model.Revenue = group.Revenue.Value;
                            model.Label = GetLabel(model);
                            model.Title = GetLabel(model);
                            dictionary.Add(date, model);
                        }
                        else
                        {
                            var model = dictionary[date];
                            model.Revenue += group.Revenue.Value;
                        }
                    }
                }
                return dictionary.Values.ToList();
            }
        }

        public List<StatisticModel> statisticByMonth(DateTime minDate, DateTime maxDate)
        {
            using (mainEntities db = new mainEntities())
            {
                var data = db.BILLs.Where(b => b.CheckoutDay >= minDate && b.CheckoutDay <= maxDate)
                    .Join(db.DETAILBILLs, b => b.ID, dt => dt.ID_Bill,
                    (b, dt) => new
                    {
                        PD_ID = dt.ID_Product,
                        Date = b.CheckoutDay,
                        Amount = dt.Amount
                    }).Join(db.PRODUCTs, r => r.PD_ID, pd => pd.ID,
                    (r, pd) => new
                    {
                        pd.Name,
                        Revenue = pd.Price * r.Amount,
                        r.Date
                    });
                Dictionary<DateTime, StatisticModel> dictionary = new Dictionary<DateTime, StatisticModel>();
                foreach (var group in data)
                {
                    if (group.Date.HasValue)
                    {
                        DateTime date = new DateTime(group.Date.Value.Year, group.Date.Value.Month, 1, 0, 0, 0);
                        if (!dictionary.ContainsKey(date))
                        {
                            var model = new StatisticModel();
                            model.TimeMin = date;
                            model.TimeMax = date.AddMonths(1).AddSeconds(-1);
                            model.Revenue = group.Revenue.Value;
                            model.Label = GetLabel(model);
                            model.Title = GetLabel(model);
                            dictionary.Add(date, model);
                        }
                        else
                        {
                            var model = dictionary[date];
                            model.Revenue += group.Revenue.Value;
                        }
                    }
                }
                return dictionary.Values.ToList();
            }
        }

        public String GetLabel(StatisticModel model)
        {
            String rs = "<Error>";
            switch (CurrentMode)
            {
                case StatisticMode.DayOfWeek:
                    rs = GetDayOfWeek(model.TimeMin.DayOfWeek);
                    break;
                case StatisticMode.WeekOfMonth:
                    rs = String.Format("{0} - {1} năm {2}", model.TimeMin.ToString("dd/MM"),
                        model.TimeMax.ToString("dd/MM"), model.TimeMin.Year.ToString());
                    break;
                case StatisticMode.MonthOfYear:
                    rs = String.Format("Tháng {0}", model.TimeMin.ToString("MM/yyyy"));
                    break;
            }
            return rs;
        }

        public String GetDayOfWeek(DayOfWeek dayOfweek)
        {
            String rs = null;
            switch (dayOfweek)
            {
                case DayOfWeek.Monday:
                    rs = "Thứ Hai";
                    break;
                case DayOfWeek.Tuesday:
                    rs = "Thứ Ba";
                    break;
                case DayOfWeek.Wednesday:
                    rs = "Thứ Tư";
                    break;
                case DayOfWeek.Thursday:
                    rs = "Thứ Năm";
                    break;
                case DayOfWeek.Friday:
                    rs = "Thứ Sáu";
                    break;
                case DayOfWeek.Saturday:
                    rs = "Thứ Bảy";
                    break;
                case DayOfWeek.Sunday:
                    rs = "Chủ Nhật";
                    break;
            }
            return rs;
        }

        public StatisticViewModel()
        {
            listModel = new ObservableCollection<StatisticModel>
            {
                new StatisticModel(){ Label = "Label1", Revenue = 10000},
                new StatisticModel(){ Label = "Label2", Revenue = 110000},
                new StatisticModel(){ Label = "Label3", Revenue = 70000},
                new StatisticModel(){ Label = "Label4", Revenue = 60000},
                new StatisticModel(){ Label = "Label5", Revenue = 30000}
            };
            formaterLabelAxisY = val => getMoneyLabel((int)val);
        }
    }
}
