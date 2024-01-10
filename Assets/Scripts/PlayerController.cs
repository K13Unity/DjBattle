using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMain = true;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private ArrowController arrowController; 
    [SerializeField] private Camera camera;
    [SerializeField] private int maxBullets = 3;
    [SerializeField] private int currentBulletCount = 3;
    Vector3 movement;
    [SerializeField] private PlayerTypes type;
    public PlayerTypes Type => type;
    private bool isActive = false;
    [SerializeField] private Trembling trembling;
    [SerializeField] private float spawnBulletDelay = 1.5f;
    
    public void SetActive(bool active)
    {
        isActive = active;
        StartCoroutine(SpawnBulletsRoutine());
    }

    private void Update()
    {
        if (!isActive) return;
        if (isMain == false) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = new Vector3(movement.x, 0f, movement.y).normalized;

        if (currentBulletCount > 0 && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                Vector3 targetPosition = hitInfo.point;
                Vector3 direction = (targetPosition - bullet.transform.position).normalized;
                bullet.Init(direction, this);
                currentBulletCount--;
                arrowController.BulletFired();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Vector3 lookDir = pointToLook - rb.position;
            lookDir.y = 0;

            if (lookDir != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDir);
                rb.MoveRotation(rotation);
            }
        }
    }


    private IEnumerator SpawnBulletsRoutine()
    {
        while (true)
        {
            yield return null;

            if (currentBulletCount < maxBullets)
            {
                yield return new WaitForSeconds(spawnBulletDelay);
                currentBulletCount++;
                arrowController.ReloadArrows();
            }
        }
    }

    public void TakeHit(int score)
    {
        trembling.StartShake();
        ScoreManager.instance.UpdateScoreText(score);
    }
}


