using TMPro;
using UnityEngine;
using System;
using UnityEditor;

public class ItemObject : MonoBehaviour
{
    public ItemSO itemStat;
    private GameObject player;
    private GameObject canvas;

    [Header("0 - 100")]
    public float damage; 
    public float rateOfFire; //��������� � �������

    public int currentAmmo = 5;
    public int allAmmo = 20;

    [Header("���������")]
    public float range = 50f;

    [Header("����������������")]
    public int maxLevelRateOfFire = 5;
    public int levelRateOfFire = 0;


    [Header("����")]
    public int maxLevelDamage = 5;
    public int levelDamage = 0;

    public bool isWeapon;

    [Header("�������� � �������� (0 - 99)")]
    public int maximumAmmo; //� ��������/��������

    [Header("������ � ��������")]
    public int maxLevelAmmo = 5;
    public int levelAmmo = 0;

    [Header("�����")]
    public TextMeshProUGUI allAmmoInGameUi;
    public TextMeshProUGUI currentAmmoInGameUi;

    [Header("��������")]
    public GameObject bulletPref;
    public Vector3 bulletRotation;
    public GameObject bulletTracer;
    public Vector3 bulletOutForce;
    public int bulletAlive;
    public Transform bulletSpawnPoint;
    public AudioSource shootSound;

    public ParticleSystem fireFx;

    [Header("������")]
    public GameObject lantern;
    public GameObject light;
    public GameObject aim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    public void Shoot()
    {
        bool onlyOneHit = true;
        
        if (currentAmmo > 0)
        {
            fireFx.Play();
            shootSound.Play(0);
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool isHit = Physics.Raycast(ray, out hit, range);
            if (isHit && onlyOneHit)
            {
                onlyOneHit = false;
                Collider hitObject = hit.collider;
                Debug.Log("� ����� � " + hitObject);
                if (hitObject.CompareTag("Enemy"))
                {
                    if (hitObject.TryGetComponent<EnemyDamage>(out EnemyDamage enemyDamage))
                    {
                        enemyDamage.GetDamage(damage);
                    }
                    if (hitObject.TryGetComponent<TargetDamage>(out TargetDamage targetDamage))
                    {
                        targetDamage.GetDamage(damage);
                    }
                }
            }
            currentAmmo -= 1;
/*            SpawnBullet();*/

        }
        else
        {
            //���� ������ ��� ����������� (����� ������)
        }


    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPref, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.right * 100f;
        GameObject bulletTrace = Instantiate(bulletTracer, bulletSpawnPoint.position, Quaternion.identity);

        bulletTrace.transform.SetParent(bullet.transform);

        Destroy(bullet, bulletAlive);
        Destroy(bulletTrace, bulletAlive);

    }

    public void CheckItemParams()
    {
        if (itemStat.type is ItemSO.Type.firearms)
        {
            Shoot();
        }
    }

    private void Update()
    {
        
/*        if (Input.GetMouseButtonDown(0) && !player.GetComponent<CustomCharacterController>().isRunning && canvas.activeInHierarchy)
        {
            
        }*/

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     if (gameObject.activeInHierarchy)
        //     {
        //         if (lantern.activeInHierarchy)
        //         {
        //             if (!light.activeInHierarchy)
        //             {
        //                 light.SetActive(true);
        //             }
        //             else
        //             {
        //                 light.SetActive(false);
        //             }
        //         }
        //     }
        // }

    }

    public void AddLantern()
    {
        lantern.SetActive(true);
    }

    public void RemoveLantern()
    {
        lantern.SetActive(false);
    }

    public void AddAim()
    {
        aim.SetActive(true);
    }

    public void RemoveAim()
    {
        aim.SetActive(false);
    }




    public void IncreaseRateOfFire(float num)
    {
        rateOfFire = Math.Min(100, rateOfFire + num);
    }

    public void DecreaseRateOfFire(float num)
    {
        rateOfFire = Math.Max(0, rateOfFire - num);
    }

    public void IncreaseDamage(float num)
    {
        damage = Math.Min(100, damage + num);
    }

    public void DecreaseRateDamage(float num)
    {
        damage = Math.Max(0, damage - num);
    }

    public void IncreaseMaxAmmo(int num)
    {
        maximumAmmo = Math.Min(1, maximumAmmo + num);
    }

    public void DecreaseMaxAmmo(int num)
    {
        maximumAmmo = Math.Max(0, maximumAmmo - num);
    }

    public void IncreaseAllAmmo(int num)
    {
        allAmmo = Math.Min(1000, allAmmo + num);
    }

    public void DecreaseAllAmmo(int num)
    {
        allAmmo = Math.Max(0, allAmmo - num);

    }

    public void IncreaseCurrentAmmo(int num)
    {
        currentAmmo = Math.Min(maximumAmmo, currentAmmo + num);
    }

    public void DecreaseCurrentAmmo(int num)
    {
        currentAmmo = Math.Max(0, currentAmmo - num);

    }
}