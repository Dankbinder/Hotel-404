using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSControls : MonoBehaviour
{
    private FPSInput m_FPSInput;
    [SerializeField] private float m_movementSpeed = 5f;
    [SerializeField] private float m_maxPitch = 90f;
    [SerializeField] private float m_lookSense = 5f;
    [SerializeField] private Transform m_camera;
    private InputAction m_move;
    private InputAction m_lookY;
    private InputAction m_lookX;
    private float m_pitch;
    private CharacterController m_controller;
    
    // Start is called before the first frame update
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
        Vector2 InputRead = m_move.ReadValue<Vector2>();
        Vector3 moveDirection = ((transform.right * InputRead.x) + (transform.forward * InputRead.y)).normalized;
        moveDirection *= m_movementSpeed * Time.deltaTime;
        m_controller.Move(moveDirection);
    }

    private void Look()
    {
        m_pitch -= m_lookY.ReadValue<float>() * m_lookSense * Time.deltaTime;
        m_pitch = Mathf.Clamp(m_pitch, -m_maxPitch, m_maxPitch);
        m_camera.localRotation = Quaternion.Euler(m_pitch,0,0);
        transform.Rotate(Vector3.up * m_lookX.ReadValue<float>() * m_lookSense * Time.deltaTime);
    }


}