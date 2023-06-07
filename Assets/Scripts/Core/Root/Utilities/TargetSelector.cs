using System.Collections.Generic;
using UnityEngine;

namespace Core.Root.Utilities
{
    public class TargetSelector : MonoBehaviour
    {
        public List<GameObject> targets         = new List<GameObject>();
        public float            maxViewDistance = 100f;
        public LayerMask        targetLayerMask;

        void UpdateTargets()
        {
            RefreshTargets();
            SortTargetsByDistance();
            SortTargetsByPriority();
            FilterVisibleTargets();
        }

        void RefreshTargets()
        {
            targets.Clear();
            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject target in potentialTargets)
            {
                if (IsValidTarget(target))
                {
                    targets.Add(target);
                }
            }
        }

        bool IsValidTarget(GameObject target)
        {
            // Add custom validation logic here, e.g., checking target's health or status
            return true;
        }

        void SortTargetsByDistance()
        {
            targets.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position)
                .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
        }

        void SortTargetsByPriority()
        {
            // You can replace this with your own priority calculation logic
            targets.Sort((a, b) => GetTargetPriority(a).CompareTo(GetTargetPriority(b)));
        }

        int GetTargetPriority(GameObject target)
        {
            // Calculate target priority based on its attributes, status, or other factors
            return 0; // Placeholder value
        }

        void FilterVisibleTargets()
        {
            List<GameObject> visibleTargets = new List<GameObject>();
            foreach (GameObject target in targets)
            {
                if (IsTargetVisible(target))
                {
                    visibleTargets.Add(target);
                }
            }
            targets = visibleTargets;
        }

        bool IsTargetVisible(GameObject target)
        {
            RaycastHit hit;
            Vector3    directionToTarget = (target.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, directionToTarget, out hit, maxViewDistance, targetLayerMask))
            {
                return hit.collider.gameObject == target;
            }
            return false;
        }
    }
}