using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool grounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D groundCheckCollider;

    private ContactFilter2D contactFilter;
    private Collider2D[] results = new Collider2D[5];
    private List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        grounded = groundCheckCollider.IsTouchingLayers(groundLayer);
    }

    public Vector2 GroundNormal()
    {
        contactPoints.Clear();
        
        int count = groundCheckCollider.OverlapCollider(contactFilter, results);

        if (count == 0)
            return Vector2.zero;
        
        for (int i = 0; i < count; i++)
        {
            Collider2D col = results[i];

            if (col != null)
            {
                col.GetContacts(contactPoints);

                if (contactPoints.Count > 0)
                {                    
                    return contactPoints[0].normal;
                }
            }
        }

        return Vector2.zero;
    }
}
