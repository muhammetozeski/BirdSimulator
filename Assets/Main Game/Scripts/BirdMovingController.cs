using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BirdMovingController : MonoBehaviour
{
    [SerializeField] protected SpaceShipSettings spaceShipSettings;
    [SerializeField] protected Rigidbody rb;


    [Tooltip("Debug purpose")]
    [SerializeField] protected bool rotateYAxis;
    protected void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves the player's ship to front and back
    /// </summary>
    protected void MoveShip3d(Vector2 dir)
    {
        //summary:
        // be sure dir.y is larger than 0
        // clamp velocity by max speed
        // go to transform forward (rb.AddForce(Vector3.forward * dir.y * spaceShipSettings.SpeedForward);)

        if (dir.y > 0)
        {
            //rb.AddForce(transform.forward * dir.y * spaceShipSettings.SpeedForward);
            rb.velocity = (transform.forward * dir.y * spaceShipSettings.SpeedForward);
            Vector3 velocity = rb.velocity;
            velocity = Vector3.ClampMagnitude(velocity.magnitude * transform.forward, spaceShipSettings.Max3DSpeed);
            rb.velocity = velocity;
        }
        if (dir.y == 0)
        {
            Vector3 velocity = rb.velocity;
            velocity = Vector3.ClampMagnitude(velocity.magnitude * transform.forward, spaceShipSettings.Max3DSpeed);
            rb.velocity = velocity;
        }
    }

    // TODO: (BUG!) This function is shaking the bird
    protected void RotateShip(Vector2 rotateDir)
    {
        // Don't rotate if magnitude smaller than dead zone
        if (rotateDir.magnitude < spaceShipSettings.TargetDeadZone) rotateDir = Vector2.zero;

        else // for smoother animation, take out the dead zone
        {
            rotateDir -= Vector2.ClampMagnitude(rotateDir, spaceShipSettings.TargetDeadZone * 100);
        }

        rotateDir /= spaceShipSettings.TargetClamp; // it's like "rotateDir.normalize" but smoother one for our system

        rotateDir.y *= -spaceShipSettings.ShipXAxisRotationClamp; //change x axis
        float YAxis;
        if (rotateYAxis)
            YAxis = rotateDir.x * spaceShipSettings.YAngularSpeed + transform.eulerAngles.y; // change y axis
        else
            YAxis = 0;
        rotateDir.x *= -spaceShipSettings.ShipZAxisRotationClamp; //change z axis

        // apply all with a lerp:
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(new Vector3(rotateDir.y, YAxis, rotateDir.x)),
            spaceShipSettings.ShipAngularSpeed * Time.fixedDeltaTime
            );
    }

}
