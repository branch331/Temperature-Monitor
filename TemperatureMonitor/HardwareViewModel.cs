using NationalInstruments.SystemConfiguration;
using System.Linq;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    internal class HardwareViewModel 
    {
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

                Temperature = " ";

                for (int i = 0; i < sensors.Length; i++)
                {
                    if (i != sensors.Length - 1)
                    {
                        Temperature += sensors[i].Reading.ToString("0.00") + ", ";
                    }
                    else
                    {
                        Temperature += sensors[i].Reading.ToString("0.00");
                    }
                }

                LimitReached = sensors.Any(s => s.Reading > temperatureLimit);
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