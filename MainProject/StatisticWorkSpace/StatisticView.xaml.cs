using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainProject.StatisticWorkSpace
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : UserControl
    {
        public StatisticView()
        {
            InitializeComponent();
            Initialized += StatisticView_Initialized;
        }

        private void StatisticView_Initialized(object sender, EventArgs e)
        {
            StatisticViewModel viewModel = (StatisticViewModel)DataContext;
            if (viewModel != null)
            {
                //SeriesCollection = 
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar calendar = sender as Calendar;
            calendar.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Console.WriteLine("On selected date change");
        }
    }
}
