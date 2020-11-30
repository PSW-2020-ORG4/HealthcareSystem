using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Pharmacies
{
    public class SftpConfig
    {
        public string Host { get; set; } = "192.168.1.3";
        public int Port { get; set; } = 22;
        public string Username { get; set; } = "tester";
        public string Password { get; set; } = "password";
    }
}
