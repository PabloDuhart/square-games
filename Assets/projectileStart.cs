using System.Collections.Generic;
using UnityEngine;

public class projectileStart : MonoBehaviour
{
    private List<GameObject> projectileList;

    public GameObject projectilePrefab;

    public int projectileCant;
    // Start is called before the first frame update
    void Start()
    {
        projectileList = new List<GameObject>();
        for (int i = 0; i < projectileCant; i++)
        {
            GameObject projectil = Instantiate(projectilePrefab);
            projectil.name = "projectile(" + (i+1) + ")";
            projectileList.Add(projectil);
        }
        for (int i = 0; i < projectileList.Count; i++)
        {
            
        }
        
    }
}
