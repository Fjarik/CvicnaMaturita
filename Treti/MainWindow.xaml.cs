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
using DataConnector;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Treti
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainConnector connector;

		public MainWindow()
		{
			connector = new MainConnector();
			InitializeComponent();

			var funcs = Enum.GetValues(typeof(FuncName)).Cast<FuncName>();

			var list = funcs.Select(x => new ComboBoxItem {
				Content = x.ToString(),
				Tag = x,
				IsSelected = x == FuncName.funcF
			});

			CmBoxFuncNames.ItemsSource = list;
			CmBoxFuncNames.SelectedIndex = 1;
		}

		private async void GetVal_OnClick(object sender, RoutedEventArgs e)
		{
			//MainChart.Series.Clear();
			if (!int.TryParse(TxtCount.Text, out int count)) {
				return;
			}
			if (!int.TryParse(TxtMin.Text, out int min)) {
				return;
			}
			if (!int.TryParse(TxtMax.Text, out int max)) {
				return;
			}
			if (max < min) {
				return;
			}
			var item = CmBoxFuncNames.SelectedItem as ComboBoxItem;
			var func = item?.Tag as FuncName?;

			if (func == null) {
				return;
			}

			double step = (double) (max - min) / (double) count;


			var models = new List<Model>();
			for (int i = 0; i < count; i++) {
				var x = min + (i * step);
				var mod = await connector.GetModel((FuncName) func, x);
				await Task.Delay(10);
				models.Add(mod);
			}

			var values = models.Select(x => new ObservablePoint(x.xVal, x.yVal));

			ChartValues<ObservablePoint> val = new ChartValues<ObservablePoint>(values);

			var serie = new ScatterSeries() {
				Values = val,
				Name = "Hodnoty"
			};


			MainChart.Series.Add(serie);
		}
	}
}