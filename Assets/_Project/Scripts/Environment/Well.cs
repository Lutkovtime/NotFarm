using UnityEngine;

public class Well : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bucket bucket = other.GetComponentInChildren<Bucket>();
        if (bucket != null && !bucket.IsFilled)
        {
            Debug.Log("A player with a bucket next to a well!");
            bucket.Fill();
        }
    }
}