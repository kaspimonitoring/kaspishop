using Domain.SharedKernel;

namespace Domain
{
    public class ProductOriginal : AggregateRoot<Guid>
    {
        private ProductOriginal() { } // For EF
        
        public string ExternalId { get; private set; }
        public string Json { get; private set; }
        public string Hash { get; private set; }

        //TODO validate and use ValueObjects instead of primitive obsession
        public ProductOriginal(string externalId, string json, string hash) : base(Guid.NewGuid())
        {
            ExternalId = externalId;
            Json = json;
            Hash = hash;
        }
    }
}