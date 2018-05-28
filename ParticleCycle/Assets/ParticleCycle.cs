using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCycle : MonoBehaviour {

    // Use this for initialization
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particlesArray;
    private float[] athtas;
    private float[] radiuses;

    public int seaResolution = 50;
    public float spacing = 0.25f;
    public float noiseScale = 0.2f;
    public float heightScale = 3f;
    private float perlinNoiseAnimX = 0.01f;
    private float perlinNoiseAnimY = 0.01f;
    public Gradient colorGradient;
    public float minRadius = 3.0f;
    public float maxRadius = 6.0f;

    void Start()
    {
        int number = seaResolution * seaResolution;
        particlesArray = new ParticleSystem.Particle[number];
        athtas = new float[number];
        radiuses = new float[number];
        GetParticles();
        var main = particleSystem.main;
        main.maxParticles = seaResolution * seaResolution;
        particleSystem.Emit(seaResolution * seaResolution);
        particleSystem.GetParticles(particlesArray);
    }
    
    void GetParticles()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                float radius = Random.Range(minRadius, maxRadius);
                float zPos = Mathf.PerlinNoise(i * noiseScale, j * noiseScale) * heightScale;
                float athta = Random.Range(0.0f, 360.0f) / 180f * Mathf.PI;
                int k = i * seaResolution + j;
                athtas[k] = athta;
                radiuses[k] = radius;
                particlesArray[k].color = colorGradient.Evaluate(zPos);
                particlesArray[k].position = new Vector3(radius * Mathf.Cos(athta),radius * Mathf.Sin(athta),zPos);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {
                int k = i * seaResolution + j;
                float zPos = Mathf.PerlinNoise(i * noiseScale + perlinNoiseAnimX, j * noiseScale + perlinNoiseAnimY) * heightScale;
                particlesArray[k].color = colorGradient.Evaluate(zPos);
                athtas[k] += 0.1f * Mathf.PI * Time.deltaTime;
                athtas[k] = athtas[k] > 2 * Mathf.PI ? athtas[k] - 2 * Mathf.PI : athtas[k];
                particlesArray[k].position = new Vector3(radiuses[k] * Mathf.Cos(athtas[k]),radiuses[k] * Mathf.Sin(athtas[k]),zPos);
            }
        }

        perlinNoiseAnimX += 0.01f;
        perlinNoiseAnimY += 0.01f;

        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }
}