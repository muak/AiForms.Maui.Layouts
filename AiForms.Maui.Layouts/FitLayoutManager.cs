using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// Layout manager for FitLayout.
/// </summary>
/// <param name="_layout"></param>
public class FitLayoutManager(FitLayout _layout) : ILayoutManager
{
    /// <summary>
    /// Arranges FitLayout's child elements to fit the parent element's size.
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public Size ArrangeChildren(Rect bounds)
    {
        var content = _layout.FirstOrDefault();
        if(content is null)
        {
            return Size.Zero;
        }
        if(content is not View contentView)
        {
            throw new InvalidCastException("FitLayout can only contain View elements.");
        }
        
        // Reset scale
        contentView.Scale = 1.0d;
        return _layout.Orientation switch
        {
            ItemsLayoutOrientation.Vertical => ArrangeVertical(bounds, contentView),
            ItemsLayoutOrientation.Horizontal => ArrangeHorizontal(bounds, contentView),
            _ => throw new NotSupportedException("FitLayout only supports Vertical or Horizontal orientation.")
        };
    }

    Size ArrangeVertical(Rect bounds, View contentView)
    {
        var estimatedHeight = _layout.EstimatedHeight > 0d ? 
            _layout.EstimatedHeight : double.PositiveInfinity;
        
        // Measure the original size of the content
        var size = contentView.Measure(bounds.Width, estimatedHeight);

        // Get required height
        var requestHeight = _layout.EstimatedHeight > 0d ? 
                _layout.EstimatedHeight : size.Height;
        // If the original height exceeds the screen content area height
        if (requestHeight > bounds.Height)
        {
            // Calculate scale to fit the screen (reduce by ScaleMargin for extra space)
            var scale = bounds.Height / requestHeight - _layout.ScaleMargin;
            // Scale down the entire content
            contentView.Scale = scale;
            // Adjust y coordinate (so content comes to the top)
            var scrollY = (requestHeight - bounds.Height) / 2d * -1;

            var rect = new Rect(0, scrollY, bounds.Width, requestHeight);
            contentView.Arrange(rect);

            return rect.Size;
        }
        // If it fits, draw as is
        else
        {
            var rect = new Rect(0, 0, bounds.Width, bounds.Height);
            contentView.Arrange(rect);

            return rect.Size;
        }
    }
    
    Size ArrangeHorizontal(Rect bounds, View contentView)
    {
        var estimatedWidth = _layout.EstimatedWidth > 0d ? 
            _layout.EstimatedWidth : double.PositiveInfinity;
        
        // Measure the original size of the content
        var size = contentView.Measure(estimatedWidth, bounds.Height);

        // Get required width
        var requestWidth = _layout.EstimatedWidth > 0d ? 
                _layout.EstimatedWidth : size.Width;
        // If the original width exceeds the screen content area width
        if (requestWidth > bounds.Width)
        {
            // Calculate scale to fit the screen (reduce by ScaleMargin for extra space)
            var scale = bounds.Width / requestWidth - _layout.ScaleMargin;
            // Scale down the entire content
            contentView.Scale = scale;
            // Adjust x coordinate (so content comes to the left)
            var scrollX = (requestWidth - bounds.Width) / 2d * -1;

            var rect = new Rect(scrollX, 0, requestWidth, bounds.Height);
            contentView.Arrange(rect);

            return rect.Size;
        }
        // If it fits, draw as is
        else
        {
            var rect = new Rect(0, 0, bounds.Width, bounds.Height);
            contentView.Arrange(rect);

            return rect.Size;
        }
    }

    /// <summary>
    /// The Measure method doesn't have particular meaning in FitLayout,
    /// so it returns the parent element's size as is.
    /// </summary>
    /// <param name="widthConstraint"></param>
    /// <param name="heightConstraint"></param>
    /// <returns></returns>
    public Size Measure(double widthConstraint, double heightConstraint)
    {
        return new Size(widthConstraint, heightConstraint);
    }
}