using System;
using System.Threading.Tasks;
using Adaptive.ReactiveTrader.Contract.Events.CreditAccount;
using Adaptive.ReactiveTrader.Contract.Events.Trade;
using Adaptive.ReactiveTrader.EventStore.Domain;
using Adaptive.ReactiveTrader.EventStore.EventHandling;
using Adaptive.ReactiveTrader.EventStore.Process;
using Adaptive.ReactiveTrader.Server.TradeExecution.Domain;

namespace Adaptive.ReactiveTrader.Server.TradeExecution
{
    public static class EventHandlers
    {
        public static EventHandlerRouter GetRouter(IProcessRepository processRepository, Func<TradeExecutionProcess> processFactory)
        {
            var router = new EventHandlerRouter();

            router.AddRoute<TradeCreatedEvent>(e => Handle(e, processRepository, processFactory));
            router.AddRoute<CreditReservedEvent>(e => Handle(e, processRepository, processFactory));
            router.AddRoute<CreditLimitBreachedEvent>(e => Handle(e, processRepository, processFactory));
            router.AddRoute<TradeCompletedEvent>(e => Handle(e, processRepository, processFactory));
            router.AddRoute<TradeRejectedEvent>(e => Handle(e, processRepository, processFactory));

            return router;
        }

        private static async Task Handle(IReadEvent<TradeCreatedEvent> @event, IProcessRepository repository, Func<TradeExecutionProcess> factory)
        {
            var process = await repository.GetByIdOrCreateAsync(@event.Payload.TradeId, factory);
            process.Transition(@event);
            await repository.SaveAsync(process);
        }

        private static async Task Handle(IReadEvent<CreditReservedEvent> @event, IProcessRepository repository, Func<TradeExecutionProcess> factory)
        {
            var process = await repository.GetByIdAsync(@event.Payload.TradeId, factory);
            process.Transition(@event);
            await repository.SaveAsync(process);
        }

        private static async Task Handle(IReadEvent<CreditLimitBreachedEvent> @event, IProcessRepository repository, Func<TradeExecutionProcess> factory)
        {
            var process = await repository.GetByIdAsync(@event.Payload.TradeId, factory);
            process.Transition(@event);
            await repository.SaveAsync(process);
        }

        private static async Task Handle(IReadEvent<TradeCompletedEvent> @event, IProcessRepository repository, Func<TradeExecutionProcess> factory)
        {
            var process = await repository.GetByIdAsync(@event.Payload.TradeId, factory);
            process.Transition(@event);
            await repository.SaveAsync(process);
        }

        private static async Task Handle(IReadEvent<TradeRejectedEvent> @event, IProcessRepository repository, Func<TradeExecutionProcess> factory)
        {
            var process = await repository.GetByIdAsync(@event.Payload.TradeId, factory);
            process.Transition(@event);
            await repository.SaveAsync(process);
        }
    }
}