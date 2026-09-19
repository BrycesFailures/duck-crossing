using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HandTremors : MonoBehaviour
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



    public float Speed = 1.0f;
    public float Amount = 10.0f;

    Vector2 offset = Vector2.zero;
    Vector2 poffset = Vector2.zero;
    Vector2 ppos = Vector2.zero;
    float count = 0.0f;

    private void Update()
    {

        count += Time.deltaTime * Speed;
        if (count >= 1.0f)
        {
            poffset = offset;
            offset = Random.insideUnitCircle;
        }

        if (Application.isFocused)
        {
            POINT p;
            GetCursorPos(out p);
            /*float tremor = Mathf.Sin(Time.time * Speed) * Amount;*/
            Vector2 pos = Vector2.Lerp(poffset, offset, ss(count)) * Amount;
            SetCursorPos(Mathf.RoundToInt(p.X - ppos.x), Mathf.RoundToInt(p.Y - ppos.y));
            SetCursorPos(Mathf.RoundToInt(p.X + pos.x), Mathf.RoundToInt(p.Y + pos.y));
            ppos = pos;
        }

    }



    float ss(float x) { return x * x * (3.0f - 2.0f * x); }

}
