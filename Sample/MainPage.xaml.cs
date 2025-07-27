using System.Collections.ObjectModel;
using Reactive.Bindings;

namespace Sample;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Hoge> ItemsSource { get; set; }
    public ReactivePropertySlim<bool> IsUniform { get; } = new();
    public ReactivePropertySlim<bool> IsSquare { get; } = new();
    public ReactivePropertySlim<int> UniformColumns { get; } = new();
    
    public ReactiveCommandSlim AddCommand { get; } = new();
    public ReactiveCommandSlim DeleteCommand { get; } = new();
    public ReactiveCommandSlim ReplaceCommand { get; } = new();
    public ReactiveCommandSlim ClearCommand { get; } = new();
    
    
    public MainPage()
    {
        BindingContext = this;
        InitializeComponent();

        ItemsSource = new ObservableCollection<Hoge>(Shuffle());
        
        IsUniform.Subscribe(x => { UniformColumns.Value = x ? 3 : 0; });

        AddCommand.Subscribe(x =>
        {
            ItemsSource.Add(GetNextItem());
        });
        
        DeleteCommand.Subscribe(x =>
        {
            if (ItemsSource.Count > 0) {
                ItemsSource.RemoveAt(ItemsSource.Count - 1);
            }
        });
        
        ReplaceCommand.Subscribe(x =>
        {
            ItemsSource[0] = GetNextItem();
        });
        
        ClearCommand.Subscribe(x =>
        {
            ItemsSource.Clear();
        });

        OnPropertyChanged(nameof(ItemsSource));
    }
    
    List<Hoge> Shuffle()
    {
        var list = new List<Hoge>();

        var rand = new Random();
        for (var i = 0; i < 8; i++) {

            var r = rand.Next(10, 245);
            var g = rand.Next(10, 245);
            var b = rand.Next(10, 245);
            var color = Color.FromRgb(r, g, b);
            var w = rand.Next(30, 100);
            var h = rand.Next(30, 60);

            list.Add(new Hoge {
                Name = $"#{r:X2}{g:X2}{b:X2}",
                Color = color,
                Width = w,
                Height = h,
            });
        }

        return list;
    }

    Hoge GetNextItem()
    {
        var rand = new Random();
        var r = rand.Next(10, 245);
        var g = rand.Next(10, 245);
        var b = rand.Next(10, 245);
        var color = Color.FromRgb(r, g, b);
        var w = rand.Next(30, 100);
        var h = rand.Next(30, 60);

        return new Hoge {
            Name = $"#{r:X2}{g:X2}{b:X2}",
            Color = color,
            Width = w,
            Height = h,
        };
    }
}

public class Hoge : BindableBase
{
    private string _Name;
    public string Name {
        get { return _Name; }
        set { SetProperty(ref _Name, value); }
    }

    private Color _Color;
    public Color Color {
        get { return _Color; }
        set { SetProperty(ref _Color, value); }
    }

    private double _Width;
    public double Width {
        get { return _Width; }
        set { SetProperty(ref _Width, value); }
    }

    private double _Height;
    public double Height {
        get { return _Height; }
        set { SetProperty(ref _Height, value); }
    }
}


