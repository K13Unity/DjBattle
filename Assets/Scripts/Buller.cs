using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _maxTouchCount = 3;
    private int _touchCounter;
    private Vector3 _direction;
    public float speed = 3.0f;
    private PlayerController _parent;
    public ParticleSystem partPrefab;
    private float _particleLifeTime = 4f;
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
        if (Physics.Raycast(start, end - start, out var hit, (end - start).magnitude))
        {
            if (hit.collider.CompareTag("Player")) 
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
            
            if (_parent.Type == PlayerTypes.RIGHT)
            {
                player.TakeHit(1);
            }
            else
            {
                player.TakeHit(-1);
            }
            PartInstantiate();
            Destroy(gameObject);
        }
    }

    void HandleCollision(Vector3 normal, Vector3 hitPoint)
    {
        _direction = Vector3.Reflect(_direction, normal);
        IncreaseTouchCount();
        transform.position = hitPoint + _direction * 0.02f; 
    }

    void IncreaseTouchCount()
    {
        _touchCounter++;
        if (_touchCounter >= _maxTouchCount)
        {
            PartInstantiate();
            Destroy(gameObject);
        }
    }

    void PartInstantiate()
    {
        var partInstance = Instantiate(partPrefab, transform.position, Quaternion.identity);
        partInstance.Play();
        Destroy(partInstance.gameObject, _particleLifeTime);
    }
}

