using UnityEngine;
using System.Collections;
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    [SerializeField] float startDelay;
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    bool canMove = false;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
        StartCoroutine(StartAfterDelay());
    }
    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canMove = true;
    }

    void Update()
    {
        if(!canMove){ return; }
        movementFactor = Mathf.PingPong(Time.time * speed,1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
