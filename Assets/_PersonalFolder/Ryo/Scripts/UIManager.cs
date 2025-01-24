using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerData status;
    [SerializeField] Image staminaL;
    [SerializeField] Image staminaR;
    [SerializeField] Image san;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        staminaL.fillAmount = status.Stamina / status.MaxStamina;
        staminaR.fillAmount = staminaL.fillAmount;
        san.fillAmount = status.SUN / status.MaxSUN;
    }
}
