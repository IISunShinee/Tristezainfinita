using UnityEngine;
using UnityEngine.AI;

public class EnemigoNavMeshSeguir : MonoBehaviour
{
    public Transform jugador; // El transform del jugador
    private NavMeshAgent agente; // El componente NavMeshAgent
    public float rangoDeteccion = 10f;   // Distancia en la que el enemigo detecta al jugador
    public float distanciaMinima = 1.5f; // Distancia m�nima para detenerse

    void Start()
    {
        // Obtener el componente NavMeshAgent
        agente = GetComponent<NavMeshAgent>();

        // Buscar el objeto del jugador en la escena
        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Aseg�rate de que el jugador tenga la etiqueta "Player"
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
            else if (distancia <= distanciaMinima)
            {
                agente.ResetPath(); // Detener al enemigo
            }
        }
    }
}


