using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    //---------VARAIBLES GLOBAL-----------//
    [SerializeField] public float walkSpeed = 15f;
    [SerializeField] public float runSpeed = 30f;
    [SerializeField] public float rotationSpeed = 12f;
    
    private Transform cameraTransform;
    
    //-------VARIABLES MOTEUR---------//
    PlayerInputReader input;
    private Rigidbody rb;
    
    void Awake()
    {
        input = GetComponent<PlayerInputReader>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }
    
    private void FixedUpdate()
    {
        Walk(input.Move, Run());
    }

    //--------FUNCTION POUR CE DEPLACER-------------//
    private void Walk(Vector2 dir, float speed)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        forward.y = 0;
        right.y = 0;
        
        forward.Normalize();
        right.Normalize();
        
        Vector3 direction = forward * dir.y + right * dir.x;
        direction = Vector3.ClampMagnitude(direction, 1f);
        
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion smoothRotation = Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
    }

    private float Run()
    {
        float currentSpeed = input.runActionPressed ? runSpeed : walkSpeed;
        return currentSpeed;
    }
}


