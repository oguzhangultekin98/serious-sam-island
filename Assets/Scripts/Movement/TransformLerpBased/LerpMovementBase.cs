using System.Collections;
using UnityEngine;

public struct VectorLerpData
{
    public Quaternion targetRotation;
    public Vector3 targetPosition;
    public float duration;
}

public class LerpMovementBase : MonoBehaviour, IAction<VectorLerpData>
{
    private Coroutine rutine;
    public bool IsActive { get; private set; }

    public void Activate(in VectorLerpData data)
    {
        IsActive = true;
        rutine = StartCoroutine(SlerpToPosition(transform.position, data.targetPosition, data.targetRotation,
            data.duration));
    }

    public void Deactivate()
    {
        IsActive = false;

        /*        if (rutine != null)
                    StopCoroutine(rutine);
          */
    }

    private IEnumerator SlerpToPosition(Vector3 startPoint, Vector3 endPoint, Quaternion endRotation, float duration)
    {
        var percent = 0f;

        var center = (startPoint + endPoint) * 0.5f;

        center -= new Vector3(0, 1, 0);

        var riseRelCenter = startPoint - center;
        var setRelCenter = endPoint - center;

        while (percent < 1)
        {
            if (!IsActive)
                yield break;

            yield return null;
            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, percent);
            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, percent);
            transform.position += center;

            percent += Time.deltaTime / duration;
        }

        transform.position = endPoint;
        transform.rotation = endRotation;
    }
}