using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    public GameObject wheelPrefab;
    public Transform[] wheelPositions;
    private Vector3[] wheelRotations;
    private float wheelHeight = -2f;


    private float maxSpeed = 180f;
    private float acceleration = 40f;
    private float steeringSpeed = 80.0f;

    private float currentSpeed = 0.0f;


    private Vector2[] wheelPositions2D = new Vector2[]
    {
        new Vector2(4.5f, 6.5f),
        new Vector2(-4.5f, 6.5f),
        new Vector2(4.5f, -6.5f),
        new Vector2(-4.5f, -6.5f)
    };

    Rigidbody rb;
    private void Start()
    {
        for (int i = 0; i < wheelPositions2D.Length; i++) 
        {
            Vector3 wheelPosition = new Vector3(transform.position.x + wheelPositions2D[i].x,transform.position.y - wheelHeight,transform.position.z + wheelPositions2D[i].y); 
            GameObject wheel = Instantiate(wheelPrefab,wheelPosition, Quaternion.identity,transform);
            wheel.transform.Rotate(Vector3.up, -90.0f);
            wheel.transform.localScale *= 1.5f;
        }
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MoveSystem();
    }
    public void MoveSystem()
    {
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");
        currentSpeed = Mathf.Clamp(currentSpeed + inputVertical * acceleration * Time.fixedDeltaTime, -maxSpeed, maxSpeed);
        float rotation = inputHorizontal * steeringSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, rotation);
        transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);
    }

}
