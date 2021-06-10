using UnityEngine;

public static class RarityColor
{
    public static Color color(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Color.white;
            case Rarity.Uncommon:
                return Color.green;
            case Rarity.Rare:
                return new Color(0, 125 / 255f, 255 / 255f, 1);
        }
        return Color.white;
    }
}
