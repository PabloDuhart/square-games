﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class main_menu : MonoBehaviour
{

	public AudioMixer audioMixer;

	public Slider slider;

	public void Awake()
	{
		audioMixer.GetFloat("volume",out float value);
		slider.value = value;
	}

	public void setAudio(float volume)
	{
		audioMixer.SetFloat("volume", volume);
		audioMixer.GetFloat("volume", out float value);
	}


	public void LoadGame1()
	{
		SceneManager.LoadScene("Map");
	}

	public void Game2Lvl1()
	{
		SceneManager.LoadScene("Nivel1G2");
	}

	public void Game2Lvl2()
	{
		SceneManager.LoadScene("Nivel2G2");
	}

	public void Game2Lvl3()
	{
		SceneManager.LoadScene("Nivel3G2");
	}

	public void Game2Lvl4()
	{
		SceneManager.LoadScene("Nivel4G2");
	}

	public void Game2Lvl5()
	{
		SceneManager.LoadScene("Nivel5G2");
	}

	public void RestartStats()
	{
		PlayerPrefs.SetInt("levelReached", 1);
		PlayerPrefs.Save();
	}

	public void Quit()
	{
		Application.Quit();
	}
}
