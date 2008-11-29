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
		string originalBuffer;

		protected string Filename
		{
			get { return filename; }
			set { filename = value; SetTitle(filename); }
		}

		public MainWindow(): base(Gtk.WindowType.Toplevel)
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
			textView.Buffer.Text = originalBuffer = "";
		}

		protected void OpenFile()
		{
			FileChooserDialog fc = new FileChooserDialog("Open File", this,
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
				originalBuffer = sr.ReadToEnd();
				sr.Close();
				fs.Close();

				Filename = path;
				textView.Buffer.Text = originalBuffer;
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

		protected bool SaveFile()
		{
			if(Filename == null)
				return SaveAsFile();
			else
				return SaveFile(Filename);
		}

		protected bool SaveFile(string path)
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
				originalBuffer = textView.Buffer.Text;
			} catch (Exception e) {
				MessageDialog md = new MessageDialog(this,
				                                     DialogFlags.DestroyWithParent,
				                                     MessageType.Error,
				                                     ButtonsType.Close,
				                                     e.Message);
				md.Run();
				md.Destroy();

				return false;
			}

			return true;
		}

		protected bool SaveAsFile()
		{
			bool result = false;
			FileChooserDialog fc = new FileChooserDialog("Save As...", this,
			                                             FileChooserAction.Save,
			                                             "Cancel", ResponseType.Cancel,
			                                             "Save", ResponseType.Accept);

			if(fc.Run() == (int)ResponseType.Accept)
				result = SaveFile(fc.Filename);

			fc.Destroy();
			return result;
		}

		protected bool ShouldQuit()
		{
			bool shouldQuit = true;

			if(originalBuffer != textView.Buffer.Text) {
				shouldQuit = false;

				MessageDialog md = new MessageDialog(this, DialogFlags.Modal,
				                                     MessageType.Warning,
				                                     ButtonsType.None,
				                                     "<span weight=\"bold\" size=\"larger\">Save changes to document before closing?</span>");
				md.SecondaryText = "Your changes will be lost if you don't save them.";

				Button closeButton  = new Button("Close without saving");
				Button cancelButton = new Button(Stock.Cancel);
				Button saveButton;
				if(Filename == null)
					saveButton = new Button(Stock.SaveAs);
				else
					saveButton = new Button(Stock.Save);
				saveButton.CanDefault = true;

				md.AddActionWidget(closeButton,  ResponseType.Reject);
				md.AddActionWidget(cancelButton, ResponseType.Cancel);
				md.AddActionWidget(saveButton, ResponseType.Accept);

				md.DefaultResponse = ResponseType.Accept;
				md.ShowAll();
				int result = md.Run();
				switch(result) {
				case (int)ResponseType.Reject:
					shouldQuit = true;
					break;
				case (int)ResponseType.Accept:
					shouldQuit = SaveFile();
					break;
				}

				if(!shouldQuit)
					md.Destroy();
			}

			return shouldQuit;
		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			if(ShouldQuit())
				Application.Quit();
			else
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
			if(ShouldQuit())
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
