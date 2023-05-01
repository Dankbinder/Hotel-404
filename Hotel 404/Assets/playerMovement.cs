using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public partial class playerMovement : MonoBehaviour
{
    private FPSInput m_FPSInput;
    [SerializeField]private Transform m_playerCamera;
    [SerializeField] private PlayerInput m_playerInput;
    [SerializeField] private float m_jumpHeight = 5f;
    [SerializeField] private float m_walkSpeed = 5f;
    [SerializeField] private float m_gravity = 5f;
    [SerializeField] private float m_maxPitch = 90f;
    [SerializeField] private Transform m_camera;
    [SerializeField] private float m_lookSense = 5f;
    /*[SerializeField] private AudioClip _spottedSFX;
    [SerializeField] private AudioClip _footStep;*/
    private AudioSource _audioSource;
    private Vector3 moveDirection;
    private Animator anim;
    private InputAction m_move;
    private InputAction m_lookX;
    private InputAction m_lookY;
    private InputAction m_fire;
    private int m_toggle = -1;
    private Vector3 m_SpawnPoint;
    private CharacterController m_controller;
    private Vector3 m_velocity;

    
    private float m_pitch;
    // Start is called before the first frame update
    private void Start()
    {
        
        m_SpawnPoint = transform.position;
    }
    
    
    void Awake()
    {
        m_playerInput = new PlayerInput();
        m_controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        m_move = m_playerInput.Player.Move;
        m_move.Enable();

        m_lookX = m_playerInput.Player.MouseX;
        m_lookX.Enable();
        m_lookY = m_playerInput.Player.MouseY;
        m_lookY.Enable();
        
        m_fire = m_playerInput.Player.Fire;
        m_fire.Enable();
        /*m_fire.performed += ToggleMouse;*/
    }

    private void OnDisable()
    {
        m_move.Disable();
        m_fire.Disable();
        m_lookY.Disable();
        m_lookX.Disable();
    }

    private void Update()
    {
        /*if (moveDirection != Vector3.zero)
        {
            anim.SetBool("Lock", true);
        }
        else
        {
            anim.SetBool("Lock", false);
        }*/
        
        Movement();
        Look();
        Gravity();
    }

    public void GoTojail()
    { 
        transform.position = m_SpawnPoint;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You collided");
        if (other.CompareTag("Guard"))
        {
            GoTojail();
            GameManager.Instance.UpdateLives(-1);
        }
    }

    /*private void ToggleMouse(InputAction.CallbackContext context)
    {
        m_toggle *= -1;
        if (m_toggle == -1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (m_toggle == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
       Debug.Log(m_toggle);
    }*/

    private void Movement()
    {
        Vector2 inputRead = m_move.ReadValue<Vector2>();
        moveDirection = ((transform.right * inputRead.x) + (transform.forward * inputRead.y)).normalized;
        moveDirection *= m_walkSpeed * Time.deltaTime;
        m_controller.Move(moveDirection);
        
        /*while (moveDirection != null)
        {
            _audioSource.PlayOneShot(_footStep);
        }*/
    }

    private void Look()
    {
        m_pitch -= m_lookY.ReadValue<float>() * m_lookSense * Time.deltaTime;
        m_pitch = Mathf.Clamp(m_pitch, -m_maxPitch, m_maxPitch);
        m_camera.localRotation = Quaternion.Euler(m_pitch,0,0);
        transform.Rotate(Vector3.up * m_lookX.ReadValue<float>() * m_lookSense * Time.deltaTime);
    }

    private void Gravity()
    {
        m_velocity.y -= m_gravity * Time.deltaTime;

        m_controller.Move(m_velocity * Time.deltaTime);
    }
    
}
