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
        if (other.CompareTag("Monster") || other.CompareTag("Boss"))
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (monster != null)
            {
                if (!monster.healthComponent.IsDead)
                {
                    bool skillAttack = PlayerSkillController.Instance.currentActiveSkill == null ? false : true;
                    int baseDamage = skillAttack ? PlayerSkillController.Instance.currentActiveSkill.baseDamage : PlayerController.Instance.atk;
                    int dmg = Random.Range(baseDamage - baseDamage / 10, baseDamage + baseDamage / 10);

                    monster.healthComponent.TakeDamage(dmg);
                    monster.OnDamaged(dmg);
                    if(other.CompareTag("Monster"))
                        monster.SetState(monster.ImpactState);
                }
            }
        }
    }
}
