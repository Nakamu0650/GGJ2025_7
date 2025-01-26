using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffect_URP : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private Volume volume;
    private Vignette vignette;

    private void Start()
    {
        // Volumeコンポーネントを取得
        volume = GetComponent<Volume>();
        if (volume == null)
        {
            Debug.LogError("Volume component is missing on this GameObject.");
            return;
        }

        // Vignette設定を取得
        if (!volume.profile.TryGet(out vignette))
        {
            Debug.LogError("Vignette override is missing in the Volume Profile.");
            return;
        }

        // 初期値を設定
        vignette.intensity.value = 0.0f;
    }

    private void Update()
    {
        if (vignette == null) return;

        // SAN値に応じてビネットの強度を調整
        if (playerData.SAN <= playerData.MaxSAN * 0.05f)
        {
            vignette.intensity.value = 1.0f; // 最大強度
        }
        else
        {
            vignette.intensity.value = 1.0f - playerData.SAN / playerData.MaxSAN;
        }
    }
}
