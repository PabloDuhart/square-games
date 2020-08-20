using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;


public class audioManager : MonoBehaviour
{


	private AudioSource myAudio;
	public AudioClip [] myMusic;
	private bool first;

	void Start()
	{
		myAudio = gameObject.GetComponent<AudioSource>();
        myAudio.volume = 0.25f;
		first = false;
		playRandomMusic();
		myAudio.outputAudioMixerGroup.audioMixer.SetFloat("volume",PlayerPrefs.GetFloat("volume", 0));
	}

	void Update()
	{
		if (!myAudio.isPlaying && first)
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
		first = true;
	}

}
