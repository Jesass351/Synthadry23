using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CustomTooltip : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private GameObject pointerParent;


    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] private Vector2 imageSize =  new Vector2(40, 40);
    [SerializeField] private int fontSize = 30;
    [SerializeField] private float lerpTime = 50;

    private GameObject newPointer;

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (newPointer != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(gameObject.transform.position);
            if (screenPos.z < 0)
            {
                newPointer.SetActive(false);
            } else
            {
                newPointer.SetActive(true);
                newPointer.transform.position = Vector3.Lerp(newPointer.transform.position, screenPos, lerpTime * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (newPointer == null)
            {
                newPointer = Instantiate(tooltipPrefab, new Vector3(0, 0, 0), Quaternion.identity); ;
                newPointer.transform.SetParent(pointerParent.transform);
                newPointer.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = imageSize;
                newPointer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = fontSize;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (newPointer)
            {
                Destroy(newPointer.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        if (newPointer)
        {
            Destroy(newPointer.gameObject);
        }
    }
}
