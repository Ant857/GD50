using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zone : MonoBehaviour
{
    private float shrinkTimer;
    private float circleShrinkSpeed;
    private Vector2 nextCircleSize;
    private Vector2 nextCirclePosition;
    public GameObject outsideZone;
    public GameObject circle;
    public GameObject nextCircle;
    public GameObject nextCircleMask;
    public Text shrinkTimerText;


    // Start is called before the first frame update
    void Start()
    {
        shrinkTimer = 30f;
        circleShrinkSpeed = 3f;
        nextCirclePosition = FindNextCirclePos();
        nextCircleSize = new Vector2(circle.transform.localScale.x / 2, circle.transform.localScale.y / 2);
        nextCircle.transform.localScale = nextCircleSize;
        nextCircle.transform.position = nextCirclePosition;
        nextCircleMask.transform.localScale = new Vector2(nextCircleSize.x - 1, nextCircleSize.y - 1);
        nextCircleMask.transform.position = nextCirclePosition;
    }

    // Update is called once per frame
    void Update()
    {
        shrinkTimer -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(shrinkTimer / 60F);
        int seconds = Mathf.FloorToInt(shrinkTimer - minutes * 60);
        shrinkTimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);


        if (AreCirclesOverlapping() == true)
        {
            nextCirclePosition = FindNextCirclePos();
        }
        else if (shrinkTimer < 0)
        {
            Vector2 sizeChange = (nextCircleSize - (Vector2)circle.transform.localScale).normalized;
            Vector2 newCircleDirection = (nextCirclePosition - (Vector2)circle.transform.position).normalized;
            circle.transform.localScale = (Vector2)circle.transform.localScale + (sizeChange * Time.deltaTime) * circleShrinkSpeed;
            circle.transform.position = (Vector2)circle.transform.position + newCircleDirection * Time.deltaTime * circleShrinkSpeed;
        }

        if (Vector2.Distance(nextCircleSize, circle.transform.localScale) < 0.1f == true)
        {
            shrinkTimer = 30f;
            nextCirclePosition = FindNextCirclePos();
            nextCircleSize = new Vector2(circle.transform.localScale.x / 2, circle.transform.localScale.y / 2);
            nextCircle.transform.localScale = nextCircleSize;
            nextCircle.transform.position = nextCirclePosition;
            nextCircleMask.transform.localScale = new Vector2(nextCircleSize.x - 1, nextCircleSize.y - 1);
            nextCircleMask.transform.position = nextCirclePosition;
        }

    }

    private Vector2 FindNextCirclePos()
    {
        Vector2 nextCirclePos = new Vector2(Random.Range(-circle.transform.localScale.x / 4, circle.transform.localScale.x / 4), Random.Range(-circle.transform.localScale.y / 4, circle.transform.localScale.y / 4));
        return nextCirclePos;
    }

    private bool AreCirclesOverlapping()
    {
        double distance = Mathf.Sqrt(Mathf.Pow(nextCirclePosition.x - circle.transform.localScale.x, 2) + Mathf.Pow(nextCirclePosition.y - circle.transform.localScale.y, 2));
        return distance <= ((circle.transform.localScale.x / 2) + (nextCircleSize.x / 2)) && distance >= Mathf.Abs((circle.transform.localScale.x / 2) - (nextCircleSize.x / 2));
    }
}
