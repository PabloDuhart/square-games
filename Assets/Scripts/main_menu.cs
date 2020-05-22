using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class main_menu : MonoBehaviour
{

	public AudioMixer audioMixer;

	public void setAudio(float volume)
	{
		audioMixer.SetFloat("volume", volume);
	}


	public void LoadGame1()
	{
		SceneManager.LoadScene("Map");
	}
	public void LoadGame2()
	{
		//Here load game 2
	}
	public void LoadGame3()
	{
		//Here load game 3
	}


	public void Quit()
	{
		Application.Quit();
	}
}
