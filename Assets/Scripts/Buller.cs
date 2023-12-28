using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _maxNumber = 3;
    private int _number;
    private Vector3 _direction;
    public float speed = 3.0f;
    private PlayerController _parent;
    public ParticleSystem partPrefab;
    private ParticleSystem partInstance;

    void Update()
    {
        if (_direction == Vector3.zero)
            return;
        Vector3 xzDirection = new Vector3(_direction.x, 0f, _direction.z).normalized;
        MoveBullet(xzDirection);
    }
    public void Init(Vector3 direction, PlayerController parent)
    {
        _direction = direction.normalized;
        _parent = parent;
    }
    void MoveBullet(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        if (CheckCollision(transform.position, newPosition, out var hitPoint, out var normal))
        {
            HandleCollision(normal, hitPoint);
        }
        else
        {
            transform.position = newPosition;
        }
    }

    bool CheckCollision(Vector3 start, Vector3 end, out Vector3 hitPoint, out Vector3 normal)
    {
        RaycastHit hit;
        if (Physics.Raycast(start, end - start, out hit, (end - start).magnitude))
        {
            if (hit.collider.CompareTag("Player")) // Перевіряємо тег гравця
            {
                OnPlayerHit(hit.collider.GetComponent<PlayerController>());
            }

            hitPoint = hit.point;
            normal = hit.normal;
            return true;
        }

        hitPoint = end;
        normal = Vector3.up;
        return false;
    }
    void OnPlayerHit(PlayerController player)
    {
        if (player != null && _parent != player)
        {
            if (_parent.type == PlayerTypes.RIGHT)
            {
                player.TakeDamage(1);
            }
            else
            {
                player.TakeDamage(-1);
            }
            PartInstantiate();
            Destroy(gameObject);
        }
    }

    void HandleCollision(Vector3 normal, Vector3 hitPoint)
    {
        _direction = Vector3.Reflect(_direction, normal);
        TouchCounter();
        transform.position = hitPoint + _direction * 0.02f; // Невелике зміщення для уникнення проникнення
    }

    void TouchCounter()
    {
        _number++;
        if (_number >= _maxNumber)
        {
            PartInstantiate();
            Destroy(gameObject);
        }
    }

    void PartInstantiate()
    {
        partInstance = Instantiate(partPrefab, transform.position, Quaternion.identity);
        partInstance.Play();
        Destroy(partInstance.gameObject, 4f);
    }
}

