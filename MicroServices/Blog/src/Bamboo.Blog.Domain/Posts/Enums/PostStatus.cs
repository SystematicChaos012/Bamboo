using Ardalis.SmartEnum;

namespace Bamboo.Posts.Enums
{
    /// <summary>
    /// 主题状态
    /// </summary>
    public sealed class PostStatus(string name, int value) : SmartEnum<PostStatus>(name, value)
    {
        /// <summary>
        /// 草稿
        /// </summary>
        public static readonly PostStatus Draft = new(nameof(Draft), 1);

        /// <summary>
        /// 已发布
        /// </summary>
        public static readonly PostStatus Published = new(nameof(Published), 2);
    }
}
