using System;

namespace ALS.Tools
{
    /// <summary>
    /// EventBus
    /// </summary>
    public interface IEvent { }

    /// <summary>
    /// EventBus
    /// </summary>
    internal interface IEventBinding<T>
	{
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
	}

    /// <summary>
    /// EventBus
    /// </summary>
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
	{
        Action<T> onEvent = _ => { };
        Action onEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent
		{
            get => onEvent;
            set => onEvent = value;
		}

        Action IEventBinding<T>.OnEventNoArgs
		{
            get => onEventNoArgs;
            set => onEventNoArgs = value;
		}

        public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;
        public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

        public void Add(Action onEvent) => onEventNoArgs += onEvent;
        public void Remove(Action onEvent) => onEventNoArgs -= onEvent;

        public void Add(Action<T> onEvent) => this.onEvent += onEvent;
        public void Remove(Action<T> onEvent) => this.onEvent -= onEvent;
    }
}
