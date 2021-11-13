using Books_Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Services
{
    public class ApiServices
    {
		#region CLIENTE HTTP

		private HttpClient ClientRequest()
		{
			HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			
			client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/api/");

			return client;
		}

		#endregion

		#region GET

		public async Task<ResultModel> Get<T>(string Accion)
		{
			try
			{
				var url =  Accion;

				HttpClient client = ClientRequest();
				HttpResponseMessage response = await client.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					return new ResultModel
					{
						Success = false,
						Messages = response.StatusCode.ToString(),
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<List<T>>(result);

				return new ResultModel
				{
					Success = true,
					Messages = "OK",
					Data = data
				};
			}
			catch (Exception ex)
			{
				return new ResultModel
				{
					Success = false,
					Messages = ex.Message,
				};
			}
		}

		public async Task<ResultModel> Get<T>(string Accion, int parametro)
		{
			try
			{
				var url = string.Format("{0}/{1}", Accion,parametro);

				HttpClient client = ClientRequest();
				HttpResponseMessage response = await client.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					return new ResultModel
					{
						Success = false,
						Messages = response.StatusCode.ToString(),
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<List<T>>(result);

				return new ResultModel
				{
					Success = true,
					Messages = "OK",
					Data = data
				};
			}
			catch (Exception ex)
			{
				return new ResultModel
				{
					Success = false,
					Messages = ex.Message,
				};
			}
		}

		#endregion

		#region POST
		public async Task<ResultModel> Post<T>(string Accion, T Modelo)
		{
			try
			{
				HttpClient client = ClientRequest();

				var request = JsonConvert.SerializeObject(Modelo);
				var content = new StringContent(request, Encoding.UTF8, "application/json");
				var url = Accion;
				var response = await client.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					return new ResultModel
					{
						Success = false,
						Messages = response.ReasonPhrase,
					};
				}

				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<T>(result);

				return new ResultModel
				{
					Success = true,
					Messages = response.ReasonPhrase,
					Data = data,
				};
			}
			catch (Exception ex)
			{

				return new ResultModel
				{
					Success = false,
					Messages = ex.Message,
				};
			}
		}
		#endregion
	}
}
