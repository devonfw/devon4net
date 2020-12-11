using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devon4Net.Application.WebAPI.Configuration.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyThaiStar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Devonfw.Configure<Startup>(args);
        }
    }
}
