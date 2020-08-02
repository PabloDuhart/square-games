using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMain : MonoBehaviour
{

    public static AudioManagerMain instance;

    public GameObject[] sounds;

    private bool playing = false;

    private int nextSong = -1;

    private bool enter = false;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        //DontDestroyOnLoad(gameObject);

    }


    public bool check()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].gameObject.GetComponent<AudioSource>().isPlaying)
            {
                playing = true;
                break;
            }
            else
            {
                playing = false;
            }
        }
        return playing;
    }

    public void decheck()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].SetActive(false);
        }
        playing = false;
    }

    void Update()
    {

        if (!check() && !enter)
        {
            enter = true;
            StartCoroutine(delay());
        }

    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        int previousSong = nextSong;
        nextSong = new System.Random().Next(0, (sounds.Length - 2));
        while (previousSong == nextSong)
        {
            nextSong = new System.Random().Next(0, (sounds.Length - 2));
        }
        decheck();
        sounds[nextSong].SetActive(true);
        enter = false;
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }


}
