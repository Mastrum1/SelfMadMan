using UnityEngine;

public class VFXStarLerp : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToActivate;

    // Function to activate specified number of objects
    public void ActivateObjects(int count)
    {
        // Activate objects up to count or the length of the array, whichever is smaller
        for (int i = 0; i < Mathf.Min(count, objectsToActivate.Length); i++)
        {
            if (objectsToActivate[i] != null)
            {
                objectsToActivate[i].SetActive(true);
            }
        }
    }
}
