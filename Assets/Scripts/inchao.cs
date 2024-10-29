using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaActual;
    public float hincharEscala = 1.2f; // Cu�nto crece el enemigo al recibir da�o
    public float radioExplosi�n = 5f;  // Radio en el que la explosi�n har� da�o a otros enemigos
    public float da�oExplosi�n = 50f;  // Cantidad de da�o que la explosi�n inflige
    public AudioClip sonidoExplosi�n;  // Sonido que se reproduce al explotar
    public LayerMask capaEnemigos;     // Capa de los enemigos para detectar a los cercanos

    private AudioSource audioSource;
    private Vector3 escalaOriginal;

    void Start()
    {
        vidaActual = vidaMaxima;
        escalaOriginal = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo para recibir da�o
    public void RecibirDa�o(float cantidad)
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

    // Corrutina para manejar la explosi�n del enemigo
    IEnumerator Explotar()
    {
        // Reproducir el sonido de explosi�n
        if (sonidoExplosi�n != null)
        {
            audioSource.PlayOneShot(sonidoExplosi�n);
        }

        // Detectar enemigos cercanos y hacerles da�o
        Collider[] enemigosCercanos = Physics.OverlapSphere(transform.position, radioExplosi�n, capaEnemigos);
        foreach (Collider enemigo in enemigosCercanos)
        {
            EnemyBehavior enemigoCercano = enemigo.GetComponent<EnemyBehavior>();
            if (enemigoCercano != null && enemigoCercano != this)
            {
                enemigoCercano.RecibirDa�o(da�oExplosi�n);
            }
        }

        // Destruir el enemigo despu�s de un breve retardo
        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(gameObject);
    }
}





