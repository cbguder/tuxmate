// Preferences.cs
//
// Copyright (C) 2009 Can Berk Güder
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
	public static class Preferences
	{
		private static string KEY_BASE = "/apps/tuxmate";
		private static GConf.Client client = new GConf.Client();

		private static object Get(string key, object def)
		{
			try {
				return client.Get(KEY_BASE + "/" + key);
			} catch {
				return def;
			}
		}

		private static void Set(string key, object val)
		{
			client.Set(KEY_BASE + "/" + key, val);
		}

		public static void AddNotify(GConf.NotifyEventHandler notify)
		{
			client.AddNotify(KEY_BASE, notify);
		}

		public static bool ShowLineNumbers
		{
			get	{ return (bool)Get("show_line_numbers", false); }
			set { Set("show_line_numbers", value); }
		}

		public static bool HighlightCurrentLine
		{
			get	{ return (bool)Get("highlight_current_line", false); }
			set { Set("highlight_current_line", value); }
		}

		public static bool ShowRightMarginIndicator
		{
			get	{ return (bool)Get("show_right_margin_indicator", false); }
			set { Set("show_right_margin_indicator", value); }
		}

		public static bool HighlightRightMargin
		{
			get	{ return (bool)Get("highlight_right_margin", false); }
			set { Set("highlight_right_margin", value); }
		}

		public static bool DisplayGroupsAndFoldersInBold
		{
			get	{ return (bool)Get("display_groups_and_folders_in_bold", false); }
			set { Set("display_groups_and_folders_in_bold", value); }
		}

		public static bool ReindentPastedText
		{
			get	{ return (bool)Get("reindent_pasted_text", true); }
			set { Set("reindent_pasted_text", value); }
		}

		public static bool AutoPairCharacters
		{
			get	{ return (bool)Get("autopair_characters", true); }
			set { Set("autopair_characters", value); }
		}

		public static bool UseSystemFont
		{
			get	{ return (bool)Get("use_system_font", true); }
			set { Set("use_system_font", value); }
		}

		public static string Font
		{
			get { return (string)Get("font", "monospace 10"); }
			set { Set("font", value); }
		}

		public static string FileEncoding
		{
			get { return (string)Get("file_encoding", "utf-8"); }
			set { Set("file_encoding", value); }
		}

		public static string LineEndings
		{
			get { return (string)Get("line_endings", "<LF>"); }
			set { Set("line_endings", value); }
		}
	}
}
