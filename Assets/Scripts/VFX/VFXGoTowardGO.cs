using UnityEngine;

public class VFXGoTowardGO : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystemComponent;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private float lerpSpeed = 5f;
    [SerializeField]
    private float destroyDistanceThreshold = 0.1f;

    private ParticleSystem.Particle[] particles;
    private Vector3 targetPosition;

    private void Start()
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

    private void Update()
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
            Vector3 directionToTarget = (targetPosition - particles[i].position).normalized;

            float distanceToTarget = Vector3.Distance(particles[i].position, targetPosition);

            float lerpingDistance = Mathf.Min(lerpSpeed * Time.deltaTime, distanceToTarget);

            particles[i].position += directionToTarget * lerpingDistance;

            if (distanceToTarget < destroyDistanceThreshold)
            {
                // Remove the particle from the system
                particles[i].remainingLifetime = -1f;
            }
        }
        particleSystemComponent.SetParticles(particles, numParticlesAlive);
    }
}
