using System.Diagnostics.Tracing;
[EventSource(Name = "Application Insights")]
public class ApplicationInsightsEventSource : EventSource
{
    public class Keywords
    {
        public const EventKeywords Starting = (EventKeywords)1;
        public const EventKeywords Random = (EventKeywords)2;
        public const EventKeywords Diagnostic = (EventKeywords)4;
        public const EventKeywords Tick = (EventKeywords)8;
    }

    public class Tasks
    {
        public const EventTask StartingApp = (EventTask)1;
        public const EventTask RandomNumber = (EventTask)2;
        public const EventTask TimerTick = (EventTask)3;
    }

    private static ApplicationInsightsEventSource _log = new ApplicationInsightsEventSource();
    private ApplicationInsightsEventSource() { }
    public static ApplicationInsightsEventSource Log { get { return _log; } }

    [Event(1, Message = "Random number {0}",
    Opcode = EventOpcode.Send,
    Task = Tasks.RandomNumber,
    Keywords = Keywords.Random,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void LogRandom(int random)
    {
        this.WriteEvent(1,random);
    }

    [Event(2, Message = "Starting up",
    Opcode = EventOpcode.Start,
    Task = Tasks.StartingApp,
    Keywords = Keywords.Starting,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Startup()
    {
        this.WriteEvent(2);
    }
    [Event(3, Message = "Timer tick triggered {0} time(s)",
    Opcode = EventOpcode.Info,
    Task = Tasks.TimerTick,
    Keywords = Keywords.Tick,
    Level = EventLevel.Warning,
    Version = 1)]
    internal void TimerTick(int cont)
    {
        this.WriteEvent(3,cont);
    }

}