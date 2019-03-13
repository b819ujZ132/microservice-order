using System.Reflection;

namespace order.DomainModels {
    /// <summary>
    /// Health check response model
    /// </summary>
    public class HealthCheck
    {
        /// <summary>
        /// Gets the application name, taken from the assembly name.
        /// </summary>
        /// <value>The application.</value>
        public string Application { get; } = Assembly.GetEntryAssembly().GetName().Name;

        /// <summary>
        /// Gets the version, taken from the VersionPrefix+VersionSuffix properties of the project file.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; } = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        /// <summary>
        /// Gets or sets a value indicating whether this application is healthy.
        /// </summary>
        /// <value><c>true</c> if healthy; otherwise, <c>false</c>.</value>
        public bool Healthy { get; set; } = true;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; } = "Green.";

        /// <summary>
        /// Returns an unhealthy response with a specific message.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="message">Message.</param>
        public static HealthCheck UnhealthyResult(string message)
        {
            return new HealthCheck {
                Healthy = false,
                Message = message
            };
        }
    }
}