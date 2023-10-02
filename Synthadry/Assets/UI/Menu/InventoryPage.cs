using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventorySystem PlayerInventory;

    [Header("�������� ������")]
    [SerializeField] private List<GameObject> MainGunsUi;

    [Header("�������������� ������")]
    [SerializeField] private List<GameObject> ExtraGunsUi;

    [Header("����")]
    [SerializeField] private List<GameObject> BuffsUi;

/*    [Header("������ ��������")]
    [SerializeField] private GameObject Things;*/

    public void OpenInventoryPage()
    {
        gameObject.SetActive(true);
    }

    public void CloseInventoryPage()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateUi();
        Cursor.lockState = CursorLockMode.None;

    }

    public void UpdateUi()
    {
        for (var i = 0; i < MainGunsUi.Count; i++)
        {
            if (i < PlayerInventory.mainGuns.Count)
            {
                MainGunsUi[i].transform.GetChild(1).GetComponent<Image>().sprite = PlayerInventory.mainGuns[i].GetComponent<ItemObject>().itemStat.iconActive1K;
                MainGunsUi[i].SetActive(true);
            } else
            {
                MainGunsUi[i].SetActive(false);
            }
        }
    } 
}
