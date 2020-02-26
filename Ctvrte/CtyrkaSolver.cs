using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConnector;

namespace Ctvrte
{
	public class CtyrkaSolver
	{
		public int Min { get; set; }
		public int Max { get; set; }
		private Random _rnd;
		private MainConnector _connector;

		public CtyrkaSolver(int min, int max)
		{
			Min = min;
			Max = max;
			_rnd = new Random();
			_connector = new MainConnector();
		}

		public async Task Solve()
		{
			await Jednicka();
			await Dvojka();
			Console.ReadLine();
		}

		public async Task Jednicka()
		{
			Console.WriteLine("První");
			for (double i = Min; i <= Max; i += 0.1) {
				var f = await _connector.GetModel(FuncName.funcF, i);
				var g = await _connector.GetModel(FuncName.funcG, i);

				double diff = f.yVal - g.yVal;
				if (Math.Abs(diff) < 2) {
					Console.WriteLine($"Pro x = {i} má funce G nižší hodnotu než fce F, a to o {diff}");
				}
			}
			Console.WriteLine();
			Console.WriteLine();
		}

		public async Task Dvojka()
		{
			/*var min = Min;
			var max = Max;

			Model first = await _connector.GetModel(FuncName.funcG, min);
			Model second;
			do {
				second = await this.GetRadom(min, max);
			} while (!(first.yVal > 0 && second.yVal < 1 ||
					   (first.yVal < 0 && second.yVal > 1)));


			var a = second.yVal;

			var g1 = await _connector.GetModel(FuncName.funcG, Min);
			var g2 = await _connector.GetModel(FuncName.funcG, Max);
			if (g1.yVal > 0 && g2.yVal < 1 ||
			   (g1.yVal < 0 && g2.yVal > 1)) {
				Console.WriteLine("AAA");
			}*/


			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Druhé");
			for (double i = Min; i <= Max; i += 0.25) {
				var g = await _connector.GetModel(FuncName.funcG, i);
				if (Math.Abs(g.yVal) < 0.001) {
					Console.WriteLine($"Pro x = {i} má fce G hodnotu {g.yVal}");
				}
			}
			var lastCheck = Math.PI;
			var last = await _connector.GetModel(FuncName.funcG, lastCheck);
			Console.WriteLine($"Pro x = {Trunc(lastCheck)} má fce G hodnotu {Trunc(last.yVal)}");
		}

		public Task<Model> GetRadom(double min, double max)
		{
			var rnd = this.GetRandomNumber(min, max);
			return _connector.GetModel(FuncName.funcG, rnd);
		}

		public double GetRandomNumber(double min, double max)
		{
			return _rnd.NextDouble() * (max - min) + min;
		}

		public double Trunc(double val)
		{
			return Math.Truncate(val * 100) / 100;
		}
	}
}