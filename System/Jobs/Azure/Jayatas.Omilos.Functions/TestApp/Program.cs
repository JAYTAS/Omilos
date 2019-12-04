using Jayatas.Omilos.Functions.Common.Common;
using Jayatas.Omilos.Functions.Common.ServiceClients.Bitly;
using Jayatas.Omilos.Functions.Notification.SendSmsNotification;
using Newtonsoft.Json;
using Refit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine((int)(Math.Round(59 / 5.0) * 5));
			//var bitlyApi = RestService.For<IBitlyClient>(Constants.Common.BitlyHostName);
			var bearerToken = $"Bearer 1e6bf31d386cf1fc43aa561c1518285409bb09ad";

			var shortenRequestModel = new ShortenRequestModel
			{
				LongUrl = "Https://google.com",
				GroupId = "Bib9bo351Bj"
			};

			SendSmsNotification.Run(null, null);

			var shortenUri = RestClient.PostAsync<ShortenResponseModel>(Constants.Common.BitlyHostName, "/v4/shorten", "1e6bf31d386cf1fc43aa561c1518285409bb09ad", JsonConvert.SerializeObject(shortenRequestModel)).GetAwaiter().GetResult();

			//var shortenUri = bitlyApi.Shorten(bearerToken, shortenRequestModel).GetAwaiter().GetResult();

			var d1 = new DateTime(2018, 11, 01);
			var d2 = new DateTime(2018, 11, 03);

			Console.WriteLine(d2 >= d1);

			var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
			var centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
			var pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
			var mountainZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");

			var utcTime = DateTime.UtcNow;
			var localTime = DateTime.Now;

			Console.WriteLine("UTC Time :" + utcTime);
			Console.WriteLine("Local Time :" + localTime);
			Console.WriteLine("EST Time :" + TimeZoneInfo.ConvertTime(utcTime, easternZone));
			Console.WriteLine("CST Time :" + TimeZoneInfo.ConvertTime(utcTime, centralZone));
			Console.WriteLine("PST Time :" + TimeZoneInfo.ConvertTime(utcTime, pacificZone));
			Console.WriteLine("MST Time :" + TimeZoneInfo.ConvertTime(utcTime, mountainZone));

			Console.WriteLine("Hello World!");
			SendSmsNotification.Run(null, null);
			Console.ReadLine();
		}
	}
}
