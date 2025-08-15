using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProductivityUnit : Unit // replace MonoBehaviour with Unit
{
    // new variables
    private ResourcePile m_CurrentPile = null;
    public float ProductivityMultiplier = 2.0f;
    private bool m_IsBoosting = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (m_CurrentPile != null)
        {
            float distance = Vector3.Distance(transform.position, m_CurrentPile.transform.position);
            if (distance > 2f) // or whatever range you define
            {
                ResetProductivity();
            }
        }

        if (m_Target != null && m_Target is ResourcePile)
        {
            BuildingInRange();
        }
    }


    // Update is called once per frame
    //void Update() // to test if the Unit is updating correctly
    //{
    //Debug.Log($"{gameObject.name} running ProductivityUnit.Update()");
    //    if (m_Target != null)
    //    {
    //        float dist = Vector3.Distance(transform.position, m_Target.transform.position);
    //        Debug.Log($"Distance to target: {dist}");

    //        if (dist < 2f)
    //        {
    //            Debug.Log("Calling BuildingInRange()");
    //            BuildingInRange();
    //        }
    //    }
    //}

    public override void GoTo(Building target)
    {
        //ResetProductivity(); // Optional: revert previous pile's boost
        base.GoTo(target);   // Crucial: sets m_Target and starts movement
        //Debug.Log($"00_GoTo called with target: {target.GetName()}");
        //Debug.Log($"01_m_Target is now: {m_Target?.GetName()}");

    }

    void ResetProductivity()
    {
        if (m_CurrentPile != null)
        {
            m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
            m_CurrentPile = null;
            m_IsBoosting = false;
            Debug.Log("Productivity reset");
        }
    }

    //protected override void BuildingInRange()
    //{
    //    Debug.Log($"2: ProductivityUnit: m_Target is {m_Target?.GetType().Name}");
    //    Debug.Log($"3: m_Target is now: {m_Target?.GetName()}");
    //    // start of new code
    //    if (m_CurrentPile == null)
    //    {
    //        ResourcePile pile = m_Target as ResourcePile;
    //        Debug.Log($"4: Checking if m_Target is a ResourcePile: {pile != null}");
    //        if (pile != null)
    //        {
    //            m_CurrentPile = pile;
    //            m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
    //            Debug.Log($"5: Productivity increased for {m_CurrentPile.Item.Name} by {ProductivityMultiplier * 100}%");
    //        }


    //    }
    // end of new code
    //}

    protected override void BuildingInRange()
    {
        ResourcePile pile = m_Target as ResourcePile;
        if (pile == null) return;

        float distance = Vector3.Distance(transform.position, pile.transform.position);
        //if (pile != null)
        //{
            if (distance < 2f)
            {
                if (!m_IsBoosting)
                {
                    m_CurrentPile = pile;
                    m_CurrentPile.ProductionSpeed *= ProductivityMultiplier;
                    m_IsBoosting = true;
                    Debug.Log("Boost started");
                }
            }
            else
            {
                if (m_IsBoosting)
                {
                    //Debug.Log("01" + m_CurrentPile.ProductionSpeed);
                    m_CurrentPile.ProductionSpeed /= ProductivityMultiplier;
                    m_CurrentPile = null;
                    m_IsBoosting = false;
                    Debug.Log("Boost ended");
                }
            }
        //}
    }

}
