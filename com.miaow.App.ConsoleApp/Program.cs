using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using com.miaow.Core.Context;
using com.miaow.Core.Events;

namespace com.miaow.App.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext.Current.EventBus.RegisterAllEventHandlerFromAssembly(Assembly.GetExecutingAssembly());

            ApplicationContext.Current.EventBus.Trigger<UserRegistRequest,UserRegistResponse>(new UserRegistRequest()
            {
                UserName = "Paul",
                EmailAddress = "paul@126.com"
            });
        }
    }

    public class UserRegistRequest : EventRequestBase
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class UserRegistResponse : EventResponseBase
    {
    }

    public class UserRegistRequestHandler: EventHandlerBase<UserRegistRequest,UserRegistResponse>
    {
        protected override UserRegistResponse HandleProcess(UserRegistRequest request)
        {
            Console.WriteLine($"UserName:{request.UserName},E-mail:{request.EmailAddress}");
            return new UserRegistResponse();
        }
    }
}
