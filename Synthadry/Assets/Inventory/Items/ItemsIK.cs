using GameCreator.Runtime.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;

public class ItemsIK : MonoBehaviour
{
    public Transform ItemPoint;
    public float lerpSpeed;
    private GameObject player;
    private List<GameObject> mainGuns;
    private GameObject activeGunGameObject;
    private Animator playerAnimator;
   // public Transform ikRightHandTarget;
   // public Transform ikRightHandHint;
    //public Transform ikLeftHandTarget;
    //public Transform ikLeftHandHint;

    private ItemSO itemInHand;
    private GameObject gameObjectInHand;

    public Transform ikRightHandTarget;
    public Transform ikRightHandHint;
    public Transform ikLeftHandTarget;
    public Transform ikLeftHandHint;



    public Rig weaponIkRig;


    private WeaponSlotManager weaponSlotManager;


    void Start()
    {
        player = gameObject;
        mainGuns = player.GetComponent<InventorySystem>().mainGuns;
        playerAnimator = gameObject.GetComponent<Animator>();
        weaponSlotManager = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlotManager>();
        // weaponSlotManager.gameObject.SetActive(false);
        weaponSlotManager.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        ClearHands();
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

            SetIKPosition(activeGunGameObject);

            SetAnimation(itemSO, endWeight);

            activeGunGameObject.transform.SetParent(ItemPoint);

            activeGunGameObject.transform.localPosition = itemInHand.positionOffset;
            activeGunGameObject.transform.localRotation = Quaternion.Euler(itemInHand.rotationOffset);



            activeGunGameObject.SetActive(true);

            weaponSlotManager.ChangeActiveWeapon(mainGuns[activeGun].GetComponent<ItemObject>());
            Debug.Log("end");
            Debug.Log(activeGunGameObject);

        }
        catch
        {
            ClearHands();
        }
    }

    public void SetIKPosition(GameObject activeGunGameObject)
    {

        ItemSO finalItem = activeGunGameObject.GetComponent<ItemObject>().itemStat;


        ikLeftHandHint.localPosition = finalItem.leftHandIkPositionHint;
        ikLeftHandHint.localRotation = Quaternion.Euler(finalItem.leftHandIkRotationHint);

        ikLeftHandTarget.localPosition = finalItem.leftHandIkPosition;
        ikLeftHandTarget.localRotation = Quaternion.Euler(finalItem.leftHandIkRotation);

        ikRightHandHint.localPosition = finalItem.righHandIkPositionHint;
        ikRightHandHint.localRotation = Quaternion.Euler(finalItem.righHandIkRotationHint);

        ikRightHandTarget.localPosition = finalItem.righHandIkPosition;
        ikRightHandTarget.localRotation = Quaternion.Euler(finalItem.righHandIkRotation);
        SetWeaponIKRig(1);
    }

    void SetAnimation(ItemSO item, float endWeight)
    {
        try
        {
            switch (item.name.ToString())
            {
                case "ak":
                    ClearLayersWeight();
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

    public void SetWeaponIKRig(int weight)
    {
        weaponIkRig.weight = weight;
    }

    public void ClearHands()
    {
        ClearLayersWeight();
        SetWeaponIKRig(0);
        itemInHand = null;
        gameObjectInHand = null;
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

    void ClearLayersWeight()
    {
        for (int i = 1; i < playerAnimator.layerCount; i++)
        {
            playerAnimator.SetLayerWeight(i, 0);
        }
    }


}
