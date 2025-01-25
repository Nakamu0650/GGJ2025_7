using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string nextSceneName;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource BGM;

    private bool isClicked;
    private bool finishSound;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        finishSound = true;
    }

    private void Update()
    {
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            isClicked = true;
            click.Play();
            BGM.Stop();
            finishSound = false;
        }

        if (!click.isPlaying && !finishSound)
        {
            finishSound = true;
        }

        if (isClicked && finishSound)
        {
            LoadScene(nextSceneName);
        }
    }
}
