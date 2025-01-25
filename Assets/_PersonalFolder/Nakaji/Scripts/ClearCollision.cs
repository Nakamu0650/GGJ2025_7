using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearCollision : MonoBehaviour
{
    public Image gameClearUI;
    bool stageClear;
    public string nextStageName;
    SceneLoader loader;

    private void Start()
    {
        loader = GetComponent<SceneLoader>();
    }

    private void Update()
    {
        if (stageClear)
        { // ç∂É{É^ÉìÇ™âüÇ≥ÇÍÇΩèuä‘Ç…é¿çs
            if (Input.GetMouseButtonDown(0))
            {
               loader.LoadScene(nextStageName);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameClearUI.enabled = true;
            stageClear = true;
        }
    }
   
}
