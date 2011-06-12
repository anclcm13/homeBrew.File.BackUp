using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using homeBrew.File;

namespace homeBrew.File.BackUp.Application
{
    public partial class Debugger : Form
    {
        public Debugger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debugger.Break();

            BackUp.Process process = new Process();
            process.OrigDirectory = @"E:\";
            process.CopyDirectory = @"F:\BackUp\";
            process.ReadAndCompairDirectories();

            System.Diagnostics.Debugger.Break();
        }
    }
}
