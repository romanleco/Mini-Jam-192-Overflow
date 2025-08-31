using UnityEngine;

public class Interpolator : MonoBehaviour
{
    [SerializeField] private float _growScale = 1.5f;
    [SerializeField] private float _growthSpeed = 3.5f;
    [SerializeField] private bool _grow;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _followTargetSpeed = 0.2f;

    public void SetGrow(bool value) => _grow = value;
    public void SetFollowTarget(Transform target) => _followTarget = target;

    void Update()
    {
        Grow();
        FollowTarget();
    }

    private void Grow()
    {
        if (_grow)
        {
            if (transform.localScale.x < _growScale)
            {
                transform.localScale += _growthSpeed * Time.deltaTime * Vector3.one;
                if (transform.localScale.x >= _growScale)
                {
                    transform.localScale = _growScale * Vector3.one;
                }
            }
        }
        else
        {
            if (transform.localScale.x > 1)
            {
                transform.localScale -= _growthSpeed * Time.deltaTime * Vector3.one;
                if (transform.localScale.x <= 1)
                {
                    transform.localScale = Vector3.one;
                }
            }
        }
    }

    private void FollowTarget()
    {
        if (_followTarget != null)
        {
            transform.parent.position = Vector3.Slerp(transform.position, _followTarget.position, _followTargetSpeed);
        }

        //If the item is on the counter area, return to its original position
        //Else, drop with gravity
    }
}
