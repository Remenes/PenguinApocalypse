using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2To3 : MonoBehaviour {

    public static Vector3 convert(Vector2 vector, float zOffset = 0) {
        return new Vector3(vector.x, vector.y, zOffset);
    }

}
