////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Resource.cs
//   Description :
//      Regen-able resource class (such as health, stamina & energy)
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
public class Resource
{
    public string resourceName;

    private int curResource_;
    public int curResource
    {
        get { return curResource_; }
        set
        {
            // make sure resource is always above 0 and below the maximum allowed resource
            if (value > maxResource)
                curResource_ = maxResource;
            else if (value >= 0)
                curResource_ = value;
            else
                curResource_ = 0;
        }
    }
    public int maxResource;

    public bool canRegen;
    public float regenRate;
    public float regenAmt;
    private int amt_;


    /// <summary>
    /// Checks if enough of the resource is avaliable to cover the given cost
    /// </summary>
    /// <param name="cost">Cost to check resourse amount against</param>
    /// <returns>Returns true if the cost can be covered, false if not</returns>
    public bool bCheckIfEnough(int cost)
    {
        if (cost > curResource)
            return false;
        else
            return true;
    }

    /// <summary>
    /// Constructor for creating Resource with curResource filled to max allowed
    /// </summary>
    /// <param name="n">Resource Name</param>
    /// <param name="m">Maximum Resource</param>
    /// <param name="regen">Can the resource regen</param>
    /// <param name="rate">Rate at which the resource regens</param>
    /// <param name="amt">Amount to restore each regen tick</param>
    public Resource(string n, int m, bool regen = false, float rate = 0.0f, float amt = 0.0f)
    {
        resourceName = n;
        maxResource = m;
        curResource = m;
        canRegen = regen;
        regenRate = rate;
        regenAmt = amt;
    }

    /// <summary>
    /// Constructor for creating Resource with specified current value
    /// </summary>
    /// <param name="n">Resource Name</param>
    /// <param name="c">Current Resource amount</param>
    /// <param name="m">Maximum Resource amount</param>
    /// <param name="regen">Can the resource regen</param>
    /// <param name="rate">Rate at which the resource regens</param>
    /// <param name="amt">Amount to restore each regen tick</param>
    public Resource(string n, int c, int m, bool regen = false, float rate = 0.0f, float amt = 0.0f)
    {
        resourceName = n;
        curResource = c;
        maxResource = m;
        canRegen = regen;
        regenRate = rate;
        regenAmt = amt;
    }


    /// need to decide on using this to create a coroutine or leave it as is
    /// <summary>
    /// Apply regeneration to the resource, to be called in fixed updates (or co-routines if decided on using those)
    /// </summary>
    /// <returns>Returns true if we were able to regen, false if not</returns>
    public bool regenTick()
    {
        // check if resource is a regen-able resource and make sure its below max allowed
        if (canRegen && curResource_ < maxResource)
        {
            // Regen only when the amount reaches a whole number
            if (regenAmt >= 1)
            {
                amt_ = (int)(regenAmt - (regenAmt % 1));
                curResource += amt_;
                regenAmt -= amt_;
            }
            regenAmt += regenRate;

            // we were able to regen this tick
            return true;
        }
        else
            return false; // regen tick failed, stop regening
    }
}