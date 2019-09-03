using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InversionEffect.GravObjects
{
    [System.Serializable]
    public struct Gravity
    {
        public Vector3 GravityOrigin;
        public Vector3 GravityDirection;
        public float GravityForce;

        public Gravity(Vector3 direction, float force)
        {
            GravityDirection = direction;
            GravityForce = force;
            GravityOrigin = Vector3.zero;
        }
    }
}