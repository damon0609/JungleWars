using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(order = 1,fileName = "Animation",menuName = "Damon/Animation")]
public class AnimationConfig : ScriptableObject
{
    public GameObject target;
    public LoopType loopType;
    public AnimationType animationType=AnimationType.Alpha;

    [Range(1,10)]
    public float timer;


    public void OnEnable()
    {

    }

    public void Awake()
    {
    }

    public void Start()
    {
    }
}


public enum AnimationType
{
    Move,
    Scale,
    Color,
    Alpha,
}