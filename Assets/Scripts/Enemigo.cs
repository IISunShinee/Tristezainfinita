using UnityEngine;

public class InflatableEnemy : MonoBehaviour
{
    public float maxHealth = 100f;              // Salud m�xima del enemigo
    public float explosionDamage = 20f;         // Da�o que inflige al explotar
    public float explosionRadius = 5f;          // Radio de explosi�n
    public GameObject explosionEffect;          // Efecto de explosi�n
    public float damageToPlayer = 10f;          // Da�o al jugador al estar cerca
    public float followDistance = 10f;           // Distancia a la que empieza a seguir al jugador
    public float attackDistance = 2f;           // Distancia para atacar al jugador
    public float moveSpeed = 3f;                 // Velocidad de movimiento del enemigo

    private float currentHealth;                 // Salud actual del enemigo
    private bool isInflated = false;             // Indica si el enemigo est� inflado
    private Transform player;                     // Referencia al jugador

    void Start()
    {
        currentHealth = maxHealth;               // Inicializa la salud actual
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Verifica si el jugador est� dentro de la distancia de seguimiento
            if (distanceToPlayer < followDistance)
            {
                FollowPlayer(distanceToPlayer);
            }
        }
    }

    void FollowPlayer(float distanceToPlayer)
    {
        // Mueve al enemigo hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Comprueba si est� cerca del jugador para hacer da�o
        if (distanceToPlayer < attackDistance)
        {
            DealDamageToPlayer();
        }
    }

    void DealDamageToPlayer()
    {
        // Aqu� debes acceder al script del jugador para aplicar el da�o
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Aseg�rate de tener este script en tu jugador
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageToPlayer); // Llama al m�todo para hacer da�o al jugador
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es una galleta
        if (other.CompareTag("Cookie"))
        {
            Inflate();                            // Infla al enemigo
            Destroy(other.gameObject);           // Destruye la galleta
        }
    }

    void Inflate()
    {
        if (!isInflated)
        {
            isInflated = true;                   // Cambia el estado a inflado
            transform.localScale *= 1.5f;        // Aumenta el tama�o del enemigo
            Invoke(nameof(Explode), 2f);          // Explota despu�s de 2 segundos
        }
    }

    void Explode()
    {
        // Crea el efecto de explosi�n
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Da�o a otros enemigos en el radio de explosi�n
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            InflatableEnemy enemy = hitCollider.GetComponent<InflatableEnemy>();
            if (enemy != null && enemy != this) // Aseg�rate de no da�ar al propio enemigo
            {
                enemy.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject); // Destruye el enemigo despu�s de explotar
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destruye el enemigo si la salud es 0 o menos
        }
    }
}