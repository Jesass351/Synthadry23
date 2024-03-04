using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbRotation : MonoBehaviour
{
    public int rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0,1,0), rotationSpeed);
    }
}
