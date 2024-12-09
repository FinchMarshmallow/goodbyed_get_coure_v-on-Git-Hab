using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

using OpenQA.Selenium.DevTools.V129;


using System;
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

		private static int numBackgroundColor = 0;
		private static int time = 0;

		static async Task Main(string[] args)
		{
			pathToBat = Environment.CurrentDirectory;
			pathToBat = pathToBat.Replace("goodbyed_get_course\\bin\\Debug\\net8.0", "PYBNCD\\BNCD1");

			Console.Clear();

			string userName;

			while (true)
			{
				Console.BackgroundColor = (ConsoleColor)2; // тёмно зеленый
				Console.ForegroundColor = (ConsoleColor)7; // светло зеленый

				Console.Write("Enter yours user name, this pc:");

				Console.BackgroundColor = (ConsoleColor)0; // чёрный
				Console.ForegroundColor = (ConsoleColor)7; // серый

				Console.Write(" ");

				userName = Console.ReadLine();

				try
				{
					if (Directory.Exists($"C:\\Users\\{userName}"))
					{
						break;
					}
					else
					{
						Console.Clear();

						Console.BackgroundColor = (ConsoleColor)4;
						Console.ForegroundColor = (ConsoleColor)15;

						Console.WriteLine("Invalid username ! \n");

						Console.BackgroundColor = (ConsoleColor)0;
						Console.BackgroundColor = (ConsoleColor)7;

					}
				}
				catch (Exception ex)
				{
					Console.BackgroundColor = (ConsoleColor)3;
					Console.ForegroundColor = (ConsoleColor)11;

					Console.WriteLine(" \nIncorrect path to user folder ! \n");

					Console.BackgroundColor = (ConsoleColor)1; // тёмно крастный
					Console.ForegroundColor = (ConsoleColor)9; // крастный

					Console.WriteLine(ex.Message);

					Console.BackgroundColor = (ConsoleColor)0;
					Console.ForegroundColor = (ConsoleColor)7;
				}
			}

			try
			{
				driverStartProcess = new Process();

				driverStartProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
				driverStartProcess.StartInfo.Arguments = $"--remote-debugging-port=9222 --user-data-dir= \"C:\\Users\\{userName}\\AppData\\Local\\Google\\Chrome\\User Data\\Default\"";

				driverStartProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;

				driverStartProcess.Start();
			}
			catch (Exception ex)
			{

				Console.BackgroundColor = (ConsoleColor)1; // тёмно крастный
				Console.ForegroundColor = (ConsoleColor)7; // крастный

				Console.WriteLine(ex.Message);
				return;
			}

			ChromeOptions options = new ChromeOptions();
			options.SetLoggingPreference(LogType.Browser, LogLevel.All);
			options.AddArgument("w3c:false");
			options.DebuggerAddress = "localhost:9222";
			
			
			var driver = new ChromeDriver(options);
			driver.Navigate().GoToUrl("https://irbis-edu.getcourse.ru/teach/control/stream/index");


			var xhrRequest = new List<string>();
			var postDatasRequest = new List<string>();

			var xhrRespon = new List<string>();
			var postDatasRespon = new List<string>();

			var requestHandler = new NetworkRequestHandler();
			var responHandler = new NetworkResponseHandler();

			requestHandler.RequestTransformer = (request) => { return request; };
			requestHandler.RequestMatcher = httprequest =>
			{
				return false;
			};

			IDevTools IDevTools = driver;
			var devTools = driver.GetDevToolsSession();

			DevToolsSession session = IDevTools.GetDevToolsSession();

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
			};

			Task monitoring = manager.StartMonitoring();
			monitoring.Wait();

			Loading();

			while (true) { }
		}

		private static async void Loading()
		{
			while (true)
			{
				numBackgroundColor++;

				if (numBackgroundColor > 15)
					numBackgroundColor = 0;

				Random random = new Random(time);
				time++;

				Console.BackgroundColor = (ConsoleColor)0;
				Console.ForegroundColor = (ConsoleColor)7;

				Console.Write("                     ");

				Console.BackgroundColor = (ConsoleColor)numBackgroundColor;
				Console.ForegroundColor = (ConsoleColor)random.Next(0, 15);

				Console.Write("___Loading...___");

				Console.BackgroundColor = (ConsoleColor)0;
				Console.ForegroundColor = (ConsoleColor)7;

				Console.Write("\n");

				await Task.Delay(200);
			}
		}

		private static void NewAnswerWindow(string body)
		{

			int hashStart = body.IndexOf("\"resultHash\":") + 14;
			int hashEnd = body.IndexOf(",\"isLastQuestion\"")  - (@"""isLastQuestion"":false,""qrid"":null,"":").Length;

			string hash = body.Substring(hashStart, hashEnd);

			Console.BackgroundColor = (ConsoleColor)2;
			Console.ForegroundColor = (ConsoleColor)15;

			Console.WriteLine($"hash:\n {hash} \n:");

			Console.BackgroundColor = (ConsoleColor)0;
			Console.ForegroundColor = (ConsoleColor)7;

			ProcessStartInfo consolleInfo = new ProcessStartInfo();

			consolleInfo.FileName = "cmd.exe";
			consolleInfo.Arguments = $"/k cd \\ & cd {pathToBat} & start start.bat --\"{hash}\"";

			consolleInfo.UseShellExecute = true;
			consolleInfo.CreateNoWindow = true;

			consolleInfo.WindowStyle = ProcessWindowStyle.Minimized;

			consolle = Process.Start(consolleInfo);
		}
	}
}
