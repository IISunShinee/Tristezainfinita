using UnityEngine;

public class Cookie : MonoBehaviour
{
    public float damage; // Daño que causa la galleta
    public float lifetime = 5f; // Tiempo de vida de la galleta antes de desaparecer

    private void Start()
    {
        // Destruir la galleta después de 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la galleta ha colisionado con un enemigo
        if (other.CompareTag("Enemigo"))
        {
            // Obtener el componente Enemigo y aplicar daño
            other.GetComponent<Enemigo>().RecibirDaño((int)damage);
            // Destruir la galleta después de hacer daño
            Destroy(gameObject);
        }
        // Opcional: Destruir la galleta si colisiona con el suelo u otros objetos
        else if (other.CompareTag("Suelo")) // Asegúrate de que el suelo tenga este tag
        {
            Destroy(gameObject); // Destruye la galleta al tocar el suelo
        }
    }
}