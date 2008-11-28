// MainWindow.cs created with MonoDevelop
// User: cbguder at 11:59 AM 11/9/2008
//
using System;
using System.IO;
using Gtk;

namespace TuxMate
{
	public partial class MainWindow: Gtk.Window
	{
		string filename;

		protected string Filename
		{
			get { return filename; }
			set { filename = value; SetTitle(filename); }
		}

		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			Build();
			textView.ModifyFont(Pango.FontDescription.FromString("monospace 10"));
			NewFile();
		}

		protected void SetTitle(string filename)
		{
			if(filename == null)
				Title = "untitled";
			else
				Title = System.IO.Path.GetFileName(filename);					
		}

		protected void NewFile()
		{
			Filename = null;
			textView.Buffer.Text = "";
		}

		protected void OpenFile()
		{
			Gtk.FileChooserDialog fc = new Gtk.FileChooserDialog("Open File", this,
			                                                     FileChooserAction.Open,
			                                                     "Cancel", ResponseType.Cancel,
			                                                     "Open", ResponseType.Accept);

			if(fc.Run() == (int)ResponseType.Accept)
				LoadFile(fc.Filename);

			fc.Destroy();

		}

		protected void LoadFile(string path)
		{
			try {
				System.IO.FileStream fs = new FileStream(path, FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				textView.Buffer.Text = sr.ReadToEnd();
				sr.Close();
				fs.Close();
				Filename = path;
			} catch (Exception e) {
				MessageDialog md = new MessageDialog(this,
				                                     DialogFlags.DestroyWithParent,
				                                     MessageType.Error,
				                                     ButtonsType.Close,
				                                     e.Message);
				md.Run();
				md.Destroy();
			}
		}

		protected void SaveFile()
		{
			if(Filename == null)
				SaveAsFile();
			else
				SaveFile(Filename);
		}

		protected void SaveFile(string path)
		{
			try {
				System.IO.FileStream fs = new FileStream(path, FileMode.Create);
				StreamWriter sw = new StreamWriter(fs);
				sw.Write(textView.Buffer.Text);
				if(!textView.Buffer.Text.EndsWith(Environment.NewLine))
					sw.Write(Environment.NewLine);
				sw.Close();
				fs.Close();
				Filename = path;
			} catch (Exception e) {
				MessageDialog md = new MessageDialog(this,
				                                     DialogFlags.DestroyWithParent,
				                                     MessageType.Error,
				                                     ButtonsType.Close,
				                                     e.Message);
				md.Run();
				md.Destroy();
			}
		}

		protected void SaveAsFile()
		{
			Gtk.FileChooserDialog fc = new Gtk.FileChooserDialog("Save As...", this,
			                                                     FileChooserAction.Save,
			                                                     "Cancel", ResponseType.Cancel,
			                                                     "Save", ResponseType.Accept);

			if(fc.Run() == (int)ResponseType.Accept)
				SaveFile(fc.Filename);

			fc.Destroy();
		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			Application.Quit();
			a.RetVal = true;
		}

		protected virtual void OnNewActionActivated(object sender, System.EventArgs e)
		{
			NewFile();
		}

		protected virtual void OnOpenActionActivated(object sender, System.EventArgs e)
		{
			OpenFile();
		}

		protected virtual void OnSaveActionActivated(object sender, System.EventArgs e)
		{
			SaveFile();
		}

		protected virtual void OnSaveAsActionActivated(object sender, System.EventArgs e)
		{
			SaveAsFile();
		}

		protected virtual void OnQuitActionActivated(object sender, System.EventArgs e)
		{
			Application.Quit();
		}

		protected virtual void OnPreferencesActionActivated(object sender, System.EventArgs e)
		{
			PreferencesDialog preferencesDialog = new PreferencesDialog();
			preferencesDialog.Run();
			preferencesDialog.Destroy();
		}
	}
}
