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
        public String CurrentMode_String => StatisticEnum.GetString(currentMode);
        public void SetCurrentMode(int index)
        {
            if (index < 0 && index >= Enum.GetValues(typeof(StatisticMode)).Length) return;
            currentMode = (StatisticMode)index;
        }

        public Func<double, string> formaterLabelAxisY { get; set; }

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
            formaterLabelAxisY = val => (val/1000).ToString("N0")+"k";
        }
    }
}
