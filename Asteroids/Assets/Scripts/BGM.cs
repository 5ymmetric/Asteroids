using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    Timer looper;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Play(AudioClipName.BGM);
        looper = gameObject.AddComponent<Timer>();
        looper.Duration = 43.5f;
        looper.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (looper.Finished && !paused)
        {
            AudioManager.Play(AudioClipName.BGM);
            looper.Run();
        }

        if (Input.GetKeyDown("m"))
        {
            AudioManager.Stop();
            paused = true;
        }

        if (Input.GetKeyDown("p"))
        {
            AudioManager.Play(AudioClipName.BGM);
            paused = false;
        }
    }
}
