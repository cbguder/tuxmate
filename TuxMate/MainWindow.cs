// MainWindow.cs
//
// Copyright (C) 2008 Can Berk Güder
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using Gtk;
using GtkSourceView;

namespace TuxMate
{
	public partial class MainWindow: Gtk.Window
	{
		string filename;
		string originalBuffer;

		SourceLanguagesManager manager;
		SourceLanguage language;
		SourceBuffer buffer;
		SourceView textView;

		protected string Filename
		{
			get { return filename; }
			set { filename = value; SetTitle(filename); }
		}

		public MainWindow(): base(Gtk.WindowType.Toplevel)
		{
			Build();

			manager = new SourceLanguagesManager();
			language = manager.GetLanguageFromMimeType("text/x-csharp");

			buffer = new SourceBuffer(language);
			buffer.Highlight = true;

			textView = new SourceView(buffer);
			textView.ShowLineNumbers = true;
			textView.TabsWidth = 4;
			textView.ModifyFont(Pango.FontDescription.FromString("monospace 10"));

			scrolledwindow.Add(textView);
			scrolledwindow.ShowAll();

			this.Focus = textView;
			originalBuffer = "";
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
			if(ShouldQuit())
			{
				Filename = null;
				textView.Buffer.Text = originalBuffer = "";
			}
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

				CloseConfirmationDialog closeConfirmationDialog = new CloseConfirmationDialog(Filename);
				int result = closeConfirmationDialog.Run();
				closeConfirmationDialog.Destroy();

				switch(result) {
				case (int)ResponseType.Reject:
					shouldQuit = true;
					break;
				case (int)ResponseType.Accept:
					shouldQuit = SaveFile();
					break;
				}
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

		protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
		{
			AboutDialog aboutDialog = new AboutDialog();
			aboutDialog.Run();
			aboutDialog.Destroy();
		}
	}
}
