using System.Threading;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GetComponent<Collider>().enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �±׷� �Ǻ��ϰų� ������Ʈ�� �Ǻ�
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.healthComponent.TakeDamage(10);
            }
        }
    }

}
