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

    [Event(1, Message = "Random number 1",
    Opcode = EventOpcode.Send,
    Task = Tasks.RandomNumber,
    Keywords = Keywords.Random,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Log1()
    {
        this.WriteEvent(1);
    }

    [Event(2, Message = "Random number 2",
    Opcode = EventOpcode.Send,
    Task = Tasks.RandomNumber,
    Keywords = Keywords.Random,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Log2()
    {
        this.WriteEvent(2);
    }

    [Event(3, Message = "Random number 3",
    Opcode = EventOpcode.Send,
    Task = Tasks.RandomNumber,
    Keywords = Keywords.Random,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Log3()
    {
        this.WriteEvent(3);
    }
    [Event(4, Message = "Random number 4",
    Opcode = EventOpcode.Send,
    Task = Tasks.RandomNumber,
    Keywords = Keywords.Random,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Log4()
    {
        this.WriteEvent(4);
    }
    [Event(5, Message = "Random number 5",
    Opcode = EventOpcode.Send,
    Task = Tasks.RandomNumber,
    Keywords = Keywords.Random,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Log5()
    {
        this.WriteEvent(5);
    }
    [Event(6, Message = "Starting up",
    Opcode = EventOpcode.Start,
    Task = Tasks.StartingApp,
    Keywords = Keywords.Starting,
    Level = EventLevel.Informational,
    Version = 1)]
    internal void Startup()
    {
        this.WriteEvent(6);
    }
    [Event(7, Message = "Timer tick triggered {0} time(s)",
    Opcode = EventOpcode.Info,
    Task = Tasks.TimerTick,
    Keywords = Keywords.Tick,
    Level = EventLevel.Warning,
    Version = 1)]
    internal void TimerTick(int cont)
    {
        this.WriteEvent(7,cont);
    }

}