using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum JellyState { Idle, Reached}

public class JellyFish : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public EnergySystem energySystem;

    private Transform target;
    private JellyState state;
    private float step;

    private void Start()
    {
        state = JellyState.Idle;
    }

    void Update()
    {
        if (state == JellyState.Reached)
        {
            if(energySystem.state == EnergyState.Dashing || energySystem.state == EnergyState.ChargingDashing)
                step = dashSpeed * Time.deltaTime;
            else
                step = speed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void Reached()
    {
        state = JellyState.Reached;
    }
}
