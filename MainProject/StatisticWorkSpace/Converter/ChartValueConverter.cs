using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LiveCharts;

namespace MainProject.StatisticWorkSpace.Converter
{
    class ChartValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<StatisticModel> list)
            {
                ChartValues<int> rs = new ChartValues<int>();
                foreach (var model in list)
                {
                    rs.Add(model.Revenue);
                }
                return rs;
            }
            else { throw new NotImplementedException(); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
