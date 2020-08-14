using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;


public class audioManager : MonoBehaviour
{


	private AudioSource myAudio;
	public AudioClip [] myMusic; 

	void Start()
	{
		myAudio = gameObject.GetComponent<AudioSource>();
		playRandomMusic();
	}

	void Update()
	{
		if (!myAudio.isPlaying)
			playRandomMusic();
	}

	void playRandomMusic()
	{
		StartCoroutine(delay());
	}

	IEnumerator delay()
	{
		yield return new WaitForSeconds(2);
		myAudio.clip = myMusic[new System.Random().Next(0, myMusic.Length)] as AudioClip;
		myAudio.Play();
	}

	public void Destroy()
	{
		Destroy();
	}

}
