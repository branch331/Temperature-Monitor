using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    class HardwareViewModel
    {
        public HardwareViewModel(HardwareResourceBase resource, double temperatureLimit)
        { 
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            Expert0ResourceName = resource.Experts[0].ResourceName;
            Expert0ProgrammaticName = resource.Experts[0].ExpertProgrammaticName;

            ProductResource productResource = resource as ProductResource; 


            if (productResource != null)
            {
                Limit_Reached = false;
 
                try
                {
                    TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);
                    Temperature = sensors[0].Reading; //Sensor 0 is the internal temperature
                    
                    if (Temperature > temperatureLimit)
                    {
                        System.Windows.MessageBox.Show(string.Format("Warning! {0} is/are above the temperature limit. Stopping scan...", UserAlias));
                        Limit_Reached = true;
                    }
                }
                catch
                {
                    Temperature = 0;
                }    
            }
        }

        public string UserAlias
        {
            get;
            private set;
        }

        public string Expert0ResourceName
        {
            get;
            private set;
        }

        public string Expert0ProgrammaticName
        {
            get;
            private set;
        }

        public double Temperature
        {
            get;
            private set;
        }

        public int NumberOfExperts
        {
            get;
            private set;
        }

        public bool Limit_Reached
        {
            get;
            private set;
        }
    }
}
