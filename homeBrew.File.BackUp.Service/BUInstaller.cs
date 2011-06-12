using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace homeBrew.File.BackUp.Service
{
    [RunInstaller(true)]
    public partial class BUInstaller : System.Configuration.Install.Installer
    {
        public BUInstaller()
        {
            InitializeComponent();
        }
    }
}
