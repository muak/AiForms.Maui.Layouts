using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// A layout that fits content to the parent element's size.
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
    /// Specifies the direction for fitting.
    /// When Orientation is Vertical, the content height is scaled down to fit the screen.
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
    /// Specifies the originally intended height.
    /// If not set, content height is measured without limit.
    /// Effective when Orientation is Vertical.
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
    /// Specifies the originally intended width.
    /// If not set, content width is measured without limit.
    /// Effective when Orientation is Horizontal.
    /// </summary>
    public double EstimatedWidth
    {
        get => (double)GetValue(EstimatedWidthProperty);
        set => SetValue(EstimatedWidthProperty, value);
    }

    public static BindableProperty ScaleMarginProperty =
        BindableProperty.Create(
            nameof(ScaleMargin),
            typeof(double),
            typeof(FitLayout),
            0d,
            defaultBindingMode: BindingMode.OneWay
        );

    /// <summary>
    /// Specifies margin to provide extra space when scaling down.
    /// Default is 0 (0%).
    /// For example, when ScaleMargin is 0.05, if the content height exceeds the screen height,
    /// the content height is scaled down to 95% of the screen height.
    /// </summary>
    public double ScaleMargin
    {
        get => (double)GetValue(ScaleMarginProperty);
        set => SetValue(ScaleMarginProperty, value);
    }
    
    protected override ILayoutManager CreateLayoutManager()
    {
        return new FitLayoutManager(this); 
    } 
}


