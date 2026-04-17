using KBCore.Refs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -30.0f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private float mouseSensY = 5.0f;
    [SerializeField] private float mobileScale = 10f;
    private float camXRotation;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        jump = InputSystem.actions.FindAction("Player/Jump");
        jump.started += Jump;
#if !UNITY_ANDROID
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }
    private void OnDisable()
    {
        jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        AudioController.Instance.PlayJumpSFX();
        EventChannelManager.Instance.voidEvent.RaiseEvent();
    }

    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();// (0,0)
        // Movement of the player
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);

#if UNITY_ANDROID
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * mobileScale * Time.deltaTime);
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * rotationSpeed * -1;
#else
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
#endif

        camXRotation = Mathf.Clamp(camXRotation, -80f, 50f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

	public void ChangeMouseSensibility(float value)
	{
		Debug.Log($"Value changed - {value}");
		mouseSensY = value;
		rotationSpeed = value;
	}
}
