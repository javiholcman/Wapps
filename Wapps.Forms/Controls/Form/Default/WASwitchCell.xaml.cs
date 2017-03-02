using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public partial class WASwitchCell : WSwitchCell
	{
		public WASwitchCell()
		{
			InitializeComponent();
		}

		public override Switch Switch
		{
			get { return ValueSwitch; }
		}

		public override Label LblTitle
		{
			get { return TitleLabel; }
		}
	}
}
