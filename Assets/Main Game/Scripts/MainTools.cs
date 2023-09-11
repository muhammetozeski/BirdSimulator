using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This class contains simple static function tools I made.
/// </summary>
public static class MainTools
{
    /// <summary>
    /// Equalize the vector2.x to x and y to z of the vector3. returns same y axis in vector3.
    /// Note: If you make "vector3 = vector2" this equalize the x to x but y to y.
    /// </summary>
    /// <returns>Vector3</returns>
    public static Vector3 Vector2To3OnFlat(Vector3 vector3, Vector2 vector2)
    {
        vector3.x = vector2.x;
        vector3.z = vector2.y;
        return vector3;
    }

    /// <summary>
    /// Equalize the vector2.x to x and y to z of the vector3. returns 0 for y axis.
    /// Note: If you make "vector3 = vector2" this equalize the x to x but y to y.
    /// </summary>
    /// <returns>Vector3</returns>
    public static Vector3 Vector2To3OnFlat(Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }

    /// <summary>
    /// Equalize the vector3.x to x and z to y of the vector3.
    /// </summary>
    /// <returns>Vector3</returns>
    public static Vector2 Vector3To2OnFlat(Vector3 vector3)
    {

        Vector2 vector2 = new Vector2(vector3.x, vector3.z);
        return vector2;
    }



    /// <summary>
    /// Get the normal to a triangle from the three corner points, a, b and c. 
    /// <br> </br>See <see href="https://docs.unity3d.com/ScriptReference/Vector3.Cross.html">here</see> for more information.
    /// </summary>
    /// <returns>Cross the two side to get a perpendicular vector, then normalize it.</returns>
    public static Vector3 GetNormal(Vector3 rootPos, Vector3 side1, Vector3 side2)
    {
        // Find vectors corresponding to two of the sides of the triangle.
        Vector3 _side1 = side1 - rootPos;
        Vector3 _side2 = side2 - rootPos;

        // Cross the vectors to get a perpendicular vector, then normalize it.
        return Vector3.Cross(_side1, _side2).normalized;
    }
    /// <summary>
    /// gives degree between 360 and 0
    /// </summary>
    public static float Vector2toAngle360(Vector2 p_vector2)
    {
        float angle = Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        if (angle < 0)
            return 180f - angle;
        else
            return angle;

    }

    /// <summary>
    /// gives degree between 180 and 0
    /// </summary>
    public static float Vector2toAngle180(Vector2 p_vector2)
    {
        var angle = Vector2toAngle360(p_vector2);
        return angle < 180 ? 180 - angle : angle;
    }
    /// <summary>
    /// gives degree between 360 and 0
    /// </summary>
    public static float Vector2toAngle(float x, float y)
    {
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        if (angle < 0)
            return (180 - Mathf.Abs(angle)) + 180f;
        else
            return angle;
    }
    /// <summary>
    /// detects value is in which ratio
    /// </summary>
    /// <returns>the index of ratio which includes value. returns -1 if it can't detect</returns>
    public static int RandomRatio(int[] ratios, int value)
    {
        int currentRatio = 0;
        for (int i = 0; i < ratios.Length; i++)
        {
            if (value >= currentRatio && value < currentRatio + ratios[i])
            {
                return i;
            }
            currentRatio += ratios[i];
        }
        return -1;//error
    }
    /// <summary>
     /// chooses a random value and detects value is in which ratio
     /// </summary>
     /// <returns>the index of ratio which includes value. returns -1 if it can't detect</returns>
    public static int RandomRatio(int[] ratios)
    {
        int ratioTop = 0; for (int i = 0; i < ratios.Length; i++) { ratioTop += ratios[i];}
        int value = Random.Range(0,ratioTop) ;
        int currentRatio = 0;
        for (int i = 0; i < ratios.Length; i++)
        {
            if (value >= currentRatio && value < currentRatio + ratios[i])
            {
                return i;
            }
            currentRatio += ratios[i];
        }
        return -1;//error
    }

    /// <summary>
    /// Sets real size of a gameobject.
    /// some gameobjects that is exported from an fbx may not show their real scale in transform.localScale
    /// </summary>
    public static void SetRealSize(Vector3 WantedSize, GameObject _gameObject)
    {
        if (_gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            Vector3 Difference = divide2Vector3(_gameObject.transform.localScale,
            renderer.bounds.size);
            _gameObject.transform.localScale = Multiply2Vector3(Difference, WantedSize);
        }
    }
    
    public static Vector3 Multiply2Vector3(Vector3 first, Vector3 second)
    {
        return new Vector3(
            first.x * second.x,
            first.y * second.y,
            first.z * second.z
            );
    }
    public static Vector3 divide2Vector3(Vector3 first, Vector3 second)
    {
        return new Vector3(
            first.x / second.x,
            first.y / second.y,
            first.z / second.z
            );
    }

    public static Vector3 GetRandomPointFromBox(BoxCollider collider)
    {
        return new Vector3(
            Random.Range(collider.size.x/2, 0 - collider.size.x/2),
            Random.Range(collider.size.y / 2, 0 - collider.size.y / 2),
            Random.Range(collider.size.z / 2, 0 - collider.size.z / 2)
            );
    }

    /// <summary>
    /// Converts single objects to array which has only 1 member.
    /// </summary>
    public static T[] ToArray<T>(T _object)
    {
        T[] _array = new T[1] {_object};
        return _array;
    }

    public static Vector3 GetRandomVector3(Vector3 min, Vector3 max)
    {
        return new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            Random.Range(min.z, max.z)
            );
    }

    public static Vector3 V3SetX(ref Vector3 vector, float X)
    { vector = new Vector3(X, vector.y, vector.z); return vector; }
    public static Vector3 V3SetX(Vector3 vector, float X)
    { return new Vector3(X, vector.y, vector.z); }

    public static Vector3 V3SetY(ref Vector3 vector, float Y)
    { vector = new Vector3(vector.x, Y, vector.z); return vector; }
    public static Vector3 V3SetY(Vector3 vector, float Y)
    { return new Vector3(vector.x, Y, vector.z); }

    public static Vector3 V3SetZ(ref Vector3 vector, float Z)
    { vector = new Vector3(vector.x, vector.y, Z); return vector; }
    public static Vector3 V3SetZ(Vector3 vector, float Z)
    { return new Vector3(vector.x, vector.y, Z); }

    /// <summary>
    /// Returns if a is bigger or equal to (b - difference) || if a is smaller or equal to (b + difference);
    /// </summary>
    public static bool Approximately(float a, float b, float difference)
    {
        return a >= (b - difference) || a <= (b + difference);
    }

    public static float Min(float min,float value)
    {
        return value < min ? min : value;
    }

    public static Vector3 Vector2to3(Vector2 vector2, float z = 0)
    {
        return new Vector3(vector2.x, vector2.y, z);
    }

    public static bool IsPointInSquare(Vector3 point, Vector3 borderUp, Vector3 borderDown)
    {
        return
            point.x < borderUp.x && point.y < borderUp.y && point.z < borderUp.z
            &&
            point.x > borderDown.x && point.y > borderDown.y && point.z > borderDown.z
            ;
    }

    public static bool IsPointInSquare(Vector2 point, Vector2 borderUp, Vector2 borderDown)
    {
        return
            point.x < borderUp.x && point.y < borderUp.y
            &&
            point.x > borderDown.x && point.y > borderDown.y
            ;
    }
}
