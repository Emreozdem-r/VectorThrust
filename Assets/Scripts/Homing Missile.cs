using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Transform player;
    [SerializeField] AudioClip launchSFX;
    [SerializeField] float lifeTime = 0f;

    Vector3 playerPosition;
    CollisionHandler handler;
    AudioSource audioSource;

    void Awake()
    {
        gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 1. ÖNCE ROKETÝ BUL:
        if (player == null)
        {
            GameObject target = GameObject.Find("Player Rocket");
            if (target != null)
            {
                player = target.transform;
            }
        }

        // 2. ROKET BULUNDUYSA BÝLEÞENÝNÝ AL:
        if (player != null)
        {
            handler = player.GetComponent<CollisionHandler>();
        }
    }

    void OnEnable()
    {
        if (audioSource && launchSFX)
        {
            audioSource.PlayOneShot(launchSFX);
        }
        if (lifeTime > 0f)
        {
            Destroy(gameObject, lifeTime);
        }
    }

    void Update()
    {
        // GÜVENLÝK: Eðer roket bir þekilde henüz bulunamadýysa (veya öldüyse) hata fýrlatma, çýk
        if (player == null)
        {
            GameObject target = GameObject.Find("Player Rocket");
            if (target != null)
            {
                player = target.transform;
                handler = player.GetComponent<CollisionHandler>();
            }
            return;
        }

        playerPosition = player.position;
        MoveToPlayer();
        DestroyWhenReached();
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    void MoveToPlayer()
    {
        transform.LookAt(player);
        transform.Rotate(0, 180, 0);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
    }

    void DestroyWhenReached()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.position) < 0.5f)
        {
            if (handler != null && handler.GetControllable())
            {
                handler.StartCrashSequence();
                Destroy(gameObject);
            }
        }
    }
}