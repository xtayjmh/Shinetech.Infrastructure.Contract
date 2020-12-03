using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shinetech.Infrastructure.Contract
{
    public class PaginatedSearchRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Required]
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页面容量
        /// </summary>
        [Required]
        public int PageSize { get; set; } = 20;
        /// <summary>
        /// 搜索条件
        /// </summary>
        public List<SearchCondition> Search { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 是否正序排序
        /// </summary>
        public bool OrderByAscent { get; set; } = true;
    }
    public class SearchCondition
    {
        /// <summary>
        /// key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        public ConditionOperation Operation { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence { get; set; }
        /// <summary>
        /// 运算方式
        /// </summary>
        public ConditionRelationship Relationship { get; set; }
        /// <summary>
        /// 优先运算
        /// </summary>
        public List<SearchCondition> Priorities { get; set; }
    }
    public enum ConditionRelationship
    {
        And = 0,
        Or = 1
    }
    public enum ConditionOperation
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal = 0,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual = 1,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 2,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 3,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual = 4,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual = 5,
        /// <summary>
        /// 包含
        /// </summary>
        Contains = 6
    }
}
