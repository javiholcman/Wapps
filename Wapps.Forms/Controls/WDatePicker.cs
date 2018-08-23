using System;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public class WDatePicker : DatePicker
    {
        #region Property: HorizontalTextAlignment

        /// <summary>
        /// The XA lign property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create("HorizontalTextAlignment", typeof(TextAlignment), typeof(WDatePicker), TextAlignment.Start);

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
        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create("HasBorder", typeof(bool), typeof(WDatePicker), true);

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
        /// The has border property.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(WDatePicker), "");

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
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

        #region Property: SelectedDate

        /// <summary>
        /// The has border property.
        /// </summary>
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create("SelectedDate", typeof(DateTime?), typeof(WDatePicker), null);

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set
            {
                SetValue(SelectedDateProperty, value);
                if (value != null)
                    Date = value.Value;
                else if (SelectedDate != null)
                    SelectedDateChanged?.Invoke(this, null);
            }
        }

        public event EventHandler SelectedDateChanged;

        #endregion

        public WDatePicker()
        {

        }

        protected override void OnParentSet()
        {
            if (Parent != null)
                DateSelected += Handle_DateSelected;
            else
                DateSelected -= Handle_DateSelected;

            base.OnParentSet();
        }

        void Handle_DateSelected(object sender, DateChangedEventArgs e)
        {
            SelectedDate = new DateTime?(Date);
            SelectedDateChanged?.Invoke(this, null);
        }
    }
}
