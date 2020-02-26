using DataConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctvrte
{
	class Program
	{
		static void Main(string[] args)
		{
			Test().Wait();
		}

		public static async Task Test()
		{
			var connector = new MainConnector();
			var min = -4;
			var max = 8;
			Console.WriteLine("Prvni");
			for (double i = min; i <= max; i += 0.1) {
				var f = await connector.GetModel(FuncName.funcF, i);
				var g = await connector.GetModel(FuncName.funcG, i);

				double diff = f.yVal - g.yVal;
				if (Math.Abs(diff) < 2) {
					Console.WriteLine($"Pro x = {i} má funce G nižší hodnotu než fce F, a to o {diff}");
				}
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Druhe");
			for (double i = min; i <= max; i += 0.25) {
				var g = await connector.GetModel(FuncName.funcG, i);
				if (Math.Abs(g.yVal) < 0.001) {
					Console.WriteLine($"Pro x = {i} má fce G hodnotu {g.yVal}");
				}
			}

			Console.ReadKey();
		}
	}
}