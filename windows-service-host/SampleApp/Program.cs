using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Message("Starting");

                var commandLineTypes = typeof(CommandLine).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && typeof(CommandLine).IsAssignableFrom(x));
                var returnCode = Parser.Default.ParseArguments(args, commandLineTypes.ToArray())
                                      .MapResult(commandLine =>
                                                 {
                                                     if (commandLine is CommandLine knownCommandLine)
                                                     {
                                                         return knownCommandLine.Execute();
                                                     }
                                                     return 2;
                                                 },
                                                 errors =>
                                                 {
                                                     foreach (var error in errors)
                                                     {
                                                         switch (error)
                                                         {
                                                             case UnknownOptionError unknownOption:
                                                                 Console.WriteLine($"Unknown option given {unknownOption.Token}");
                                                                 break;
                                                             case MissingRequiredOptionError missingRequiredOption:
                                                                 Console.WriteLine($"Missing required option {missingRequiredOption.NameInfo.NameText}");
                                                                 break;
                                                             default:
                                                                 Console.WriteLine(error);
                                                                 break;
                                                         }
                                                     }
                                                     return 1;
                                                 });
                return returnCode;

                
            }
            catch (Exception e)
            {
                Log.Message($"Exception - {e}");
            }
            return 3;
        }
    }
}
