// Main.cs created with MonoDevelop
// User: cbguder at 11:59 AM 11/9/2008
//
using System;
using Gtk;

namespace TuxMate
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}