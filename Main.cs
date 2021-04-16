using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using pileTest.Logging;

namespace pileTest
{
	public partial class pileTestMainWindow : Form
	{
		public pileTestMainWindow()
		{
			InitializeComponent();
			Logger.initializeLogger();
			Logger.log(Severity.Info, "App Initializing");
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
			Logger.log(Severity.Info, "Hello");
		}

		private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//TODO Transfer this in to a different form.
			MessageBox.Show("Developed By Timothy Ganoza", "pileTestStatus");
		}

		private void pileTestMainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			Logger.log(Severity.Fatal, "Window Closed");
			Thread.Sleep(200); // Stop for 200ms
			Logger.killAll();
		}

		private void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//TODO Create an update system
		}
	}
}
