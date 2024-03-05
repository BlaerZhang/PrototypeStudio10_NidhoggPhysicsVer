using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : WeaponPart
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
                    KnockBack(col, 0.5f);
                    parryFeedback.transform.position = col.GetContact(0).point;
                    parryFeedback.PlayFeedbacks();
                    return;
            
                case "Spike":
                    KnockBack(col, 1f);
                    parryFeedback.transform.position = col.GetContact(0).point;
                    parryFeedback.PlayFeedbacks();
                    return;
                
                case "Spike Body":
                    KnockBack(col, 1f);
                    parryFeedback.transform.position = col.GetContact(0).point;
                    parryFeedback.PlayFeedbacks();
                    return;
                
                case "Hammer":
                    return;

                default:
                    return;
            }
        }
    }
}
