using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StunColorChanger : MonoBehaviour
{
    private SpriteRenderer handSprite;
    private PlayerActions _playerActions;
    private bool isColorChanged;
    void Start()
    {
        handSprite = GetComponent<SpriteRenderer>();
        _playerActions = transform.parent.parent.GetComponent<PlayerActions>();
        isColorChanged = false;
    }
    
    void Update()
    {
        if (_playerActions.isStunned && !isColorChanged)
        {
            isColorChanged = true;
            handSprite.DOColor(Color.red, _playerActions.stunDuration / 2f).OnComplete((() =>
            {
                handSprite.DOColor(new Color32(8, 8, 8, 255), _playerActions.stunDuration / 2f).OnComplete((() =>
                {
                    isColorChanged = false;
                }));
            }));
        }
    }
}
