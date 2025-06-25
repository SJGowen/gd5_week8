using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;

    private float m_BaseProductionSpeed = 0.5f;
    private float m_ProductionSpeed = 0.5f;
    public float ProductionSpeed => m_ProductionSpeed;

    private HashSet<ProductivityUnit> productivityUnits = new();

    public void RegisterProductivityUnit(ProductivityUnit unit)
    {
        if (productivityUnits.Add(unit))
        {
            RecalculateProductionSpeed();
        }
    }

    public void UnregisterProductivityUnit(ProductivityUnit unit)
    {
        if (productivityUnits.Remove(unit))
        {
            RecalculateProductionSpeed();
        }
    }

    private void RecalculateProductionSpeed()
    {
        float speed = m_BaseProductionSpeed;

        foreach (var unit in productivityUnits)
        {
            speed *= unit.productivityMultiplier;
        }

        m_ProductionSpeed = Mathf.Min(speed, 10.0f); // Cap the production speed at 10.0f
    }

    private float m_CurrentProduction = 0.0f;

    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += ProductionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {ProductionSpeed}/s";
        
    }
}
