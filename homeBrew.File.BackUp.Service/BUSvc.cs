using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace homeBrew.File.BackUp.Service
{
    public partial class BUSvc : ServiceBase
    {
        public BUSvc()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }
         
        protected override void OnStop()
        {
        }
    }
}
