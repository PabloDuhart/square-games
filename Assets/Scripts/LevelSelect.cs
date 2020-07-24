using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update

    public Button [] levelButtons;

    

    void Start()
    {

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
			{
                levelButtons[i].interactable = false;
			}
			else
			{
                levelButtons[i].interactable = true;
            }
		}
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }
        PlayerPrefs.Save();
    }
}
