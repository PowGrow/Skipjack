using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    private Rigidbody Rigidbody; 
    void FixedUpdate()
    {
        //transform.Translate(new Vector3(0, -1, -1) * Speed * Time.deltaTime, Space.World);
        //transform.Rotate(new Vector3(-1, 0, 0) * RotationSpeed * Time.deltaTime);
        Rigidbody.AddForce(new Vector3(0, -1, -1) * Rigidbody.mass * Speed);
    }
}
