using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
	public GameObject pause;
    public int rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (pause.activeSelf)
		{
			transform.Rotate(Vector3.forward * 0);
		}
		else
		{
			transform.Rotate(Vector3.forward * rotationSpeed);
		}
        
    }
}
