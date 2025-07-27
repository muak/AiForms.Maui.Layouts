using System;
using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// 自動的に折り返しを行うレイアウトです。
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
    /// 要素間のスペースを指定します。
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
    /// 均等幅のカラム数を指定します。
    /// 1以上で画面幅に合わせて均等に分割されます。
    /// 0の場合は要素の幅に合わせて折り返しを行います。
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
    /// 要素を正方形にするかどうかを指定します。
    /// trueの場合、要素の幅と高さを同じにします。
    /// UniformColumnsが1以上の場合のみ有効です。
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

