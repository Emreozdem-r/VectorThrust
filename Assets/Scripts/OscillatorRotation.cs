using UnityEngine;

public class OscillatorRotation : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector;  // hangi eksenlerde döneceğini belirle
    [SerializeField] float speed = 1f;

    Quaternion startRotation;
    Quaternion endRotation;
    float rotationFactor;

    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(transform.eulerAngles + rotationVector);
    }

    void Update()
    {
        rotationFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationFactor);
    }
}
