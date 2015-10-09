using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NF.HelloWorld.Interfaces;
namespace NF.HelloWorld.MessageManagers
{

    public class ConsoleImplementation: IOutputMethod
    {

        public MessageWriterResponse Write(string Message)
        {
            MessageWriterResponse Response = new MessageWriterResponse();
           try
            {
                Console.WriteLine(Message);
                Response.Success = true;
                Response.ExtendedInformation = "";
            }
            catch (System.Exception ex)
            {
                Response.Success = false;
                Response.ExtendedInformation = ex.Message;
            }
            return (Response);
        }
    }
    //This would typically be in a seperate project
    public class DBImplementation : IOutputMethod
    {
        public MessageWriterResponse Write(string Message)
        {
            MessageWriterResponse Response = new MessageWriterResponse();
            try
            {
                Console.WriteLine("This would be written to a database");
                Response.Success = true;
                Response.ExtendedInformation = "";
            }
            catch (System.Exception ex)
            {
                Response.Success = false;
                Response.ExtendedInformation = ex.Message;
            }
            return (Response);
        }
    }

}
