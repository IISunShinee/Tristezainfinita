using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    public int da�o = 10;
    public float tiempoEntreGolpes = 2f;
    private float proximoGolpe = 0f;

    public AudioClip sonidoPasos;   // Sonido que se reproduce cuando camina
    public AudioClip sonidoMuerte;  // Sonido que se reproduce al morir
    private AudioSource audioSource;
    private bool caminando = false;

    private Animator animator;

    void Start()
    {
        vidaActual = vidaMaxima;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Simulaci�n de caminar
        if (EstaCaminando())  // M�todo para determinar si el enemigo est� caminando
        {
            if (!caminando)
            {
                audioSource.clip = sonidoPasos;
                audioSource.loop = true;  // Repetir sonido mientras camina
                audioSource.Play();
                caminando = true;
            }
        }
        else
        {
            if (caminando)
            {
                audioSource.Stop();  // Detener sonido de pasos
                caminando = false;
            }
        }
    }

    // M�todo para que el enemigo reciba da�o
    public void RecibirDa�o(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibi� da�o. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("Enemigo ha muerto.");
        audioSource.PlayOneShot(sonidoMuerte);  // Reproducir sonido de muerte
        Destroy(gameObject, 2f);  // Destruir despu�s de 2 segundos
    }

    private bool EstaCaminando()
    {
        return GetComponent<NavMeshAgent>().velocity.magnitude > 0.1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= proximoGolpe)
            {
                other.GetComponent<Jugador>().TomarDa�o(da�o);
                proximoGolpe = Time.time + tiempoEntreGolpes;
            }
        }
    }
}
