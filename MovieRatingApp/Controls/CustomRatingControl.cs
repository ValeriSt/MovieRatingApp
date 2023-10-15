using MovieRatingApp.Common;
using MovieRatingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRatingApp.Controls
{
    public class CustomRatingControl : HorizontalStackLayout
    {
        public static readonly BindableProperty CurrentValueProperty =
         BindableProperty.Create(
             nameof(CurrentValue),
             typeof(double),
             typeof(CustomRatingControl),
             defaultValue: 0d,
             propertyChanged: OnRefreshControl);

        public double CurrentValue
        {
            get => (double)GetValue(CurrentValueProperty);
            set => SetValue(CurrentValueProperty, value);
        }

        public static readonly BindableProperty AmountProperty =
          BindableProperty.Create(
              nameof(Amount),
              typeof(int),
              typeof(CustomRatingControl),
              defaultValue: 10,
              propertyChanged: OnRefreshControl);

        public int Amount
        {
            get => (int)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly BindableProperty StarSizeProperty =
          BindableProperty.Create(
              nameof(StarSize),
              typeof(double),
              typeof(CustomRatingControl),
              defaultValue: 24d,
              propertyChanged: OnRefreshControl);

        public double StarSize
        {
            get => (double)GetValue(StarSizeProperty);
            set => SetValue(StarSizeProperty, value);
        }

        public static readonly BindableProperty AccentColorProperty =
            BindableProperty.Create(
                nameof(AccentColor),
                typeof(Color),
                typeof(CustomRatingControl),
                defaultValue: Colors.Red,
                propertyChanged: OnRefreshControl);

        public Color AccentColor
        {
            get => (Color)GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }


        private static void OnRefreshControl(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRatingControl ratingControl)
                ratingControl.UpdateLayout();
        }


        public CustomRatingControl()
        {
            Spacing = -8;
            UpdateLayout();
        }


        private void UpdateLayout()
        {
            Children.Clear();

            var intValue = (int)ClampValue(CurrentValue);
            var decimalPart = CurrentValue - intValue;
            var isHalfStarNeeded = false;

            if (decimalPart > .25)
            {
                if (decimalPart > 0.25 && decimalPart <= .75)
                    isHalfStarNeeded = true;
                else
                    intValue++;
            }

            for (int i = 0; i < Amount; i++)
            {
                if (intValue > i)
                {
                    Add(CreateStarLabel(StarState.Full));
                }
                else if (intValue <= i && isHalfStarNeeded)
                {
                    Add(CreateStarLabel(StarState.Half));
                    isHalfStarNeeded = false;
                }
                else
                {
                    Add(CreateStarLabel(StarState.Empty));
                }
            }
        }

        private Label CreateStarLabel(StarState state)
        {
            return new Label
            {
                FontFamily = "MaterialDesignIcons",
                TextColor = Application.Current.UserAppTheme == AppTheme.Light ? AccentColor : Color.FromArgb("#FFFFFF"),
                FontSize = StarSize,
                Text = state switch
                {
                    StarState.Empty => MaterialDesignIconsFont.StarOutline,
                    StarState.Half => MaterialDesignIconsFont.StarHalfFull,
                    StarState.Full => MaterialDesignIconsFont.Star,
                    _ => MaterialDesignIconsFont.StarOutline,
                }
            };
        }

        private double ClampValue(double value)
        {
            if (value < 0)
                return 0;

            if (value > Amount)
                return Amount;

            return value;
        }
    }
}
