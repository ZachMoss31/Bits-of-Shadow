using UnityEngine;

public class ActivateDeactivate : MonoBehaviour
{
    public GameObject gameObject;

    public void ActivateObject()
    {
        gameObject.gameObject.SetActive(true);
    }

    public void DeactivateObject()
    {
        gameObject.gameObject.SetActive(false);
    }
}
