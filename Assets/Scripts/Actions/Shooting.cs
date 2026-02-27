using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Animator anim;

    const float DEFAULT_SHOOT_INTERVAL = 0.8f;
    const int DEFAULT_MAGAZINE_COUNT = 5;

    public float shootInterval;
    private float shootTimer = 0f; // 0이하면 shooting 가능

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

        shootInterval = DEFAULT_SHOOT_INTERVAL;
    }

    public void HandleShoot(bool isShoot)
    {
        if (isShoot && shootTimer <= 0)
        {
            anim.SetTrigger("OnShoot");
            Debug.Log("Shoot!");

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
}
