using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    [SerializeField] private float multiply = 2;

    public void GetDamage(float damage)
    {
        transform.parent.GetComponent<TargetHealth>().GetDamage(damage, multiply);
        
    }
}
