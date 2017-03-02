using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Reflection;

namespace Wapps
{
	public abstract class RestAPICall
	{
		protected bool UseMockResponse { get; set; }

		protected string Url { get; set; }

		protected bool Log { get; set; }

		protected bool HeaderApplicationJson { get; set; } = false;

		public RestAPICall()
		{
			this.UseMockResponse = false;
			this.Log = false;
		}

		public virtual string GetMockResponse(string api, object input)
		{
			return "";
		}

		#region Private

		protected async Task<TOut> PutAsync<TOut>(string api, object input = null) where TOut : new()
		{
			return await PostAsync<TOut>(api, input, "PUT");
		}

		protected async Task<TOut> PostAsync<TOut>(string api, object input = null, Stream file = null) where TOut : new()
		{
			return await PostAsync<TOut>(api, input, "POST", file);
		}

		private async Task<TOut> PostAsync<TOut>(string api, object input = null, string method = "POST", Stream file = null) where TOut : new()
		{
			if (UseMockResponse)
			{
				return await Task.Run<TOut>(async delegate
				{
					await Task.Delay(1000);
					var strResponse = this.GetMockResponse(api, input);
					if (typeof(TOut) != typeof(NoResponse))
					{
						var output = (TOut)this.Deserialize(strResponse, typeof(TOut));
						return output;
					}
					else
					{
						return default(TOut);
					}
				});
			}

			HttpClient client = new HttpClient();

			var uri = new Uri(this.Url + api);
			HttpContent httpContent;

			if (file != null)
			{
				httpContent = new StreamContent(file);
			}
			else
			{
				var json = this.Serialize(input);
				Debug.WriteLine(json);
				if (HeaderApplicationJson)
					httpContent = new StringContent(json, Encoding.UTF8, "application/json");
				else
					httpContent = new StringContent(json);
			}

			this.WillSendRequest(client);

			if (this.Log)
			{
				Debug.WriteLine("REQUEST:");
				Debug.WriteLine(client.ToString());
			}

			HttpResponseMessage response;
			if (method.ToUpper() == "PUT")
			{
				response = await client.PutAsync(uri, httpContent);
			}
			else {
				response = await client.PostAsync(uri, httpContent);
			}

			if (this.Log)
			{
				Debug.WriteLine("RESPONSE:");
				Debug.WriteLine(response.ToString());
			}

			if (response.IsSuccessStatusCode)
			{
				var strResponse = await response.Content.ReadAsStringAsync();
				if (typeof(TOut) != typeof(NoResponse))
				{
					var output = (TOut)this.Deserialize(strResponse, typeof(TOut));
					return output;
				}
				else
				{
					return default(TOut);
				}
			}
			else {
				var message = await response.Content.ReadAsStringAsync();
				Deserialize(message, typeof(TOut), (int)response.StatusCode);
				throw new RestAPICallException(message, response.StatusCode.ToString());
			}
		}

		protected async Task<TOut> GetAsync<TOut>(string api, dynamic parameters = null) where TOut : new()
		{
			var dicParameters = parameters as IDictionary<string, object>;

			if (this.UseMockResponse)
			{
				return await Task.Run<TOut>(async delegate
				{
					await Task.Delay(1000);

					var strResponse = this.GetMockResponse(api, dicParameters);
					if (typeof(TOut) != typeof(NoResponse))
					{
						var output = (TOut)this.Deserialize(strResponse, typeof(TOut));
						return output;
					}
					else
					{
						return default(TOut);
					}
				});
			}

			HttpClient client = new HttpClient();

			var url = this.Url + api;

			if (parameters != null)
			{
				string queryString = this.QueryString(dicParameters);
				url = string.Format("{0}?{1}", url, queryString);
			}

			this.WillSendRequest(client);

			if (this.Log)
			{
				Debug.WriteLine("REQUEST:");
				Debug.WriteLine(client.ToString());
			}

			var response = await client.GetAsync(url);

			if (this.Log)
			{
				Debug.WriteLine("RESPONSE:");
				Debug.WriteLine(response.ToString());
			}

			if (response.IsSuccessStatusCode)
			{
				var strResponse = await response.Content.ReadAsStringAsync();
				if (typeof(TOut) != typeof(NoResponse))
				{
					var output = (TOut)this.Deserialize(strResponse, typeof(TOut));
					return output;
				}
				else
				{
					return default(TOut);
				}
			}
			else {
				var message = await response.Content.ReadAsStringAsync();
				Deserialize(message, typeof(TOut), (int)response.StatusCode);
				throw new RestAPICallException(message, response.StatusCode.ToString());
			}
		}

		protected virtual string Serialize(object input)
		{
			var json = JsonConvert.SerializeObject(input);
			return json;
		}

		protected virtual object Deserialize(string output, Type type, int code = 200)
		{
			return JsonConvert.DeserializeObject(output, type);
		}

		string QueryString(IDictionary<string, object> dic)
		{
			if (dic == null)
			{
				return "";
			}

			var parameters = dic;
			var url = "";
			int i = 0;
			foreach (var item in parameters)
			{
				url += item.Key + "=";
				if (!string.IsNullOrEmpty(item.Value.ToString()))
				{
					url += item.Value;
				}
				url += i != parameters.Count ? "&" : string.Empty;
				i++;
			}

			return url;
		}

		protected string ReadMockFile(string resourceId)
		{
			var assembly = this.GetType().GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream(resourceId);
			string text = "";
			using (var reader = new System.IO.StreamReader(stream))
			{
				text = reader.ReadToEnd();
			}
			return text;
		}

		#endregion

		protected virtual void WillSendRequest(HttpClient client)
		{

		}
	}

	public class NoResponse
	{

	}

	public class RestAPICallException : Exception
	{
		public string Code { get; set; }

		public RestAPICallException(string message, string code) : base(message)
		{
			this.Code = code;
		}
	}

}

