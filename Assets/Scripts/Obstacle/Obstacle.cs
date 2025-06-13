using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleHealth), typeof(ObstaclePooler), typeof(ObstacleAnimationHandler))]
public class Obstacle : MonoBehaviour, IHittable {

    [SerializeField] private ObstacleHealth health;
    [SerializeField] private ObstacleAnimationHandler animationHandler;
    [SerializeField] private ObstaclePooler pooler;

    private void Awake()
    {
        SetNullParameters();
    }

    private void SetNullParameters()
    {
        if(health == null) health = GetComponent<ObstacleHealth>();
        if(pooler == null) pooler = GetComponent<ObstaclePooler>();
        if(animationHandler == null) animationHandler = GetComponent<ObstacleAnimationHandler>();
    }

    public void GetHit()
    {
        health.GetDamage(() =>
        {
            animationHandler.OnHitAnimate();
            pooler.PoolOut();
        });
    }
}
