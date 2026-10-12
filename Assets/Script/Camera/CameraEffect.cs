using System;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    //--------VARIABLE GLOBAL----------//
    private float runOffset = 1.5f;
    private float turnOffset = 1f;
    private float transitionSpeed = 3f;
    
    //---------VARIABLE MOTEUR------------//
    public PlayerInputReader input;
    public Transform cameraTransform;

    private Vector3 initialPos;
    public void Awake()
    {
        input = GetComponent<PlayerInputReader>();
        cameraTransform = transform;
        
        initialPos = cameraTransform.localPosition;
    }

    private void Update()
    {
        RunEffect();
    }

    public void RunEffect()
    {
        Vector2 dir = input.Move;
        
        Vector3 targetPos = initialPos;

        if (input.runActionPressed)
        {
            targetPos.z -= runOffset;
            if ( dir.x > 0.3f)
            {
                targetPos.x -= turnOffset;
            
            }
            else if (dir.x < -0.3f){
                targetPos.x += turnOffset;
         
            }
        }
        

        cameraTransform.localPosition =
            Vector3.Lerp(cameraTransform.localPosition, targetPos, transitionSpeed * Time.deltaTime);

       
    }
}
