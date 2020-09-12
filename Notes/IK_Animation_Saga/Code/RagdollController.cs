// Working RagdollController
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RagdollController : NetworkBehaviour
{
    public Animator animator;
    public List<Collider> RagdollColliders = new List<Collider>();

    private Rigidbody rigid;
    public Rigidbody RIGID_BODY
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponentInParent<Rigidbody>();
            }
            return rigid;
        }
    }

    private void Awake()
    {
        SetRagdollParts();
    }

    private void SetRagdollParts()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                if (c.attachedRigidbody)
                    c.attachedRigidbody.isKinematic = true;

                RagdollColliders.Add(c);
            }
        }
    }

    public void TurnOnRagdoll()
    {
        // Modify components on the root character gameobject
        RIGID_BODY.useGravity = false;
        RIGID_BODY.velocity = Vector3.zero;
        this.gameObject.GetComponentInParent<CapsuleCollider>().enabled = false;
        animator.enabled = false;

        // Modify body parts
        foreach (Collider c in RagdollColliders)
        {
            c.isTrigger = false;
            if (c.attachedRigidbody)
            {
                c.attachedRigidbody.isKinematic = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }

    public void TurnOffRagdoll()
    {
        // Modify components on the root character gameobject
        RIGID_BODY.useGravity = true;
        RIGID_BODY.velocity = Vector3.zero;
        this.gameObject.GetComponentInParent<CapsuleCollider>().enabled = true;
        animator.enabled = true;

        // Modify body parts
        foreach (Collider c in RagdollColliders)
        {
            c.isTrigger = true;
            if (c.attachedRigidbody)
            {
                c.attachedRigidbody.isKinematic = true;
            }
        }
    }
}
