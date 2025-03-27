using UnityEngine;

public static class BobbyExtensions 
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D ridgedbody, Vector2 direction)
    {

        if (ridgedbody.isKinematic)
        {
            return false;

        }
            float radius = 0.25f;
            float distance = 0.375f;

            RaycastHit2D hit = Physics2D.CircleCast(ridgedbody.position, radius, direction.normalized, distance, layerMask);
            return hit.collider != null && hit.rigidbody != ridgedbody;

    }

    public static bool HeadHit(this Transform transform, Transform other, Vector2 Hdirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, Hdirection) > 0.25f;
    }

}
