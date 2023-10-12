using UnityEngine.UI;
using UnityEngine;

public class HeadlightSystem : MonoBehaviour
{
    public GameObject headLight;
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float energyLostPerFixedUpdate = 5f;
    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventorySystem.haveHeadlight)
            {
                if (headLight.activeInHierarchy)
                {
                    headLight.SetActive(false);
                }
                else
                {
                    if (currentEnergy > 0)
                    {
                        headLight.SetActive(true);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (headLight.activeInHierarchy)
        {
            currentEnergy = Mathf.Max(currentEnergy - energyLostPerFixedUpdate, 0);

            if (currentEnergy <= 0)
            {
                inventorySystem.UseBattery();
            }
        }
    }
}