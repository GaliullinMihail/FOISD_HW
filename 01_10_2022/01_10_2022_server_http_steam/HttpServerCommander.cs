using NetConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _01_10_2022_server_http_steam
{
    public static class HttpServerCommander
    {
        private static HttpServer? _httpServer;
        private static bool _isStarted;
        private static string? _lastPort;
        private static string? _lastName;
        private static string? _lastPath;
        // private static string _pathToSteam = "C:\Users\mihai\Desktop\FOISD_HW\FOISD_HW\01_10_2022\html_steam\index.html";
        public static void Start()
        {
            PrintHelpInfo();
            while (MakeResponse(Console.ReadLine())) ;
        }

        private static void PrintHelpInfo()
        {
            Console.WriteLine("help - выводит комманды\n" +
                "start <port/name> - запускает сервер http://localhost:port/name/\n" +
                "stop - останавливает сервер\n" +
                "request <pathToFile> - добавляет на сервер html файл\n" +
                "restart - перезапускает сервер(и добавляет туда информацию, если она была добавлена" +
                "end - завершает работу\n");
        }

        private static void StopServer()
        {
            Console.WriteLine("Попытка остановки сервера");
            if (_httpServer != null)
            {
                if (_isStarted)
                {
                    _isStarted = false;
                    _httpServer.Stop();
                    Console.WriteLine("Сервер успешно остановлен");
                }
                else
                {
                    Console.WriteLine("Сервер не запущен");
                }
            }
            else
            {
                Console.WriteLine("Сервер не был создан");
            }
        }

        private static void StartServer(string port, string name)
        {
            Console.WriteLine("Попытка запуска сервера");
            if (_isStarted)
            {
                Console.WriteLine("Сервер уже запущен");
            }
            else
            {
                _httpServer = new HttpServer(port, name);
                _isStarted = true;
                _lastPort = port;
                _lastName = name;
                _httpServer.Start();
                Console.WriteLine("Сервер запущен");
            }
        }

        private static void AddInfoToServer(string pathToFile)
        {
            if (_httpServer == null)
            {
                Console.WriteLine("Сервер не был создан");
                return;
            }
            if (!_isStarted)
            { 
                Console.WriteLine("Сервер не был запущен");
                return;
            }

            if (!File.Exists(pathToFile))
            {
                Console.WriteLine("Файл html не был найден");
            }
            else
            { 
                _httpServer.MakeResponse(pathToFile);
                _lastPath = pathToFile;
                Console.WriteLine("Файл html успешно добавлен");
            }
        }

        private static bool MakeResponse(string? response)
        {
            if (response == null)
            {
                Console.WriteLine("Встречен пустой запрос, пожалуйста повторите запрос");
                return true;
            }
            var responseSplit = response.Split(' ');
            switch (responseSplit[0])
            {
                case "help":
                    {
                        PrintHelpInfo();
                        return true;
                    }

                case "start":
                    {
                        Regex start = new Regex(@"[0-9]{4,10}[/]\w+");
                        if (!start.IsMatch(responseSplit[1]))
                        {
                            Console.WriteLine("<port/name> введен не правильно, повторите запрос");
                        }
                        else
                        {
                            var portNameInfo = responseSplit[1].Split('/');
                            StartServer(portNameInfo[0], portNameInfo[1]);
                        }
                        return true;
                    }

                case "stop":
                    {
                        StopServer();
                        return true;
                    }

                case "request":
                    {
                        AddInfoToServer(responseSplit[1]);
                        return true;
                    }

                case "end":
                    {
                        if (_isStarted)
                            StopServer();
                        return false;
                    }

                case "restart":
                    {
                        if (_lastName != null && _lastPort != null)
                        {
                            StopServer();
                            StartServer(_lastPort, _lastName);
                        }
                        else
                        {
                            Console.WriteLine("Сервер не может быть перезапущен, так как не был создан");
                            return true;
                        }
                        if (_lastPath != null)
                        {
                            AddInfoToServer(_lastPath);
                        }
                        else
                        {
                            Console.WriteLine("Сервер не имел пути к прошлому html файлу");
                        }
                        return true;
                    }

                default:
                    {
                        Console.WriteLine("Не удалось идентифицировать запрос, повторите ввод(для справки введите help)");
                        return true;
                    }
            };
        }

        public static bool IsStartet => _isStarted;
    }
}