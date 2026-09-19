using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [Header("Forwards Stuff")]
    public float TimeLimit = 120.0f;
    Transform progressCar;
    SpriteRenderer progressCarRenderer;
    public Sprite[] progressCarSprites;
    public float progressStart = 0.0f;

    [Header("Not Forwards Stuff")]
    public Vector2 Limits = new Vector2(-2.0f, 2.0f);
    public float TurnSpeed = 0.01f;
    public static Vector3 Force = Vector3.zero;
    float position = 0.0f;
    Transform world;
    public Transform carButt;

    private void Awake()
    {
        progressCar = GameObject.Find("Progress Car").transform;
        progressStart = progressCar.localPosition.x;
        progressCarRenderer = progressCar.GetComponent<SpriteRenderer>();
        world = GameObject.Find("World").transform;
    }

    private void Update()
    {
        progressCarRenderer.sprite = progressCarSprites[Mathf.FloorToInt(Time.time) % 2];
        progressCar.localPosition = new Vector3(
            progressStart - 2.0f * progressStart * Time.time / TimeLimit,
            progressCar.localPosition.y, 
            progressCar.localPosition.z
        );

        position -= SteeringWheel.Angle * TurnSpeed * Time.deltaTime;

        world.position = new Vector3(
            Mathf.Clamp(-position, Limits.x, Limits.y), 
            transform.position.y, 
            transform.position.z
        );

        carButt.localPosition = new Vector3(
            position / 2.6f * 0.7f,
            carButt.localPosition.y,
            carButt.localPosition.z
        );

        //Force = Vector3.zero;
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Force += Vector3.up * 3.0f;
        Force = Vector3.zero;
        if (Random.value < Time.deltaTime * 0.1f + Mathf.Abs(world.position.x) * Time.deltaTime)
        {
            Force += Vector3.up * Random.value * 3.0f;
            Force += Vector3.right * (Random.value * 2.0f - 1.0f);
            AudioSystem.PlaySound("SFX/Thud", Random.Range(0.8f, 1.2f));
            SteeringWheel.Velocity += (Random.value * 2.0f - 1.0f) * 2.0f;
        }
    }

}
