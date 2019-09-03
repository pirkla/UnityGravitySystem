using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InversionEffect.GravObjects
{
    public interface IGravityReceiver
    {
        Vector3 GravityDirection { get; }
        void SetRotationFromNormal(Vector3 normal);
    }
}