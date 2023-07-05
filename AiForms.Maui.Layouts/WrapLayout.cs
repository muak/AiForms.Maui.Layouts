using System;
using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

public class WrapLayout: Layout
{
    public static BindableProperty SpacingProperty = BindableProperty.Create(
            nameof(Spacing),
            typeof(double),
            typeof(WrapLayout),
            default(double),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).InvalidateMeasure()
        );

    public double Spacing{
        get { return (double)GetValue(SpacingProperty); }
        set { SetValue(SpacingProperty, value); }
    }

    public static BindableProperty UniformColumnsProperty = BindableProperty.Create(
            nameof(UniformColumns),
            typeof(int),
            typeof(WrapLayout),
            default(int),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).InvalidateMeasure()
        );

    public int UniformColumns{
        get { return (int)GetValue(UniformColumnsProperty); }
        set { SetValue(UniformColumnsProperty, value); }
    }

    public static BindableProperty IsSquareProperty = BindableProperty.Create(
            nameof(IsSquare),
            typeof(bool),
            typeof(WrapLayout),
            default(bool),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).InvalidateMeasure()
        );

    public bool IsSquare{
        get { return (bool)GetValue(IsSquareProperty); }
        set { SetValue(IsSquareProperty, value); }
    }

    protected override ILayoutManager CreateLayoutManager()
    {
        return new WrapLayoutManager(this);
    }
}

