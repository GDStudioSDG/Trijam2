using UnityEngine;


 public class playerController : MonoBehaviour
{
    public float Speed = 0.3f;
    public float JumpForce = 1f;


    [SerializeField] private float sensitivity = 3; // ���������������� �����
    private float X, Y;

    //���� ����������� ������� ��� ����.
    //��� �� ��������� ��� ��� Player ��� �� ��������� � ������ ����. 

    //!!!!�������� �� ���� ������������� Layer, �������� Player!!!!
    public LayerMask GroundLayer = 1; // 1 == "Default"

    private Rigidbody _rb;
    private BoxCollider _collider; // ������ ��������� ������������ CapsuleCollider
    //� ������� ���� ��������� ���� �� ����
    private Transform HeadTransform;

    private bool _isGrounded
    {
        get
        {
            var bottomCenterPoint = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);

            //������� ��������� ���������� ������� � ��������� �� ���������� �� ��� ������ ������� ��������� � ����

            //_collider.bounds.size.x / 2 * 0.9f -- ��� �������� ����������� ����� ������ �������.
            // ��� �� ����������� ������ -- ������ �� ������ ��������, � ��� ����� ��-�������������

            return Physics.CheckCapsule(_collider.bounds.center, bottomCenterPoint, _collider.bounds.size.x / 2 * 0.6f, GroundLayer);
            // ���� ����� ����� ������� � �������, �� ����� ����� �������� ���������� 0.9 �� �������.
        }
    }

    private Vector3 _movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            Vector3 direction =  (transform.forward*vertical)+ (horizontal * transform.right); 

            return direction;
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();

        HeadTransform = transform.Find("head").GetComponent<Transform>();

        //�.�. ��� �� ����� ��� �� �������� ��� ������ ��� ��-���� ��� ������ �� �� ��������.
        //�� ����� ��������� ������� �� ���� X � Z
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //  ������ �� ������
        if (GroundLayer == gameObject.layer)
            Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer!");
    }


    private void Update()
    {
        X = Input.GetAxis("Mouse X") * sensitivity;
        Y += Input.GetAxis("Mouse Y") * sensitivity;
        Y = Mathf.Clamp(Y, -45f, 45f);
        transform.localEulerAngles += new Vector3(0, X, 0);
        HeadTransform.localEulerAngles = new Vector3(-Y, 0, 0);
    }
    void FixedUpdate()
    {
        JumpLogic();
        MoveLogic();  
    }

    private void MoveLogic()
    {
        // �.�. �� ������ ������ ������������ ���������� �������� �����,
        // �� ������ � ��������� Time.fixedDeltaTime
        //_rb.AddForce((_movementVector * Speed)+BoatRb.velocity, ForceMode.Impulse);
        _rb.MovePosition(_rb.position + _movementVector * Speed * Time.fixedDeltaTime);
    }

    private void JumpLogic()
    {
        if (_isGrounded && (Input.GetAxis("Jump") > 0))
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
}