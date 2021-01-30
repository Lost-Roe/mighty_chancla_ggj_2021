using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{

    [Header("Character Controller")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;

    [Header("Movement")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity = 0.1f;
    private Vector3 moveDir;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashtime;
    [SerializeField] private float dashtimeDelay = 2;
    private float dashDelay;

    [Header("Energy")]
    [SerializeField] private EnergySystem energySystem;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        dashDelay = dashtimeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        DoADash();
    }

    private void DoADash()
    {
        if(energySystem.currentEnergy > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                dashDelay -= Time.deltaTime;
                if(dashDelay <= 0)
                {
                    energySystem.state = EnergyState.Dashing;
                    StartCoroutine(Dash());
                }
                else
                    animator.SetBool("Dash",true);
            }
        }
        else
        {
            energySystem.state = EnergyState.Consuming;
            animator.SetBool("Dash", false);
            dashDelay = dashtimeDelay;
        }
        
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            energySystem.state = EnergyState.Consuming;
            animator.SetBool("Dash",false);
            dashDelay = dashtimeDelay;
        }
    }

    private IEnumerator Dash()
    {
        float startTime = Time.time;
        
        while (Time.time < startTime + dashtime)
        {
            controller.Move(moveDir * dashSpeed * Time.deltaTime);


            yield return null;
        }
    }
}
