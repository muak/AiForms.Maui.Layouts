using System;
using System.Globalization;
using ColorMine.ColorSpaces;
using Microsoft.Maui.Controls;
using SysColor = System.Drawing.Color;

namespace Sample;

public class BackForeColorConverter:IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var backColor = value switch
        {
            IColorSpace colorSpace => colorSpace.ToMaui(),
			SysColor sysColor      => Color.FromInt(sysColor.ToArgb()),
            Color color            => color,
			int intColor           => Color.FromInt(intColor),
            string strColor        => Color.FromArgb(strColor),
            _                      => KnownColor.Default,
        };

        if (backColor.IsDefault()) {
			return backColor;
		}

		// 知覚的表色系を利用しないと調整が難しい
		var lch = backColor.To<Lch>();
		var foreColor = backColor.To<Lch>();

        //色がどぎつい時
        if (lch.C >= 70 && lch.L >= 35 && lch.L <= 60)
        {
            // 赤みが強ければ白っぽくする
            if (lch.H >= 0 && lch.H <= 30 || lch.H >= 330 && lch.H <= 360)
            {
                foreColor.L = 90;
                foreColor.C = 20;
            }
            // それ以外は黒っぽくする
            else
            {
                foreColor.L = 10;
                foreColor.C = 20;
            }
        }
		// それ以外は明度を調整してコントラストを強める
		else if(lch.L <= 50)
		{
            foreColor.L = 80;
        }
		else
		{
			foreColor.L = 20;
		}
		

		return foreColor.ToMaui();
	}

	public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		throw new NotImplementedException();
	}
}