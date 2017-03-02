using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Wapps.Forms.IOS.Controls;
using Xamarin.Forms.Platform.iOS;
using Wapps.Forms.Controls;

[assembly: ExportRenderer(typeof(WEntry), typeof(Wapps.Forms.IOS.Controls.WEntryRenderer))]

namespace Wapps.Forms.IOS.Controls
{
	/// <summary>
	/// A renderer for the WEntry control.
	/// </summary>
	public class WEntryRenderer : EntryRenderer
	{
		
		/// <summary>
		/// The on element changed callback.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			var view = e.NewElement as WEntry;

			if (view != null)
			{
				SetFont (view);
				SetBorder (view);
				SetMaxLength (view);
				SetSuggestionsBarVisibleProperty(view);
				SetReturnKey(view);
			}
		}

		/// <summary>
		/// The on element property changed callback
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (WEntry)Element;

			if (e.PropertyName == WEntry.FontProperty.PropertyName)
				SetFont(view);
			else if (e.PropertyName == WEntry.HasBorderProperty.PropertyName)
				SetBorder(view);
			else if (e.PropertyName == WEntry.SuggestionsBarVisibleProperty.PropertyName)
				SetSuggestionsBarVisibleProperty(view);
			else if (e.PropertyName == WEntry.ReturnKeyTypeProperty.PropertyName) 
				SetReturnKey(view);
		}

		void SetSuggestionsBarVisibleProperty(WEntry view)
		{
			if (!view.SuggestionsBarVisible)
			{
				Control.AutocorrectionType = UITextAutocorrectionType.No;
			}
			else 
			{
				Control.AutocorrectionType = UITextAutocorrectionType.Default;
			}
		}

		void SetFont(WEntry view)
		{
			var fontSize = (int)view.Font.FontSize;
			if (fontSize == 0)
				fontSize = 15;

			UIFont uiFont;
			if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
				Control.Font = uiFont.WithSize(fontSize);
			else if (view.Font == Font.Default)
			{
				Control.Font = UIFont.SystemFontOfSize((nfloat)fontSize);
			}
		}

		void SetBorder(WEntry view)
		{
			Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
		}

		void SetMaxLength(WEntry view)
		{
			Control.ShouldChangeCharacters = (textField, range, replacementString) =>
			{
				var newLength = textField.Text.Length + replacementString.Length - range.Length;
				return newLength <= view.MaxLength;
			};
		}

		#region Property: ReturnKey 

		void SetReturnKey(WEntry view)
		{
			Control.ReturnKeyType = view.ReturnKeyType.GetValueFromDescription();
		}

		#endregion

	}

	public static class EnumExtensions
	{
		public static UIReturnKeyType GetValueFromDescription(this WEntry.ReturnKeyTypes value)
		{
			var type = typeof(UIReturnKeyType);
			if (!type.IsEnum) throw new InvalidOperationException();
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null)
				{
					if (attribute.Description == value.ToString())
						return (UIReturnKeyType)field.GetValue(null);
				}
				else
				{
					if (field.Name == value.ToString())
						return (UIReturnKeyType)field.GetValue(null);
				}
			}
			return UIReturnKeyType.Default;
			throw new NotSupportedException($"Not supported on iOS: {value}");
		}
	}
}