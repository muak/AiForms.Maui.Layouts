using System;
using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// Layout manager for WrapLayout.
/// </summary>
/// <param name="_layout"></param>
public class WrapLayoutManager(WrapLayout _layout) : ILayoutManager
{
    double WidthRequest => _layout.WidthRequest;
    double HeightRequest => _layout.HeightRequest;
    int UniformColumns => _layout.UniformColumns;
    double Spacing => _layout.Spacing;
    bool IsSquare => _layout.IsSquare;

    /// <summary>
    /// Arranges the child elements of WrapLayout.
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public Size ArrangeChildren(Rect bounds)
    {
        if(_layout.UniformColumns > 0)
        {
            return UniformMeasureAndLayout(bounds.Width, bounds.Height, true, bounds.X, bounds.Y);
        }
        else
        {
            return VariableMeasureAndLayout(bounds.Width, bounds.Height, true, bounds.X, bounds.Y);            
        }
    }    

    /// <summary>
    /// Measures the size of WrapLayout.
    /// </summary>
    /// <param name="widthConstraint"></param>
    /// <param name="heightConstraint"></param>
    /// <returns></returns>
    public Size Measure(double widthConstraint, double heightConstraint)
    {

        if (WidthRequest > 0)
            widthConstraint = Math.Min(widthConstraint, WidthRequest);
        if (HeightRequest > 0)
            heightConstraint = Math.Min(heightConstraint, HeightRequest);

        double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
        double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

        if (double.IsPositiveInfinity(widthConstraint) && double.IsPositiveInfinity(heightConstraint))
        {
            return Size.Zero;
        }        

        return UniformColumns > 0 ? UniformMeasureAndLayout(internalWidth, internalHeight) :
                                    VariableMeasureAndLayout(internalWidth, internalHeight);
    }

    private Size UniformMeasureAndLayout(double widthConstraint, double heightConstraint, bool doLayout = false, double x = 0, double y = 0)
    {
        double totalWidth = 0;
        double totalHeight = 0;
        double rowHeight = 0;
        double minWidth = 0;
        double minHeight = 0;
        double xPos = x + _layout.Padding.Left;
        double yPos = y + _layout.Padding.Top;
        double limitX = x + widthConstraint - _layout.Padding.Left + 0.0001d;

        totalWidth = widthConstraint;
        widthConstraint -= _layout.Padding.HorizontalThickness;
        heightConstraint -= _layout.Padding.VerticalThickness;

        var Children = _layout.Children;
        
        var exceptedWidth = widthConstraint - (UniformColumns - 1) * Spacing; // expected spacing width

        var columnsSize = exceptedWidth / UniformColumns;
        if (columnsSize < 1)
        {
            columnsSize = 1;
        }

        foreach (var child in Children.Where(c => c.Visibility == Visibility.Visible))
        {

            var size = child.Measure(columnsSize, heightConstraint);
            var itemWidth = (double)columnsSize;
            var itemHeight = size.Height;

            if (IsSquare)
            {
                itemHeight = columnsSize;
            }

            rowHeight = Math.Max(rowHeight, itemHeight + Spacing);

            minHeight = Math.Max(minHeight, itemHeight);
            minWidth = Math.Max(minWidth, itemWidth);

            if (doLayout)
            {
                var region = new Rect(xPos, yPos, itemWidth, itemHeight);
                child.Arrange(region);
            }

            xPos += itemWidth + Spacing;

            if (xPos + columnsSize > limitX)
            {
                xPos = x + _layout.Padding.Left;
                yPos += rowHeight;
                totalHeight += rowHeight;
                rowHeight = 0;
            }
        }

        totalHeight += _layout.Padding.VerticalThickness;
        totalHeight = Math.Max(totalHeight + rowHeight - Spacing, 0);

        return new Size(totalWidth, totalHeight);

    }

    private Size VariableMeasureAndLayout(double widthConstraint, double heightConstraint, bool doLayout = false, double x = 0, double y = 0)
    {
        double totalWidth = 0;
        double totalHeight = 0;
        double rowHeight = 0;
        double rowWidth = 0;
        double minWidth = 0;
        double minHeight = 0;
        double xPos = x + _layout.Padding.Left;
        double yPos = y + _layout.Padding.Top;
        double limitX = x + widthConstraint - _layout.Padding.Left + 0.0001d;

        widthConstraint -= _layout.Padding.HorizontalThickness;
        heightConstraint -= _layout.Padding.VerticalThickness;

        var Children = _layout.Children;

        var visibleChildren = Children.Where(c => c.Visibility == Visibility.Visible).Select(c => new {
            child = c,
            size = c.Measure(widthConstraint, heightConstraint)
        });

        var nextChildren = visibleChildren.Skip(1).ToList();
        nextChildren.Add(null); //make element count same

        var zipChildren = visibleChildren.Zip(nextChildren, (c, n) => new { current = c, next = n });

        foreach (var childBlock in zipChildren)
        {

            var child = childBlock.current.child;
            var size = childBlock.current.size;
            var itemWidth = size.Width;
            var itemHeight = size.Height;

            rowHeight = Math.Max(rowHeight, itemHeight + Spacing);
            rowWidth += itemWidth + Spacing;

            minHeight = Math.Max(minHeight, itemHeight);
            minWidth = Math.Max(minWidth, itemWidth);

            if (doLayout)
            {
                var region = new Rect(xPos, yPos, itemWidth, itemHeight);
                child.Arrange(region);
            }

            if (childBlock.next == null)
            {
                totalHeight += rowHeight;
                totalWidth = Math.Max(totalWidth, rowWidth);
                break;
            }

            xPos += itemWidth + Spacing;
            var nextWidth = childBlock.next.size.Width;

            if (xPos + nextWidth > limitX)
            {
                xPos = x + _layout.Padding.Left;
                yPos += rowHeight;
                totalHeight += rowHeight;
                totalWidth = Math.Max(totalWidth, rowWidth);
                rowHeight = 0;
                rowWidth = 0;
            }
        }

        totalWidth += _layout.Padding.HorizontalThickness;
        totalHeight += _layout.Padding.VerticalThickness;

        totalWidth = Math.Max(totalWidth  - Spacing, 0);
        totalHeight = Math.Max(totalHeight - Spacing, 0);

        return new Size(totalWidth, totalHeight);
    }
}

