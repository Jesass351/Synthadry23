using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float lerpPosition;

    public float sensitivityX = 2;
    public float sensitivityY = 2;

    [SerializeField] private int maxAngleUp = 65; //����
    [SerializeField] private int maxAngleDown = 65; //�����

    Vector3 rot = new Vector3(0, 0, 0);

    public void ChangeSensitivity(int x, int y)
    {
        sensitivityX = x;
        sensitivityY = y;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition.position, lerpPosition * Time.deltaTime);

        float MouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        rot.x = Math.Min(rot.x - MouseY, maxAngleUp);
        rot.x = Math.Max(rot.x - MouseY, maxAngleDown);

        rot.y = rot.y + MouseX;
        transform.eulerAngles = rot;

/*        player.transform.eulerAngles = new Vector3(0, rot.y, 0);*/
    }
}

/*using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player; //target

    [SerializeField] private float sensitivityX = 2;
    [SerializeField] private float sensitivityY = 2;

    [SerializeField] private int maxAngleUp = 65; //����
    [SerializeField] private int maxAngleDown = 65; //�����

    private Vector3 initialCameraPosition;
    Vector3 rot = new Vector3(0, 0, 0);
    float cameraMoveOffset = 0.02f;
    private bool isIdle = false;
    private Coroutine resetCameraCoroutine;
    private Coroutine setPosCameraCoroutine;

    void Start()
    {
        initialCameraPosition = transform.localPosition;
    }

    bool MoveW() { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); }
    bool MoveA() { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow); }

    bool MoveS() { return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow); }

    bool MoveD() { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow); }
    bool MoveAny() { return MoveW() || MoveA() || MoveS() || MoveD(); }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        rot.x = rot.x - MouseY;
        if (rot.x > maxAngleDown)
        {
            rot.x = maxAngleDown;
        }
        if (rot.x < -maxAngleUp)
        {
            rot.x = -maxAngleUp;
        }
        rot.y = rot.y + MouseX;

        transform.eulerAngles = rot;

        player.transform.eulerAngles = new Vector3(0, rot.y, 0);

        bool isRunning = MoveAny();

        if (isRunning && !isIdle)
        {
            isIdle = true;

            if (resetCameraCoroutine != null)
            {
                StopCoroutine(resetCameraCoroutine);
            }

            Vector3 targetCameraPosition = initialCameraPosition;
            if(MoveW() || MoveS()) targetCameraPosition.z += cameraMoveOffset;
            if (MoveA()) targetCameraPosition.x += cameraMoveOffset;
            else if (MoveD()) targetCameraPosition.x -= cameraMoveOffset;
            setPosCameraCoroutine = StartCoroutine(SetCameraPos(targetCameraPosition));
            //transform.localPosition = targetCameraPosition;
        }
        else if (!isRunning && isIdle)
        {
            isIdle = false;
            if (setPosCameraCoroutine != null)
            {
                StopCoroutine(setPosCameraCoroutine);
            }
            resetCameraCoroutine = StartCoroutine(ResetCameraPosition());
        }
    }

    private IEnumerator SetCameraPos(Vector3 newPos)
    {
        while (transform.localPosition != newPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator ResetCameraPosition()
    {
        yield return new WaitForSeconds(0.1f);
        while(transform.localPosition != initialCameraPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialCameraPosition, Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}*/

