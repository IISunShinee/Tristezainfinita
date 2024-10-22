using UnityEngine;

public class InflatableEnemy : MonoBehaviour
{
    public float maxHealth = 100f;              // Salud máxima del enemigo
    public float explosionDamage = 20f;         // Daño que inflige al explotar
    public float explosionRadius = 5f;          // Radio de explosión
    public GameObject explosionEffect;          // Efecto de explosión
    public float damageToPlayer = 10f;          // Daño al jugador al estar cerca
    public float followDistance = 10f;           // Distancia a la que empieza a seguir al jugador
    public float attackDistance = 2f;           // Distancia para atacar al jugador
    public float moveSpeed = 3f;                 // Velocidad de movimiento del enemigo

    private float currentHealth;                 // Salud actual del enemigo
    private bool isInflated = false;             // Indica si el enemigo está inflado
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

            // Verifica si el jugador está dentro de la distancia de seguimiento
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

        // Comprueba si está cerca del jugador para hacer daño
        if (distanceToPlayer < attackDistance)
        {
            DealDamageToPlayer();
        }
    }

    void DealDamageToPlayer()
    {
        // Aquí debes acceder al script del jugador para aplicar el daño
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Asegúrate de tener este script en tu jugador
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageToPlayer); // Llama al método para hacer daño al jugador
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
            transform.localScale *= 1.5f;        // Aumenta el tamaño del enemigo
            Invoke(nameof(Explode), 2f);          // Explota después de 2 segundos
        }
    }

    void Explode()
    {
        // Crea el efecto de explosión
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Daño a otros enemigos en el radio de explosión
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            InflatableEnemy enemy = hitCollider.GetComponent<InflatableEnemy>();
            if (enemy != null && enemy != this) // Asegúrate de no dañar al propio enemigo
            {
                enemy.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject); // Destruye el enemigo después de explotar
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