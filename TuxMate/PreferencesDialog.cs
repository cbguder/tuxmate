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
		static string[] encodings = { "iso-8859-1", "windows-1252", "utf-8", "utf-16", "utf-32" };
		static string[] endings   = { "<LF>", "<CR>", "<CR><LF>" };

		public PreferencesDialog()
		{
			Build();
			Preferences.AddNotify(new GConf.NotifyEventHandler(OnGConfChanged));
			OnGConfChanged(null, null);
		}

		protected void OnGConfChanged(object sender, System.EventArgs e)
		{
			highlightCurrentLine.Active          = Preferences.HighlightCurrentLine;
			showRightMarginIndicator.Active      = Preferences.ShowRightMarginIndicator;
			highlightRightMargin.Active          = Preferences.HighlightRightMargin;
			displayGroupsAndFoldersInBold.Active = Preferences.DisplayGroupsAndFoldersInBold;

			reindentPastedText.Active            = Preferences.ReindentPastedText;
			autopairCharacters.Active            = Preferences.AutoPairCharacters;

			useSystemFont.Active                 = Preferences.UseSystemFont;
			fontButton.FontName                  = Preferences.Font;

			fileEncoding.Active                  = Array.BinarySearch(encodings, Preferences.FileEncoding);
			lineEndings.Active                   = Array.BinarySearch(endings,   Preferences.LineEndings);
		}

		protected virtual void OnHighlightCurrentLineClicked(object sender, System.EventArgs e)
		{
			Preferences.HighlightCurrentLine = highlightCurrentLine.Active;
		}

		protected virtual void OnShowRightMarginIndicatorClicked(object sender, System.EventArgs e)
		{
			Preferences.ShowRightMarginIndicator = showRightMarginIndicator.Active;
			highlightRightMargin.Sensitive = showRightMarginIndicator.Active;
		}

		protected virtual void OnHighlightRightMarginClicked(object sender, System.EventArgs e)
		{
			Preferences.HighlightRightMargin = highlightRightMargin.Active;
		}

		protected virtual void OnDisplayGroupsAndFoldersInBoldClicked(object sender, System.EventArgs e)
		{
			Preferences.DisplayGroupsAndFoldersInBold = displayGroupsAndFoldersInBold.Active;
		}

		protected virtual void OnReindentPastedTextClicked(object sender, System.EventArgs e)
		{
			Preferences.ReindentPastedText = reindentPastedText.Active;
		}

		protected virtual void OnAutopairCharactersClicked(object sender, System.EventArgs e)
		{
			Preferences.AutoPairCharacters = autopairCharacters.Active;
		}

		protected virtual void OnUseSystemFontClicked(object sender, System.EventArgs e)
		{
			Preferences.UseSystemFont = useSystemFont.Active;
			fontLabel.Sensitive = fontButton.Sensitive = !useSystemFont.Active;
		}

		protected virtual void OnFontButtonFontSet(object sender, System.EventArgs e)
		{
			Preferences.Font = fontButton.FontName;
		}

		protected virtual void OnFileEncodingChanged(object sender, System.EventArgs e)
		{
			Preferences.FileEncoding = encodings[fileEncoding.Active];
		}

		protected virtual void OnLineEndingsChanged(object sender, System.EventArgs e)
		{
			Preferences.LineEndings = endings[lineEndings.Active];
		}
	}
}
