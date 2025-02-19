using UnityEngine;

public class GardenBed : MonoBehaviour
{
    public Renderer gardenBedRenderer;
    public Material dryMaterial;
    public Material wateredMaterial;
    public float dryOutTime = 3f;

    private bool _isWatered;

    private void Start()
    {
        gardenBedRenderer.material = dryMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        Bucket bucket = other.GetComponentInChildren<Bucket>();
        if (bucket != null && bucket.IsFilled && !_isWatered)
        {
            bucket.Empty();
            WaterGardenBed();
        }
    }

    private void WaterGardenBed()
    {
        _isWatered = true;
        gardenBedRenderer.material = wateredMaterial;
        StartCoroutine(DryOut());
    }

    private System.Collections.IEnumerator DryOut()
    {
        yield return new WaitForSeconds(dryOutTime);
        _isWatered = false;
        gardenBedRenderer.material = dryMaterial;
    }
}
