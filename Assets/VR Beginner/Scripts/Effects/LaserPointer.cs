using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    LineRenderer m_LineRenderer;

    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.SetPosition(0, Vector3.zero); // Ensure the start position is set
    }

    void Update()
    {
        RaycastHit info;
        if (Physics.Raycast(transform.position, transform.up, out info, 100.0f, ~0, QueryTriggerInteraction.Ignore))
        {
            m_LineRenderer.SetPosition(1, new Vector3(0, info.distance, 0));

            // Tjekker om det objekt, der er blevet ramt, implementerer IHittable
            IHittable hittable = info.collider.GetComponent<IHittable>();
            if (hittable != null)
            {
                // Kalder på metoden på det objekt, der er blevet ramt
                hittable.OnHit();
            }
        }
        else
        {
            m_LineRenderer.SetPosition(1, new Vector3(0, 100.0f, 0));
        }
    }
}

// Difinerer et interface, som objekter kan implementere for at blive ramt af laseren
public interface IHittable
{
    void OnHit();
}