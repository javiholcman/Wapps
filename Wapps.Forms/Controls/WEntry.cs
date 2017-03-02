using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	/// <summary>
	/// An extended entry control 
	/// </summary>
	public class WEntry : Entry
	{
		#region Property: MaxLength

		/// <summary>
		/// The MaxLength property
		/// </summary>
		public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(WEntry), int.MaxValue);

		/// <summary>
		/// Gets or sets the MaxLength
		/// </summary>
		public int MaxLength
		{
			get { return (int)this.GetValue(MaxLengthProperty); }
			set { this.SetValue(MaxLengthProperty, value); }
		}

		#endregion

		#region Property: Font

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty FontProperty = BindableProperty.Create("Font", typeof(Font), typeof(WEntry), new Font());

		/// <summary>
		/// Gets or sets the Font
		/// </summary>
		public Font Font
		{
			get { return (Font)GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		#endregion

		#region Property: HasBorder

		/// <summary>
		/// The HasBorder property
		/// </summary>
		public static readonly BindableProperty HasBorderProperty = BindableProperty.Create("HasBorder", typeof(bool), typeof(WEntry), true);

		/// <summary>
		/// Gets or sets if the border should be shown or not
		/// </summary>
		public bool HasBorder
		{
			get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

		#endregion

		#region Property: ReturnKey

		public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create("ReturnKeyType", typeof(ReturnKeyTypes), typeof(WEntry), ReturnKeyTypes.Done);

		public ReturnKeyTypes ReturnKeyType
		{
			get { return (ReturnKeyTypes)GetValue(ReturnKeyTypeProperty); }
			set { SetValue(ReturnKeyTypeProperty, value); }
		}

		public enum ReturnKeyTypes : int
		{
			Default,
			Go,
			Google,
			Join,
			Next,
			Route,
			Search,
			Send,
			Yahoo,
			Done,
			EmergencyCall,
			Continue
		}
		#endregion		

		#region Property: SuggestionsBarVisible

		public static readonly BindableProperty SuggestionsBarVisibleProperty = BindableProperty.Create("SuggestionsBarVisible", typeof(bool), typeof(WEntry), true);

		public bool SuggestionsBarVisible
		{
			get { return (bool)GetValue(SuggestionsBarVisibleProperty); }
			set { SetValue(SuggestionsBarVisibleProperty, value); }
		}

		#endregion

		public WEntry()
		{
			FontSize = 15;
		}
	}
}