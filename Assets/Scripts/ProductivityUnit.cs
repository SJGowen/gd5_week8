using UnityEngine;

public class ProductivityUnit : Unit
{
    ResourcePile currentPile = null;
    public float productivityMultiplier = 2f;

    protected override void BuildingInRange()
    {
        ResourcePile pile = m_Target as ResourcePile;

        if (pile != null && currentPile != pile)
        {
            // If we are in range of a resource pile, we increase the productivity of the pile
            if (currentPile != null)
            {
                currentPile.UnregisterProductivityUnit(this);
            }

            currentPile = pile;
            currentPile.RegisterProductivityUnit(this);
        }
    }

    void ResetProductivity()
    {
        if (currentPile != null)
        {
            // If we were in range of a resource pile and now we are not, we reset the productivity
            currentPile.UnregisterProductivityUnit(this);
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

    private void OnDisable()
    {
        ResetProductivity();
    }
}

