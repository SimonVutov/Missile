using UnityEngine;

[System.Serializable] // Allows the class to be serialized and displayed in the Unity Inspector.
public class Wing
{
    public Vector3 position;
    public Quaternion rotation;
    public float wingLength;
    public Color wingColor;
    public bool pitch;
    public bool roll;
    public bool pitchInvert;
    public bool rollInvert;
}
