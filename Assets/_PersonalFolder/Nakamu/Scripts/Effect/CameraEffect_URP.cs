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
        // Volume�R���|�[�l���g���擾
        volume = GetComponent<Volume>();
        if (volume == null)
        {
            Debug.LogError("Volume component is missing on this GameObject.");
            return;
        }

        // Vignette�ݒ���擾
        if (!volume.profile.TryGet(out vignette))
        {
            Debug.LogError("Vignette override is missing in the Volume Profile.");
            return;
        }

        // �����l��ݒ�
        vignette.intensity.value = 0.0f;
    }

    private void Update()
    {
        if (vignette == null) return;

        // SAN�l�ɉ����ăr�l�b�g�̋��x�𒲐�
        if (playerData.SAN <= playerData.MaxSAN * 0.05f)
        {
            vignette.intensity.value = 1.0f; // �ő勭�x
        }
        else
        {
            vignette.intensity.value = 1.0f - playerData.SAN / playerData.MaxSAN;
        }
    }
}
