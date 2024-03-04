using UnityEngine;



public class CustomCharacterControllerx : MonoBehaviour
{
    [SerializeField] private Transform aimTarget;
    public float aimLerp;
    public float multy;

    public Canvas canvas;

    public Animator anim;
    public Rigidbody rig;
    public Transform mainCamera;
    public float jumpForce = 3.5f;
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public float currentSpeed;
    private float animationInterpolation = 1f;

    public float horisontal;
    public float vertical;
    private float lerpMulti = 7f;
    // private TakeInHand takeInHand;

    public bool canGo = true;

    public bool IsRunning //����������
    {
        get { return isRunning; }
        set
        {
            if (value)
            {
                // takeInHand.SetIk(endWeight: 0);
                // takeInHand.SetRunItemOffset();
            } else
            {
                // takeInHand.SetIk(endWeight: 1);
                // takeInHand.SetRunItemOffset(stopRunning: true);
            }
        }
    }


    public bool isRunning = false;



    private CharacterController characterController;


    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        characterController = GetComponent<CharacterController>();
        this.GetComponent<CustomCharacterController>().enabled = false;
        this.GetComponent<CustomCharacterController>().enabled = true;
        // takeInHand = this.GetComponent<TakeInHand>();
    }

    void Run()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);
        anim.SetFloat("x", horisontal * animationInterpolation);
        anim.SetFloat("y", vertical * animationInterpolation);

        currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, Time.deltaTime * 3);
        anim.SetBool("isRunning", true);
        IsRunning = true;
    }
    public void Walk()
    {
        // Mathf.Lerp - ������� �� ��, ����� ������ ���� ����� animationInterpolation(� ������ ������) ������������ � ����� 1 �� ��������� Time.deltaTime * 3.
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1f, Time.deltaTime * 3);
        anim.SetFloat("x", horisontal * 0.25f);
        anim.SetFloat("y", vertical * 0.25f);

        //currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
        currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
        anim.SetBool("isRunning", false);
        IsRunning = false;

    }

    private void Update()
    {

        Ray desiredTargetRay = mainCamera.gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Vector3 desiredTargetPosition = desiredTargetRay.origin + desiredTargetRay.direction * multy;
        aimTarget.position = Vector3.Lerp(aimTarget.position, desiredTargetPosition, aimLerp * Time.deltaTime);


        horisontal = Input.GetAxis("Horizontal") * animationInterpolation;
        vertical = Input.GetAxis("Vertical") * animationInterpolation;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
        {

            Run();
        }
        else
        {
            anim.SetBool("RifleRunning", false);
            Walk();
        }

        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /*  // ����� �� ������ �������� ��������� � ����������� �� ����������� � ������� ������� ������
          // ��������� ����������� ������ � ������ �� ������ 
          Vector3 camF = mainCamera.forward;
          Vector3 camR = mainCamera.right;
          // ����� ����������� ������ � ������ �� �������� �� ���� ������� �� ������ ����� ��� ����, ����� ����� �� ������� ������, �������� ����� ���� ������� ��� ����� ������� ����� ��� ����
          // ������ ���� ��������� ��� ����� ����� camF.y = 0 � camR.y = 0 :)
          camF.y = 0;
          camR.y = 0;
          Vector3 movingVector;
          // ��� �� �������� ���� ������� �� ������ W & S �� ����������� ������ ������ � ���������� � �������� �� ������ A & D � �������� �� ����������� ������ ������
          movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horisontal * currentSpeed, currentSpeed);
          // Magnitude - ��� ������ �������. � ���� ������ �� currentSpeed ��� ��� �� �������� ���� ������ �� currentSpeed �� 86 ������. � ���� �������� ����� �������� 1.
          anim.SetFloat("magnitude", movingVector.magnitude / currentSpeed);
          //Debug.Log(movingVector.magnitude / currentSpeed);
          // ����� �� ������� ���������! ������������� �������� ������ �� x & z ������ ��� �� �� ����� ����� ��� �������� ������� � ������
          rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
          // � ���� ��� ���, ��� �������� �������� �� ����� � ��� �������� � ������� ���� ������
          rig.angularVelocity = Vector3.zero;*/


        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        camF.y = 0;
        camR.y = 0;
        Vector3 movingVector;
        movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horisontal * currentSpeed, currentSpeed);
        anim.SetFloat("magnitude", movingVector.magnitude / currentSpeed);

        if (!characterController.isGrounded)
        {
            movingVector.y -= walkingSpeed * 2;
        }


        characterController.Move(movingVector * Time.fixedDeltaTime);

    }
    public void Jump()
    {

         rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (characterController.isGrounded)
        {
            characterController.Move(Vector3.up * jumpForce * Time.fixedDeltaTime);
        }
    }
}