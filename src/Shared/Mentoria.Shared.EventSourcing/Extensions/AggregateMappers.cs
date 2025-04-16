

namespace Mentoria.Shared.EventSourcing.Extensions
{
    public static class AggregateMappers
    {
        public static AggregateChange ToAggregatechange(AggregateChangeDto change)
        {
            return new AggregateChange(
                change.Content,
                change.AggregateId,
                change.GetType(),
                change.TransactionId,
                change.AggregateVersion,
                false
            );
        }

        public static AggregateChangeDto ToTypedAggregateChangeDto(Guid Id, string aggregateType, AggregateChange change)
        {
            return new AggregateChangeDto(
                change.Content,
                Id,
                aggregateType,
                change.TransactionId,
                change.Version
            ); 
        }
    }
}
