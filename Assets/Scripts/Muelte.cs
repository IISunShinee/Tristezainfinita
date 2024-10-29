using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    public GameObject pantallaMuerte; // Panel de la pantalla de muerte
    public Button botonReiniciar;     // Botón para reiniciar el juego
    public Button botonSalir;         // Botón para salir del juego
    public AudioSource audioSource;   // AudioSource para reproducir sonidos
    public AudioClip sonidoMuerte;    // Sonido al morir
    public TMP_Text textoMuerte;      // Texto de muerte (opcional)

    private bool estaMuerto = false;

    // Inicializa los botones y oculta la pantalla de muerte
    void Start()
    {
        pantallaMuerte.SetActive(false);

        // Asigna las funciones a los botones
        botonReiniciar.onClick.AddListener(JugarOtraVez);
        botonSalir.onClick.AddListener(SalirDelJuego);
    }

    void Update()
    {
        // Simular la muerte del jugador al presionar "K"
        if (Input.GetKeyDown(KeyCode.K) && !estaMuerto)
        {
            MatarJugador();  // Aquí llamas al método MatarJugador()
        }
    }

    // Método para manejar la muerte del jugador
    public void MatarJugador()
    {
        if (!estaMuerto)
        {
            estaMuerto = true;
            pantallaMuerte.SetActive(true);

            // Reproduce el sonido de muerte
            if (audioSource != null && sonidoMuerte != null)
            {
                audioSource.PlayOneShot(sonidoMuerte);
            }

            // Detiene el tiempo del juego
            Time.timeScale = 0f;

            // Muestra el texto de muerte (opcional)
            if (textoMuerte != null)
            {
                textoMuerte.text = "¡Has muerto!";
            }
        }
    }

    // Método para reiniciar el juego
    public void JugarOtraVez()
    {
        Time.timeScale = 1f; // Reinicia el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {
        Application.Quit(); // Sale del juego (solo funciona en una compilación)
    }
}

