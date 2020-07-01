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

	public void Game2Lvl1()
	{
		
	}

	public void Game2Lvl2()
	{
		
	}

	public void Game2Lvl3()
	{
		
	}

	public void Game2Lvl4()
	{
		
	}

	public void Game2Lvl5()
	{
		
	}

	public void Quit()
	{
		Application.Quit();
	}
}
