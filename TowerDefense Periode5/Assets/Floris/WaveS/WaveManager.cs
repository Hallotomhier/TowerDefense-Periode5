using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude = 1f;
    public float lenght = 2f;
    public float speed = 1f;
    public float offSet = 0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        offSet += Time.deltaTime * speed;

    }
    public float GetWaveHeight(float _x)
    {
        return amplitude * Mathf.Sin(_x / lenght + offSet);
    }
}
