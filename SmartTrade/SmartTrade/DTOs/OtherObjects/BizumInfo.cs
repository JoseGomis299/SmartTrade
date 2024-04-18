namespace SmartTrade.Entities;

public class BizumInfo
{
    public string TelephonNumber { get; set; }

    public BizumInfo(string telephonNumber)
    {
        TelephonNumber = telephonNumber;
    }
}