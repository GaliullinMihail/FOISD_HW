using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_10_2022_server_http_steam
{
    public class ServerSettings
    {
        public int Port { get; set; } = 7777;

        public string Path { get; set; } = @"./site/";
    }
}
