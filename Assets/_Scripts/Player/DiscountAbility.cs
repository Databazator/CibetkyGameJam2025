using UnityEngine;

public class DiscountAbility : PlayerAbility
{
    public float Discount = 0f;

    public override bool AbilityInUse()
    {
        return true;
    }

    public override bool CanUseAbility()
    {
        return true;
    }

    public override void TriggerAbility(Vector3 _)
    { }

    public void IncreaseDiscountAmount(float amount)
    {
        Discount += amount;

        if (Discount > 1f)
        {
            Discount = 1f;
        }
    }

    public void DecreaseDiscountAmount(float amount)
    {
        Discount -= amount;

        if (Discount < 0f)
        {
            Discount = 0f;
        }
    }
}
