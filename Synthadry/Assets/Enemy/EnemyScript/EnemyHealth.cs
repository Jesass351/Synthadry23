using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 500;
    [SerializeField] private float timeToDestroy = 5;

    private ragdollController ragdoll;

/*    private void Awake()
    {
        ragdoll = GetComponent<ragdollController>();
    }*/

    public void GetDamage(float damage, float multiply)
    {
        this.health -= damage * multiply;
        if (this.health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        /*ragdoll.ActivateRagdoll();*/
        GetComponent<Animator>().enabled = false;
        GetComponent<StandartAi>().enabled = false;
        Destroy(gameObject, timeToDestroy);
    }
}
