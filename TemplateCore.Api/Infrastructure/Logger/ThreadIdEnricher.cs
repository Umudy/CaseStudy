using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace TemplateCore.Api.Infrastructure.Logger
{
    public class ThreadIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "ThreadId", Thread.CurrentThread.ManagedThreadId));
        }
    }
}
