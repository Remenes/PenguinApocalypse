using UnityEngine;

public class Helper : MonoBehaviour
{
    public delegate void VoidDelegate();

    public static Vector3 Vector2toVector3(Vector2 vector, float zOffset = 0)
    {
        return new Vector3(vector.x, vector.y, zOffset);
    }

}
