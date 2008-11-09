// MainWindow.cs created with MonoDevelop
// User: cbguder at 11:59 AM 11/9/2008
//
using System;
using System.IO;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		textView.ModifyFont(Pango.FontDescription.FromString("monospace 10"));
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnOpenActionActivated (object sender, System.EventArgs e)
	{
		Gtk.FileChooserDialog fc = new Gtk.FileChooserDialog("Open File", this, FileChooserAction.Open, "Cancel", ResponseType.Cancel, "Open", ResponseType.Accept);

		if (fc.Run() == (int)ResponseType.Accept)
		{
			System.IO.FileStream fs = new FileStream(fc.Filename, FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			textView.Buffer.Text = sr.ReadToEnd();
			sr.Close();
			fs.Close();
		}

		fc.Destroy ();
	}

	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		Application.Quit();
	}
}

