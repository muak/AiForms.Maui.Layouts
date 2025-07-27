using Microsoft.Maui.Layouts;

namespace AiForms.Maui.Layouts;

/// <summary>
/// FitLayoutのレイアウトマネージャーです。
/// </summary>
/// <param name="_layout"></param>
public class FitLayoutManager(FitLayout _layout) : ILayoutManager
{
    /// <summary>
    /// FitLayoutの子要素を親要素のサイズに合わせて配置します。
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
        
        // スケールをリセット
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
        
        // コンテンツの本来のサイズを計測
        var size = contentView.Measure(bounds.Width, estimatedHeight);

        // 必要な高さを取得
        var requestHeight = _layout.EstimatedHeight > 0d ? 
                _layout.EstimatedHeight : size.Height;
        // 本来の高さが画面コンテンツ領域の高さをオーバーしていれば
        if (requestHeight > bounds.Height)
        {
            // 画面に収まるスケールを計算（余裕を持たせるため5%低くする）
            var scale = bounds.Height / requestHeight - 0.05d;
            // 全体を縮小する
            contentView.Scale = scale;
            // y座標調整(コンテンツが上にくるように)
            var scrollY = (requestHeight - bounds.Height) / 2d * -1;

            var rect = new Rect(0, scrollY, bounds.Width, requestHeight);
            contentView.Arrange(rect);

            return rect.Size;
        }
        // 収まる場合はそのまま描画
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
        
        // コンテンツの本来のサイズを計測
        var size = contentView.Measure(estimatedWidth, bounds.Height);

        // 必要な幅を取得
        var requestWidth = _layout.EstimatedWidth > 0d ? 
                _layout.EstimatedWidth : size.Width;
        // 本来の幅が画面コンテンツ領域の幅をオーバーしていれば
        if (requestWidth > bounds.Width)
        {
            // 画面に収まるスケールを計算（余裕を持たせるため5%低くする）
            var scale = bounds.Width / requestWidth - 0.05d;
            // 全体を縮小する
            contentView.Scale = scale;
            // x座標調整(コンテンツが左にくるように)
            var scrollX = (requestWidth - bounds.Width) / 2d * -1;

            var rect = new Rect(scrollX, 0, requestWidth, bounds.Height);
            contentView.Arrange(rect);

            return rect.Size;
        }
        // 収まる場合はそのまま描画
        else
        {
            var rect = new Rect(0, 0, bounds.Width, bounds.Height);
            contentView.Arrange(rect);

            return rect.Size;
        }
    }

    /// <summary>
    /// Measureメソッドは、FitLayoutでは特に意味を持たないため、
    /// 親要素のサイズをそのまま返します。
    /// </summary>
    /// <param name="widthConstraint"></param>
    /// <param name="heightConstraint"></param>
    /// <returns></returns>
    public Size Measure(double widthConstraint, double heightConstraint)
    {
        return new Size(widthConstraint, heightConstraint);
    }
}