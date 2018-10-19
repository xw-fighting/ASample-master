namespace Thirdparty.Wechat.Service.WxFilters
{
    public interface IWxCustomFilterContainer
    {
        IWxCustomFilter CustomFilter { get; }
    }

    public interface IWxCustomFilter
    {
        string OpenId { get; set; }
    }
}