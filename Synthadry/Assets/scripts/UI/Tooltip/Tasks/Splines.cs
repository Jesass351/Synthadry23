using TMPro;
using UnityEngine;

public class Splines : MonoBehaviour
{
    public GameObject splineToHide;
    public GameObject splineToShow;
    public TaskSO newTask;
    public GameObject UI;


    void OnTriggerEnter(Collider other)
    {
        TextMeshProUGUI titleUI = UI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textUI = UI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        if (other.tag == "Player")
        {
            if (splineToShow != null && splineToHide != null)
            {
                splineToHide.SetActive(false);
                GameObject splineToShowMesh = splineToShow.transform.GetChild(0).transform.GetChild(0).gameObject;
                if (splineToShowMesh.TryGetComponent<MeshCollider>(out MeshCollider collider))
                {
                    collider.enabled = false;
                }
                splineToShow.SetActive(true);
                if (newTask == null)
                {
                    UI.SetActive(false);
                } else
                {
                    titleUI.text = newTask.Title;
                    textUI.text = newTask.Text;
                    UI.SetActive(true);
                }
            }
        }
        Destroy(gameObject);
    }
}
