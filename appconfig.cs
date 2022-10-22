using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ColorsWeb
{
    public class AppConfig
    {
        private string _AdminPWVal;
        public AppConfig(IConfiguration config)
        {
            _AdminPWVal = config.GetValue<string>("AdminPW");
        }
 
        public string AdminPW
        {
            get => this._AdminPWVal;
            set => this._AdminPWVal = value;
        }
    }
}
