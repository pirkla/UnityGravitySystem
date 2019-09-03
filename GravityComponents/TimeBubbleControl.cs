using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InversionEffect.TimeControl;
public class TimeBubbleControl : MonoBehaviour {
    
    [SerializeField] float bubbleRadius = 5;
    [SerializeField] float slowFactor = 5;
    GameObject originObject;
    [SerializeField]SphereCollider myCollider;
    List<Slowable> affectedObjects;
    public void InitializeBubble(GameObject origin)
    {
        originObject = origin;
        affectedObjects = new List<Slowable>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, bubbleRadius);
        foreach (Collider other in colliders)
        {
            Debug.Log(other.gameObject.name);
            Slowable otherSlowable = other.GetComponent<Slowable>();
            if (otherSlowable != null)
            {
                Debug.Log("found");
                if (!affectedObjects.Contains(otherSlowable))
                {
                    Debug.Log("not contained");
                    otherSlowable.SlowDown(slowFactor, this);
                    affectedObjects.Add(otherSlowable);
                }
            }
        }
        myCollider.radius = bubbleRadius;
        Debug.Log("bam");
    }

    private void OnTriggerEnter(Collider other)
    {
        Slowable otherSlowable = other.GetComponent<Slowable>();
        if(otherSlowable != null)
        {
            if (!affectedObjects.Contains(otherSlowable))
            {
                otherSlowable.SlowDown(slowFactor, this);
                affectedObjects.Add(otherSlowable);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Slowable otherSlowable = other.GetComponent<Slowable>();
        if (otherSlowable != null)
        {
            if (affectedObjects.Contains(otherSlowable))
            {
                otherSlowable.SlowDown(slowFactor, this);
                otherSlowable.EndSlowDown(this);
                affectedObjects.Remove(otherSlowable);
            }
        }
    }
}
