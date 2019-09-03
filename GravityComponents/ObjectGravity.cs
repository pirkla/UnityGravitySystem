using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InversionEffect.GravObjects
{
    public class ObjectGravity : MonoBehaviour, IGravityReceiver
    {
        public Vector3 GravityDirection
        {
            get { return myGravity.GravityDirection; }
        }
        private float GravityForce
        {
            get { return myGravity.GravityForce; }
        }
        private Rigidbody myRigidBody;
        private float rotationSpeed = 6;
        [SerializeField]
        private string gravObjectTagName = "Gravitates";
        [SerializeField]
        private Gravity myGravity;
        private float gravityForceHolder;

        ISlowable mySlowable;
        private void Start()
        {
            mySlowable = GetComponent<ISlowable>();
            if(mySlowable!=null)
            {
                mySlowable.OnSlowed += OnSlowed;
                mySlowable.OnSlowEnd += OnEndSlow;
            }
            myRigidBody = GetComponentInChildren<Rigidbody>();
            myRigidBody.useGravity = false;
            myRigidBody.freezeRotation = true;
            gravityForceHolder = myGravity.GravityForce;
        }
        private void OnDestroy()
        {
            if (mySlowable != null)
            {
                mySlowable.OnSlowed -= OnSlowed;
                mySlowable.OnSlowEnd -= OnEndSlow;
            }
        }
        private void OnSlowed(float slowFactor)
        {
            myGravity.GravityForce = gravityForceHolder / slowFactor;
        }
        private void OnEndSlow()
        {
            myGravity.GravityForce = gravityForceHolder;
        }
        private void FixedUpdate()
        {
            ApplyGravity();
            ApplyRotation();
        }
        private void ApplyGravity()
        {
            myRigidBody.velocity += GravityDirection * Time.deltaTime * GravityForce;
        }
        private void ApplyRotation()
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -GravityDirection) * transform.rotation;
            float Angle = Quaternion.Angle(transform.rotation, targetRotation);
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Angle / rotationSpeed);
            transform.rotation = newRotation;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag(gravObjectTagName))
            {
                return;
            }
            Vector3 newDir = myGravity.GravityDirection;
            ContactPoint item = collision.contacts[0];
            myRigidBody.velocity = Vector3.zero;//TODO should only restrict up, fix later
            newDir = -item.normal;
            myGravity.GravityDirection = newDir;

        }
        private void OnCollisionStay(Collision collision)
        {
            if (!collision.collider.CompareTag(gravObjectTagName))
            {
                return;
            }
            Vector3 newDir = myGravity.GravityDirection;
            ContactPoint item = collision.contacts[0];
            newDir = -item.normal;
            myGravity.GravityDirection = newDir;

        }
        private void OnCollisionExit(Collision collision)
        {
            if (!collision.collider.CompareTag(gravObjectTagName))
            {
                return;
            }
        }
        public void SetRotationFromNormal(Vector3 normal)
        {
            myGravity.GravityDirection = -normal;
            myGravity.GravityForce = gravityForceHolder;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -GravityDirection) * transform.rotation;
            myRigidBody.velocity = Vector3.zero;
            transform.rotation = targetRotation;
        }
        public Gravity GetGravity()
        {
            return myGravity;
        }
        public void SetGravity(Gravity newGravity)
        {
            myGravity = newGravity;
        }
    }
}