using System;
using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// A layout that automatically wraps elements.
/// </summary>
public class WrapLayout: Layout
{
    public static BindableProperty SpacingProperty = BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(WrapLayout),
            0d,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((WrapLayout)bindable).InvalidateMeasure()
        );

    /// <summary>
    /// Specifies the space between elements.
    /// </summary>
    public double Spacing{
        get { return (double)GetValue(SpacingProperty); }
        set { SetValue(SpacingProperty, value); }
    }

    public static BindableProperty UniformColumnsProperty = BindableProperty.Create(
            nameof(UniformColumns),
            typeof(int),
            typeof(WrapLayout),
            0,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((WrapLayout)bindable).InvalidateMeasure()
        );

    /// <summary>
    /// Specifies the number of uniform-width columns.
    /// When set to 1 or higher, elements are divided evenly according to screen width.
    /// When set to 0, wrapping is performed according to element width.
    /// </summary>
    public int UniformColumns{
        get { return (int)GetValue(UniformColumnsProperty); }
        set { SetValue(UniformColumnsProperty, value); }
    }

    public static BindableProperty IsSquareProperty = BindableProperty.Create(
            nameof(IsSquare),
            typeof(bool),
            typeof(WrapLayout),
            false,
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldValue, newValue) => ((WrapLayout)bindable).InvalidateMeasure()
        );

    /// <summary>
    /// Specifies whether to make elements square.
    /// When true, makes element width and height equal.
    /// Only effective when UniformColumns is 1 or higher.
    /// </summary>
    public bool IsSquare{
        get { return (bool)GetValue(IsSquareProperty); }
        set { SetValue(IsSquareProperty, value); }
    }

    protected override ILayoutManager CreateLayoutManager()
    {
        return new WrapLayoutManager(this);
    }
}

