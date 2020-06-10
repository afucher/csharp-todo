using System;
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
            //ConsoleProgram.Executa();
            APIProgram.Executa();
        }
    }
}