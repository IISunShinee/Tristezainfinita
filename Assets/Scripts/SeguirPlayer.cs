using UnityEngine;
using UnityEngine.AI;

public class EnemigoNavMeshSeguir : MonoBehaviour
{
    public Transform jugador; // El objetivo que el enemigo sigue
    private NavMeshAgent agente;
    public float rangoDeteccion = 10f;   // Radio en el que el enemigo detecta al jugador
    public float distanciaMinima = 1.5f; // Distancia m�nima para detenerse

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (jugador != null)
        {
            // Calcular la distancia al jugador
            float distancia = Vector3.Distance(transform.position, jugador.position);

            // Si el jugador est� dentro del rango de detecci�n
            if (distancia < rangoDeteccion && distancia > distanciaMinima)
            {
                // Mover al enemigo hacia el jugador
                agente.SetDestination(jugador.position);
            }
            // Si est� fuera del rango de detecci�n o muy cerca, detener al enemigo
            else if (distancia >= rangoDeteccion || distancia <= distanciaMinima)
            {
                agente.ResetPath(); // Detener al enemigo
            }
        }
    }
}