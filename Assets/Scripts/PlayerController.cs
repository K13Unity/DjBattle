using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isMain = true;
    [SerializeField] private float moveSpeed = 2f;
    public Transform bulletSpawnPoint;
    public Rigidbody rb;
    public Bullet bulletPrefab;
    public ArrowController arrowController; // Призначте це через інспектор
    public ScoreManager scoreManager;
    public new Camera camera;
    public int maxBullets = 3;
    public int currentBulletCount = 3;
    Vector3 movement;
    public PlayerTypes type;

    private void Start()
    {
        StartCoroutine(SpawnBulletsRoutine());
        arrowController.AssignPlayer(this);
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }
    }

    private void Update()
    {
        if (isMain == false)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = new Vector3(movement.x, 0f, movement.y).normalized;

        if (currentBulletCount > 0 && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                Vector3 targetPosition = hitInfo.point;
                Vector3 direction = (targetPosition - bullet.transform.position).normalized;
                bullet.Init(direction, this);
                currentBulletCount--;

                if (arrowController != null)
                {
                    arrowController.BulletFired();
                }
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

    public void GetPlayerAndMousePosition(out Vector3 playerPos, out Vector3 mousePos)
    {
        playerPos = transform.position;
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private IEnumerator SpawnBulletsRoutine()
    {
        while (true)
        {
            yield return null;

            if (currentBulletCount < maxBullets)
            {
                yield return new WaitForSeconds(1f);
                currentBulletCount++;
                arrowController.ReloadArrows();
            }
        }
    }

    public void TakeDamage(int score)
    {
        scoreManager.UpdateScoreText(score);
    }
}


