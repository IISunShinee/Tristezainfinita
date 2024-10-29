using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    public float hincharEscala = 1.2f; // Cuánto crece el enemigo al recibir daño
    public float radioExplosión = 5f;  // Radio en el que la explosión hará daño a otros enemigos
    public float dañoExplosión = 50f;  // Cantidad de daño que la explosión inflige
    public AudioClip sonidoExplosión;  // Sonido que se reproduce al explotar
    public LayerMask capaEnemigos;     // Capa de los enemigos para detectar a los cercanos

    private AudioSource audioSource;
    private Vector3 escalaOriginal;

    void Start()
    {
        vidaActual = vidaMaxima;
        escalaOriginal = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    // Método para recibir daño
    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;

        // Hinchamos el sprite del enemigo
        transform.localScale = escalaOriginal * hincharEscala;

        // Si la vida es menor o igual a 0, el enemigo muere
        if (vidaActual <= 0)
        {
            StartCoroutine(Explotar());
        }
    }

    // Corrutina para manejar la explosión del enemigo
    IEnumerator Explotar()
    {
        // Reproducir el sonido de explosión
        if (sonidoExplosión != null)
        {
            audioSource.PlayOneShot(sonidoExplosión);
        }

        // Detectar enemigos cercanos y hacerles daño
        Collider[] enemigosCercanos = Physics.OverlapSphere(transform.position, radioExplosión, capaEnemigos);
        foreach (Collider enemigo in enemigosCercanos)
        {
            EnemyBehavior enemigoCercano = enemigo.GetComponent<EnemyBehavior>();
            if (enemigoCercano != null && enemigoCercano != this)
            {
                enemigoCercano.RecibirDaño(dañoExplosión);
            }
        }

        // Destruir el enemigo después de un breve retardo
        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(gameObject);
    }
}





