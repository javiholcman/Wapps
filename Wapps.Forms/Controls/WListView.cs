using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public class WListView : ListView
	{
		#region Property: SelectionEnabled

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty SelectionEnabledProperty = BindableProperty.Create("SelectionEnabled", typeof(bool), typeof(WListView), true);

		/// <summary>
		/// Gets or sets the SeparatorColor
		/// </summary>
		public bool SelectionEnabled
		{
			get { return (bool)GetValue(SelectionEnabledProperty); }
			set { SetValue(SelectionEnabledProperty, value); }
		}

		#endregion

		#region Property: SelectionColor

		/// <summary>
		/// The font property
		/// </summary>
		public static readonly BindableProperty SelectionColorProperty = BindableProperty.Create("SelectionColor", typeof(Color), typeof(WListView), Color.Default);

		/// <summary>
		/// Gets or sets the SelectionColorProperty
		/// </summary>
		public Color SelectionColor
		{
			get { return (Color)GetValue(SelectionColorProperty); }
			set { SetValue(SelectionColorProperty, value); }
		}

		#endregion

		#region Property: ScrollEnabled

		/// <summary>
		/// The ScrollEnabledProperty
		/// </summary>
		public static readonly BindableProperty ScrollEnabledProperty = BindableProperty.Create("ScrollEnabled", typeof(bool), typeof(WListView), true);

		/// <summary>
		/// Gets or sets the ScrollEnabledProperty
		/// </summary>
		public bool ScrollEnabled
		{
			get { return (bool)GetValue(ScrollEnabledProperty); }
			set { SetValue(ScrollEnabledProperty, value); }
		}

		#endregion


		#region Method: ScrollToTop

		/// <summary>
		/// Must be used just by renderers.
		/// </summary>
		/// <value>The scroll to top delegate.</value>
		public Action ScrollToTopDelegate { get; set; }

		public void ScrollToTop()
		{
			ScrollToTopDelegate?.Invoke();
		}

		#endregion

		public WListView()
		{
		}
	}
}
