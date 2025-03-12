using System;
using _Project.Scripts.Environment;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _interactionDistance = 2.0f;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Inventory _inventory;

        private readonly Collider[] _interactionResults = new Collider[10];
        private Bucket _heldBucket;

        public void TryInteractOrDrop()
        {
            if (_heldBucket != null)
            {
                DropHeldBucket();
                return;
            }

            int hits = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionDistance, _interactionResults, _interactionLayer);
            if (hits <= 0)
            {
                return;
            }

            for (int i = 0; i < hits; i++)
            {
                var hit = _interactionResults[i];
                if (ProcessInteraction(hit))
                {
                    break;
                }
            }
            Array.Clear(_interactionResults, 0, _interactionResults.Length);
        }

        private bool ProcessInteraction(Collider hit)
        {
            if (hit.TryGetComponent(out Bucket bucket))
            {
                return TryPickUpBucket(bucket);
            }
            if (hit.TryGetComponent(out GardenBed bed))
            {
                return TryPlantSeed(bed);
            }
            if (hit.TryGetComponent(out Flower flower))
            {
                return TryHarvestFlower(flower);
            }
            return false;
        }

        private bool TryPickUpBucket(Bucket bucket)
        {
            if (_heldBucket != null)
            {
                return false;
            }

            _heldBucket = bucket;
            bucket.PickUp(_interactionPoint);
            Debug.Log($"Picked up bucket: {bucket.name}");
            return true;
        }

        private bool TryPlantSeed(GardenBed bed)
        {
            if (_inventory.TryUseItem(ItemType.Seed, out bool success) && bed.TryPlantSeed())
            {
                Debug.Log("Seed planted!");
                return true;
            }
            Debug.Log("Failed to plant seed!");
            return false;
        }

        private bool TryHarvestFlower(Flower flower)
        {
            if (!flower.IsFullyGrown)
            {
                Debug.Log("Flower is not ready yet!");
                return false;
            }
            flower.Harvest();
            Debug.Log("Flower harvested!");
            return true;
        }

        private void DropHeldBucket()
        {
            var dropPosition = _interactionPoint.position + _interactionPoint.forward;
            if (_heldBucket != null)
            {
                _heldBucket.Drop(dropPosition);
                Debug.Log($"Dropped bucket: {_heldBucket.name}");
                _heldBucket = null;
            }
        }
    }
}