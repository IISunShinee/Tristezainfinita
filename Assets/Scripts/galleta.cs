using UnityEngine;

public class Cookie : MonoBehaviour
{
    public float damage; // Da�o que causa la galleta
    public float lifetime = 5f; // Tiempo de vida de la galleta antes de desaparecer

    private void Start()
    {
        // Destruir la galleta despu�s de 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la galleta ha colisionado con un enemigo
        if (other.CompareTag("Enemigo"))
        {
            // Obtener el componente Enemigo y aplicar da�o
            other.GetComponent<Enemigo>().RecibirDa�o((int)damage);
            // Destruir la galleta despu�s de hacer da�o
            Destroy(gameObject);
        }
        // Opcional: Destruir la galleta si colisiona con el suelo u otros objetos
        else if (other.CompareTag("Suelo")) // Aseg�rate de que el suelo tenga este tag
        {
            Destroy(gameObject); // Destruye la galleta al tocar el suelo
        }
    }
}