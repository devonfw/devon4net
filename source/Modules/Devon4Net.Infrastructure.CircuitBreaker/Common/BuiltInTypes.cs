namespace Devon4Net.Infrastructure.CircuitBreaker.Common
{
    public class BuiltInTypes : IBuiltInTypes
    {
        private List<string> BuiltInTypeObjecNames { get; }

        public BuiltInTypes()
        {
            BuiltInTypeObjecNames = new List<string>();
            BuiltInTypeObjecNames = typeof(Type).Assembly.GetTypes().Where(x => x.IsPublic && x.IsSealed && x.IsSerializable && x.Namespace=="System").Select(x => x.Name).ToList();
        }

        public List<string> GetBuiltInTypeObjecNames()
        {
            return BuiltInTypeObjecNames;
        }
    }
}
