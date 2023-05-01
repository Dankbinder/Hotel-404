using UnityEngine;
using UnityEngine.InputSystem;

public class FPSControls : MonoBehaviour
{
    private FPSInput m_FPSInput;
    [SerializeField] private float n_movementSpeed = 5f;
    [SerializeField] private float n_lookSensitivity = 5f;
    [SerializeField] private float m_maxPitch = 90f;
    [SerializeField] private Transform camera;
    
    private InputAction m_move;
    private InputAction m_lookX;
    private InputAction m_lookY;

    private float n_pitch;
    private CharacterController m_controller;
    private Transform m_velocity;

    private void Awake()
    {
        m_FPSInput = new FPSInput();
        m_controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        m_move = m_FPSInput.Player.Move;
        m_lookX = m_FPSInput.Player.MouseX;
        m_lookY = m_FPSInput.Player.MouseY;
        m_move.Enable();
        m_lookX.Enable();
        m_lookY.Enable();
    }

    private void OnDisable()
    {
        m_move.Disable();
        m_lookX.Disable();
        m_lookY.Disable();
    }

    private void Update()
    {
        Movement();
        Look();
    }

    private void Movement()
    {
        Vector2 inputRead = m_move.ReadValue<Vector2>();
        Vector3 moveDirection = ((transform.right * inputRead.x) + (transform.forward * inputRead.y)).normalized;
        moveDirection *= n_movementSpeed * Time.deltaTime;
        m_controller.Move(moveDirection);
    }

    private void Look()
    {
        n_pitch -= m_lookY.ReadValue<float>() * n_lookSensitivity * Time.deltaTime;
        n_pitch = Mathf.Clamp(n_pitch, -m_maxPitch, m_maxPitch);
        camera.localRotation = Quaternion.Euler(n_pitch, 0f, 0f);
        transform.Rotate(Vector3.up * m_lookX.ReadValue<float>() * n_lookSensitivity * Time.deltaTime);
    }
}
