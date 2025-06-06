using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.IO;

namespace goodbyed_get_course
{
	internal class Program
	{
		private static Process? driverStartProcess;

		public static ConsoleColor defBackground = (ConsoleColor)0; // чёрный
		public static ConsoleColor defForegroun = (ConsoleColor)7; // серый

		//private static string? pathToBat = null;

		private static Process? consolle = null;

		private static ChromeDriver? driver;

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

			string userName = GetUserName();

			if (!StartDriver(userName))
				return;

			await Task.Run(() => Loading());




			ChromeOptions options = new ChromeOptions();
			options.SetLoggingPreference(LogType.Browser, LogLevel.All);
			options.DebuggerAddress = "localhost:9222";

			// отключение фоток
			//string defProfile = "profile.default_content_setting_values.";
			//options.AddUserProfilePreference($"{defProfile}images", 2);

			//options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);


			//options.AddArgument("--blink-settings=imagesEnabled=false");
			//options.AddArgument("--disable-images");

			options.AddArguments
			([
				"--headless=new",          // Фоновый режим без GUI
				//"--disable-gpu",           // Отключение GPU
				"--no-sandbox",            // Отключение песочницы
				"--disable-dev-shm-usage", // Фикс для ограниченной памяти
				"--disable-extensions",    // Отключение расширений
				"--ignore-certificate-errors",
				"--disable-images",
				"--blink-settings=imagesEnabled=false"
			]);

			driver = new ChromeDriver(options);
			driver.Navigate().GoToUrl("https://irbis-edu.getcourse.ru/teach/control/stream/index");

			NetworkRequestHandler requestHandler = new NetworkRequestHandler();

			requestHandler.RequestTransformer = (request) => { return request; };
			requestHandler.RequestMatcher = httprequest => { return false; };

			IDevTools IDevTools = driver;

			DevToolsSession session = IDevTools.GetDevToolsSession();

			INetwork networkInterceptor = driver.Manage().Network;

			networkInterceptor.AddRequestHandler(requestHandler);
			await networkInterceptor.StartMonitoring().ConfigureAwait(false);


			var domains = session.GetVersionSpecificDomains<DevToolsSessionDomains>();
			var network = session.Domains.Network;

			Task enableNetwork = network.EnableNetwork();
			Task networkCaching = network.EnableNetworkCaching();

			await enableNetwork;
			await networkCaching;


			NetworkManager manager = new NetworkManager(driver);
			networkInterceptor.NetworkResponseReceived += (sender, e) =>
			{
				if (e != null && e.ResponseBody != null && e.ResponseUrl != null && e.ResponseBody.Length > 15 && e.ResponseUrl.Contains("/pl/teach/questionary-public/testing"))
				{
					Task.Run(() => ChekResponseBody(e.ResponseBody));
				}
			};

			Task monitoring = manager.StartMonitoring();
			await monitoring;

			Console.ReadLine();
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

				if (consolle != null)
				{
					Console.Write("    " + consolle.ToString());
				}

				await Task.Delay(800);

				time++;
			}
		}


		private static async Task ChekResponseBody(string body)
		{
			Console.WriteLine(body);

			await Task.Run(() =>
			{
				if (!(body.IndexOf(@"""resultHash"":", StringComparison.Ordinal) >= 0)) return;

				SetConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Green);

				Console.WriteLine($":\n {body} \n:");

				ResetConsoleColors();

				NewWindows(body);

				//currentMasage = body;
			});
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


				JsonDocument jsonDoc = JsonDocument.Parse(unescapeHash);
				JsonElement variants = jsonDoc.RootElement.GetProperty("question").GetProperty("variants");

				currentMasage = "";

				foreach (JsonElement variant in variants.EnumerateArray())
				{
					if (variant.GetProperty("is_right").GetInt32() == 1)
					{
						string? jsontAnswer = variant.GetProperty("value").GetString();

						if (jsontAnswer == null)
							return;

						string jsonString = Regex.Unescape(jsontAnswer);

						Console.WriteLine($"decryptedString: \n{jsonString}\n");

						Console.WriteLine(jsonString);
						currentMasage += jsonString + "\n";

						PaintingButton(jsonString);
					}
				}
			}
			catch (Exception e)
			{
				currentMasage = e.Message;
			}

		}

		private static void PaintingButton(string targetValue)
		{
			const string targetClass = "btn btn-default btn-mark-variant js__btn-variant";

			string script = @"
			var elements = document.querySelectorAll('button, input[type=""button""]');
				
			for (var i = 0; i < elements.length; i++) {{
				var element = elements[i];
				
				var value = element.getAttribute('value');
				var classes = element.getAttribute('class') || '';
				console.log('"+targetValue+@"')

				if (classes === '"+targetClass+@"' && element.outerHTML.includes('data-value="""+targetValue+@"""')) {{
					element.style.cssText = `
						background-color: #83c7a4 !important;
						color: white !important;
						border: none !important;
						padding: 5px 10px !important;
						border-radius: 4px !important;
						box-shadow: 0 2px 4px rgba(0,0,0,0.2) !important;
					`;
				
					console.log('Element:', element.outerHTML);
					console.log('Class:', classes);
					console.log('Value:', value);
					console.log('-------------------');
				}}
			}}
			";
			//element.toString().includes('data-value=" + targetValue + @"') && 
			//var classes = element.getAttribute('class') || '';
			//var id = element.getAttribute('id') || '';

			//			var elements = document.querySelectorAll('button, input[type=""button""]');

			//			for (var i = 0; i < elements.length; i++)
			//			{
			//				var element = elements[i];

			//				var value = element.getAttribute('value');
			//				var classes = element.getAttribute('class') || '';
			//				var id = element.getAttribute('id') || '';

			//				console.log('Element:', element);
			//				console.log('Tag:', element.tagName);
			//				console.log('Class:', classes);
			//				console.log('ID:', id);
			//				console.log('Value:', value);
			//				console.log('-------------------');

			//				if (value === ' targetValue')
			//				{
			//					element.style.cssText = `
			//            background - color: #58875e !important;
			//            color: white!important;
			//				border: none!important;
			//				padding: 5px 10px!important;
			//					border - radius: 4px!important;
			//					box - shadow: 0 2px 4px rgba(0,0,0,0.2) !important;
			//        `;
			//			break;
			//		}
			//}


			// Выполнить скрипт
			if (driver is IJavaScriptExecutor js == false)
			{
				return;
			}

			try
			{
				var result = js.ExecuteScript(script);
				Console.WriteLine($"JavaScript выполнен: {result}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка при выполнении скрипта: {ex.Message}");
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



