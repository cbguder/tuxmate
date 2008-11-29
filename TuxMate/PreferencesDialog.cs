// PreferencesDialog.cs
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

namespace TuxMate
{
	public partial class PreferencesDialog : Gtk.Dialog
	{
		public PreferencesDialog()
		{
			this.Build();
		}

		protected virtual void OnShowRightMarginIndicatorClicked (object sender, System.EventArgs e)
		{
			highlightRightMargin.Sensitive = ((Gtk.CheckButton)sender).Active;
		}

		protected virtual void OnUseSystemFontClicked (object sender, System.EventArgs e)
		{
			bool newStatus = !((Gtk.CheckButton)sender).Active;
			fontLabel.Sensitive = fontButton.Sensitive = newStatus;
		}
	}
}
