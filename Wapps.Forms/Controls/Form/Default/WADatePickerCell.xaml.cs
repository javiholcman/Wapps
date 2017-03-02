using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public partial class WADatePickerCell : WDatePickerCell
	{
		public WADatePickerCell()
		{
			InitializeComponent();
		}

		public override WDatePicker DatePicker
		{
			get { return ValueDatePicker; }
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
