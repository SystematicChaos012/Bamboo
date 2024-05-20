using Blog.Core.Topics;
using Blog.Core.Topics.Entities;
using Blog.Core.Topics.ValueObjects;

namespace SharedKernel.Tests
{
    public class AggregateRoot_Tests
    {
        /*
         * 目前未编辑任何单元测试，此用例是用于测试聚合根工作情况的
         * 
         * 目前版本不支持半自动属性和部分属性，功能实现并不优雅，未来会配合源生成器 + 部分属性 + 半自动属性并且配合 EF Core 的属性模型来完成后续的一系列操作
         * 
         */
        [Fact]
        public void For_Debug()
        {
            var topic = new TestTopic(TestTopicId.NewTopicId(), "Title", "Content");

            topic.Authors.Add(new TestTopicAuthor(TestTopicAuthorId.NewTopicAuthorId(), topic.Id, TestAuthorId.NewAuthorId(), "authorName"));
        }
    }
}
