using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    [Header("Interaction")]
    [SerializeField] private LayerMask _platformLayerMask = -1;
    [SerializeField] private LayerMask _wallLayerMask = -1;
    [Header("Ray")]
    [SerializeField] private float _bottomDistance = 1f;
    [SerializeField] private float _centerDistance = 1f;
    [SerializeField] private float _topDistance = 1f;
    [SerializeField] private float _xOffset = 1f;

    private RaycastHit2D _groundInfoBottom, _groundInfoTop, _groundInfoCenter;
    private bool isJumping = false;

    public override float RetrieveMoveInput(GameObject gameObject)
    {

        _groundInfoBottom = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.down, _bottomDistance, _platformLayerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.down * _bottomDistance, Color.green);

        _groundInfoCenter = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.right * gameObject.transform.localScale.x, _centerDistance, _wallLayerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.right * _centerDistance * gameObject.transform.localScale.x, Color.green);

        if (_groundInfoTop.collider == true || _groundInfoBottom.collider == false)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }

        return gameObject.transform.localScale.x;
    }

    public override bool RetrieveJumpInput(GameObject gameObject)
    {
        _groundInfoTop = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.up, _topDistance, _platformLayerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.up * _topDistance, Color.green);

        _groundInfoCenter = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.right * gameObject.transform.localScale.x, _centerDistance, _platformLayerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.right * _centerDistance * gameObject.transform.localScale.x, Color.green);

        if (_groundInfoCenter.collider && !_groundInfoTop.collider)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        return isJumping;
    }

    public override bool RetrieveJumpHoldInput(GameObject gameObject)
    {
        return false;
    }
}
