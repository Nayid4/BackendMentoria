﻿
namespace Mentoria.Shared.Communication.Consumer.Manager
{
    public interface IConsumerManager<TMessage>
    {
        void RestartExecution();
        void StopExecution();
        CancellationToken GetCancellationToken();
    }
}
