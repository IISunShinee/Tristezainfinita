using UnityEngine;

public class RockFall : MonoBehaviour
{
    public GameObject rock; // Asigna tu roca aquí en el Inspector
    public AudioClip crashSound; // Asigna el sonido de estruendo aquí en el Inspector
    private AudioSource audioSource;

    public float groundHeight = 0f; // Define la altura a la que quieres teletransportar la roca
    private bool hasFallen = false; // Evita que la roca se teletransporte múltiples veces

    void Start()
    {
        // Obtiene el componente AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que la roca esté en su posición inicial
        // Asegúrate de que no cambie su posición aquí
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger activado por: " + other.name); // Muestra qué objeto activó el trigger

        if (other.CompareTag("Player") && !hasFallen)
        {
            hasFallen = true; // Evita que se teletransporte múltiples veces

            // Teletransportar la roca a la altura del suelo
            rock.transform.position = new Vector3(rock.transform.position.x, groundHeight, rock.transform.position.z);

            // Reproducir el sonido
            if (audioSource != null && crashSound != null)
            {
                Debug.Log("Reproduciendo sonido...");
                audioSource.PlayOneShot(crashSound);
            }
            else
            {
                Debug.LogError("AudioSource o crashSound no están asignados.");
            }
        }
    }
}
