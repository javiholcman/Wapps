using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public class WPicker : Picker
    {

        #region Property: HorizontalTextAlignment

        /// <summary>
        /// The XA lign property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create("HorizontalTextAlignment", typeof(TextAlignment), typeof(WPicker), TextAlignment.Start);

        /// <summary>
        /// Gets or sets the horizontal text alignment.
        /// </summary>
        /// <value>The horizontal text alignment.</value>
        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        #endregion

        #region Property: HasBorder

        /// <summary>
        /// The has border property.
        /// </summary>
        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create("HasBorder", typeof(bool), typeof(WPicker), true);

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Wapps.Forms.Controls.WPicker"/> has border.
        /// </summary>
        /// <value><c>true</c> if has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        #endregion

        #region Property: Placeholder

        /// <summary>
        /// The placeholder property.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(WPicker), "");

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        #endregion

        #region Property: PlaceholderTextColor

        /// <summary>
        /// The placeholder text color property.
        /// </summary>
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(WPicker), Color.Default);

        /// <summary>
        /// Gets or sets the color of the placeholder text.
        /// </summary>
        /// <value>The color of the placeholder text.</value>
        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        #endregion

        public WPicker()
        {

        }

    }
}
