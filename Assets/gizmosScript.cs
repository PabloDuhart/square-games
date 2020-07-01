using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmosScript : MonoBehaviour
{
    public GravityGame2 code;
    private SpriteRenderer spriteRenderer;
    private int gravitationRadius;
    void Update()
    {
        gravitationRadius = code.gravitationRadius;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector2(gravitationRadius/6f, gravitationRadius/6f);
    }
}
