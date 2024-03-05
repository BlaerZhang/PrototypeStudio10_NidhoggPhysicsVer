using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class WeaponPart : MonoBehaviour
{
    public float hitCoolDown;
    public bool isInHitCD;
    protected MMF_Player hitFeedback;
    protected MMF_Player parryFeedback;
    protected MMF_Player hitSwordFeedback;
    
    protected virtual void Start()
    {
        isInHitCD = false;
        hitFeedback = GameObject.Find("Hit Feedback").GetComponent<MMF_Player>();
        parryFeedback = GameObject.Find("Parry Feedback").GetComponent<MMF_Player>();
        hitSwordFeedback = GameObject.Find("Hit Sword Feedback").GetComponent<MMF_Player>();
    }
    
    protected virtual void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Grabber"))
        {
            if (!isInHitCD)
            {
                isInHitCD = true;
                print("CD set!");
                Invoke("ResetHitCD", hitCoolDown);
            }
        }
    }

    private void ResetHitCD()
    {
        isInHitCD = false;
    }

    protected virtual void KnockBack(Collision2D col, float knockBackForce)
    {
        Vector2 contactVelocity = col.GetContact(0).relativeVelocity;

        col.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(
            contactVelocity.normalized * -knockBackForce *
            (1 + (float)Math.Atan(contactVelocity.magnitude) / (float)Math.PI),
            col.GetContact(0).point, ForceMode2D.Impulse);
        
        print("Force: " + contactVelocity.normalized * -knockBackForce *
            (1 + (float)Math.Atan(contactVelocity.magnitude) / (float)Math.PI));
    }

    protected virtual void GetKnockedBack(Collision2D col, Rigidbody2D selfRB2D, float knockBackForce)
    {
        Vector2 contactVelocity = col.GetContact(0).relativeVelocity;

        selfRB2D.AddForceAtPosition(
            contactVelocity.normalized * knockBackForce *
            (1 + (float)Math.Atan(contactVelocity.magnitude) / (float)Math.PI), col.GetContact(0).point,
            ForceMode2D.Impulse);

        print("Force: " + contactVelocity.normalized * knockBackForce *
            (1 + (float)Math.Atan(contactVelocity.magnitude) / (float)Math.PI));
    }
}
