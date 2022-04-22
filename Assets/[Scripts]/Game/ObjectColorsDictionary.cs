using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectColor
{
    Red,
    Blue,
    Green,
    Cyan,
    Magenta,
    Yellow,
    NumColors
}

public class ObjectColorsDictionary
{
    private static Dictionary<ObjectColor, Color> colorDictionary = new Dictionary<ObjectColor, Color>();

    public static void CreateDictionary()
    {
        colorDictionary.Clear();

        colorDictionary.Add(ObjectColor.Red, Color.red);
        colorDictionary.Add(ObjectColor.Blue, Color.blue);
        colorDictionary.Add(ObjectColor.Green, Color.green);
        colorDictionary.Add(ObjectColor.Cyan, Color.cyan);
        colorDictionary.Add(ObjectColor.Magenta, Color.magenta);
        colorDictionary.Add(ObjectColor.Yellow, Color.yellow);
    }

    public static Color GetColor(ObjectColor oc)
    {
        if (colorDictionary.TryGetValue(oc, out Color c))
            return c;

        // We don't have that as a color
        return Color.white;
    }
}
