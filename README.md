# AiForms.Layouts for NET MAUI

This is a collection of NET MAUI custom layouts

## Referenced source code

* https://forums.xamarin.com/discussion/comment/57486/#Comment_57486
* https://forums.xamarin.com/discussion/21635/xforms-needs-an-itemscontrol/p2
* https://github.com/hartez/CustomLayoutExamples

## Features

* [WrapLayout](#wraplayout)

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

_By Flex Layout having come, there is seldom opportunity using this layout. But it can be used when you want to arrange uniformly each items depending on screen width or make it square._

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


## License

MIT Licensed.
