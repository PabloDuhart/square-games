using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;


public class audioManager : MonoBehaviour
{

	public static audioManager instance;

	public GameObject [] sounds;

    public GameObject canvasWin;

    public GameObject canvasLose;

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
		for (int i = 0; i < sounds.Length-2; i++)
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
		for (int i = 0; i < sounds.Length-2; i++)
		{
			sounds[i].SetActive(false);
		}
		playing = false;
	}

	void Update()
	{
        
		if (!check() && !enter && !canvasWin.activeSelf && !canvasLose.activeSelf)
		{
			enter = true;
			StartCoroutine(delay());	
		}
        
        if (canvasWin.activeSelf)
        {
            decheck();
            sounds[sounds.Length-2].SetActive(true);
        }
        else
        {
            sounds[sounds.Length - 2].SetActive(false);
            if (canvasLose.activeSelf)
            {
                decheck();
                sounds[sounds.Length - 1].SetActive(true);
            }
            else
            {
                sounds[sounds.Length - 1].SetActive(false);
            }
        }
        
	}
	IEnumerator delay()
	{
		yield return new WaitForSeconds(2);
		int previousSong = nextSong;
		nextSong = new System.Random().Next(0, (sounds.Length-2));
		while(previousSong==nextSong)
		{
			nextSong = new System.Random().Next(0, (sounds.Length-2));
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
