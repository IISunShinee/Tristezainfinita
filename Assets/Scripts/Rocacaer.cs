using UnityEngine;

public class RockFall : MonoBehaviour
{
    public GameObject rock; // Asigna tu roca aqu� en el Inspector
    public AudioClip crashSound; // Asigna el sonido de estruendo aqu� en el Inspector
    private AudioSource audioSource;

    public float groundHeight = 0f; // Define la altura a la que quieres teletransportar la roca
    private bool hasFallen = false; // Evita que la roca se teletransporte m�ltiples veces

    void Start()
    {
        // Obtiene el componente AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();

        // Aseg�rate de que la roca est� en su posici�n inicial
        // Aseg�rate de que no cambie su posici�n aqu�
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger activado por: " + other.name); // Muestra qu� objeto activ� el trigger

        if (other.CompareTag("Player") && !hasFallen)
        {
            hasFallen = true; // Evita que se teletransporte m�ltiples veces

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
                Debug.LogError("AudioSource o crashSound no est�n asignados.");
            }
        }
    }
}
