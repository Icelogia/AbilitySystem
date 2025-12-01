using UnityEngine;

public static class MathExtensions
{
    public static int Round(float a)
    {
        return Mathf.FloorToInt(a + 0.5f);
    }

    public static Vector2Int Round(Vector2 a)
    {
        return new Vector2Int(Mathf.FloorToInt(a.x + 0.5f), Mathf.FloorToInt(a.y + 0.5f));
    }

    public static Vector3Int Round(Vector3 a)
    {
        return new Vector3Int(Mathf.FloorToInt(a.x + 0.5f), Mathf.FloorToInt(a.y + 0.5f), Mathf.FloorToInt(a.z + 0.5f));
    }
}
