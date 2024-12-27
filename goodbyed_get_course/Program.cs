using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System.IO;
using System.Text;

using System;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Threading.Channels;

namespace goodbyed_get_course
{
	internal class Program
	{
		private static Process? driverStartProcess;

		public static ConsoleColor defBackground = (ConsoleColor)0; // чёрный
		public static ConsoleColor defForegroun = (ConsoleColor)7; // серый

		//private static string? pathToBat = null;

		private static Process? consolle = null;

		public static string icons = "@#$%&*+=-";

		public static int numBackgroundColor = 0;
		public static int time = 0;

		public static string currentMasage = "";

		static async Task Main(string[] args)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


			Console.OutputEncoding = Encoding.UTF8;
			Console.InputEncoding = Encoding.UTF8;

			Console.Clear();

			/* SetConsoleColors(ConsoleColor.Magenta, ConsoleColor.Cyan);

			ResetConsoleColors();

			pathToBat = pathToBat.Replace("goodbyed_get_course\\bin\\Debug\\net8.0", "PYBNCD\\BNCD1");
			
			pathToBat =
				pathToBat.Substring(
					0,
					pathToBat.LastIndexOf(
						"goodbyed_get_coure_v on Git Hab")) +
					"goodbyed_get_coure_v on Git Hab\\PYBNCD\\BNCD1";


			while (!File.Exists(pathToBat + "\\start_python_di_hash.bat"))
			{
				SetConsoleColors(ConsoleColor.DarkRed, ConsoleColor.White);
				Console.WriteLine("path to not found !, please enter path to:");
			
				SetConsoleColors(ConsoleColor.DarkYellow, ConsoleColor.White);
				Console.Write(" start_python_di_hash.bat:");
			
				ResetConsoleColors();
				Console.Write("  ");
			
				pathToBat = Console.ReadLine();
			
				SetConsoleColors(ConsoleColor.DarkCyan, ConsoleColor.Magenta);
				Console.WriteLine(pathToBat + "\\start_python_di_hash.bat");
			
				ResetConsoleColors();
			}

			Console.WriteLine("Local path: " + pathToBat);
			Console.WriteLine();

			ResetConsoleColors(); */

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

			NetworkRequestHandler requestHandler = new NetworkRequestHandler();

			requestHandler.RequestTransformer = (request) => { return request; };
			requestHandler.RequestMatcher = httprequest => { return false; };

			IDevTools IDevTools = driver;

			DevToolsSession session = IDevTools.GetDevToolsSession();

			INetwork networkInterceptor = driver.Manage().Network;
			await networkInterceptor.StartMonitoring();

			networkInterceptor.AddRequestHandler(requestHandler);
			await networkInterceptor.StartMonitoring();


			var domains = session.GetVersionSpecificDomains<DevToolsSessionDomains>();
			var network = session.Domains.Network;

			Task enableNetwork = network.EnableNetwork();
			Task networkCaching = network.EnableNetworkCaching();
			enableNetwork.Wait();
			networkCaching.Wait();


			NetworkManager manager = new NetworkManager(driver);
			networkInterceptor.NetworkResponseReceived += async (object sender, NetworkResponseReceivedEventArgs e) =>
			{
				await ChekResponseBody(e.ResponseBody);
			};

			Task monitoring = manager.StartMonitoring();
			monitoring.Wait();

			Console.ReadLine();

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
				Console.Clear();

				SetConsoleColors(ConsoleColor.DarkGray, ConsoleColor.Gray);
				Console.WriteLine("Ответ:");
				ResetConsoleColors();

				Console.WriteLine("\n" + currentMasage + "\n\n\n\n");

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

				await Task.Delay(800);
				
				time++;
			}
		}


		private static async Task ChekResponseBody(string body)
		{
			if (body == null || body == "" || !(body.Length >= 15) || !(body.IndexOf(@"""resultHash"":", StringComparison.Ordinal) >= 0)) return;

			SetConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Green);

			Console.WriteLine($":\n {body} \n:");

			ResetConsoleColors();

			NewWindows(body);
		}

		private static void NewWindows(string body)
		{
			try
			{
				int hashStart = body.IndexOf("\"resultHash\":") + 14;
				int hashEnd = body.IndexOf(",\"isLastQuestion\"")  - (@""",""isLastQuestion"":false,""qrid"":null,"":").Length;

				string hash = body.Substring(hashStart, hashEnd);

				SetConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Green);
				Console.WriteLine($"hash:\n{hash}\n:");
				ResetConsoleColors();

				Console.WriteLine($"original hash: \n{hash}\n");

				byte[] decryptedByteHash = Convert.FromBase64String(hash);

				Console.WriteLine($"decrypted byte hash: \n{BitConverter.ToString(decryptedByteHash)}\n");


				string unescapeHash = Encoding.UTF8.GetString(decryptedByteHash);

				Console.WriteLine($"unescapeHash: \n{unescapeHash}\n");


				string jsonString = Regex.Unescape(unescapeHash);

				Console.WriteLine($"decryptedString: \n{jsonString}\n");

				JsonDocument jsonDoc = JsonDocument.Parse(jsonString);
				JsonElement variants = jsonDoc.RootElement.GetProperty("question").GetProperty("variants");

				currentMasage = "";

				foreach (JsonElement variant in variants.EnumerateArray())
				{
					if (variant.GetProperty("is_right").GetInt32() == 1)
					{
						string? correctAnswer = variant.GetProperty("value").GetString();

						if (correctAnswer == null)
							return;
						Console.WriteLine(correctAnswer);
						currentMasage += correctAnswer + "\n";
					}
				}

				if(currentMasage == "")
				{
					currentMasage = "Правильный ответ не найден.";
					Console.WriteLine("Правильный ответ не найден.");
				}

				/*ProcessStartInfo consolleInfo = new ProcessStartInfo()
				{
					FileName = "cmd.exe",
					Arguments = $"/k cd \\ & cd {pathToBat} & start start_python_di_hash.bat --\"{hash}\"",
					UseShellExecute = true,
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Minimized
				};

				consolle = Process.Start(consolleInfo); */
			} 
			catch (Exception e)
			{
				currentMasage = e.Message;
			}

		}

		private static void KillPhytonWindows()
		{
			Process[] pythonProcesses = Process.GetProcessesByName("python");

			KillProcess(pythonProcesses);

			/*pythonProcesses = Process.GetProcessesByName("goodbyed_get_course");

			KillProcess(pythonProcesses);*/
		}

		private static void KillProcess(Process[] pythonProcesses)
		{
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



