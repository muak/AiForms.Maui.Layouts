using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// コンテンツを親要素のサイズに合わせてFitさせるレイアウトです。
/// </summary>
public class FitLayout : Layout
{
    public static BindableProperty OrientationProperty =
        BindableProperty.Create(
            nameof(Orientation),
            typeof(ItemsLayoutOrientation),
            typeof(FitLayout),
            ItemsLayoutOrientation.Vertical,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((FitLayout)bindable).InvalidateMeasure()
        );

    /// <summary>
    /// Fitさせる方向を指定します。
    /// OrientationがVerticalの場合、コンテンツの高さを画面に合わせて縮小します。
    /// </summary>
    public ItemsLayoutOrientation Orientation
    {
        get => (ItemsLayoutOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public static BindableProperty EstimatedHeightProperty =
        BindableProperty.Create(
            nameof(EstimatedHeight),
            typeof(double),
            typeof(FitLayout),
            -1d,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((FitLayout)bindable).InvalidateMeasure()
        );

    /// <summary>
    /// 本来想定される高さを指定します。
    /// 未設定の場合はコンテンツの高さを無制限に計測します。
    /// OrientationがVerticalの場合に有効です。
    /// </summary>
    public double EstimatedHeight
    {
        get => (double)GetValue(EstimatedHeightProperty);
        set => SetValue(EstimatedHeightProperty, value);
    }

    public static BindableProperty EstimatedWidthProperty =
        BindableProperty.Create(
            nameof(EstimatedWidth),
            typeof(double),
            typeof(FitLayout),
            -1d,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((FitLayout)bindable).InvalidateMeasure()
        );

    /// <summary>
    /// 本来想定される幅を指定します。
    /// 未設定の場合はコンテンツの幅を無制限に計測します。
    /// OrientationがHorizontalの場合に有効です。
    /// </summary>
    public double EstimatedWidth
    {
        get => (double)GetValue(EstimatedWidthProperty);
        set => SetValue(EstimatedWidthProperty, value);
    }
    
    protected override ILayoutManager CreateLayoutManager()
    {
        return new FitLayoutManager(this); 
    } 
}


