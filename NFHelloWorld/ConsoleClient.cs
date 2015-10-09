using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NF.HelloWorld.MessageManagers;
using NF.HelloWorld.Interfaces;
using Autofac;
namespace NF.HelloWorld.Clients
{
    class ConsoleClient
    {
        private static IContainer myIOCContainer { get; set; }
        const string ConsoleTarget = "Console";
        const string DBTarget = "Database";
        static void Main(string[] args)
        {
            try
            {
                if (!InitializeIOC())
                {
                    return;
                }
                using (var iScope = myIOCContainer.BeginLifetimeScope())
                {
                    var ToConsole = iScope.ResolveNamed<IOutputMethod>(ConsoleTarget);
                    MessageWriterResponse resp = new MessageWriterResponse();
                    resp = ToConsole.Write("Hello World");
                    if (resp.Success == false)
                    {
                        //additional error handling here, use ExtendedInformation to resolve the error
                    }
                    Console.ReadLine();
                }
            }
            catch (System.Exception consoleWriteFailed)
            {
                //would probably want some other method besides a console.write to record this failure... since that's 
                //what failed in the first place
                Console.Write("Failed to Instantiate/Load Hello World Helper Process Error: {0}",consoleWriteFailed.Message);
            }
        }
        private static bool InitializeIOC()
        {
             //Instantiate the DI Builder for Autofac.  Register the Dependencies that we will be using.  
            //There are two mocked up dependencies to demonstrate future proofing.  Autofac supports
            //configuration via app.config, but that would be overkill for an actual application which 
            //knows what its output target(s) are.
            //ConsoleImplementation ToConsole = new ConsoleImplementation();
            bool InitSuccess = false;
            var aBuilder = new ContainerBuilder();
            try
            {
                aBuilder.RegisterType<ConsoleImplementation>().Named<IOutputMethod>(ConsoleTarget);
                aBuilder.RegisterType<DBImplementation>().Named<IOutputMethod>(DBTarget);
                myIOCContainer = aBuilder.Build();
                InitSuccess = true;
            }
            catch (System.Exception iocError)
            {
                Console.WriteLine("Error Initializing DI Container {0}", iocError.Message);
                Console.WriteLine("Press Any Key to Exit Program");
                Console.ReadLine();
            }
            return (InitSuccess);
       }
    }
}
