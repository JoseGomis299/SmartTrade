namespace SmartTradeLib.Entities;

public partial class BizumInfo : IPayMethod
{
    public BizumInfo(string telephonNumber)
    {
        TelephonNumber = telephonNumber;
    }

    public void Pay(float amount)
    {
        
    }
}