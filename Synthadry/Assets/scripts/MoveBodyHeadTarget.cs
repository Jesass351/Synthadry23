using GameCreator.Runtime.Cameras;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBodyHeadTarget : MonoBehaviour
{
    private Transform mainCamera;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = mainCamera.position + offset;
        
    }
}
