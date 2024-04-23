namespace Audit.EntityFrameworkCore
{
    public class MyEntity : IConcurrencyStamp, ICreationTime, ICreator<int>, IModificationTime, IModifier<int>, IDeletionTime, IDeleter<int>, ILogicalDeletion
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
