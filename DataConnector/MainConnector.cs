using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
			var request = Url.SetQueryParams(new {
				funcName = func.ToString(),
				xVal,
			}).AllowHttpStatus(HttpStatusCode.NotFound);

			Model resp;
			try {
				resp = await request
						   .GetJsonAsync<Model>();
			} catch (FlurlHttpTimeoutException) {
				throw new TimeoutException("Špatné připojení k internetu");
			} catch (FlurlHttpException) {
				await Task.Delay(20);
				return await GetModel(func, xVal);
			}

			if (resp.Status == "error") {
				throw new InvalidCastException("Neplatné parametry");
			}

			resp.Func = func;
			resp.xVal = xVal;

			return resp;
		}
	}
}