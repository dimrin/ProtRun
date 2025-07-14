using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstaclePooler), typeof(ObstaclePooler), typeof(ObstacleAnimationHandler))]
public class Obstacle : MonoBehaviour, IHittable {

    [SerializeField] private ObstaclePooler health;
    [SerializeField] private ObstacleAnimationHandler animationHandler;

    private void Awake()
    {
        SetNullParameters();
    }

    private void SetNullParameters()
    {
        if(health == null) health = GetComponent<ObstaclePooler>();
        if(animationHandler == null) animationHandler = GetComponent<ObstacleAnimationHandler>();
    }

    public void GetHit()
    {
        health.GetDamage(() =>
        {
            animationHandler.OnHitAnimate();
            health.DeactivateOnDamage();
        });
    }
}
