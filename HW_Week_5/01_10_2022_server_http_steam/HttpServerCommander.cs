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
        public static void Start()
        {
            PrintHelpInfo();
            while (MakeResponse(Console.ReadLine())) ;
        }

        private static void PrintHelpInfo()
        {
            Console.WriteLine("help - выводит комманды\n" +
                "start - запускает сервер http://localhost:port/\n" +
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

        private static void StartServer()
        {
            Console.WriteLine("Попытка запуска сервера");
            if (_isStarted)
            {
                Console.WriteLine("Сервер уже запущен");
            }
            else
            {
                _httpServer = new HttpServer();
                _isStarted = true;
                _httpServer.Start();
                Console.WriteLine("Сервер запущен");
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
                        StartServer();
                        return true;
                    }

                case "stop":
                    {
                        StopServer();
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
                        StopServer();
                        StartServer();
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