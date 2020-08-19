using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldChild : MonoBehaviour
{
    public Shield shield;
    private bool ready;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ready = shield.getReadyToTransparentChild();
        if (ready)
        {
            Color childrenColor = GetComponent<SpriteRenderer>().color;
            childrenColor.a -= 0.01f;
            GetComponent<SpriteRenderer>().color = childrenColor;
            if (gameObject.transform.localScale.magnitude>0 )
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.1f, gameObject.transform.localScale.x - 0.1f, 0);
            }




        }
        else
        {
            Color childrenColor = GetComponent<SpriteRenderer>().color;
            childrenColor.a = 1f;
            GetComponent<SpriteRenderer>().color = childrenColor;
            gameObject.transform.localScale = scale;
        }
    }
}
