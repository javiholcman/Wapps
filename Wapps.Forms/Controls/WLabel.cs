using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public class WLabel : Label
    {
        #region Property: Lines

        public static readonly BindableProperty LinesProperty = BindableProperty.Create("Lines", typeof(int), typeof(WLabel), -1);

        /// <summary>
        /// Gets or sets the Lines
        /// </summary>
        public int Lines
        {
            get { return (int)GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }

        #endregion

        #region Property: BorderRadius

        public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create("BorderRadius", typeof(int), typeof(WLabel), 0);

        /// <summary>
        /// Gets or sets the Lines
        /// </summary>
        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        #endregion

        #region Property: BorderColor

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(WLabel), Color.Transparent);

        /// <summary>
        /// Gets or sets the Lines
        /// </summary>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        #endregion

        #region Property: BorderWidth

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(WLabel), 0);

        /// <summary>
        /// Gets or sets the Lines
        /// </summary>
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        #endregion

        #region Property: Padding

        /// <summary>
        /// The font property
        /// </summary>
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create("Padding", typeof(Thickness), typeof(WLabel), new Thickness());

        /// <summary>
        /// Gets or sets the Padding
        /// </summary>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        #endregion
    }
}
