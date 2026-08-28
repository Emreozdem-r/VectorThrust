using UnityEngine;

public class MissileTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] missiles;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < missiles.Length; i++)
            {
                if (missiles[i]) missiles[i].SetActive(true);
            }
        }
    }
}
