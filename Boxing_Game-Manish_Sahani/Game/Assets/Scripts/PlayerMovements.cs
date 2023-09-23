using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

  
    [SerializeField]
    private Transform cameraTransform;

    public Animator animator;
    private Animator enemyAnimator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);

            if (Input.GetKeyDown(KeyCode.P))
            {
                animator.SetBool("IsPunching", true);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                animator.SetBool("IsBurpee", true);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetBool("IsCrouching", true);
            }


        }


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            enemyAnimator = FindObjectOfType<Enemy>().animator;
            enemyAnimator.SetBool("IsPunched", true);
        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ChangePunchBool()
    {
        animator.SetBool("IsPunching", false);   
    }

    public void ChangeBurpeeBool()
    {
        animator.SetBool("IsBurpee", false);
    }

    public void ChangeDuckingBool()
    {
        animator.SetBool("IsDucking", false);
    }

    public void ChangeCrouchingBool()
    {
        animator.SetBool("IsCrouching", false);
    }
}
