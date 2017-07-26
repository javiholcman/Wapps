using System;
using UIKit;
using Wapps.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WViewCell), typeof(Wapps.Forms.IOS.Controls.ViewCell.WViewCellRenderer))]

namespace Wapps.Forms.IOS.Controls.ViewCell
{
    public class WViewCellRenderer : ViewCellRenderer
    {
        public WViewCellRenderer()
        {
        }

        public override UIKit.UITableViewCell GetCell(Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            if (item.Parent is WTableView)
            {
                if (!(item.Parent as WTableView).SelectionEnabled)
                    cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;
                else
                    cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.Default;
            }
            else if (item.Parent is WListView)
            {
                var listview = item.Parent as WListView;

                if (!(listview.Parent as WListView).SelectionEnabled)
                    cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;
                else
                    cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.Default;


                if (listview.SelectionColor != Color.Default)
                {
                    var bgView = new UIView();
                    bgView.BackgroundColor = listview.SelectionColor.ToUIColor();
                    cell.SelectedBackgroundView = bgView;
                }
            }

            return cell;
        }
    }
}
