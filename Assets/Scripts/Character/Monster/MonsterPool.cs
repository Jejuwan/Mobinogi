using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool Instance { get; private set; }

    public Queue<GameObject> pool { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        pool = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
