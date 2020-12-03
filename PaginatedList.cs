using System.Collections.Generic;

namespace Shinetech.Infrastructure.Contract
{
    public class PaginatedList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 返回的数据集合
        /// </summary>
        public List<T> Items { get; set; }
        public PaginatedList()
        {

        }
        public PaginatedList(int pageIndex, int pageSize, int count, List<T> items)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Items = items ?? new List<T>();
        }
    }
}
