using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MassTransit.Pipeline;
using MassTransit.Saga;
using MassTransit;
using MassTransit.Util;

namespace Coordinator
{
    public class SagaRepositoryLearningShim<TSaga> :
        ISagaRepository<TSaga>
        where TSaga : class, ISaga
    {
        IndexedSagaDictionary<TSaga> _sagas;

        public SagaRepositoryLearningShim()
        {
            _sagas = new IndexedSagaDictionary<TSaga>();
        }

        public IEnumerable<Action<IConsumeContext<TMessage>>> GetSaga<TMessage>(
            IConsumeContext<TMessage> context,
            Guid sagaId, 
            InstanceHandlerSelector<TSaga, TMessage> selector, 
            ISagaPolicy<TSaga, TMessage> policy)
            where TMessage : class
        {
            bool needToLeaveSagas = true;
            Monitor.Enter(_sagas);
            try
            {
                TSaga instance = _sagas[sagaId];

                if (instance == null)
                {
                    if (policy.CanCreateInstance(context))
                    {
                        instance = policy.CreateInstance(context, sagaId);
                        _sagas.Add(instance);

                        lock (instance)
                        {
                            Monitor.Exit(_sagas);
                            needToLeaveSagas = false;

                            yield return x =>
                                {
                                    Console.WriteLine("SAGA: {0} Creating New {1} for {2}",
                                            typeof(TSaga).ToFriendlyName(), instance.CorrelationId,
                                            typeof(TMessage).ToFriendlyName());

                                        foreach (var callback in selector(instance, x))
                                        {
                                            callback(x);
                                        }

                                        if (policy.CanRemoveInstance(instance))
                                            _sagas.Remove(instance);
                                };
                        }
                    }
                    else
                    {
                            Console.WriteLine("SAGA: {0} Ignoring Missing {1} for {2}", typeof(TSaga).ToFriendlyName(),
                                sagaId,
                                typeof(TMessage).ToFriendlyName());
                    }
                }
                else
                {
                    if (policy.CanUseExistingInstance(context))
                    {
                        Monitor.Exit(_sagas);
                        needToLeaveSagas = false;
                        lock (instance)
                        {
                            yield return x =>
                                {
                                        Console.WriteLine("SAGA: {0} Using Existing {1} for {2}",
                                            typeof(TSaga).ToFriendlyName(), instance.CorrelationId,
                                            typeof(TMessage).ToFriendlyName());

                                        foreach (var callback in selector(instance, x))
                                        {
                                            callback(x);
                                        }

                                        if (policy.CanRemoveInstance(instance))
                                            _sagas.Remove(instance);
                                };
                        }
                    }
                    else
                    {
                            Console.WriteLine("SAGA: {0} Ignoring Existing {1} for {2}", typeof(TSaga).ToFriendlyName(),
                                sagaId, typeof(TMessage).ToFriendlyName());
                    }
                }
            }
            finally
            {
                if (needToLeaveSagas)
                    Monitor.Exit(_sagas);
            }
        }

        public IEnumerable<Guid> Find(ISagaFilter<TSaga> filter)
        {
            return _sagas.Where(filter).Select(x => x.CorrelationId);
        }

        public IEnumerable<TSaga> Where(ISagaFilter<TSaga> filter)
        {
            return _sagas.Where(filter);
        }

        public IEnumerable<TResult> Where<TResult>(ISagaFilter<TSaga> filter, Func<TSaga, TResult> transformer)
        {
            return _sagas.Where(filter).Select(transformer);
        }

        public IEnumerable<TResult> Select<TResult>(Func<TSaga, TResult> transformer)
        {
            return _sagas.Select(transformer);
        }

        public void Add(TSaga newSaga)
        {
            lock (_sagas)
                _sagas.Add(newSaga);
        }

        public void Remove(TSaga saga)
        {
            lock (_sagas)
                _sagas.Remove(saga);
        }
    }

    public static class ObjectExtensions
    {
        public static string ToFriendlyName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return type.FullName;
            }

            string name = type.GetGenericTypeDefinition().FullName;
            if (name == null)
                return type.Name;

            name = name.Substring(0, name.IndexOf('`'));
            name += "<";

            Type[] arguments = type.GetGenericArguments();
            for (int i = 0; i < arguments.Length; i++)
            {
                if (i > 0)
                    name += ",";

                name += arguments[i].Name;
            }

            name += ">";

            return name;
        }
    }
}
