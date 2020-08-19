using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class pause_menu : MonoBehaviour
{

	public AudioMixer audioMixer;
	public string sceneName;
	public Slider slider;

	void Awake()
	{
		audioMixer.GetFloat("volume", out float value);
		slider.value = value;
	}


	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
	}

	public void setVolume(float volume)
	{
		audioMixer.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("volume", volume);
		PlayerPrefs.Save();
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


	public void NextLevel()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(sceneName);
	}

}