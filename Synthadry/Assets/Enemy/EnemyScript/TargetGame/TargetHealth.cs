using UnityEngine.UI;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    [SerializeField] private float health = 1;
    [SerializeField] private GameObject counter;
    [SerializeField] private int points;


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
        counter.GetComponent<TargetGame>().Increase(points);
        Destroy(gameObject);
    }
}
