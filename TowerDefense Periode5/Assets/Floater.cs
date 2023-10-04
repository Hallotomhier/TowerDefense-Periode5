using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody body;
    public float depthBeforeSub = 1f;
    public float displacementAmount = 3f;

    public int floaterCounter = 1;
    public float waterdrag = 0.99f;
    public float waterAngularDrag = 0.5f;
    private void FixedUpdate()
    {
        body.AddForceAtPosition(Physics.gravity/ floaterCounter, transform.position, ForceMode.Acceleration);
        
        float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        if(transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSub) * displacementAmount;
            body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);
            body.AddForce(displacementMultiplier * -body.velocity * waterdrag * Time.fixedDeltaTime,ForceMode.VelocityChange);
            body.AddTorque(displacementMultiplier * -body.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        
    }
}
