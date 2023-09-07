using System;
using System.Collections;
using UnityEngine;

public class Strufe : MonoBehaviour
{
    public event Action StrufeMovedCallback;

    public void Move(float speed)
    {
        var currentPosition = gameObject.transform.position;
        var targetPosition = new Vector3(currentPosition.x, --currentPosition.y, --currentPosition.z);
        StartCoroutine(MoveCoroutine(targetPosition, speed));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition,float speed)
    {
        while (gameObject.transform.position != targetPosition)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Time.deltaTime * speed);
            yield return null;
        }
        StrufeMovedCallback?.Invoke();
    }
}
