using UnityEngine;

public class ProductivityUnit : Unit
{
    ResourcePile currentPile = null;
    public float productivityMultiplier = 2;

    private static int m_ProductivityUnits = 0;
    public static int ProductivityUnits
    {
        get => m_ProductivityUnits;
        private set
        {
            m_ProductivityUnits = value;
        }
    }

    protected override void BuildingInRange()
    {
        if (currentPile == null)
        {
            ResourcePile pile = m_Target as ResourcePile;

            if (pile != null)
            {
                // If we are in range of a resource pile, we increase the productivity of the pile
                currentPile = pile;
                pile.ProductionSpeed *= productivityMultiplier;
                ProductivityUnits++;
            }
        }
    }

    void ResetProductivity()
    {
        if (currentPile != null)
        {
            // If we were in range of a resource pile and now we are not, we reset the productivity
            ProductivityUnits--;
            currentPile.ProductionSpeed /= productivityMultiplier;
            currentPile = null;
        }
    }

    public override void GoTo(Building target)
    {
        ResetProductivity();
        base.GoTo(target);
    }

    public override void GoTo(Vector3 position)
    {
        ResetProductivity();
        base.GoTo(position);
    }
}
