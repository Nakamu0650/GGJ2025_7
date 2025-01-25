using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearCollision : MonoBehaviour
{
    public GameObject gameClear;
    bool stageClear;
    public string nextStageName;

    private void Start()
    {
    }

    private void Update()
    {
        if (stageClear)
        { 
          if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(nextStageName);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameClear.SetActive(true);
            stageClear = true;
        }
    }
   
}
