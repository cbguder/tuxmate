// PreferencesDialog.cs created with MonoDevelop
// User: cbguder at 12:18 AM 11/15/2008
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
