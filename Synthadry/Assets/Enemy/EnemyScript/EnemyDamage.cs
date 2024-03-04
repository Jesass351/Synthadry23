using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float multiply = 2;

    public void GetDamage(float damage)
    {
        transform.parent.GetComponent<EnemyHealth>().GetDamage(damage, multiply);
        Debug.Log("Попал");
    }
}
