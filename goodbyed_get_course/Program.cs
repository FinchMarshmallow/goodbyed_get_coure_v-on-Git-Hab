using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace goodbyed_get_course
{
	internal class Program
	{
		private static Process? driverStartProcess;

		public static ConsoleColor defBackground = (ConsoleColor)0; // чёрный
		public static ConsoleColor defForegroun = (ConsoleColor)7; // серый

		private static string? pathToBat = null;

		private static Process? consolle = null;

		public static string icons = "@#$%&*+=-";

		public static int numBackgroundColor = 0;
		public static int time = 0;

		static async Task Main(string[] args)
		{
			Console.Clear();

			SetConsoleColors(ConsoleColor.Magenta, ConsoleColor.Cyan);

			pathToBat = Environment.CurrentDirectory;
			Console.WriteLine("Original path: " + pathToBat);

			//pathToBat = pathToBat.Replace("goodbyed_get_course\\bin\\Debug\\net8.0", "PYBNCD\\BNCD1");

			pathToBat =
				pathToBat.Substring(0, pathToBat.LastIndexOf("goodbyed_get_coure_v on Git Hab")) +
				"goodbyed_get_coure_v on Git Hab\\PYBNCD\\BNCD1";


			Console.WriteLine("Local path: " + pathToBat);
			Console.WriteLine();

			ResetConsoleColors();

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

				SetConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Green);

				Console.WriteLine($":\n {e.ResponseBody} \n:");

				ResetConsoleColors();

				StartNewWindow(body);
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
						//Console.Clear();

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

				Console.Write("    ");

				Math.Clamp(random.Next(-1, 16), 0, 15);

				SetConsoleColors(
					(ConsoleColor)numBackgroundColor,
					(ConsoleColor)(Math.Clamp(random.Next(-1, 16), 0, 15)));

				Console.Write(icons[random.Next(0, icons.Length - 1)]);
				Console.Write(icons[random.Next(0, icons.Length - 1)]);
				Console.Write(icons[random.Next(0, icons.Length - 1)]);

				ResetConsoleColors();

				if(consolle != null)
				{
					Console.Write("    " + consolle.ToString());
				}

				await Task.Delay(500);
				
				time++;
			}
		}

		private static void StartNewWindow(string body)
		{
			if (consolle != null && !consolle.HasExited)
					consolle.Kill();

			KillPhytonWindows();

			int hashStart = body.IndexOf("\"resultHash\":") + 14;
			int hashEnd = body.IndexOf(",\"isLastQuestion\"")  - (@"""isLastQuestion"":false,""qrid"":null,"":").Length;

			string hash = body.Substring(hashStart, hashEnd);

			SetConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Green);

			Console.WriteLine($"hash:\n {hash} \n:");

			ResetConsoleColors();

			ProcessStartInfo consolleInfo = new ProcessStartInfo()
			{
				FileName = "cmd.exe",
				Arguments = $"/k cd \\ & cd {pathToBat} & start start.bat --\"{hash}\"",
				UseShellExecute = true,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Minimized
			};

			consolle = Process.Start(consolleInfo);

		}

		private static void KillPhytonWindows()
		{
			Process[] pythonProcesses = Process.GetProcessesByName("python");

			foreach (var process in pythonProcesses)
			{
				try
				{
					SetConsoleColors(ConsoleColor.Yellow, ConsoleColor.DarkBlue);
					Console.Write($"kill process: id: {process.Id} name: {process.ProcessName} st: {process.ToString}");
					ResetConsoleColors();

					process.Kill();
					process.WaitForExit();
				}
				catch (Exception ex)
				{
					SetConsoleColors(ConsoleColor.DarkRed, ConsoleColor.Red);
					Console.WriteLine($"Eror: close python: {ex.Message}");
					ResetConsoleColors();
				}
			}
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
