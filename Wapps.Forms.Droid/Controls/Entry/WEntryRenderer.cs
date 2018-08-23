// ***********************************************************************
// Assembly         : XLabs.Forms.Droid
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="WEntryRenderer.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Text;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using Wapps.Forms.Controls;
using Wapps.Forms.Controls.Droid;
using Wapps.Forms.Droid;
using Android.Views.InputMethods;
using Android.Content;

[assembly: ExportRenderer(typeof(WEntry), typeof(WEntryRenderer))]

namespace Wapps.Forms.Controls.Droid
{
    /// <summary>
    /// Class WEntryRenderer.
    /// </summary>
    public class WEntryRenderer : EntryRenderer
    {
        public WEntryRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (WEntry)e.NewElement;

            if (view != null)
            {
                SetHasBorder(view);
                SetSuggestionsBarVisibleProperty(view);
                SetReturnKey(view);
            }
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (WEntry)Element;

            if (e.PropertyName == WEntry.SuggestionsBarVisibleProperty.PropertyName)
                SetSuggestionsBarVisibleProperty(view);
            else if (e.PropertyName == WEntry.HasBorderProperty.PropertyName)
                SetHasBorder(view);
            else if (e.PropertyName == WEntry.ReturnKeyTypeProperty.PropertyName)
                SetReturnKey(view);
        }

        void SetHasBorder(WEntry view)
        {
            if (!view.HasBorder)
            {
                Control.SetBackgroundColor(Color.Transparent.ToAndroid());
                Control.SetPadding(0, 0, 0, 0);
            }
        }

        void SetSuggestionsBarVisibleProperty(WEntry view)
        {
            if (!view.SuggestionsBarVisible)
            {
                Control.InputType = Control.InputType | InputTypes.TextFlagNoSuggestions;
                Control.PrivateImeOptions = "nm";
            }
        }

        void SetReturnKey(WEntry view)
        {
            Control.ImeOptions = view.ReturnKeyType.GetValueFromDescription();
            // This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
            Control.SetImeActionLabel(view.ReturnKeyType.ToString(), Control.ImeOptions);
        }
    }

    public static class EnumExtensions
    {
        public static ImeAction GetValueFromDescription(this WEntry.ReturnKeyTypes value)
        {
            return ImeAction.Done;

            var type = typeof(ImeAction);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == value.ToString())
                        return (ImeAction)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString())
                        return (ImeAction)field.GetValue(null);
                }
            }
            return ImeAction.Done;
            //throw new NotSupportedException($"Not supported on Android: {value}");
        }
    }
}

