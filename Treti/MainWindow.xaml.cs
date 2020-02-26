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
		private readonly MainConnector _connector;
		private bool _shouldCancel = false;

		public MainWindow()
		{
			_connector = new MainConnector();
			InitializeComponent();

			var funcs = Enum.GetValues(typeof(FuncName)).Cast<FuncName>();

			var list = funcs.Select(x => new ComboBoxItem {
				Content = x.ToString(),
				Tag = x
			});

			CmBoxFuncNames.ItemsSource = list;
			CmBoxFuncNames.SelectedIndex = 0;
		}

		private async void GetVal_OnClick(object sender, RoutedEventArgs e)
		{
			if (ChBoxAddSeries.IsChecked == false) {
				MainChart.Series.Clear();
			}
			_shouldCancel = false;

			if (!int.TryParse(TxtCount.Text, out int count)) {
				MessageBox.Show("Neplatná hodnota Počet");
				return;
			}
			if (!double.TryParse(TxtMin.Text, out double min)) {
				MessageBox.Show("Neplatná hodnota Minimum");
				return;
			}
			if (!double.TryParse(TxtMax.Text, out double max)) {
				MessageBox.Show("Neplatná hodnota Maximum");
				return;
			}
			if (max < min) {
				MessageBox.Show("Maximum je menší než minimum. (To je špatně)");
				return;
			}

			if (!(CmBoxFuncNames.SelectedItem is ComboBoxItem item) || !(item.Tag is FuncName func)) {
				MessageBox.Show("Neplatný výběr funkce");
				return;
			}
			GridLoading.Visibility = Visibility.Visible;

			double step = (double) (max - min) / (double) count;


			var models = new List<Model>();
			for (int i = 0; i < count; i++) {
				if (_shouldCancel) {
					// Při kliknutí na "Zrušit"
					GridLoading.Visibility = Visibility.Hidden;
					MessageBox.Show("Akce zrušena uživatelem");
					return;
				}

				var x = min + (i * step);
				var mod = await _connector.GetModel(func, x);
				models.Add(mod);
			}

			var points = models.Select(m => new ObservablePoint(m.xVal, m.yVal));

			var values = new ChartValues<ObservablePoint>(points);

			var serie = new ScatterSeries() {
				Values = values,
				Name = "Hodnoty"
			};

			MainChart.Series.Add(serie);

			GridLoading.Visibility = Visibility.Hidden;
		}

		private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
		{
			this._shouldCancel = true;
		}
	}
}