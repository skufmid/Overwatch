using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Animator anim;
    private Camera camera;

    const float DEFAULT_SHOOT_INTERVAL = 0.8f;
    const int DEFAULT_MAGAZINE_COUNT = 5;
    const int HIT_TARGET_LAYER = 1 << 7;

    public float shootInterval;
    private float shootTimer = 0f; // 0이하면 shooting 가능

    private float max_range = 50f;
    private float max_damage = 50f;
    private float min_damage = 5f;

    int magazineCount;

    private bool isShooting = false;
    public bool IsShooting
    {
        set
        {
            isShooting = value;
            HandleShoot(value);
        }
        get
        {
            return isShooting;
        }
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        camera = Camera.main;

        shootInterval = DEFAULT_SHOOT_INTERVAL;
    }

    private void Start()
    {
    }

    public void HandleShoot(bool isShoot)
    {
        if (isShoot && shootTimer <= 0)
        {
            anim.SetTrigger("OnShoot");
            Shoot();

            StartCoroutine(CoShootTimer());
        }
        else
        {
            if (shootTimer < 0)
            {
                shootTimer = 0;
            }
        }
    }

    IEnumerator CoShootTimer()
    {
        shootTimer += shootInterval;

        while (true)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                HandleShoot(IsShooting);
                break;
            }
            yield return null;
        }
    }

    private void Shoot()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, max_range))
        {
            float distance = hit.distance;
            HitTarget target = hit.collider.GetComponent<HitTarget>();
            if (target != null)
            {
                float damage = Mathf.Lerp(max_damage, min_damage, distance / max_range);
                damage = Mathf.Clamp(damage, min_damage, max_damage);
                target.GetHit(damage);
            }
        }
    }
}

