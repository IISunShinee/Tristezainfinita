using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    public int daño = 10;
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
        // Simulación de caminar
        if (EstaCaminando())  // Método para determinar si el enemigo está caminando
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

    // Método para que el enemigo reciba daño
    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibió daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("Enemigo ha muerto.");
        audioSource.PlayOneShot(sonidoMuerte);  // Reproducir sonido de muerte
        Destroy(gameObject, 2f);  // Destruir después de 2 segundos
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
                other.GetComponent<Jugador>().TomarDaño(daño);
                proximoGolpe = Time.time + tiempoEntreGolpes;
            }
        }
    }
}
