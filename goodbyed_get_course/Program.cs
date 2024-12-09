using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace goodbyed_get_course
{
	internal class ProgramA
	{
		private static Process? driverStartProcess;


		public static ConsoleColor defBackground = (ConsoleColor)0; // чёрный
		public static ConsoleColor defForegroun = (ConsoleColor)7; // серый

		private static string? pathToBat = null;

		private static Process? consolle;

		public static string icons = "@#$%&*+=-";

		public static int numBackgroundColor = 0;
		public static int time = 0;

		static async Task Main(string[] args)
		{

			pathToBat = Environment.CurrentDirectory;
			pathToBat = pathToBat.Replace("goodbyed_get_course\\bin\\Debug\\net8.0", "PYBNCD\\BNCD1");
			Console.Clear();

			string userName = GetUserName();

			if (!StartDriver(userName))
				return;

			Loading();

			ChromeOptions options = new ChromeOptions();
			options.SetLoggingPreference(LogType.Browser, LogLevel.All);
			options.AddArgument("w3c:false");
			options.DebuggerAddress = "localhost:9222";
			
			var driver = new ChromeDriver(options);
			driver.Navigate().GoToUrl("https://irbis-edu.getcourse.ru/teach/control/stream/index");

			var requestHandler = new NetworkRequestHandler();

			requestHandler.RequestTransformer = (request) => { return request; };
			requestHandler.RequestMatcher = httprequest => { return false; };

			IDevTools IDevTools = driver;

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

			while (true) { }
		}

		private static string GetUserName()
		{
			string? userName;

			while (true)
			{
				SetConsoleColors(ConsoleColor.Green, ConsoleColor.White);

				Console.Write("Enter yours user name, this pc:");

				ResetConsoleColors();

				Console.Write(" ");

				userName = Console.ReadLine();

				try
				{
					if (userName != null && Directory.Exists($"C:\\Users\\{userName}"))
					{
						return userName;
					}
					else
					{
						Console.Clear();

						SetConsoleColors(ConsoleColor.DarkRed, ConsoleColor.White);

						Console.WriteLine("Invalid username ! \n");

						ResetConsoleColors();
					}
				}
				catch (Exception ex)
				{
					SetConsoleColors(ConsoleColor.DarkCyan, ConsoleColor.Cyan);

					Console.WriteLine(" \nIncorrect path to user folder ! \n");

					SetConsoleColors(ConsoleColor.DarkRed, ConsoleColor.Red);

					Console.WriteLine(ex.Message);

					ResetConsoleColors();
				}
			}

		}

		private static async void Loading()
		{
			while (true)
			{
				numBackgroundColor++;

				if (numBackgroundColor > 15)
					numBackgroundColor = 0;

				Random random = new Random(time);

				ResetConsoleColors();

				Console.Write("\n                     ");

				SetConsoleColors(
					(ConsoleColor)numBackgroundColor,
					(ConsoleColor)random.Next(0, 15));

				Console.Write($"___Loading...___ ");

				ResetConsoleColors();

				Console.Write("      ");


				SetConsoleColors(
					(ConsoleColor)numBackgroundColor,
					(ConsoleColor)random.Next(0, 15));

				Console.Write(icons[random.Next(0, icons.Length - 1)]);
				Console.Write(icons[random.Next(0, icons.Length - 1)]);
				Console.Write(icons[random.Next(0, icons.Length - 1)]);


				await Task.Delay(500);
				
				time++;
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

		private static void SetConsoleColors(ConsoleColor background, ConsoleColor foreground)
		{
			Console.BackgroundColor = background;
			Console.ForegroundColor = foreground;
		}

		private static void ResetConsoleColors()
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		private static bool StartDriver(string userName)
		{
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
				SetConsoleColors(ConsoleColor.DarkRed, ConsoleColor.Red);

				Console.WriteLine(ex.Message);
				return false;
			}

			return true;
		}
	}
}
