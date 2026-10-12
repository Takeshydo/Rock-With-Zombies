using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //---------VARAIBLES GLOBAL-----------//
    public Transform anchor;
    [SerializeField] public Transform player;
    
    private float lookSensitivity = 0.2f;
    private float minAngle = -30f;
    private float maxAngle = 30f;

    private float recenterDelay = 1.3f;
    private float recenterSmoothTime = 0.15f;
    private bool recenterOnlyWhenMoving = true;
    private float timeSinceLastLook;
    private float yawVelocity = 5f;
    
    //-------VARIABLES MOTEUR---------//
    private PlayerInputReader input;

    public float yaw;
    public float pitch;


    private void Awake()
    {
        input = GetComponent<PlayerInputReader>();
        anchor = transform.parent;
    }

    void Start()
    {
        Vector3 angles = anchor.eulerAngles;
        
        yaw = angles.y;
        pitch = 0f;
    }

    private void Update()
    {
        //LookAround(input.Look, anchor);
        Center(input.Look);
    }


    private void LookAround(Vector2 dir, Transform target)
    {
        yaw += dir.x; // VITESSE trop haute, a réduire
        pitch -= dir.y;
        
        pitch = Mathf.Clamp(pitch, minAngle, maxAngle);
        
        target.rotation = Quaternion.Euler(pitch, yaw, 0f); // PROBLEME TROP SENSIBLE SUR LES AXES DU VECTOR2 PAS AGREABLE POUR TOURNER AUTOUR DU JOUEUR
        
    }

    private void Center(Vector2 look) //A étudier pour rendre ça plus Smooth etc... 
    {
        if (look.sqrMagnitude > 0.0001f)
        {
            yaw += look.x * lookSensitivity;
            pitch -= look.y * lookSensitivity;

            pitch = Mathf.Clamp(pitch, minAngle, maxAngle);

            timeSinceLastLook = 0f;
            yawVelocity = 0f;
        }
        else
        {
            timeSinceLastLook += Time.deltaTime;

            bool playerIsMoving = input.Move.sqrMagnitude > 0.01f;

            if (timeSinceLastLook >= recenterDelay &&
                (!recenterOnlyWhenMoving || playerIsMoving))
            {
                float targetYaw = player.eulerAngles.y;
                  
                yaw = Mathf.SmoothDampAngle(
                    yaw,
                    targetYaw,
                    ref yawVelocity,
                    recenterSmoothTime
                );
            }
        }
        // Rotation horizontale de l'anchor
        anchor.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
