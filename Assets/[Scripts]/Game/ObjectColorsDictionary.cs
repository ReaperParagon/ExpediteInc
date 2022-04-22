using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectColor
{
    Orange,
    Purple,
    Green,
    NumColors
}

public class ObjectColorsDictionary
{
    private static Dictionary<ObjectColor, Color> colorDictionary = new Dictionary<ObjectColor, Color>();

    public static void CreateDictionary()
    {
        colorDictionary.Clear();

        colorDictionary.Add(ObjectColor.Green, new Color(0.0f, 1.0f, 0.5f, 1.0f));
        colorDictionary.Add(ObjectColor.Purple, new Color(0.5f, 0.0f, 1.0f, 1.0f));
        colorDictionary.Add(ObjectColor.Orange, new Color(1.0f, 0.5f, 0.0f, 1.0f));
    }

    public static Color GetColor(ObjectColor oc)
    {
        if (colorDictionary.TryGetValue(oc, out Color c))
            return c;

        // We don't have that as a color
        return Color.white;
    }
}
