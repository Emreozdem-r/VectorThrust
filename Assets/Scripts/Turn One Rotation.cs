using UnityEngine;

public class TurnOneRotation : MonoBehaviour
{
    [SerializeField] float value;
    [SerializeField] Vector3 direction = Vector3.forward;
    [SerializeField] bool isRotate = true;
    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, value * Time.deltaTime, 0);
        }
        else
        {
            transform.position += direction.normalized * value * Time.deltaTime;
        }
    }
}