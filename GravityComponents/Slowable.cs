using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace InversionEffect.TimeControl
{
    public class Slowable : MonoBehaviour, ISlowable
    {

        public event Action<float> OnSlowed = (slowAmount) => { };
        public event Action OnSlowEnd = () => { };

        Rigidbody myRigidbody;
        float slowFactorHolder;
        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }
        public void SlowDown(float slowFactor, TimeBubbleControl timeBubbleControl)
        {
            Debug.Log("slw");
            OnSlowed(slowFactor);
            if(myRigidbody)
            {
                slowFactorHolder = slowFactor;
                myRigidbody.velocity /= slowFactor;
            }

        }
        public void EndSlowDown(TimeBubbleControl timeBubbleControl)
        {
            OnSlowEnd();
            if(myRigidbody)
            {
                myRigidbody.velocity *= slowFactorHolder;
            }
        }
    }
}