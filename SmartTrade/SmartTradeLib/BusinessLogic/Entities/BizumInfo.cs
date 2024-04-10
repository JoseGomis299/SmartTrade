namespace SmartTrade.Entities;

public partial class BizumInfo : IPayMethod
{
    public BizumInfo() { }

    public BizumInfo(string telephonNumber)
    {
        TelephonNumber = telephonNumber;
    }

    public void Pay(float amount)
    {
        
    }
}