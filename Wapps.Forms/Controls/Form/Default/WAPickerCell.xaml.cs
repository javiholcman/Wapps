using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public partial class WAPickerCell : WPickerCell
	{
		public WAPickerCell()
		{
			InitializeComponent();
		}

		public override WPicker Picker
		{
			get { return ValuePicker; }
		}

		public override Label LblError
		{
			get { return ErrorLabel; }
		}

		public override Label LblTitle
		{
			get { return TitleLabel; }
		}
	}
}
