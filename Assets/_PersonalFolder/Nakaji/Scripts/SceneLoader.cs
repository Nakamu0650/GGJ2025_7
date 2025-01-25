using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName;
   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void Update()
    {
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            LoadScene(nextSceneName);
        }
    }
}
