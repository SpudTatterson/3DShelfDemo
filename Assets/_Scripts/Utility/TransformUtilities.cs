using System;
using System.Collections;
using UnityEngine;

public static class TransformUtilities
{
    public static IEnumerator RotateOverTime(Transform transform, float rotationAmount, float rotationDuration, Action OnFinishRotation)
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + rotationAmount, 0);

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        OnFinishRotation?.Invoke();
    }
    public static IEnumerator LocalMoveOverTime(Transform transform, Vector3 targetPosition, float moveDuration, Action OnFinishedMove = null)
    {
        Vector3 initialPosition = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        OnFinishedMove?.Invoke();
    }
}
