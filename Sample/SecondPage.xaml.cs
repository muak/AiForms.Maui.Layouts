using Reactive.Bindings;

namespace Sample;

public partial class SecondPage : ContentPage
{
    public ReactivePropertySlim<double> LayoutScale { get; } = new(1.0,mode: ReactivePropertyMode.DistinctUntilChanged);
    public ReactivePropertySlim<double> MinSize { get; } = new(0.25);
    public ReactivePropertySlim<double> SmallSize { get; } = new(0.5);
    public ReactivePropertySlim<double> MediumSize { get; } = new(0.75);
    public ReactivePropertySlim<double> LargeSize { get; } = new(1.0);

    public ReactivePropertySlim<double> LayoutWidth { get; } = new(-1.0);
    public ReactivePropertySlim<double> LayoutHeight { get; } = new(-1.0);

    public ReactivePropertySlim<ItemsLayoutOrientation> Orientation { get; } = new(ItemsLayoutOrientation.Vertical,mode: ReactivePropertyMode.DistinctUntilChanged);
    public ReactivePropertySlim<ItemsLayoutOrientation> Vertical { get; } = new(ItemsLayoutOrientation.Vertical);
    public ReactivePropertySlim<ItemsLayoutOrientation> Horizontal { get; } = new(ItemsLayoutOrientation.Horizontal);
    
    public ReactivePropertySlim<bool> UseEstimatedWidth { get; } = new(false,mode: ReactivePropertyMode.DistinctUntilChanged);
    public ReactivePropertySlim<bool> UseEstimatedHeight { get; } = new(false,mode: ReactivePropertyMode.DistinctUntilChanged);
    
    public ReactivePropertySlim<double> EstimatedWidth { get; } = new(-1d);
    public ReactivePropertySlim<double> EstimatedHeight { get; } = new(-1d);
    
    public SecondPage()
    {
        BindingContext = this;
        InitializeComponent();

        Orientation.Subscribe(x =>
        {
            ChangeScaleOrOrientation(x, LayoutScale.Value);
        });
        
        LayoutScale.Subscribe(x =>
        {
            ChangeScaleOrOrientation(Orientation.Value, x);
        });
        
        UseEstimatedWidth.Subscribe(x =>
        {
            if (x)
            {
                EstimatedWidth.Value = 320d;
            }
            else
            {
                EstimatedWidth.Value = -1.0d;
            }
        });
        
        UseEstimatedHeight.Subscribe(x =>
        {
            if (x)
            {
                EstimatedHeight.Value = 480d;
            }
            else
            {
                EstimatedHeight.Value = -1.0d;
            }
        });
        
        OnPropertyChanged(nameof(Orientation));
        OnPropertyChanged(nameof(LayoutScale));
    }
    
    void ChangeScaleOrOrientation(ItemsLayoutOrientation orientation, double scale)
    {
        Orientation.Value = orientation;
        LayoutScale.Value = scale;

        if (orientation == ItemsLayoutOrientation.Vertical)
        {
            LayoutWidth.Value = -1.0d;
            if (Math.Abs(scale - 1.0d) < double.Epsilon)
            {
                LayoutHeight.Value = -1.0d;
                return;
            }

            LayoutHeight.Value = container.Height * scale;
        }
        else if (orientation == ItemsLayoutOrientation.Horizontal)
        {
            LayoutHeight.Value = -1.0d;
            if (Math.Abs(scale - 1.0d) < double.Epsilon)
            {
                LayoutWidth.Value = -1.0d;
                return;
            }

            LayoutWidth.Value = container.Width * scale;
        }
    }
} 