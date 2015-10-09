using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.HelloWorld.Interfaces
{
    public interface IOutputMethod
    {
        MessageWriterResponse Write(string Message);
    }
    public class MessageWriterResponse
    {
        public bool Success { get; set; }
        public string ExtendedInformation { get; set; }
    }
}
