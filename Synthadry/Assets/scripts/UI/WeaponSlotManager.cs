using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WeaponSlotManager : MonoBehaviour
{
    private Image weaponImage;
    private TextMeshProUGUI currentBulletsCount;
    private TextMeshProUGUI allBulletsCount;


    private void Start()
    {
        weaponImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        currentBulletsCount = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        allBulletsCount = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
    public void ChangeActiveWeapon(ItemObject weapon = null)
    {
        if (weapon is not null)
        {
            // gameObject.SetActive(true);
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            weaponImage.sprite = weapon.itemStat.iconActive1K;
            weaponImage.SetNativeSize();
            Debug.Log(weapon.currentAmmo);
            currentBulletsCount.SetText(weapon.currentAmmo.ToString());
            allBulletsCount.SetText(weapon.allAmmo.ToString());
            
        } else
        {
            // gameObject.SetActive(false);
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            weaponImage.sprite = null;
            currentBulletsCount.text = "";
            allBulletsCount.text = "";
        }
    }
}
