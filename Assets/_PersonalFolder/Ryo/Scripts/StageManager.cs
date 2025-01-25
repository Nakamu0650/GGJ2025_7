using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] List<AudioSource> sources;

    [SerializeField] GameObject gameoverUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
