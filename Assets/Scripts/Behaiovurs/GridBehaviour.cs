using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enums;
public class GridBehaviour : MonoBehaviour
{
    public LevelEnum.Side CorrectSide => _correctSide;
    public Vector3[] PathForLeft => _pathForLeft;
    public Vector3[] PathForRight => _pathForRight;
    public bool IsFull;

    [SerializeField] Vector3[] _pathForLeft;
    [SerializeField] Vector3[] _pathForRight;

    [SerializeField] private LevelEnum.Side _correctSide;
    
    private SpriteRenderer _spriteRenderer;


    public void Initialize(Color colorLeft, Color colorRight)
    {
        IsFull = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_correctSide == LevelEnum.Side.Left)
        {
            _spriteRenderer.color = colorLeft;
        }
        else
        {
            _spriteRenderer.color = colorRight;
        }
    }
}
