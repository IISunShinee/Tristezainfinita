using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir este espacio de nombres
using TMPro; // Asegúrate de incluir este espacio de nombres para TextMesh Pro

public class PlayerHealing : MonoBehaviour
{
    public float cantidadCuracion = 20f; // Cantidad de salud que se recupera
    public float tiempoRecargaCuracion = 10f; // Tiempo de recarga en segundos
    public AudioClip sonidoCuracion; // Sonido a reproducir cuando el jugador se cura
    public AudioClip sonidoCuracionLista; // Sonido a reproducir cuando la curación esté lista
    private AudioSource audioSource;

    public Slider barraSalud; // Asigna tu Slider de salud aquí en el Inspector
    public TextMeshProUGUI textoCuracionLista; // Asigna tu TextMesh Pro aquí en el Inspector

    private Jugador jugador; // Referencia al script de Jugador
    private bool estaCurando = false; // Controla si el jugador puede curarse

    void Start()
    {
        jugador = GetComponent<Jugador>(); // Obtiene la referencia al script de Jugador
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        ActualizarBarraSalud(); // Actualiza la barra de salud al inicio
        textoCuracionLista.gameObject.SetActive(false); // Asegúrate de que el texto esté desactivado al inicio
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
        // Comprueba si la salud actual es menor que la salud máxima
        if (jugador.vidaActual < jugador.vidaMaxima)
        {
            jugador.vidaActual += cantidadCuracion; // Aumenta la salud
            jugador.vidaActual = Mathf.Clamp(jugador.vidaActual, 0, jugador.vidaMaxima); // Asegúrate de que no exceda la salud máxima
            Debug.Log("Salud actual: " + jugador.vidaActual);

            ActualizarBarraSalud(); // Actualiza la barra de salud

            // Reproduce el sonido de curación
            if (audioSource != null && sonidoCuracion != null)
            {
                audioSource.PlayOneShot(sonidoCuracion); // Reproduce el sonido de curación
            }

            // Comienza el tiempo de recarga
            estaCurando = true;
            Invoke("ReiniciarCuracion", tiempoRecargaCuracion); // Reinicia el estado de curación después del tiempo de recarga
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

        // Muestra el texto de curación lista
        textoCuracionLista.gameObject.SetActive(true);
        textoCuracionLista.text = "¡Curación lista!";

        // Desactiva el texto después de un tiempo
        Invoke("OcultarTextoCuracion", 2f); // Oculta el texto después de 2 segundos
    }

    void OcultarTextoCuracion()
    {
        textoCuracionLista.gameObject.SetActive(false); // Desactiva el texto
    }

    void ActualizarBarraSalud()
    {
        if (barraSalud != null)
        {
            barraSalud.value = (float)jugador.vidaActual / jugador.vidaMaxima; // Asegúrate de que sea float
        }
    }
}



