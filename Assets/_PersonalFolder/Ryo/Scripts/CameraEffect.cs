using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class CameraEffect : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private PostProcessVolume processVolume;
    private Vignette vignette;

    private void Start()
    {
        processVolume = GetComponent<PostProcessVolume>();
        processVolume.profile.TryGetSettings(out vignette);
        vignette.intensity.value = 0.0f;
    }

    private void Update()
    {
        if (playerData.SAN <= playerData.MaxSAN * 0.05f)
        {
            vignette.intensity.value = 1.0f;
        }
        else
        {
            vignette.intensity.value = 1.0f - playerData.SAN / playerData.MaxSAN;
        }
    }
}
