// CloseConfirmationDialog.cs
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
using Gtk;

namespace TuxMate
{
	public partial class CloseConfirmationDialog: Gtk.MessageDialog
	{
		Button closeButton;
		Button cancelButton;
		Button saveButton;

		public CloseConfirmationDialog(string filename)
		{
			Modal = true;
			MessageType = Gtk.MessageType.Warning;

			Markup = "<span weight=\"bold\" size=\"larger\">Save changes to document before closing?</span>";
			SecondaryText = "Your changes will be lost if you don't save them.";

			closeButton  = new Button("Close without saving");
			cancelButton = new Button(Stock.Cancel);

			if(filename == null)
				saveButton = new Button(Stock.SaveAs);
			else
				saveButton = new Button(Stock.Save);

			saveButton.CanDefault = true;

			AddActionWidget(closeButton,  ResponseType.Reject);
			AddActionWidget(cancelButton, ResponseType.Cancel);
			AddActionWidget(saveButton, ResponseType.Accept);

			DefaultResponse = ResponseType.Accept;
			ShowAll();
		}
	}
}
