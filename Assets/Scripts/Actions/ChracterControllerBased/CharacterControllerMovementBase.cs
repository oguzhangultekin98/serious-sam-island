using UnityEngine;
using System.Collections;

public abstract class CharacterControllerMovementBase : MovementBase,
    IMover<Vector3, CharacterController>, IAction
{
    private GravityHolder _gravityHolder = new GravityHolder();

    protected Vector3 impact;
    public CharacterController MovementComponent { get; private set; }
    public bool Activated { get; private set; }

    private Animator _animator;

    [SerializeField] protected TransformInterpolator transformInterpolater;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        MovementComponent = GetComponent<CharacterController>();

        transformInterpolater.oldVector = Vector3.zero;
        transformInterpolater.oldQuaternion = transform.rotation;
    }

    private void OnFallMove(Vector3 normalizedVollisionVector)
    {
        AddImpact(new Vector3(-normalizedVollisionVector.x, 1f, -normalizedVollisionVector.z), 5);
    }

  
    protected virtual void Update()
    {
        if (!Activated)
            return;
        impact = Vector3.Lerp(impact, Vector3.zero, Time.deltaTime);
    }


    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force;
    }

    public void MoveTo(Vector3 data)
    {
        data.Normalize();

        data *= (speedMultiplier * maxSpeed * Time.deltaTime);


        Vector3 velocity = Vector3.Lerp(transformInterpolater.oldVector,
            data, transformInterpolater.vectorLerpCoefficient);

        if (impact != Vector3.zero && impact.sqrMagnitude < 12)
            impact = Vector3.zero;

        MovementComponent.Move(velocity + Vector3.up * (_gravityHolder.gravityCoefficient * Time.deltaTime) +
                               impact * Time.deltaTime);

        _animator.SetFloat("Speed", MovementComponent.velocity.sqrMagnitude);

        if (velocity != Vector3.zero)
            Rotate(data);

        SetGravity();
    }

    protected void SetGravity()
    {
        if (MovementComponent.isGrounded)
            _gravityHolder.gravityCoefficient =
                Mathf.Lerp(_gravityHolder.gravityCoefficient, 0, Time.deltaTime * 10f);
        else
            _gravityHolder.gravityCoefficient += GravityHolder.Gravity * Time.deltaTime;
    }

    public void SetGravityToZero()
    {
        _gravityHolder.gravityCoefficient = 0f;
    }


    //protected abstract void Rotate(Vector3 data);
    public void Rotate(Vector3 data)
    {
        var position = transform.position;

        var indicatorPos = new Vector3(position.x - data.x, position.y, position.z - data.z);

        Quaternion targetRotation = Quaternion.Lerp(transformInterpolater.oldQuaternion,
            Quaternion.LookRotation(transform.position - indicatorPos),
            transformInterpolater.quaternionLerpCoefficient);

        transformInterpolater.oldQuaternion = transform.rotation;

        transform.rotation = targetRotation;
    }

    public bool IsActive { get; }

    public void Deactivate()
    {
        //  MovementComponent.enabled = false;
        _animator.SetFloat("Speed", 0);
        Activated = false;
    }

    [ContextMenu("Start")]
    public virtual void Activate()
    {
        Activated = true;
    }

}