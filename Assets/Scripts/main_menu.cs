using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class main_menu : MonoBehaviour
{

	public AudioMixer audioMixer;

	public Text loadText;

	public Slider load;
	
	public Slider slider;

	public GameObject LoadC;
	public GameObject GS1;
	public GameObject GS2;


	void Awake()
	{
		float volume = PlayerPrefs.GetFloat("volume", 0);	
		audioMixer.SetFloat("volume", volume);
		slider.value = volume;
	}

	public void setAudio(float volume)
	{
		audioMixer.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("volume", volume);
		PlayerPrefs.Save();
	}


	public void LoadGame1Lvl1()
	{
		
		StartCoroutine(Load("G1Nivel1"));

	}

	public void LoadGame1Lvl2()
	{
		StartCoroutine(Load("G1Nivel2"));
	}
	public void LoadGame1Lvl3()
	{
		StartCoroutine(Load("G1Nivel3"));
	}
	public void LoadGame1Lvl4()
	{
		StartCoroutine(Load("G1Nivel4"));
	}
	public void LoadGame1Lvl5()
	{
		StartCoroutine(Load("G1Nivel5"));
	}


	public void Game2Lvl1()
	{
		StartCoroutine(Load("Nivel1G2"));
		
	}


	public void Game2Lvl2()
	{
		StartCoroutine(Load("Nivel2G2"));
	
	}

	public void Game2Lvl3()
	{
		StartCoroutine(Load("Nivel3G2"));
	
	}

	public void Game2Lvl4()
	{
		StartCoroutine(Load("Nivel4G2"));
	
	}

	public void Game2Lvl5()
	{
		StartCoroutine(Load("Nivel5G2"));
	
	}

	public void RestartStats()
	{
		PlayerPrefs.SetInt("levelReached", 1);
		PlayerPrefs.SetInt("levelReached2", 1);
		PlayerPrefs.Save();
	}


	public IEnumerator Load(string sceneName)
	{
		GS1.SetActive(false);
		GS2.SetActive(false);
		LoadC.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;
		while (load.value < 0.9f)
		{
			load.value += (operation.progress/6f) + 0.01f;
			loadText.text = string.Format("{0}%", load.value * 100 + "%");
			yield return null;
		}

		loadText.text = "100%";
		load.value = 1;
		operation.allowSceneActivation = true;

	}


	public void Quit()
	{
		Application.Quit();
	}
}
