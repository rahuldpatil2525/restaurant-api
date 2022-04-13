using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace JustEat.Operations.Restaurant.Api.Logging
{
    public interface IInstrumentor
    {
        void LogException(int eventId, Exception exception, IDictionary<string, object> customProperties = null);
    }

    public class ApplicationInsightsInstrumentor : IInstrumentor
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightsInstrumentor(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void LogException(int eventId, Exception exception, IDictionary<string, object> customProperties = null)
        {
            var exceptionTelemetry = new ExceptionTelemetry(exception);
            EnrichTelemetryProperties(eventId, customProperties, exceptionTelemetry);
            _telemetryClient.TrackException(exceptionTelemetry);
        }

        private static void EnrichTelemetryProperties(int eventId, IDictionary<string, object> customProperties, ISupportProperties telemetry)
        {
            telemetry.Properties.TryAdd("EventId", eventId.ToString());
        }
    }
}
