using NationalInstruments.SystemConfiguration;
using System.Linq;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    class HardwareViewModel // Internal class
    {
        public HardwareViewModel(HardwareResourceBase resource, double temperatureLimit)
        { 
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            ExpertResourceName = resource.Experts[0].ResourceName;
            ExpertProgrammaticName = resource.Experts[0].ExpertProgrammaticName;

            ProductResource productResource = resource as ProductResource;

            if (productResource == null)
            {
                return;
            }

            LimitReached = false;

            try
            {
                TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);
                Temperature = sensors[0].Reading.ToString("0.00"); // Sensor 0 is the internal temperature.

                LimitReached = sensors.Any(s => s.Reading > temperatureLimit);
                //if (System.Convert.ToDouble(Temperature) > temperatureLimit)
                //{
                //    LimitReached = true;
                //}
            }
            catch
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