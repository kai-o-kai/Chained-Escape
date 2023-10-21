using UnityEngine;

public static class Utilities {
    public static float GetAngleToPoint(Vector2 from, Vector2 to) {
        Vector2 dir = to - from;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}