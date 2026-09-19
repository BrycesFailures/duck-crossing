using UnityEngine;

public class SteeringWheel : Interactable
{

    /// The minimum and maximum rotation of the wheel. [-180, 180]
    public Vector2 RotationLimits = new Vector2(-100.0f, 100.0f);

    /// The past mouse position.
    Vector3 pm = Vector3.zero;
    /// The distance from the wheel to the mouse.
    float dist = 0.0f;
    /// If the wheel is currently grabed.
    bool grabbed = false;



    private void Update()
    {

        if (grabbed)
        {
            // rotate with mouse movement
            Vector3 from = transform.position - Camera.main.ScreenToWorldPoint(pm);
            Vector3 to = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, Vector2.SignedAngle(from, to));
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x, 
                transform.rotation.eulerAngles.y, 
                Mathf.Clamp((transform.rotation.eulerAngles.z + 180.0f) % 360.0f - 180.0f, RotationLimits.x, RotationLimits.y)
            );

            // stick the cursor to the wheel
            Vector2 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 constrained = sp + ClampMagnitude((Vector2)Input.mousePosition - sp, dist * 0.8f, dist * 1.2f);
            //Mouse.current.WarpCursorPosition(constrained);
        }
        pm = Input.mousePosition;

    }



    public override void OnInteraction()
    {
        grabbed = true;
        dist = Vector2.Distance(Camera.main.WorldToScreenPoint(transform.position), Input.mousePosition);
    }

    public override void OnRelease()
    {
        grabbed = false;
    }



    static Vector2 ClampMagnitude(Vector2 vector, float min, float max)
    {
        float magnitude = vector.magnitude;
        if (magnitude < min) return vector.normalized * min;
        if (magnitude > max) return vector.normalized * max;
        return vector;
    }

}
