using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public bool useMouse = true;
    float turnAmount = 10f;
    float wingForce = 0.09f;

    Rigidbody rb;

    float throttle = 0f;
    public ParticleSystem ps;

    public GameObject[] wings;
    public bool[] roll;
    public bool[] pitch;
    public bool[] yaw;
    public bool[] invertroll;
    public bool[] invertpitch;
    public bool[] invertyaw;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Get the center of the screen in pixel coordinates
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        // Get the current mouse position
        Vector2 mousePosition = Input.mousePosition;

        // Calculate the mouse position relative to the center of the screen
        Vector2 mousePositionRelativeToCenter = mousePosition - screenCenter;
        //Normalized
        Vector2 mouse = new Vector2(mousePositionRelativeToCenter.x / screenCenter.x, mousePositionRelativeToCenter.y / screenCenter.y);


        //get roll amount
        float rollAmount = Mathf.DeltaAngle(transform.eulerAngles.z, 0f) * 0.0004f; // Adjust the turnAmount as needed



        if (Input.GetKey(KeyCode.Space))
        {
            throttle = 1f;
            ps.Play();
        }
        else
        {
            throttle = 0;
            ps.Stop();
        }
        Debug.Log(throttle);

        rb.AddForce(transform.up * throttle * 20f);

        for (int i = 0; i < wings.Length; i++)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0); // roll, pitch, yaw
            if (Input.GetKey("e")) input.z = 1;
            else input.z = -1;

            if (useMouse) input = new Vector3(-rollAmount, -mouse.y, mouse.x);
            if (invertpitch[i]) input.y *= -1;
            if (invertroll[i]) input.x *= -1;
            if (invertyaw[i]) input.z *= -1;

            input *= turnAmount;
            if (roll[i] && pitch[i]) wings[i].transform.localEulerAngles = new Vector3(input.x + input.y, 0, 0);
            else if (roll[i]) wings[i].transform.localEulerAngles = new Vector3(input.x, 0, 0);
            else if (pitch[i]) wings[i].transform.localEulerAngles = new Vector3(input.y, 0, 0);

            if (yaw[i]) wings[i].transform.localEulerAngles = new Vector3(input.z, 0, 0);
            


            Vector3 forceDir = wings[i].transform.InverseTransformDirection(rb.velocity).z * wings[i].transform.forward * -1f * wingForce;
            Debug.DrawLine(wings[i].transform.position, wings[i].transform.position + forceDir * 10, Color.red);

            rb.AddForceAtPosition(forceDir, wings[i].transform.position);
        }
    }
}
