using System;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class SteeringWheel : Interactable
{

    // Imports ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [DllImport("user32.dll")]
    public static extern long SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    // End of Imports ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



    /// The minimum and maximum rotation of the wheel. [-180, 180]
    public Vector2 RotationLimits = new Vector2(-100.0f, 100.0f);
    public static float Angle { get; private set; } = 0.0f;
    public static float Velocity = 0.0f;

    /// The past mouse position.
    Vector3 pm = Vector3.zero;
    /// The distance from the wheel to the mouse.
    float dist = 0.0f;
    /// If the wheel is currently grabed.
    bool grabbed = false;



    private void Update()
    {

        transform.rotation *= Quaternion.Euler(0.0f, 0.0f, Velocity);
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            Mathf.Clamp((transform.rotation.eulerAngles.z + 180.0f) % 360.0f - 180.0f, RotationLimits.x, RotationLimits.y)
        );
        Velocity = Mathf.Lerp(Velocity, 0.0f, Time.deltaTime);

        if (grabbed)
        {
            Velocity = Mathf.Lerp(Velocity, 0.0f, Time.deltaTime * 2.0f);

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
            /*Vector2 sp = Camera.main.WorldToScreenPoint(transform.position);
            #if UNITY_EDITOR
                Type gameViewType = typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView");
                EditorWindow gameView = EditorWindow.GetWindow(gameViewType);
                float scale = Screen.dpi / 96.0f;
                sp.x += gameView.position.xMin;
                sp.y += gameView.position.yMin;
            #endif
            sp.y = Screen.currentResolution.height - sp.y;
            POINT p;
            GetCursorPos(out p);
            Vector2 constrained = sp + ClampMagnitude(new Vector2(p.X, p.Y) - sp, dist * 0.7f, dist * 1.1f);
            SetMousePos(constrained);*/
        }

        Angle = (transform.rotation.eulerAngles.z + 180.0f) % 360.0f - 180.0f;
        pm = Input.mousePosition;
    }



    void SetMousePos(Vector2 screenPoint)
    {
        SetCursorPos(Mathf.RoundToInt(screenPoint.x), Mathf.RoundToInt(screenPoint.y));
    }



    public override void OnInteraction()
    {
        grabbed = true;
        float scale = Screen.dpi / 96.0f;
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
