using System;
using CoreGraphics;
using UIKit;
using Wapps.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WEditor), typeof(Wapps.Forms.IOS.Controls.WEditorRenderer))]

namespace Wapps.Forms.IOS.Controls
{
    public class WEditorRenderer : EditorRenderer
    {
        UILabel LblPlaceHolder { get; set; }

        public WEditorRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.LblPlaceHolder == null)
                {
                    this.LblPlaceHolder = new UILabel();
                    this.LblPlaceHolder.TextColor = UIColor.LightGray;
                    this.Control.Changed += Control_Changed;
                    this.Control.AddSubview(this.LblPlaceHolder);
                    SetPlaceholder();
                }
            }

            if (e.NewElement == null)
            {
                this.Control.Changed -= Control_Changed;
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            LblPlaceHolder.Frame = new CGRect(5, 10, this.Control.Frame.Width - 10, this.Control.Frame.Height - 20);
            LblPlaceHolder.SizeToFit();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == WEditor.PlaceholderProperty.PropertyName)
            {
                SetPlaceholder();
            }
        }

        void SetPlaceholder()
        {
            var element = this.Element as WEditor;
            this.LblPlaceHolder.Text = element.Placeholder;
            this.LblPlaceHolder.ClipsToBounds = true;

            if (Control.Font != null)
                LblPlaceHolder.Font = this.Control.Font.WithSize((nfloat)element.FontSize);
            else
                LblPlaceHolder.Font = UIFont.SystemFontOfSize((nfloat)element.FontSize);
        }

        void Control_Changed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Control.Text))
            {
                LblPlaceHolder.Hidden = false;
            }
            else
            {
                LblPlaceHolder.Hidden = true;
            }
        }
    }
}
