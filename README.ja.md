# AiForms.Layouts for NET MAUI

.NET MAUI用のカスタムレイアウトコレクション

[English](README.md) | 日本語

## 参考ソースコード

* https://forums.xamarin.com/discussion/comment/57486/#Comment_57486
* https://forums.xamarin.com/discussion/21635/xforms-needs-an-itemscontrol/p2
* https://github.com/hartez/CustomLayoutExamples

## 機能

* [WrapLayout](#wraplayout)
* [FitLayout](#fitlayout)

<img src="images/1.png" width=200 /><img src="images/2.png" width=200 /><img src="images/3.png" width=200 />

## デモ

- WrapLayout
  - https://x.com/muak_x/status/830061279330996224
- FitLayout
  - https://x.com/muak_x/status/1949374845109813758

## NuGetインストール

[https://www.nuget.org/packages/AiForms.Maui.Layouts/](https://www.nuget.org/packages/AiForms.Maui.Layouts/)

```bash
Install-Package AiForms.Maui.Layouts
```

## WrapLayout

折り返しを行うレイアウトです。

### パラメータ

* Spacing
    * 要素間に追加されるスペース
* UniformColumns
    * 均等な子要素幅のための列数（デフォルト: 0）
    * 0の場合、子要素は自身の幅となります。
    * 0より大きい場合、子要素の幅は親要素の幅をこの数で割った幅になります。
* IsSquare
    * trueの場合、UniformColumns > 0のときにアイテムの高さを幅と同じにします（デフォルト: false）

### XAMLでの記述方法

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

コンテンツがオーバーフローした際に、親コンテナ内にフィットするよう自動的にスケールするレイアウトです。

### パラメータ

* Orientation
    * コンテンツをフィットさせる方向（VerticalまたはHorizontal）   
    * デフォルト: Vertical
    * Vertical
        * コンテンツの高さがコンテナの高さを超える場合、100%からScaleMarginを引いた値でスケールダウンします。
    * Horizontal
        * コンテンツの幅がコンテナの幅を超える場合、100%からScaleMarginを引いた値でスケールダウンします。
* EstimatedHeight
    * コンテンツの本来の想定される高さ（OrientationがVerticalの場合に使用）
    * 未設定（-1）の場合、コンテンツの高さは制約なしで測定されます。
    * 例えば600を指定すると高さ600であることを想定して測定された上で縮小されます。
    * Orientation: Verticalの時のみ有効。
    * デフォルト: -1
* EstimatedWidth
    * コンテンツの本来の想定される幅（OrientationがHorizontalの場合に使用）
    * 未設定（-1）の場合、コンテンツの幅は制約なしで測定されます。
    * 例えば300を指定すると幅300であることを想定して測定された上で縮小されます。
        * 文字列の折り返し等も影響するのでその辺りを考慮したい場合に有効です。
    * Orientation: Horizontalの時のみ有効。
    * デフォルト: -1
* ScaleMargin
    * コンテンツがオーバーフローした際の余白を提供するためのスケールマージン
    * 値はマージン比率を表します（例：0.05 = 5%マージン）
    * 例えば0.05の場合、親のコンテンツに対して95%に縮小されます。
    * デフォルト: 0（0%）

### XAMLでの記述方法

```xml
<ContentPage
		xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
		xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
		xmlns:f="clr-namespace:AiForms.Maui.Layouts;assembly=AiForms.Maui.Layouts"
		x:Class="Sample.Views.MainPage">

    <!-- 基本的な縦フィットの例 -->
    <f:FitLayout Orientation="Vertical" EstimatedHeight="800" ScaleMargin="0.05">
        <VerticalStackLayout>
            <Label Text="このコンテンツは縦にフィットするようスケールされます" />
            <BoxView Color="Red" HeightRequest="500" />
            <BoxView Color="Blue" HeightRequest="300" />
        </VerticalStackLayout>
    </f:FitLayout>    

    <!-- 基本的な横フィットの例 -->
    <f:FitLayout Orientation="Horizontal" EstimatedWidth="800" ScaleMargin="0.05">
        <HorizontalStackLayout>
            <Label Text="このコンテンツは縦にフィットするようスケールされます" />
            <BoxView Color="Red" WidthRequest="500" />
            <BoxView Color="Blue" WidthRequest="300" />
        </HorizontalStackLayout>
    </f:FitLayout> 
</ContentPage>
```

## ライセンス

MIT Licensed.