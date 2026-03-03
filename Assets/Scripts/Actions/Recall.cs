using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Recall : MonoBehaviour
{
    private PlayerController playerController;
    public struct PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;

        public PointInTime(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }

    private LinkedList<PointInTime> pointsInTime = new LinkedList<PointInTime>();
    private float recordTime = 3f;
    private WaitForSeconds fixedDeltaTime3 = new WaitForSeconds(0.02f * 3);

    private bool isRewinding = false;
    public bool IsRewinding {
        get { return isRewinding; }

        set
        {
            isRewinding = value;
            if (isRewinding)
            {
                playerController.IsMovable = false;
                playerController.IsRotatable = false;
                playerController.IsShootable = false;
            }
            else
            {
                playerController.IsMovable = true;
                playerController.IsRotatable = true;
                playerController.IsShootable = true;

                StartCoroutine(Record());
            }
        }

    }

    int maxStorage;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        maxStorage = Mathf.RoundToInt(recordTime / (Time.fixedDeltaTime * 3));
    }

    private void Start()
    {
        StartCoroutine(Record());
    }

    public void HandleRecall()
    {
        StartCoroutine(Rewind());
    }

    IEnumerator Record()
    {
        while (!IsRewinding)
        {
            if (pointsInTime.Count >= maxStorage)
            {
                pointsInTime.RemoveFirst();
            }
            pointsInTime.AddLast(new PointInTime(transform.position, transform.rotation));

            yield return fixedDeltaTime3;
        }
    }

    IEnumerator Rewind()
    {
        IsRewinding = true;
        Debug.Log("Rewinding");
        while (pointsInTime.Count > 0)
        {
            PointInTime point = pointsInTime.Last.Value;

            transform.position = point.position;
            transform.rotation = point.rotation;

            pointsInTime.RemoveLast();

            yield return new WaitForFixedUpdate();
        }

        IsRewinding = false;
    }
}
