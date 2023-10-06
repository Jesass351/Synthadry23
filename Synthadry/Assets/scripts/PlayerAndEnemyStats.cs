using UnityEngine;

public class Stats : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public int damage;

    public int armor;

    public string collisionTag;


    public void TakeDamage(int damage)//��������� �����
    {
        damage -= armor;//���� ������� ���� �����
        if (damage < 0)
        {
            damage = 0;
        }
        currentHealth -= damage;
        Debug.Log("-" + damage);
        DeathCheck();
    }


    public void TakeHeal(int heal)//���
    {
        currentHealth += heal;
        Debug.Log("+" + heal);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DeathCheck()//������
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject); //���-�� ��� ����� ��������
        }
    }

    private void OnCollisionEnter(Collision collision)//��������������� � ��������(���� � ������� ������� ����, ����� ���������� ��)(������� ����� ����� ������)
    {
        if (collision.gameObject.tag == collisionTag)
        {
            Stats statsEn = collision.gameObject.GetComponent<Stats>();
            statsEn.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Heal")
        {
            TakeHeal(10);
            Destroy(collision.gameObject);
        }
    }
}
