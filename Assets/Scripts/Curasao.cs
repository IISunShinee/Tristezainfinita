using UnityEngine;
using UnityEngine.UI; // Aseg�rate de incluir este espacio de nombres
using TMPro; // Aseg�rate de incluir este espacio de nombres para TextMesh Pro

public class PlayerHealing : MonoBehaviour
{
    public float cantidadCuracion = 20f; // Cantidad de salud que se recupera
    public float tiempoRecargaCuracion = 10f; // Tiempo de recarga en segundos
    public AudioClip sonidoCuracion; // Sonido a reproducir cuando el jugador se cura
    public AudioClip sonidoCuracionLista; // Sonido a reproducir cuando la curaci�n est� lista
    private AudioSource audioSource;

    public Slider barraSalud; // Asigna tu Slider de salud aqu� en el Inspector
    public TextMeshProUGUI textoCuracionLista; // Asigna tu TextMesh Pro aqu� en el Inspector

    private Jugador jugador; // Referencia al script de Jugador
    private bool estaCurando = false; // Controla si el jugador puede curarse

    void Start()
    {
        jugador = GetComponent<Jugador>(); // Obtiene la referencia al script de Jugador
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        ActualizarBarraSalud(); // Actualiza la barra de salud al inicio
        textoCuracionLista.gameObject.SetActive(false); // Aseg�rate de que el texto est� desactivado al inicio
    }

    void Update()
    {
        // Comprueba si se presiona la tecla F
        if (Input.GetKeyDown(KeyCode.F) && !estaCurando)
        {
            Curar();
        }
    }

    void Curar()
    {
        // Comprueba si la salud actual es menor que la salud m�xima
        if (jugador.vidaActual < jugador.vidaMaxima)
        {
            jugador.vidaActual += cantidadCuracion; // Aumenta la salud
            jugador.vidaActual = Mathf.Clamp(jugador.vidaActual, 0, jugador.vidaMaxima); // Aseg�rate de que no exceda la salud m�xima
            Debug.Log("Salud actual: " + jugador.vidaActual);

            ActualizarBarraSalud(); // Actualiza la barra de salud

            // Reproduce el sonido de curaci�n
            if (audioSource != null && sonidoCuracion != null)
            {
                audioSource.PlayOneShot(sonidoCuracion); // Reproduce el sonido de curaci�n
            }

            // Comienza el tiempo de recarga
            estaCurando = true;
            Invoke("ReiniciarCuracion", tiempoRecargaCuracion); // Reinicia el estado de curaci�n despu�s del tiempo de recarga
        }
        else
        {
            Debug.Log("Salud completa. No se puede curar.");
        }
    }

    void ReiniciarCuracion()
    {
        estaCurando = false; // Permite que el jugador se cure nuevamente

        // Reproducir el sonido de recarga
        if (audioSource != null && sonidoCuracionLista != null)
        {
            audioSource.PlayOneShot(sonidoCuracionLista);
        }

        // Muestra el texto de curaci�n lista
        textoCuracionLista.gameObject.SetActive(true);
        textoCuracionLista.text = "�Curaci�n lista!";

        // Desactiva el texto despu�s de un tiempo
        Invoke("OcultarTextoCuracion", 2f); // Oculta el texto despu�s de 2 segundos
    }

    void OcultarTextoCuracion()
    {
        textoCuracionLista.gameObject.SetActive(false); // Desactiva el texto
    }

    void ActualizarBarraSalud()
    {
        if (barraSalud != null)
        {
            barraSalud.value = (float)jugador.vidaActual / jugador.vidaMaxima; // Aseg�rate de que sea float
        }
    }
}



