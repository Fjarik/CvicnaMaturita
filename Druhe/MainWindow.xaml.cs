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
		private MainConnector connector;

		public MainWindow()
		{
			connector = new MainConnector();
			InitializeComponent();

			var funcs = Enum.GetValues(typeof(FuncName)).Cast<FuncName>();

			var list = funcs.Select(x => new ComboBoxItem {
				Content = x.ToString(),
				Tag = x
			});

			CmBoxFuncNames.ItemsSource = list;
		}

		private async void GetVal_OnClick(object sender, RoutedEventArgs e)
		{
			if (!double.TryParse(TxtXvalue.Text, out double xVal)) {
				return;
			}
			var item = CmBoxFuncNames.SelectedItem as ComboBoxItem;
			var func = item?.Tag as FuncName?;

			if (func == null) {
				return;
			}

			var res = await connector.GetModel((FuncName) func, xVal);


			TxtYvalue.Text = res.yVal.ToString();
		}
	}
}