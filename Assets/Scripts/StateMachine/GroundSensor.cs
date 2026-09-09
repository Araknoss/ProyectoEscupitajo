using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool grounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D groundCheckCollider;
    [SerializeField] private CircleCollider2D groundCheckTrigger;

    private ContactFilter2D contactFilter;
    private Collider2D[] results = new Collider2D[5];
    private List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    public Vector2 groundNormal;

    private void Awake()
    {
        // El trigger de margen empieza desactivado; solo se activa tras el primer contacto real
        groundCheckTrigger.enabled = false;
    }
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        bool coreTouch = groundCheckCollider.IsTouchingLayers(groundLayer);
        bool marginTouch = groundCheckTrigger.enabled && groundCheckTrigger.IsTouchingLayers(groundLayer);

        grounded = coreTouch || marginTouch;

        // Activa el trigger de margen solo cuando se confirma contacto real
        if (grounded && !groundCheckTrigger.enabled)
        {
            groundCheckTrigger.enabled = true;
        }
        // Desactiva el trigger de margen en cuanto se pierde todo contacto (ni core ni margen tocan suelo)
        else if (!grounded && groundCheckTrigger.enabled)
        {
            groundCheckTrigger.enabled = false;
        }

        if (grounded) // If grounded, find the ground normal
        {
            int count = groundCheckCollider.OverlapCollider(contactFilter, results);
            for (int i = 0; i < count; i++)
            {
                Collider2D col = results[i];

                if (col != null)
                {
                    col.GetContacts(contactPoints);

                    if (contactPoints.Count > 0)
                    {
                        Debug.DrawRay(transform.position, contactPoints[0].normal, Color.red, 1f);
                        if (contactPoints[0].normal.y < 0.1f)
                            groundNormal = new Vector2(-contactPoints[0].normal.x, 0);
                        groundNormal.Normalize();

                    }
                }
            }
        }
    }
    
}
