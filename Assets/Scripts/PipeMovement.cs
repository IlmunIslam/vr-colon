using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public  float speed = 10f;
    public float speed2 = 10f;
    Vector3 m_EulerAngleVelocityL, m_EulerAngleVelocityR, m_EulerAngleVelocityT, m_EulerAngleVelocityB;
  
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_EulerAngleVelocityR = new Vector3(0, speed2, 0);
        m_EulerAngleVelocityL = new Vector3(0, -speed2, 0);
        m_EulerAngleVelocityT = new Vector3(speed2, 0, 0);
        m_EulerAngleVelocityB = new Vector3(-speed2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation1 = Quaternion.Euler(m_EulerAngleVelocityR * Time.fixedDeltaTime);
        Quaternion deltaRotation2 = Quaternion.Euler(m_EulerAngleVelocityL * Time.fixedDeltaTime);
        Quaternion deltaRotationT = Quaternion.Euler(m_EulerAngleVelocityT * Time.fixedDeltaTime);
        Quaternion deltaRotationB = Quaternion.Euler(m_EulerAngleVelocityB * Time.fixedDeltaTime);


        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MoveRotation(rb.rotation * deltaRotation1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MoveRotation(rb.rotation * deltaRotation2);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            rb.MoveRotation(rb.rotation * deltaRotationT);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            rb.MoveRotation(rb.rotation * deltaRotationB);
        }
    }
}