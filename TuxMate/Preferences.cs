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

		public static bool ShowLineNumbers
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/show_line_numbers");
				} catch {
					return true;
				}
			}
			set { client.Set(KEY_BASE + "/show_line_numbers", value); }
		}

		public static bool HighlightCurrentLine
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/highlight_current_line");
				} catch {
					return false;
				}
			}
			set { client.Set(KEY_BASE + "/highlight_current_line", value); }
		}

		public static bool ShowRightMarginIndicator
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/show_right_margin_indicator");
				} catch {
					return false;
				}
			}
			set { client.Set(KEY_BASE + "/show_right_margin_indicator", value); }
		}

		public static bool HighlightRightMargin
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/highlight_right_margin");
				} catch {
					return false;
				}
			}
			set { client.Set(KEY_BASE + "/highlight_right_margin", value); }
		}

		public static bool DisplayGroupsAndFoldersInBold
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/display_groups_and_folders_in_bold");
				} catch {
					return false;
				}
			}
			set { client.Set(KEY_BASE + "/display_groups_and_folders_in_bold", value); }
		}

		public static bool ReindentPastedText
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/reindent_pasted_text");
				} catch {
					return true;
				}
			}
			set { client.Set(KEY_BASE + "/reindent_pasted_text", value); }
		}

		public static bool AutoPairCharacters
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/autopair_characters");
				} catch {
					return true;
				}
			}
			set { client.Set(KEY_BASE + "/autopair_characters", value); }
		}

		public static bool UseSystemFont
		{
			get	{
				try {
					return (bool)client.Get(KEY_BASE + "/use_system_font");
				} catch {
					return true;
				}
			}
			set { client.Set(KEY_BASE + "/use_system_font", value); }
		}

		public static string Font
		{
			get {
				try {
					return (string)client.Get(KEY_BASE + "/font");
				} catch {
					return "monospace 10";
				}
			}
			set { client.Set(KEY_BASE + "/font", value); }
		}

		public static string FileEncoding
		{
			get {
				try {
					return (string)client.Get(KEY_BASE + "/file_encoding");
				} catch {
					return "utf-8";
				}
			}
			set { client.Set(KEY_BASE + "/file_encoding", value); }
		}

		public static string LineEndings
		{
			get {
				try {
					return (string)client.Get(KEY_BASE + "/line_endings");
				} catch {
					return "<LF>";
				}
			}
			set { client.Set(KEY_BASE + "/line_endings", value); }
		}

		public static void AddNotify(GConf.NotifyEventHandler notify)
		{
			client.AddNotify(KEY_BASE, notify);
		}
	}
}
