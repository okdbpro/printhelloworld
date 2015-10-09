using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NF.HelloWorld.MessageManagers;
using NF.HelloWorld.Interfaces;
using Autofac;
namespace NF.HelloWorld.Tests
{
    [TestClass]
    public class HelloWorldTests
    {
        private static IContainer myIOCContainer { get; set; }
        //since these constants are shared between test and any actual UI implementation, I'd typically push this into a shared dependency.
        const string ConsoleTarget = "Console";
        const string DBTarget = "Database";
        [TestInitialize()]
        public void Initialize()
        {
            var aBuilder = new ContainerBuilder();
            try
            {
                aBuilder.RegisterType<ConsoleImplementation>().Named<IOutputMethod>(ConsoleTarget);
                aBuilder.RegisterType<DBImplementation>().Named<IOutputMethod>(DBTarget);
                myIOCContainer = aBuilder.Build();
            }
            catch (System.Exception iocError)
            {
                Assert.Fail("Error Initializing DI Container {0}", iocError.Message);
            }
        }
        [TestMethod]
        public void TestHelloWorldConsole()
        {
            using (var iScope = myIOCContainer.BeginLifetimeScope())
            {
                var ToConsole = iScope.ResolveNamed<IOutputMethod>(ConsoleTarget);
                MessageWriterResponse resp = new MessageWriterResponse();
                resp = ToConsole.Write("Hello World");
                Assert.IsTrue(resp.Success,String.Format("Output to Console Failed with the following error {0}",resp.ExtendedInformation));
            }
        }
        [TestMethod]
        public void TestHelloWorldDB()
        {
            using (var iScope = myIOCContainer.BeginLifetimeScope())
            {
                var ToConsole = iScope.ResolveNamed<IOutputMethod>(DBTarget);
                MessageWriterResponse resp = new MessageWriterResponse();
                resp = ToConsole.Write("It Doesn't Matter");
                Assert.IsTrue(resp.Success,String.Format("Output to Console Failed with the following error {0}",resp.ExtendedInformation));
            }
        }
    }
}
