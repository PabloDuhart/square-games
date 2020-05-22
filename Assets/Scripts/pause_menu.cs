﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class pause_menu : MonoBehaviour
{

	public AudioMixer audioMixer;


	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
	}

	public void setVolume(float volume)
	{
		audioMixer.SetFloat("volume", volume);
	}

	public void BackToMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MenuPrincipal");
	}


	public void Pause()
	{
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
	}

}