using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TakeInHand : MonoBehaviour
{

    public Transform ItemPoint;
    public float lerpSpeed;
    private GameObject player;
    private List<GameObject> mainGuns;
    private GameObject activeGunGameObject;
    private Animator playerAnimator;
    public Transform ikRightHandTarget;
    public Transform ikRightHandHint;
    public Transform ikLeftHandTarget;
    public Transform ikLeftHandHint;

    private ItemSO itemInHand;
    private GameObject gameObjectInHand;

    public Rig mainIkRig;


    private WeaponSlotManager weaponSlotManager;


    void Start()
    {
        player = gameObject;
        mainGuns = player.GetComponent<InventorySystem>().mainGuns;
        playerAnimator = gameObject.GetComponent<Animator>();
        weaponSlotManager = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlotManager>();
        // weaponSlotManager.gameObject.SetActive(false);
        weaponSlotManager.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void SetRunItemOffset(bool stopRunning = false) //����������
    {
        if (itemInHand && gameObjectInHand)
        {
            if (stopRunning == false)
            {
                gameObjectInHand.transform.localPosition = itemInHand.runPositionOffset;
                gameObjectInHand.transform.localRotation = Quaternion.Euler(itemInHand.runRotationOffset);
            } else
            {
                gameObjectInHand.transform.localPosition = itemInHand.positionOffset;
                gameObjectInHand.transform.localRotation = Quaternion.Euler(itemInHand.rotationOffset);
            }
        }
    }

    public void takeMainGun(int activeGun, float endWeight = 1)
    {
        try
        {
            foreach (GameObject gun in mainGuns)
            {
                gun.SetActive(false);
            }

            activeGunGameObject = mainGuns[activeGun];
            ItemSO itemSO = activeGunGameObject.GetComponent<ItemObject>().itemStat;

            itemInHand = itemSO;
            gameObjectInHand = activeGunGameObject;

            SetOffset(activeGunGameObject, itemSO, endWeight);

            SetAnimation(itemSO, endWeight);

            activeGunGameObject.transform.SetParent(ItemPoint);


            activeGunGameObject.SetActive(true);

            weaponSlotManager.ChangeActiveWeapon(mainGuns[activeGun].GetComponent<ItemObject>());
        }
        catch
        {
            ClearHands();
        }
    }

    void ClearLayersWeight()
    {
        for (int i = 1; i < playerAnimator.layerCount; i++)
        {
            playerAnimator.SetLayerWeight(i, 0);
        }
    }

    void SetOffset(GameObject item ,ItemSO itemSO, float endWeight)
    {
        if (endWeight == 0)
        {
            item.transform.localPosition = itemSO.downPositionOffset;
            item.transform.localRotation = Quaternion.Euler(itemSO.downRotationOffset);
        } 
        else
        {
            item.transform.localPosition = itemSO.positionOffset;
            item.transform.localRotation = Quaternion.Euler(itemSO.rotationOffset);
        }
    }

    void SetAnimation(ItemSO item, float endWeight)
    {
        try
        {
            switch (item.name.ToString())
            {
                case "ak":
                    ClearLayersWeight();
                    SetIk(item, endWeight);
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                case "revolver":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight)); //������ �������� ��������� �����, ��� ���������
                    break;
                default:
                    break;
            
            }
        }
        catch
        {
            ClearHands();
        }
    }


    public void SetIk(ItemSO item = null, float endWeight = 0) //����������
    {
        try
        {
            if (playerAnimator.GetBool("IsRunning"))
            {
                mainIkRig.weight = 0;
            } else
            {
                ItemSO finalItem = null;
                if (item != null)
                {
                    finalItem = item;
                } else if (itemInHand != null)
                {
                    finalItem = itemInHand;
                }
                // Debug.Log(finalItem);
                if (finalItem)
                {
                    if (endWeight == 1)
                    {

                        mainIkRig.weight = endWeight;

                        ikLeftHandHint.localPosition = finalItem.leftHandIkPositionHint;
                        ikLeftHandHint.localRotation = Quaternion.Euler(finalItem.leftHandIkRotationHint);

                        ikLeftHandTarget.localPosition = finalItem.leftHandIkPosition;
                        ikLeftHandTarget.localRotation = Quaternion.Euler(finalItem.leftHandIkRotation);

                        ikRightHandHint.localPosition = finalItem.righHandIkPositionHint;
                        ikRightHandHint.localRotation = Quaternion.Euler(finalItem.righHandIkRotationHint);

                        ikRightHandTarget.localPosition = finalItem.righHandIkPosition;
                        ikRightHandTarget.localRotation = Quaternion.Euler(finalItem.righHandIkRotation);

                    } else
                    {
                        mainIkRig.weight = 0;
                    }
                }
            }

        } catch
        {
            ClearHands();
        }
    }

    IEnumerator LerpSetWeight(int layerIndex, float endWeight)
    {
        float timeToStart = Time.time;
        float targetValue = endWeight;
        while (playerAnimator.GetLayerWeight(layerIndex) != targetValue)
        {
            playerAnimator.SetLayerWeight(layerIndex, Mathf.Lerp(playerAnimator.GetLayerWeight(layerIndex), endWeight, (Time.time - timeToStart) * lerpSpeed));

            yield return null;
        }
    }
    
    public void ClearHands()
    {
        ClearLayersWeight();
        SetIk(null, 0);
        itemInHand = null;
        gameObjectInHand = null;
    }

}
