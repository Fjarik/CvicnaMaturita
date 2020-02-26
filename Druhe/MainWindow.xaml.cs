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

namespace Druhe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainConnector _connector;

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
			if (!double.TryParse(TxtXvalue.Text, out double xVal)) {
				MessageBox.Show("Neplatná hodnota X");
				return;
			}
			if (!(CmBoxFuncNames.SelectedItem is ComboBoxItem item) || !(item.Tag is FuncName func)) {
				MessageBox.Show("Neplatný výběr funkce");
				return;
			}
			GridLoading.Visibility = Visibility.Visible;

			var res = await _connector.GetModel(func, xVal);

			TxtYvalue.Text = res.yVal.ToString();

			GridLoading.Visibility = Visibility.Hidden;
		}
	}
}