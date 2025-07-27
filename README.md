# AiForms.Layouts for NET MAUI

This is a collection of NET MAUI custom layouts

## Referenced source code

* https://forums.xamarin.com/discussion/comment/57486/#Comment_57486
* https://forums.xamarin.com/discussion/21635/xforms-needs-an-itemscontrol/p2
* https://github.com/hartez/CustomLayoutExamples

## Features

* [WrapLayout](#wraplayout)
* [FitLayout](#fitlayout)

<img src="images/1.png" width=200 /><img src="images/2.png" width=200 /><img src="images/3.png" width=200 />

## Demo

https://twitter.com/muak_x/status/830061279330996224

## Nuget Installation

[https://www.nuget.org/packages/AiForms.Maui.Layouts/](https://www.nuget.org/packages/AiForms.Maui.Layouts/)

```bash
Install-Package AiForms.Maui.Layouts
```


## WrapLayout

This Layout performs wrapping on the boundaries.

### Parameters

* Spacing
    * added between elements
* UniformColumns
    * number for uniform child width (default 0)
    * If it is 0,it will obey WidthRequest value.
    * If it is more than 0 ,a child width will be  width which divide parent width by this number.
* IsSquare
    * If it is true,it make item height equal to item width when UniformColums > 0 (default false)

### How to write with Xaml

```xml
<ContentPage
		xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
		xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
		xmlns:l="clr-namespace:AiForms.Maui.Layouts;assembly=AiForms.Maui.Layouts"
		x:Class="Sample.Views.MainPage">

    <l:WrapLayout Spacing="4" UniformColumns="3" IsSquare="true">
    	<BoxView Color="Red" />
        <BoxView Color="Blue" />
        <BoxView Color="Green" />
        <BoxView Color="Black" />
        <BoxView Color="Yellow" />
    </l:WrapLayout>

</ContentPage>
```

## FitLayout

This Layout automatically scales content to fit within the parent container when it overflows.

### Parameters

* Orientation
    * Direction to fit the content (Vertical or Horizontal)
    * Default: Vertical
* EstimatedHeight
    * Expected height of the content (used when Orientation is Vertical)
    * If not set (-1), content height is measured without constraints
    * Default: -1
* EstimatedWidth
    * Expected width of the content (used when Orientation is Horizontal)
    * If not set (-1), content width is measured without constraints
    * Default: -1

### How it works

* **Vertical Mode**: When content height exceeds container height, it scales down to 95% of the fitting ratio
* **Horizontal Mode**: When content width exceeds container width, it scales down to 95% of the fitting ratio
* Content is positioned to align with the top-left corner after scaling

### How to write with Xaml

```xml
<ContentPage
		xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
		xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
		xmlns:l="clr-namespace:AiForms.Maui.Layouts;assembly=AiForms.Maui.Layouts"
		x:Class="Sample.Views.MainPage">

    <!-- Vertical fitting example -->
    <l:FitLayout Orientation="Vertical" EstimatedHeight="800">
        <StackLayout>
            <Label Text="This content will be scaled to fit vertically" />
            <BoxView Color="Red" HeightRequest="500" />
            <BoxView Color="Blue" HeightRequest="300" />
        </StackLayout>
    </l:FitLayout>

    <!-- Horizontal fitting example -->
    <l:FitLayout Orientation="Horizontal" EstimatedWidth="1200">
        <StackLayout Orientation="Horizontal">
            <BoxView Color="Green" WidthRequest="600" />
            <BoxView Color="Yellow" WidthRequest="600" />
        </StackLayout>
    </l:FitLayout>

</ContentPage>
```

## License

MIT Licensed.
