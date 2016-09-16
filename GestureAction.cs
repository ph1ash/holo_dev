using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

/// <summary>
/// GestureAction performs custom actions based on 
/// which gesture is being performed.
/// </summary>
public class GestureAction : MonoBehaviour
{
    [Tooltip("Rotation max speed controls amount of rotation.")]
    public float RotationSensitivity = 10.0f;

    [Tooltip("Enable rotation around x axis")]
    public bool xAxisRotationEnable = false;
    [Tooltip("Enable rotation around y axis")]
    public bool yAxisRotationEnable = false;
    [Tooltip("Enable rotation around z axis")]
    public bool zAxisRotationEnable = false;

    [Tooltip("+/- max rotation around x axis")]
    public double xRotationLimit = 25.0f;
    [Tooltip("+/- max rotation around y axis")]
    public double yRotationLimit = 0;
    [Tooltip("+/- max rotation around z axis")]
    public double zRotationLimit = 0;

    private Vector3 manipulationPreviousPosition;

    private float rotationFactor;
    
    private Text throttle_text;
    private Text side_throttle_text;
    
    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Do a raycast into the world based on the user's
    // head position and orientation.
    private Vector3 headPosition;
    private Vector3 gazeDirection;

    void Start()
    {
        headPosition = Camera.main.transform.position;
        gazeDirection = Camera.main.transform.forward; 

        throttle_text = GameObject.FindGameObjectWithTag("flightStatus").GetComponent<Text>();
        side_throttle_text = GameObject.FindGameObjectWithTag("ThrottleIndicator").GetComponent<Text>();
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                bool colliderHit = (FocusedObject) ? true : false;
                if (throttle_text != null)
                {
                    throttle_text.text += colliderHit.ToString() + "\n";
                }
            }
        };
    }

    void Update()
    {
        PerformRotation();
        GameObject throttle_object = gameObject;
        int throttle_rotation = (int) (Math.Floor((100.0f / 90.0f) * throttle_object.transform.localEulerAngles.x));
        if (throttle_text != null)
        {
            throttle_text.text = throttle_rotation.ToString() + "%\n" + (throttle_object.transform.localEulerAngles.x).ToString() + "\n";
        }
        if (side_throttle_text != null)
        {
            side_throttle_text.text = throttle_rotation.ToString() + "%\n";
        }
    }

    private void PerformRotation()
    {
        bool tagCompareResult;
        
        // Check to see that the object where navigation started is the one that we're looking for.
        if (GestureManager.Instance.ObjectStartedOn != null)
            tagCompareResult = GestureManager.Instance.ObjectStartedOn.CompareTag("ThrottleAssembly");
        else
            tagCompareResult = false;

        if (GestureManager.Instance.IsNavigating && tagCompareResult)
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.c */

            // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X and multiply by RotationSensitivity.
            // This will help control the amount of rotation.
            rotationFactor = GestureManager.Instance.NavigationPosition.x * RotationSensitivity;
            throttle_text.text += GestureManager.Instance.NavigationPosition.x;
            Debug.Log("y axis = " + GestureManager.Instance.NavigationPosition.y);
            Debug.Log("z axis = " + GestureManager.Instance.NavigationPosition.z);

            int xEnableFactor = (xAxisRotationEnable) ? 1 : 0;
            int yEnableFactor = (yAxisRotationEnable) ? 1 : 0;
            int zEnableFactor = (zAxisRotationEnable) ? 1 : 0;

            // Set boundaries for rotation in both positive and negative directions


            transform.localEulerAngles = new Vector3(
                Mathf.Clamp(transform.localEulerAngles.x + (-1 * xEnableFactor * rotationFactor),0f,90f),
                transform.localEulerAngles.y,
                transform.localEulerAngles.z
                );
            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            /*transform.Rotate(new Vector3(
                (-1 * xEnableFactor * rotationFactor),
                (-1 * yEnableFactor * rotationFactor), 
                (-1 * zEnableFactor * rotationFactor)));*/

        }
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsManipulating)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 moveVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.

            // 4.a: Update the manipulationPreviousPosition with the current position.


            // 4.a: Increment this transform's position by the moveVector.

        }
    }
}