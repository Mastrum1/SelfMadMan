using UnityEngine;

public class ParticleLerper : MonoBehaviour
{
    public ParticleSystem particleSystemComponent; // Reference to the Particle System
    public GameObject targetObject; // Target object to lerp particles towards
    public float lerpSpeed = 5f; // Lerp speed of particles
    public float destroyDistanceThreshold = 0.1f; // Distance threshold to destroy particles

    private ParticleSystem.Particle[] particles;
    private Vector3 targetPosition;

    void Start()
    {
        // Ensure the target object is assigned
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned.");
            return;
        }

        // Get the world position of the target object
        targetPosition = targetObject.transform.position;
    }

    void Update()
    {
        // Ensure the particle system component is assigned
        if (particleSystemComponent == null)
        {
            Debug.LogError("Particle System component is not assigned.");
            return;
        }

        // Get the particles from the particle system
        int maxParticles = particleSystemComponent.main.maxParticles;
        particles = new ParticleSystem.Particle[maxParticles];
        int numParticlesAlive = particleSystemComponent.GetParticles(particles);

        // Lerp each alive particle towards the target position
        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Calculate the direction from the particle to the target position
            Vector3 directionToTarget = (targetPosition - particles[i].position).normalized;

            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(particles[i].position, targetPosition);

            // Determine the lerping distance based on the lerping speed and the distance to the target
            float lerpingDistance = Mathf.Min(lerpSpeed * Time.deltaTime, distanceToTarget);

            // Update the particle position using the lerping distance
            particles[i].position += directionToTarget * lerpingDistance;

            // Check if the particle is close enough to the target to destroy it
            if (distanceToTarget < destroyDistanceThreshold)
            {
                // Set particle position to the target position and pause the particle system
                particles[i].position = targetPosition;
                particleSystemComponent.Pause();
            }
        }

        // Set the modified particles back to the particle system
        particleSystemComponent.SetParticles(particles, numParticlesAlive);
    }
}
