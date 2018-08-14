using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    class HardwareViewModel
    {
        public HardwareViewModel(HardwareResourceBase resource)
        { 
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            Expert0ResourceName = resource.Experts[0].ResourceName;
            Expert0ProgrammaticName = resource.Experts[0].ExpertProgrammaticName;

            ProductResource productResource = resource as ProductResource; 


            if (productResource != null)
            {       
                    TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);
                    
                    try
                    {
                        Temperature = sensors[0].Reading.ToString(); //Sensor 0 is the internal temperature
                    }
                    catch
                    {
                        Temperature = "0.00";
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
    }
}
