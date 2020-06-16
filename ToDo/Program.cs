using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ToDo.Adapters;
using ToDo.Adapters.Driving;
using ToDo.Services;

namespace ToDo
{
    class Program
    {
        public static string parametrosConexão =
            "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=todo";
        static void Main(string[] args)
        {
            var tipoDeAplicação = args.Length > 0 ? args[0] : "console";
            switch (tipoDeAplicação)
            {
                case "console" : 
                    ConsoleProgram.Executa();
                    break;
                case "api":
                    APIProgram.Executa();
                    break;
            }
        }
    }
}