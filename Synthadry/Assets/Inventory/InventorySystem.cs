using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Canvas canvas;

    private ItemsIK takeInHand;
    public bool haveHeadlight;

    public List<GameObject> mainGuns;
    public List<GameObject> extraGuns;


    private int mainGunCounterToggler = 0;

    private WeaponSlotManager weaponSlotManager;
    private BuffsSlotManager buffsSlotManager;


    public int ActiveMainGun
    {
        get { return activeMainGun; }
        set
        {
            if (activeMainGun == value)
            {
                if (mainGunCounterToggler != 0)
                {
                    activeMainGun = value;
                    mainGunCounterToggler = 0;
                    takeInHand.takeMainGun(activeMainGun);
                }
                else
                {
                    takeInHand.ClearHands();
                    mainGunCounterToggler++;
                }
            }
            else
            {
                if (value <= mainGuns.Count)
                {
                    activeMainGun = value;
                    takeInHand.takeMainGun(activeMainGun);
                    mainGunCounterToggler = 0;
                }
            }

        }
    }


    public int activeMainGun = -1;

    public List<GameObject> hpBuffs;
    public List<GameObject> armorBuffs;
    public List<GameObject> speedBuffs;

    [SerializeField] private int activeBuff = 0;

    private ItemSO itemObject;

    private HPAndArmor hpAndArmor;

    /*
        [SerializeField] private GrenadeThrow grenadeThrow;

        [SerializeField] private GameObject mainGunSpawn;*/

    public float currentBatteryEnergy = 1f;
    public int batteries = 0;

    [Header("КОМПОНЕНТЫ")]
    public int fuel = 0;
    public int cloth = 0;
    public int metal = 0;
    public int plastic = 0;
    public int chemical = 0;
    public int wires = 0;

    /*    public void GetActiveMainGun()
        {
            foreach (GameObject gun in mainGuns)
            {
                gun.SetActive(false);
            }
            mainGuns[activeMainGun].SetActive(true);
            mainGuns[activeMainGun].transform.SetParent(mainGunSpawn.transform);
            mainGuns[activeMainGun].transform.position = mainGunSpawn.transform.position;
        }*/

    // Start is called before the first frame update
    void Start()
    {
        hpAndArmor = player.GetComponent<HPAndArmor>();
        takeInHand = player.GetComponent<ItemsIK>();
        weaponSlotManager = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlotManager>();
        buffsSlotManager = GameObject.FindGameObjectWithTag("BuffsSlot").GetComponent<BuffsSlotManager>();
        buffsSlotManager.DrawBuffs(hpBuffs.Count, armorBuffs.Count, speedBuffs.Count, activeBuff);
        buffsSlotManager.DrawGrenades(extraGuns.Count);
    }

    public void OnBuffChange(InputAction.CallbackContext ctx) //СМЕНА АКТИВНОГО БАФФА
    {
        if (ctx.performed)
        {
            activeBuff = (activeBuff + 1) % 3;
            buffsSlotManager.DrawBuffs(hpBuffs.Count, armorBuffs.Count, speedBuffs.Count, activeBuff);
        }
    }

    public void OnBuffUse(InputAction.CallbackContext ctx) //ПРИМЕНЕНИЕ АКТИВНОГО БАФФА
    {
        if (ctx.performed)
        {
            UseBuff(activeBuff);
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q)) //СМЕНА АКТИВНОГО БАФФА
        // {
        //     activeBuff = (activeBuff + 1) % 3;
        //     buffsSlotManager.DrawBuffs(hpBuffs.Count, armorBuffs.Count, speedBuffs.Count, activeBuff);
        // }

        // if (Input.GetKeyDown(KeyCode.X)) //ПРИМЕНЕНИЕ АКТИВНОГО БАФФА
        // {
        //     UseBuff(activeBuff);
        // }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveMainGun = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveMainGun = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveMainGun = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActiveMainGun = 4;
        }

        if (Input.GetKeyDown(KeyCode.G)) //ВЫКИНУТЬ ПРЕДМЕТ
        {
            DiscardTheItem(mainGuns[activeMainGun]);

            ActiveMainGun = Math.Max(mainGuns.Count - 1, 0);
            mainGuns[ActiveMainGun].layer = 20;




        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //КОЛЁСИКОМ ВПЕРЁД
        {
            if (mainGuns.Count > 0)
            {
                ActiveMainGun = (ActiveMainGun + 1) % mainGuns.Count;
            }
            else
            {
                ActiveMainGun = 0;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //КОЛЁСИКОМ НАЗАД
        {
            if (mainGuns.Count > 0)
            {
                ActiveMainGun = (mainGuns.Count + ActiveMainGun - 1) % mainGuns.Count;
            }
            else
            {
                ActiveMainGun = 0;
            }
        }
    }

    public void UseBattery()
    {
        batteries--;
        if (batteries > 0)
        {
            currentBatteryEnergy = 1f;
        }
    }


    //-------------ПОДОБРАТЬ ИЛИ ВЫБРОСИТЬ ПРЕДМЕТ (ОРУЖИЕ И ГРАНАТЫ)---------------
    public void DiscardTheItem(GameObject item)
    {
        item.transform.position = player.position + new Vector3(0, 2f, 0);
        item.transform.parent = null;

        item.GetComponent<CustomTooltip>().enabled = true;
        item.GetComponent<SphereCollider>().enabled = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<BoxCollider>().enabled = true;

        item.layer = 20;
        item.SetActive(true);
        mainGuns.Remove(item);
        takeInHand.ClearHands();
        weaponSlotManager.ChangeActiveWeapon();

    }

    public void SetActiveMainGun(int active)
    {
        activeMainGun = active;
    }

    public void PickUpItem(GameObject item)
    {
        if (item.GetComponent<ItemObject>())
        {
            if (mainGuns.Count < 3)
            {
                mainGuns.Add(item);
                item.SetActive(false);
                item.GetComponent<CustomTooltip>().enabled = false;
                item.GetComponent<SphereCollider>().enabled = false;
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.GetComponent<BoxCollider>().enabled = false;

                item.layer = 0;
            }
            else
            {
                Debug.Log("Основной инвентарь уже полный");
            }

        }
        /*        else if (item.GetComponent<ExtraObject>())
                {
                    if (extraGuns.Count < 9)
                    {
                        Debug.Log("------------");
                        Debug.Log("extraGuns:");
                        extraGuns.Add(item);

                        UpdateInventoryUIItems(activeMainGun);
                    }
                    else
                    {
                        Debug.Log("---------");
                        Debug.Log("Дополнительный (гранатный) инвентарь уже полный");
                    }

                }*/

    }



    //-------------ПОДОБРАТЬ БАФФЫ (ХП, СИЛА, СКОРОСТЬ)---------------
    public void PickUpBuff(GameObject item)
    {
        if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "hp")
        {
            if (hpBuffs.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("hpBuffs:");
                hpBuffs.Add(item);
                for (int i = 0; i < hpBuffs.Count; i++)
                {
                    Debug.Log(hpBuffs[i]);
                }

            }
            else
            {
                Debug.Log("---------");
                Debug.Log("ХП инвентарь уже полный");
            }
        }
        else if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "speed")
        {
            if (speedBuffs.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("speedBuffs:");
                speedBuffs.Add(item);
                for (int i = 0; i < speedBuffs.Count; i++)
                {
                    Debug.Log(speedBuffs[i]);
                }

            }
            else
            {
                Debug.Log("---------");
                Debug.Log("Speed инвентарь уже полный");
            }
        }
        else if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "armor")
        {
            if (armorBuffs.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("armorBuffs:");
                armorBuffs.Add(item);
                for (int i = 0; i < armorBuffs.Count; i++)
                {
                    Debug.Log(armorBuffs[i]);
                }

            }
            else
            {
                Debug.Log("---------");
                Debug.Log("armor инвентарь уже полный");
            }
        }

        buffsSlotManager.DrawBuffs(hpBuffs.Count, armorBuffs.Count, speedBuffs.Count, activeBuff);

    }



    public void UseBuff(int active)
    {
        if (activeBuff == 0 && hpBuffs.Count != 0)
        {
            hpAndArmor.TakeHeal(hpBuffs[0].GetComponent<BuffObject>().BuffStat.increase, hpBuffs[0].GetComponent<BuffObject>().BuffStat.type.ToString());
            hpBuffs.RemoveAt(0);

        }
        else if (activeBuff == 1 && armorBuffs.Count != 0)
        {
            hpAndArmor.TakeHeal(armorBuffs[0].GetComponent<BuffObject>().BuffStat.increase, armorBuffs[0].GetComponent<BuffObject>().BuffStat.type.ToString());
            armorBuffs.RemoveAt(0);
        }
        else if (activeBuff == 2 && speedBuffs.Count != 0)
        {
            speedBuffs.RemoveAt(0);
        }

        buffsSlotManager.DrawBuffs(hpBuffs.Count, armorBuffs.Count, speedBuffs.Count, activeBuff);

    }

    public void PickUpComponent(GameObject component)
    {
        for (int i = 0; i < component.GetComponent<ComponentsObject>().componentStat.Count; i++)
        {
            string type = component.GetComponent<ComponentsObject>().componentStat[i].type.ToString();
            switch (type)
            {
                case "fuel":
                    fuel += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "cloth":
                    cloth += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "metal":
                    metal += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "plastic":
                    plastic += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "chemical":
                    chemical += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "wires":
                    wires += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "":
                    break;

            }
        }
    }
}
