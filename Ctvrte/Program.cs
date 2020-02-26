﻿using DataConnector;
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
			for (int i = min; i <= max; i++) {
				var f = await connector.GetModel(FuncName.funcF, i);
				var g = await connector.GetModel(FuncName.funcG, i);

				double diff = f.yVal - g.yVal;
				if (diff > 0) {
					Console.WriteLine($"Pro x = {i} má funce G nižší hodnotu než fce F, a to o {diff}");
				}
			}

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