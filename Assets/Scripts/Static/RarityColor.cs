using UnityEngine;

public static class RarityColor
{
    public static Color color(Rarity rarity)
    {
        Color color = Color.white;
        switch (rarity)
        {
            case Rarity.Common:
                return color;
            case Rarity.Uncommon:
                ColorUtility.TryParseHtmlString("#9ADEA8", out color);
                return color;
            case Rarity.Rare:
                ColorUtility.TryParseHtmlString("#85A2D7", out color);
                return color;
        }
        return Color.white;
    }

    public static Color GetColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public static Color MapZoneColor(int rarity)
    {
        Color color = Color.white;
        switch (rarity)
        {
            case 0:
                ColorUtility.TryParseHtmlString("#9ADEA8", out color);
                return color;
            case 1:
                ColorUtility.TryParseHtmlString("#85A2D7", out color);
                return color;
        }
        return color;
    }
}
