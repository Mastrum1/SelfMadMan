using UnityEngine;

public class VFXParticleSpin : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust this value to control the rotation speed
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Initialize array if needed
        if (particles == null || particles.Length < ps.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }

        int numParticlesAlive = ps.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Calculate perpendicular vector for rotation
            Vector3 perpendicular = new Vector3(particles[i].position.y, -particles[i].position.x, 0);

            // Calculate rotation angle based on rotation speed
            float rotationAngle = rotationSpeed * Time.deltaTime;

            // Rotate particle position around the center
            particles[i].position = Quaternion.AngleAxis(rotationAngle, perpendicular) * particles[i].position;
        }

        // Apply changes to the particle system
        ps.SetParticles(particles, numParticlesAlive);
    }
}
