using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] List<AudioSource> sources;
    [SerializeField] string tilteSceneName;
    [SerializeField] string nextSceneName;
    [SerializeField] AudioSource gameclearSE;

    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject gameclearUI;
 
    private float interval;
    private float clearInterval;
    private bool isRead;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        interval = 2.0f;
        clearInterval = 3.0f;
        isRead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead)
        {
            gameoverUI.SetActive(true);
            foreach (var source in sources)
            {
                source.Stop();
            }
            if (interval >= 0)interval -= Time.deltaTime;
        }

        if (gameclearUI.activeSelf)
        {
            if (isRead)
            {
                gameclearSE.Play();
                isRead = false;
            }
            foreach (var source in sources)
            {
                source.Stop();
            }
            if (clearInterval >= 0)
            {
                clearInterval -= Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0.0f;
                if (Input.anyKey)
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            }
        }

        if (gameoverUI.activeSelf && interval < 0.0f && Input.anyKey)
        {
            SceneManager.LoadScene(tilteSceneName);
        }
    }
}
