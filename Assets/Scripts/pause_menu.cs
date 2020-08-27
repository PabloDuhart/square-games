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
	public GameObject LoadC;
	public Slider load;
	public Text loadText;
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
		StartCoroutine(Load("MenuPrincipal"));
	}

	public IEnumerator Load(string sceneName)
	{
		LoadC.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;
		while (load.value < 0.9f)
		{
			load.value += (operation.progress / 6f) + 0.01f;
			loadText.text = (int)(load.value * 100) * 100 + "%";
			yield return null;
		}

		loadText.text = "100%";
		load.value = 1;
		operation.allowSceneActivation = true;

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