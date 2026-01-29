using UnityEngine;
using KBCore.Refs;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -30.0f;
    private Vector3 velocity;
    [SerializeField] private float mouseSenseY = 5.0f;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);

        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);

        mouseSenseY = mouseSenseY * readLook.y;
        mouseSenseY = Mathf.Clamp(mouseSenseY, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(mouseSenseY, 0, 0);
    
        Debug.Log(readMove);
    }
}
