using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public partial class WAEntryCell : WEntryCell
	{
		public WAEntryCell()
		{
			InitializeComponent();
		}

		public override WEntry Entry
		{
			get { return TxtValue; }
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
