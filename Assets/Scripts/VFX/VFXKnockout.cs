using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXKnockout : MonoBehaviour
{
    [SerializeField] private float floatValue = 0.0f; // Serialized float parameter

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        // Get the material attached to the object
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AdjustFloatParameter(floatValue);
    }

    // Function to adjust the "_Float" reference in the material
    public void AdjustFloatParameter(float floatValue)
    {
        if (material != null)
        {
            // Adjust the "_Float" reference in the material using the serialized float value
            material.SetFloat("_Float", floatValue);
        }
        else
        {
            Debug.LogError("Material not found!");
        }
    }
}
