using System.Linq;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    internal class HardwareViewModel
    {
        /// <summary>
        /// Class for each device in the system that contains temperature data.
        /// </summary>
        public HardwareViewModel(ProductResource resource, double temperatureLimit)
        {
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            ExpertResourceName = resource.Experts[0].ResourceName;
            ExpertProgrammaticName = resource.Experts[0].ExpertProgrammaticName;
            LimitReached = false;

            try
            {
                TemperatureSensor[] sensors = resource.QueryTemperatureSensors(SensorInfo.Reading);

                Temperature = string.Join(", ", sensors
                    .Select(s => s.Reading.ToString("0.00")));

                LimitReached = sensors
                    .Any(s => s.Reading > temperatureLimit);
            }
            catch (SystemConfigurationException) // If device does not have internal temperature sensor(s), display temperature as "N/A".
            {
                Temperature = "N/A";
            }
        }

        public string UserAlias
        {
            get;
            private set;
        }

        public string ExpertResourceName
        {
            get;
            private set;
        }

        public string ExpertProgrammaticName
        {
            get;
            private set;
        }

        public string Temperature
        {
            get;
            private set;
        }

        public int NumberOfExperts
        {
            get;
            private set;
        }

        public bool LimitReached
        {
            get;
            private set;
        }
    }
}