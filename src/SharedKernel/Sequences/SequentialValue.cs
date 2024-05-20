namespace SharedKernel.Sequences
{
    /// <summary>
    /// 有序的值
    /// </summary>
    public class SequentialValue
    {
        private uint _value;

        /// <summary>
        /// 值
        /// </summary>
        public uint Value => _value;

        /// <summary>
        /// 获取一个值
        /// </summary>
        public uint Next()
        {
            return Interlocked.Increment(ref _value);
        }
    }
}
