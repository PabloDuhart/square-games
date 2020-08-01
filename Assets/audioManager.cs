using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;


public class audioManager : MonoBehaviour
{

	public static audioManager instance;

	public GameObject [] sounds;

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


		DontDestroyOnLoad(gameObject);

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
		nextSong = new System.Random().Next(0, (sounds.Length));
		while(previousSong==nextSong)
		{
			nextSong = new System.Random().Next(0, (sounds.Length));
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
