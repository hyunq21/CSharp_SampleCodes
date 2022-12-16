using System.ServiceProcess;

namespace slackbot
{
    static class Program
    {
        public static readonly string TOKEN = "*";  // token from last step in section above
        static void Main()
        {
            var ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
