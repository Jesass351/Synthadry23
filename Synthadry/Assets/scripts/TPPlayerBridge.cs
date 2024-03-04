
using UnityEngine;

public class TPPlayerBridge : MonoBehaviour
{

    public Transform TPPoint;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position = TPPoint.position;
        }
    }
}
