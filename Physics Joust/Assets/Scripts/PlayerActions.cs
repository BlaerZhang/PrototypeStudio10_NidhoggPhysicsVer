using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    public FixedJoint2D playerGrabberFixedJoint2D;
    public Rigidbody2D playerHand;
    public Rigidbody2D playerGrabber;
    public Rigidbody2D playerWeapon;
    public Vector2 movingVector2;
    public Vector2 rotatingVector2;
    public bool isLoosen;
    public bool isStunned;
    public float stunDuration;
    
    void Start()
    {
        isLoosen = false;
        isStunned = false;
    }


    void FixedUpdate()
    {
        Moving();
        Rotating();
        Loosing();

        if (GameManager.Instance.gameState != GameManager.GameState.IsInGame)
            playerWeapon.bodyType = RigidbodyType2D.Static;
        else playerWeapon.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnMoving(InputAction.CallbackContext context)
    {
        movingVector2 = context.ReadValue<Vector2>();
    }

    public void Moving()
    {
        if (movingVector2 == Vector2.zero || GameManager.Instance.gameState != GameManager.GameState.IsInGame)
        {
            playerHand.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            playerHand.bodyType = RigidbodyType2D.Dynamic;
            playerHand.velocity = movingVector2 * 20;
        }
    }

    public void OnRotating(InputAction.CallbackContext context)
    {
        rotatingVector2 = context.ReadValue<Vector2>();
    }

    public void Rotating()
    {
        if (rotatingVector2 == Vector2.zero || GameManager.Instance.gameState != GameManager.GameState.IsInGame)
        {
            
        }
        else if (!isStunned)
        {
            playerGrabber.AddForce(rotatingVector2 * -100f, ForceMode2D.Force);
        }
    }

    public void GetStunned()
    {
        isStunned = true;
        Invoke("ResetStun", stunDuration);
    }

    private void ResetStun()
    {
        isStunned = false;
    }

    public void OnLoosing(InputAction.CallbackContext context)
    {
        if (context.started) isLoosen = true;
        if (context.canceled) isLoosen = false;
    }

    public void Loosing()
    {
        if (isLoosen && GameManager.Instance.gameState == GameManager.GameState.IsInGame)
        {
            playerGrabberFixedJoint2D.enabled = false;
            playerGrabberFixedJoint2D.connectedBody = null;
        }
        else
        {
            playerGrabberFixedJoint2D.enabled = true;
            if (playerGrabberFixedJoint2D.connectedBody == null) playerGrabberFixedJoint2D.connectedBody = playerWeapon;
        }
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        if (context.canceled) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
