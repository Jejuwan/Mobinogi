using UnityEngine;

public class GhostController : MonsterController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        ((ChrController)this).Init();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
