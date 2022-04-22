using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public ObjectColor colorType;
    public Renderer[] materialRenderers;

    private void Awake()
    {
        // Get all renderers...
        materialRenderers = GetComponentsInChildren<Renderer>();
    }

    public void SetRandomObjectColor()
    {
        ObjectColor c = (ObjectColor)Random.Range(0, (int)ObjectColor.NumColors);
        SetObjectColor(c);
    }

    public void SetObjectColor(ObjectColor color)
    {
        colorType = color;
        UpdateMaterialColor();
    }

    private void UpdateMaterialColor()
    {
        for (int r = 0; r < materialRenderers.Length; r++)
        {
            Renderer MR = materialRenderers[r];

            for (int i = 0; i < MR.materials.Length; i++)
            {
                MR.materials[i].color = MR.materials[i].color.grayscale * GetObjectColor();
            }
        }
    }

    private Color GetObjectColor()
    {
        return ObjectColorsDictionary.GetColor(colorType);
    }

}
