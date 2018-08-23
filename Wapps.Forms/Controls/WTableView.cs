using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public class WTableView : TableView
    {
        #region Property: SeparatorColor

        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create("SeparatorColor", typeof(Color), typeof(WTableView), Color.Silver);

        /// <summary>
        /// Gets or sets the SeparatorColor
        /// </summary>
        public Color SeparatorColor
        {
            get { return (Color)GetValue(SeparatorColorProperty); }
            set { SetValue(SeparatorColorProperty, value); }
        }

        #endregion

        #region Property: SelectionEnabled

        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty SelectionEnabledProperty = BindableProperty.Create("SelectionEnabled", typeof(bool), typeof(WTableView), true);

        /// <summary>
        /// Gets or sets the SeparatorColor
        /// </summary>
        public bool SelectionEnabled
        {
            get { return (bool)GetValue(SelectionEnabledProperty); }
            set { SetValue(SelectionEnabledProperty, value); }
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

        public WTableView()
        {

        }
    }
}
