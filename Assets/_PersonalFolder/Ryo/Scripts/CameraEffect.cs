using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEffect : MonoBehaviour
{
    public Material effectMaterial;

    [Range(0, 1)] public float blurStrength = 0.5f;
    [Range(0, 1)] public float darkenStrength = 0.5f;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (effectMaterial != null)
        {
            effectMaterial.SetFloat("_BlurStrength", blurStrength);
            effectMaterial.SetFloat("_DarkenStrength", darkenStrength);
            Graphics.Blit(src, dest, effectMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
