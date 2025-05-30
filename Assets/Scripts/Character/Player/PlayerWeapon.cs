using System.Threading;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
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
        if (other.CompareTag("Monster"))
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (monster != null)
            {
               if(!monster.healthComponent.IsDead)
                   monster.SetState(monster.ImpactState);
            }
        }
    }

}
