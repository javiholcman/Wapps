using System;
using UIKit;

namespace Wapps.Forms.IOS
{
	public class WUILabel : UILabel
	{
		public UIEdgeInsets EdgeInsets { get; set; }

		public WUILabel()
		{
			EdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
		}

		public override void DrawText(CoreGraphics.CGRect rect)
		{
			base.DrawText(EdgeInsets.InsetRect(rect));
		}
	}
}
