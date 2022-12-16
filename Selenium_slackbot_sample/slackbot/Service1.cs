using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace slackbot
{
    public partial class Service1 : ServiceBase
    {
        ITNewsBot iTNewsBot { get; set; } = new ITNewsBot();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(iTNewsBot.RunSlackBot);
        }

        protected override void OnStop()
        {
        }
    }
}
