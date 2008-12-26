// AboutDialog.cs
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
using System.Reflection;

namespace TuxMate
{
	public partial class AboutDialog: Gtk.AboutDialog
	{
		public AboutDialog()
		{
			Assembly asm = Assembly.GetExecutingAssembly();

			ProgramName = (asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0] as AssemblyTitleAttribute).Title;
			Version = asm.GetName().Version.ToString();
			Copyright = (asm.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute).Copyright;
			Authors = new string[] {"Can Berk Güder"};
			Website = "http://cbg.me/";
		}
	}
}
