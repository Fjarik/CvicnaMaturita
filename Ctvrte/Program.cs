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
			var solver = new CtyrkaSolver(min: -4, max: 8);
			solver.Solve().Wait();
		}
	}
}