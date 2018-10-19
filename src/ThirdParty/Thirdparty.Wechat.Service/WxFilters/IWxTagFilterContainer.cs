namespace Thirdparty.Wechat.Service.WxFilters
{
    public interface IWxTagFilterContainer
    {
        IWxTagFilter TagFilter { get; set; }
    }

    public interface IWxTagFilter
    {
        /// <summary>
        /// 群发
        /// </summary>
          bool IsAll { get;  }

        /// <summary>
        /// 标签号，IsAll为True的时候， TagId可不填
        /// </summary>
          string TagId { get;   }
    }
}