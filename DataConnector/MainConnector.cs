using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace DataConnector
{
	public class MainConnector
	{
		public readonly string Url = "http://maturita.delta-studenti.cz/prakticka/cvicna-tajne-funkce/tajne-funkce.php";

		public async Task<Model> GetModel(FuncName func, double xVal)
		{
			var resp = await Url.SetQueryParams(new {
				funcName = func.ToString(),
				xVal,
			}).GetJsonAsync<Model>();

			resp.Func = func;
			resp.xVal = xVal;

			return resp;
		}
	}
}