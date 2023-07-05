using System.Collections.ObjectModel;

namespace Sample;

public partial class MainPage : ContentPage
{
    public ObservableCollection<string> ItemsSource { get; set; }
    public MainPage()
    {
        InitializeComponent();

        ItemsSource = new ObservableCollection<string>(new List<string>
        {
            "Abccc",
            "Def",
            "GHIii",
            "JKL",
            "MNOoooooooooooo",
            "PQr",
            "Stuuuu",
            "vxy",
            "zzz"
        });

        OnPropertyChanged(nameof(ItemsSource));
    }
}


