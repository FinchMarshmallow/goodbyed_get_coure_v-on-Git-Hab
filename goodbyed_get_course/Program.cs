


//using OpenQA.Selenium;
//using OpenQA.Selenium.BiDi.Communication;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Interactions;
using System.Diagnostics;
//
//using OpenQA.Selenium.DevTools;
//using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//
//
//using DevToolsVer = OpenQA.Selenium.DevTools.V85;
using System.Collections.Concurrent;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using OpenQA.Selenium.DevTools;

using OpenQA.Selenium.DevTools.V129;

using System.Collections.ObjectModel;
using OpenQA.Selenium.Internal.Logging;
using System.Net;
using OpenQA.Selenium.BiDi.Modules.Network;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
//using OpenQA.Selenium.DevTools.V129.Fetch;


using System;
using System.Diagnostics;
using System.Management;

namespace goodbyed_get_course
{
	internal class Program
	{
		private static Process driverStartProcess;


		private static ConsoleColor defBackground = (ConsoleColor)0; // чёрный
		private static ConsoleColor defForegroun = (ConsoleColor)7; // серый

		private static string? pathToBat = null;

		private static Process? consolle;
		private static Process? answerWindow;

		private static List<Process> answersWindows = new();

		static async Task Main(string[] args)
		{
			pathToBat = Environment.CurrentDirectory;
			pathToBat = pathToBat.Replace("goodbyed_get_course\\bin\\Debug\\net8.0", "PYBNCD\\BNCD1");




			// тест боди для хеша
			//NewAnswerWindow(@"{""success"":true,""data"":{""resultHash"":""eyJzaG93X2Fuc3dlcl9yZXN1bHQiOiJzZWxlY3RlZCIsInF1ZXN0aW9uIjp7ImlkIjo1MTM1ODM4MCwidGl0bGUiOiJcdTA0MWFcdTA0MzBcdTA0M2FcdTA0M2VcdTA0MzJcdTA0M2UgXHUwNDM3XHUwNDNkXHUwNDMwXHUwNDQ3XHUwNDM1XHUwNDNkXHUwNDM4XHUwNDM1IFx1MDQ0MVx1MDQzOFx1MDQzZFx1MDQ0M1x1MDQ0MVx1MDQzMCBcdTA0NDNcdTA0MzNcdTA0M2JcdTA0MzAgMCBcdTA0MzNcdTA0NDBcdTA0MzBcdTA0MzRcdTA0NDNcdTA0NDFcdTA0M2VcdTA0MzI/IiwiZGVzY3JpcHRpb24iOiIiLCJ2YXJpYW50cyI6W3siaWQiOjE3Mzg0ODk5MywidmFsdWUiOiIxIiwiaXNfcmlnaHQiOjAsInBhcmFtcyI6eyJyaWdodF90ZXh0IjoiIiwiZXJyb3JfdGV4dCI6IiJ9LCJwb2ludHMiOm51bGx9LHsiaWQiOjE3Mzg0ODk5NCwidmFsdWUiOiItMSIsImlzX3JpZ2h0IjowLCJwYXJhbXMiOnsicmlnaHRfdGV4dCI6IiIsImVycm9yX3RleHQiOiIifSwicG9pbnRzIjpudWxsfSx7ImlkIjoxNzM4NDg5OTUsInZhbHVlIjoiMC41IiwiaXNfcmlnaHQiOjAsInBhcmFtcyI6eyJyaWdodF90ZXh0IjoiIiwiZXJyb3JfdGV4dCI6IiJ9LCJwb2ludHMiOm51bGx9LHsiaWQiOjE3NDA3NjA4OCwidmFsdWUiOiJcdTA0MWUiLCJpc19yaWdodCI6MSwicGFyYW1zIjp7InJpZ2h0X3RleHQiOiIiLCJlcnJvcl90ZXh0IjoiIn0sInBvaW50cyI6bnVsbH1dLCJwYXJhbXMiOnsicmlnaHRfaW1hZ2UiOm51bGwsInJpZ2h0X3RleHQiOiIiLCJlcnJvcl90ZXh0IjoiIiwicmlnaHRfcG9pbnRzIjoiIiwicmlnaHRfaWZfYWxsIjp0cnVlLCJyZXF1aXJlZF9xdWVzdGlvbiI6ZmFsc2V9fX0="",""isLastQuestion"":false,""qrid"":null,""question_id"":51358380,""resultHtml"":""\n\t\n\t\t<div class=\""questionary-title-wrapper\"">\n\t\t\t<h3 class=\""questionary-title\"">\n\t\t\t\t<span>Тригонометрические функции</span><hr>\n\t\t\t</h3>\n\t\t</div>\n\n\t\t                    \t\t\t\t\n\t\t<div class=\""question\"">\n\t\t\t\t\t\t<div class=\""question-number text-center\"">\n\t\t\t\tВопрос №2 из 10\t\t\t</div>\n\n\t\t\t\t\t\t\t<div class=\""question-multi-answers-hint text-center\"">\n\t\t\t\t\tВыберите один или несколько вариантов ответа\t\t\t\t</div>\n\t\t\t\n\t\t\t<div class=\""question-title-big\"">\n\t\t\t\tКаково значение синуса угла 0 градусов?\t\t\t</div>\n\n\t\t\t\t\t\t\t\t\t\t\t\t<div class=\""question-data row\"">\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<div class=\""question-answer-block  col-md-12 \"">\n\t\t\t\t\t<div class=\""button-list\"">\n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n\t\t\t\t\t\n\t\t\t\t\t\t\t\t\t\t\n\t\t\t\t\t\t<button\n\t\t\t\t\t\t\t\tclass=\""btn btn-default btn-mark-variant js__btn-variant\""\n\t\t\t\t\t\t\t\ttype=\""button\""\n\t\t\t\t\t\t\t\tdata-marked=\""0\""\n\t\t\t\t\t\t\t\tdata-question-id=\""51358380\""\n\t\t\t\t\t\t\t\tdata-value=\""-1\""\n\t\t\t\t\t\t\t>\n\t\t\t\t\t\t\t-1\n\t\t\t\t\t\t</button>\n\t\t\t\t\t\t<button\n\t\t\t\t\t\t\t\tclass=\""btn btn-default btn-mark-variant js__btn-variant\""\n\t\t\t\t\t\t\t\ttype=\""button\""\n\t\t\t\t\t\t\t\tdata-marked=\""0\""\n\t\t\t\t\t\t\t\tdata-question-id=\""51358380\""\n\t\t\t\t\t\t\t\tdata-value=\""1\""\n\t\t\t\t\t\t\t>\n\t\t\t\t\t\t\t1\n\t\t\t\t\t\t</button>\n\t\t\t\t\t\t<button\n\t\t\t\t\t\t\t\tclass=\""btn btn-default btn-mark-variant js__btn-variant\""\n\t\t\t\t\t\t\t\ttype=\""button\""\n\t\t\t\t\t\t\t\tdata-marked=\""0\""\n\t\t\t\t\t\t\t\tdata-question-id=\""51358380\""\n\t\t\t\t\t\t\t\tdata-value=\""0.5\""\n\t\t\t\t\t\t\t>\n\t\t\t\t\t\t\t0.5\n\t\t\t\t\t\t</button>\n\t\t\t\t\t\t<button\n\t\t\t\t\t\t\t\tclass=\""btn btn-default btn-mark-variant js__btn-variant\""\n\t\t\t\t\t\t\t\ttype=\""button\""\n\t\t\t\t\t\t\t\tdata-marked=\""0\""\n\t\t\t\t\t\t\t\tdata-question-id=\""51358380\""\n\t\t\t\t\t\t\t\tdata-value=\""О\""\n\t\t\t\t\t\t\t>\n\t\t\t\t\t\t\tО\n\t\t\t\t\t\t</button>\n\t\t\t\t\t\t\t\t\t\t\t<button class=\""btn btn-primary btn-send-all-variants btn-send-variant\""\n\t\t\t\t\t\t\t\ttype=\""button\""\n\t\t\t\t\t\t\t\tstyle=\""display: none;\""\n\t\t\t\t\t\t\t\tdata-question-id=\""51358380\""\n\t\t\t\t\t\t\t\tdata-value=\""\"">Ответить</button>\n\t\t\t\t\t\n\t\t\t\t\t</div>\n\t\t\t\t</div>\n\t\t\t</div>\n\t\t</div>\n\n\t\t\t\n<script>\n\tconsole.log('2');\n\twindow.lessonId = ;\n\t\twindow.isEnabledMobileTesting = 0;\n\n\t$(function () {\n\t\t$('#conversation-link').on('click', function () {\n\t\t\tif (window.appHandleAction) {\n\t\t\t\twindow.appHandleAction({\n\t\t\t\t\ttype: 'navigate',\n\t\t\t\t\turl: location.origin + '/c/s/resp/my'\n\t\t\t\t})\n\t\t\t} else {\n\t\t\t\tlocation.href = '/pl/talks/conversation';\n\t\t\t}\n\t\t})\n\t});\n</script>\n<style>\n    .fsz-14px {\n        font-size: 14px!important;\n    }\n\n    .video-wrapper {\n\t    text-align: center;\n    }\n\n    .testing-restart-timer {\n\t    color: #808080;\n\t    font-size: 34px;\n\t    display: block;\n    }\n</style>\n""},""message"":null}");



			driverStartProcess = new Process();
			
			driverStartProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
			driverStartProcess.StartInfo.Arguments = "--remote-debugging-port=9222 --user-data-dir= \"C:\\Users\\Sima\\AppData\\Local\\Google\\Chrome\\User Data\\Default\"";

			driverStartProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized; 

			driverStartProcess.Start();


			ChromeOptions options = new ChromeOptions();
			options.SetLoggingPreference(LogType.Browser, LogLevel.All);
			options.AddArgument("w3c:false");
			options.DebuggerAddress = "localhost:9222";
			
			
			var driver = new ChromeDriver(options);
			driver.Navigate().GoToUrl("https://irbis-edu.getcourse.ru/teach/control/stream/index");

			int numBackgroundColor = 0;
			int time =0;


			var xhrRequest = new List<string>();
			var postDatasRequest = new List<string>();

			var xhrRespon = new List<string>();
			var postDatasRespon = new List<string>();

			var requestHandler = new NetworkRequestHandler();
			var responHandler = new NetworkResponseHandler();




			requestHandler.RequestTransformer = (request) => { return request; };
			requestHandler.RequestMatcher = httprequest =>
			{
				//xhrRequest.Add(httprequest.Url);
				//postDatasRequest.Add(httprequest.PostData);

				return false;
			};




			IDevTools IDevTools = driver;
			var devTools = driver.GetDevToolsSession();

			DevToolsSession session = IDevTools.GetDevToolsSession();
			//await session.Domains.Network.EnableNetwork();


			INetwork networkInterceptor = driver.Manage().Network;
			await networkInterceptor.StartMonitoring();



			networkInterceptor.AddRequestHandler(requestHandler);
			await networkInterceptor.StartMonitoring();


			var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.DevToolsSessionDomains>();
			var network = session.Domains.Network;

			Task enableNetwork = network.EnableNetwork();
			Task networkCaching = network.EnableNetworkCaching();
			enableNetwork.Wait();
			networkCaching.Wait();


			NetworkManager manager = new NetworkManager(driver);
			networkInterceptor.NetworkResponseReceived += (object sender, NetworkResponseReceivedEventArgs e) =>
			{
				string body = e.ResponseBody;
				if (body == null || body == "" || !body.Contains(@"""resultHash"":")) return;

				Console.BackgroundColor =(ConsoleColor)6;
				Console.ForegroundColor = (ConsoleColor)10;

				Console.WriteLine($":\n {e.ResponseBody} \n:");

				Console.BackgroundColor = (ConsoleColor)0; // чёрный
				Console.ForegroundColor = (ConsoleColor)7; // серый



				NewAnswerWindow(body);






				// all: Console.WriteLine($"Http status: {e.ResponseStatusCode} : {e.ResponseBody} | Url: {e.ResponseUrl} ");
			};

			Task monitoring = manager.StartMonitoring();
			monitoring.Wait();






			responHandler.ResponseTransformer = (request) => { return request; };
			responHandler.ResponseMatcher += respon =>
			{
				//xhrRespon.Add(respon.Url);
				//postDatasRespon.Add(respon?.ToString());

				HttpResponseData responseData = respon;

				Console.WriteLine($"respon.Url: {respon.Url}");
				Console.WriteLine($"respon.StatusCode: {responseData.StatusCode}");
				Console.WriteLine($"respon.RequestId: {responseData.RequestId}");
				Console.WriteLine($"respon.Body: {responseData.Body}");
				Console.WriteLine($"respon.Content: {responseData.Content}");
				Console.WriteLine($"respon.CookieHeaders: {responseData.CookieHeaders}");

				return false;
			};


			while (true)
			{
			





				//evTools.("Network.responseReceived", args =>
				//
				//	var response = args.Response;
				//	Console.WriteLine($"Received Response for: {response.Url} with status: {response.Status}");
				//
				//	// Пример проверки: если статус 200, выводим содержимое
				//	if (response.Status == 200)
				//	{
				//		Console.WriteLine("Response body can be checked here (if needed).");
				//		// Здесь можно добавить логику для проверки или изменения ответа
				//	}
				//);


				//await devTools.SendCommand("Network.enable", networkInterceptor.);

				//session.DevToolsEventReceived += (sender, e) =>
				//{
				//	//var response = e.EventData.AsValue();
				//	//Console.WriteLine($"AsValue: {response.GetValue<string>()}");
				//	//Console.WriteLine($"Status: {e.E}");
				//	
				//	
				//	//if (!e.EventData.ToString().Contains("")) return;
				//	
				//	//Console.WriteLine($"Network \n: {e.EventData}");
				//};



				//var enableTask = session.Domains.Network.EnableNetwork();
				//enableTask.Wait();





				//session.DevToolsEventReceived += (sender, e) =>
				//{
				//	Console.WriteLine($"Network: \n: {e.EventData}");
				//
				//	DevToolsSession session1 = sender as DevToolsSession;
				//
				//	Console.WriteLine($"Sender: \n: {session1?.Equals()}");
				//};



				// Устанавливаем условия для проверки ответов






				//if (xhrRequest.Count == 0) continue;
				//
				//for(int i = 0; i < xhrRequest.Count; i++)
				//{
				//	//Console.WriteLine(xhrUrls[i]);
				//	//Console.WriteLine("postDatas: " + postDatas[i]);
				//}
				//
				//for (int i = 0; i < xhrRespon.Count; i++)
				//{
				//	//Console.WriteLine(xhrRespon[i]);
				//	//Console.WriteLine("postDatas: " + postDatasRespon[i]);
				//}
				//
				//var entries = driver.Manage().Logs.GetLog(LogType.Browser);
				//foreach (var entry in entries)
				//{
				//	//Console.WriteLine(entry.ToString());
				//	
				//}


				//var Tools = driver.GetDevToolsSession();

				//devTools.SendCommand(new Command("Network.enable", " ")); // Включаем сетевые функции



				// Переход к нужному URL

				// Ваш дальнейший код тестирования








				// Переход на целевую страницу
				//driver.Navigate().GoToUrl("https://example.com");

				// Другие действия, вызывающие сетевые запросы, могут быть здесь

				// Ждем, чтобы все сетевые запросы завершились (можно заменять по необходимости)
				//System.Threading.Thread.Sleep(5000);




				//driver.Navigate().GoToUrl("https://example.com");
				//
				//// Получаем DevTools
				//var devTools = driver.GetDevToolsSession();
				//devTools.SendCommand("Network.enable");
				//
				//// Обработчик для получения ответов
				//devTools.MessageReceived += (sender, e) =>
				//{
				//	if (e.MessageID == "Network.responseReceived")
				//	{
				//		var response = e.GetEventBody<NetworkResponseReceivedEventArgs>();
				//		Console.WriteLine($"Response URL: {response.Response.Url}");
				//		Console.WriteLine($"Status: {response.Response.Status}");
				//	}
				//};

				// Выполняем действия на сайте, которые вызывают сетевые запросы
				// Например: driver.FindElement(By.Id("someId")).Click();



				//IReadOnlyCollection<string> a = driver.Manage().Logs.AvailableLogTypes;
				//
				//foreach (string log in a)
				//{
				//
				//	Console.WriteLine($"log: {log}");
				//	Console.WriteLine($"{log}: {driver.Manage().Logs.GetLog(log)}");
				//
				//	var masageLog = driver.Manage().Logs.GetLog(log);
				//
				//	foreach(var mas in masageLog)
				//	{
				//		Console.WriteLine($"mas: {mas.ToString()}");
				//	}
				//}

				numBackgroundColor++;

				if (numBackgroundColor > 15)
					numBackgroundColor = 0;

				Random random = new Random(time);
				time++;

				Console.BackgroundColor = (ConsoleColor)numBackgroundColor;
				Console.ForegroundColor = (ConsoleColor)random.Next(0, 15);

				Console.WriteLine("___Loading...___");

				Console.BackgroundColor = 0;
				Console.ForegroundColor = (ConsoleColor)7;

				Console.Write("                     ");

				await Task.Delay(1200);
			}


			//while (true)
			//{

			//	var xhrUrls = new List<string>();
			//	var handler = new NetworkRequestHandler();
			//	handler.RequestTransformer = (request) => { return request; };
			//	handler.RequestMatcher = httprequest =>
			//	{
			//		xhrUrls.Add(httprequest.Url);
			//		return false;
			//	};

			//	INetwork networkInterceptor = driver.Manage().Network;
			//	networkInterceptor.AddRequestHandler(handler);

			//	//networkInterceptor.StartMonitoring();

			//	//driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

			//	foreach (var url in xhrUrls)
			//	{
			//		Console.WriteLine(url);
			//	}



			//	await Task.Delay(800);
			//}

			//IDevToolsSession Session;
			//IDevTools Tools;
			//DevToolsVer.DevToolsSessionDomains devTools;
			//
			//
			//
			//driver.Navigate().GoToUrl("https://irbis-edu.getcourse.ru/teach/control/stream/index");
			//
			//Tools = driver;
			//
			//Session = Tools.GetDevToolsSession();
			//
			//devTools = Session.GetVersionSpecificDomains<DevToolsVer.DevToolsSessionDomains>();
			//
			//
			//
			//
			//
			//// Включаем перехват сетевых запросов
			////await devTools.SendCommandAsync("Network.enable");
			//
			//
			//// Список для хранения приходящих XHR-запросов
			//List<string> xhrRequests = new List<string>();
			//
			//// Обработка отправляемых запросов
			//devTools.Network.RequestWillBeSent += (sender, e) =>
			//{
			//	Console.WriteLine("RequestWillBeSent Looding...");
			//	if (e.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
			//		e.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
			//	{
			//		Console.WriteLine($"Request Sent: {e.Request.Url}");
			//		xhrRequests.Add(e.Request.Url);
			//	}
			//};
			//
			//// Обработка полученных ответов
			//devTools.Network.ResponseReceived += (sender, e) =>
			//{
			//	Console.WriteLine($"Response Received: {e.Response.Url} with Status: {e.Response.Status}");
			//};
			//
			//// Переход на нужный URL
			//driver.Navigate().GoToUrl("https://www.example.com");
			//
			//// Даем странице немного времени для загрузки и обработки XHR-запросов
			//await Task.Delay(10000); // Пауза в 10 секунд
			//
			//// Вывод XHR-запросов
			//Console.WriteLine("XHR Requests:");
			//foreach (var request in xhrRequests)
			//{
			//	Console.WriteLine(request);
			//}
			//
			//driver.Quit();


			//// Список для хранения приходящих XHR-запросов
			//List<string> xhrRequests = new List<string>();
			//
			//// Обработка отправляемых запросов
			//devTools.Network.RequestWillBeSent += (sender, e) =>
			//{
			//	if (e.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
			//		e.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
			//	{
			//		Console.WriteLine($"Request Sent: {e.Request.Url}");
			//		xhrRequests.Add(e.Request.Url);
			//	}
			//};
			//
			//// Обработка полученных ответов
			//devTools.Network.ResponseReceived += (sender, e) =>
			//{
			//	Console.WriteLine($"Response Received: {e.Response.Url} with Status: {e.Response.Status}");
			//};
			//
			//// Переход на нужный URL (например, Google)
			//driver.Navigate().GoToUrl("https://www.example.com");
			//
			//// Даем странице немного времени для загрузки и обработки XHR-запросов
			//await Task.Delay(10000); // Пауза в 10 секунд, измените в зависимости от вашего случая
			//
			//// Здесь вы можете вывести или сохранить XHR-запросы
			//Console.WriteLine("XHR Requests:");
			//foreach (var request in xhrRequests)
			//{
			//	Console.WriteLine(request);
			//}
			//
			//// Закрыть драйвер
			//driver.Quit();




			/*
			//	private static IWebDriver Driver = null;
			//private static IDevTools Tools = null;
			//private static IDevToolsSession Session = null;
			//private static DevToolsVer.DevToolsSessionDomains Domains = null;

			//private static 

			//public struct Response
			//{
			//	public string RequestId { get; set; }
			//	public string ResponseUrl { get; set; }
			//	public long ResponseStatus { get; set; }
			//	public bool ResponseBodySuccess { get; set; }
			//	public string ResponseBody { get; set; }
			//}

			//private static async Task Main()
			//{
			//	// Инициализация

			//	Driver = new ChromeDriver();
			//	Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
			//	Driver.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 30);
			//	Driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, 30);
			//	Driver.Manage().Window.Maximize();

			//	Tools = Driver as IDevTools;

			//	Session = Tools.GetDevToolsSession();

			//	Domains = Session.GetVersionSpecificDomains<DevToolsVer.DevToolsSessionDomains>();
			//	await Domains.Network.Enable(new DevToolsVer.Network.EnableCommandSettings());

			//	// Подготавливаем хранилище данных
			//	CollectionXHR = new ConcurrentBag<Task<Response>>();

			//	// Включаем запись получаемых данных
			//	Domains.Network.ResponseReceived += ResponseReceived;

			//	// Какие-то действия на сайте
			//	Instagram("username", "password"); // ! ввести логин и пароль

			//	// Отключаем запись получаемых данных
			//	Domains.Network.ResponseReceived -= ResponseReceived;

			//	// Ожидаем завершение извлечения полученных данных
			//	Task.WaitAll(CollectionXHR.ToArray());

			//	// Количество неудачных извлечений полученных данных 
			//	var failResponseBody = CollectionXHR.Where(w => w.Result.ResponseBodySuccess == false).Count();

			//	// Выводим данные неудачных извлечений полученных данных 
			//	string failDescr = string.Empty;
			//	foreach (var i in CollectionXHR.Where(w => w.Result.ResponseBodySuccess == false).ToList())
			//	{
			//		failDescr += $"RequestId = {i.Result.RequestId} | ResponseStatus = {i.Result.ResponseStatus} | ResponseBodySuccess = {i.Result.ResponseBodySuccess} \n";
			//	}
			//}

			//private static void ResponseReceived(object sender, DevToolsVer.Network.ResponseReceivedEventArgs e)
			//{
			//	if (e.Type == DevToolsVer.Network.ResourceType.XHR)
			//	{
			//		CollectionXHR.Add(GetResponseBodyAsync(e));
			//	}
			//}

			//private static async Task<Response> GetResponseBodyAsync(DevToolsVer.Network.ResponseReceivedEventArgs e)
			//{
			//	try
			//	{
			//		var cmd = new DevToolsVer.Network.GetResponseBodyCommandSettings();
			//		cmd.RequestId = e.RequestId;

			//		var data = await Domains.Network.GetResponseBody(cmd);

			//		return new Response()
			//		{
			//			RequestId = e.RequestId,
			//			ResponseUrl = e.Response.Url,
			//			ResponseStatus = e.Response.Status,
			//			ResponseBodySuccess = true,
			//			ResponseBody = data.Body
			//		};
			//	}
			//	catch
			//	{
			//		return new Response()
			//		{
			//			RequestId = e.RequestId,
			//			ResponseUrl = e.Response.Url,
			//			ResponseStatus = e.Response.Status,
			//			ResponseBodySuccess = false,
			//			ResponseBody = null
			//		};
			//	}
			//}

			//private static void Instagram(string username, string password)
			//{
			//	// Переходим на instagram.com
			//	Driver.Navigate().GoToUrl("https://www.instagram.com/");

			//	// Вводим логин и пароль
			//	{
			//		var byUsernameInput = By.XPath("//form[@id='loginForm']//input[@name='username']");
			//		var byPasswordInput = By.XPath("//form[@id='loginForm']//input[@name='password']");
			//		var byLoginButton = By.XPath("//form[@id='loginForm']//button[@type='submit']");

			//		if (Driver.FindElements(byLoginButton).Count > 0)
			//		{
			//			Driver.FindElement(byUsernameInput).SendKeys(username);
			//			Driver.FindElement(byPasswordInput).SendKeys(password);
			//			Driver.FindElement(byLoginButton).Click();
			//		}
			//	}

			*/
		}






		private static void NewAnswerWindow(string body)
		{
			if (consolle != null)
			{
				consolle.Kill();
				consolle = null;
			}

			//CloseWinAnsser();




			int hashStart = body.IndexOf("\"resultHash\":") + 14;
			int hashEnd = body.IndexOf(",\"isLastQuestion\"")  - (@"""isLastQuestion"":false,""qrid"":null,"":").Length;

			string hash = body.Substring(hashStart, hashEnd);



			Console.BackgroundColor = (ConsoleColor)2;
			Console.ForegroundColor = (ConsoleColor)15;

			Console.WriteLine($"hash:\n {hash} \n:");

			Console.BackgroundColor = (ConsoleColor)0; // чёрный
			Console.ForegroundColor = (ConsoleColor)7; // серый



			ProcessStartInfo consolleInfo = new ProcessStartInfo();

			consolleInfo.FileName = "cmd.exe";
			consolleInfo.Arguments = $"/k cd \\ & cd {pathToBat} & start start.bat --\"{hash}\"";
					
			consolleInfo.UseShellExecute = true;
			consolleInfo.CreateNoWindow = true;

			consolleInfo.WindowStyle = ProcessWindowStyle.Minimized;

			consolle = Process.Start(consolleInfo);





			//WhraiteCloseWin(Process.GetCurrentProcess().Id); // текущий прочесс);

		}


		private static async void CloseWinAnsser()
		{
			foreach (var winAnser in answersWindows)
			{
				if( winAnser == driverStartProcess) continue;
				if( winAnser.Id == driverStartProcess.Id) continue;

				winAnser?.Kill();
				await Task.Delay(15);
			}

			answersWindows = new();
		}

		private static async void WhraiteCloseWin(int parentId)
		{
			// Получаем дочерние процессы через WMI
			string query = $"SELECT * FROM Win32_Process WHERE ParentProcessId = {parentId}";

			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
			{
				foreach (ManagementObject process in searcher.Get())
				{
					//Console.WriteLine($"Found child process: {process["Name"]}, ID: {process["ProcessId"]}");

					int id = Convert.ToInt32(process["ProcessId"]);

					try
					{
						answersWindows.Add(Process.GetProcessById(id));
					}
					catch (Exception ex) { }

					await Task.Delay(50);
				}
			}


			consolle?.WaitForExit();
		}

	}
}
