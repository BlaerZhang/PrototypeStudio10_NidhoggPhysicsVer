using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBody : WeaponPart
{
    
    protected override void Start()
    {
        base.Start();
    }

    
    protected override void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (!isInHitCD)
        {
            base.OnCollisionEnter2D(col);
            switch (col.collider.gameObject.transform.tag)
            {
                case "Body":
                    return;
            
                case "Handle":
                    return;
            
                case "Spike":
                    KnockBack(col, 0.2f);
                    hitSwordFeedback.transform.position = col.GetContact(0).point;
                    hitSwordFeedback.PlayFeedbacks();
                    return;
                
                case "Spike Body":
                    KnockBack(col, 0.2f);
                    hitSwordFeedback.transform.position = col.GetContact(0).point;
                    hitSwordFeedback.PlayFeedbacks();
                    return;
                
                case "Hammer":
                    transform.parent.parent.parent.GetComponent<PlayerActions>().GetStunned();
                    return;
            
                default:
                    return;
            }
        }
    }
}