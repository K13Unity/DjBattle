using UnityEngine;

public class Trembling : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float offsetX = 0.2f;
    [SerializeField] private float offsetZ = 0.2f;

    private bool isShaking = false;
    private float shakeDuration = 0.6f;
    private float shakeStartTime;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = _camera.localPosition;
    }

    private void FixedUpdate()
    {
        if (isShaking)
        {
            float elapsed = Time.time - shakeStartTime;
            if (elapsed < shakeDuration)
            {
                Vector3 newPosition = initialPosition + new Vector3(Random.Range(-offsetX, offsetX), 0f, Random.Range(-offsetZ, offsetZ));
                _camera.localPosition = Vector3.Lerp(_camera.localPosition, newPosition, 0.1f);
            }
            else
            {
                _camera.localPosition = initialPosition;
                isShaking = false;
            }
        }
    }

    public void StartShake()
    {
        isShaking = true;
        shakeStartTime = Time.time;
    }
}
