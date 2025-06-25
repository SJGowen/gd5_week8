using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;

    private float m_ProductionSpeed = 0.5f;
    public float ProductionSpeed
    {
        get => m_ProductionSpeed;
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("Production speed cannot be negative.");
            }
            else if (value >= 10.0f)
            {
                Debug.LogWarning($"Production speed capped at {10.0f} attempt to set it to {value}.");
                m_ProductionSpeed = 10.0f;
            }
            else if (m_ProductionSpeed == 10.0f && value == 5.0f)
            {
                Debug.Log($"Production speed of {value} attempted to be set.\n" +
                    "Production speed calculated by number of Productivity Units in attendance.");
                float calculatedSpeed = 0.5f;
                int prodUnitCount = ProductivityUnit.ProductivityUnits;
                while (prodUnitCount > 0)
                {
                    calculatedSpeed *= 2;
                    prodUnitCount--;
                }
                calculatedSpeed = calculatedSpeed > 10.0f ? 10.0f : calculatedSpeed;
                m_ProductionSpeed = calculatedSpeed;
            }
            else
            {
                m_ProductionSpeed = value;
            }
        }
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
