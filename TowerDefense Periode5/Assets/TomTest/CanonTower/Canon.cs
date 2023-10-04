using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private float rotationSpeed = 5f;
    private float returnSpeed = 2f;
    private float triggerDistance = 10f;
    public float maxRotationAngle = 45f;

    private bool isTargetInRange = false;
    private Quaternion initialLocalRotation;


    private Quaternion initialRotation;


    [Header("TargetSettings")]
    public Test detect;
    public Transform target;

    [Header("DamageSettings")]
    public float[] damage;
    public float[] delay;

    [Header("Level")]
    private float timer;
    public int level;


    public void Start()
    {
        initialLocalRotation = transform.localRotation;
    }
    void Update()
    {

        target = detect.ChooseTarget;

        if (target != null)
        {
            timer += Time.deltaTime;
            if (timer > delay[level])
            {
                Shoot();
                timer -= delay[level];
            }
        }
        else
        {
            timer = 0f;
        }
        RotateWithTarget();
    }

    private void Shoot()
    {
        target.GetComponent<EnemyHealth>().health -= damage[level];
    }

    private void RotateWithTarget()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= triggerDistance)
            {
                // Calculate the direction vector to the target
                Vector3 directionToTarget = target.position - transform.position;
                directionToTarget.y = 0; // Ensure that only the Y-axis is considered

                // Calculate the target rotation based on the direction vector
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                // Ensure the Y-axis rotation does not exceed the maximum angle
                float yRotation = targetRotation.eulerAngles.y;
                yRotation = Mathf.Clamp(yRotation, -maxRotationAngle, maxRotationAngle);
                targetRotation = Quaternion.Euler(0, yRotation, 0);

                // Rotate the cannon towards the target
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

                isTargetInRange = true;
            }
            else if (isTargetInRange)
            {
                // Smoothly return the local Y-axis rotation to its initial state
                transform.localRotation = Quaternion.Slerp(transform.localRotation, initialLocalRotation, returnSpeed * Time.deltaTime);
                isTargetInRange = false;
            }
        }
        else
        {
            // If the target is null, smoothly return the local Y-axis rotation to its initial state
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialLocalRotation, returnSpeed * Time.deltaTime);
        }
    }
}

