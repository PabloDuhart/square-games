using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmosScript : MonoBehaviour
{
    public GravityGame2 code;
    private SpriteRenderer spriteRenderer;
    private float gravitationRadius;
    public float sizeVariable;
    void Update()
    {
        gravitationRadius = code.gravitationRadius;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector2(gravitationRadius/sizeVariable, gravitationRadius/sizeVariable);
    }
}
