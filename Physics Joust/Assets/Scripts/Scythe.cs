using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : WeaponPart
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
                    hitFeedback.transform.position = col.GetContact(0).point;
                    hitFeedback.transform.rotation =
                        Quaternion.LookRotation(Vector3.forward, col.GetContact(0).relativeVelocity);
                    hitFeedback.PlayFeedbacks();
                    GameManager.Instance.gameState = GameManager.GameState.IsGameOver;
                    if (col.gameObject.transform.parent.name == "P1 Character") GameManager.Instance.winner = "Player 2";
                    if (col.gameObject.transform.parent.name == "P2 Character") GameManager.Instance.winner = "Player 1";
                    return;
            
                case "Handle":
                    parryFeedback.transform.position = col.GetContact(0).point;
                    parryFeedback.PlayFeedbacks();
                    GetKnockedBack(col, transform.parent.GetComponent<Rigidbody2D>(), 0.25f);
                    transform.parent.parent.GetComponent<PlayerActions>().GetStunned();
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
                    transform.parent.parent.GetComponent<PlayerActions>().GetStunned();
                    return;
            
                default:
                    return;
            }
        }
    }
}
