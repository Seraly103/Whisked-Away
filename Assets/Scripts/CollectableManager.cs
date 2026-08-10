using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public void Collect()
    {
        Debug.Log(gameObject.name + " collected!");

        Destroy(gameObject);
    }
}
