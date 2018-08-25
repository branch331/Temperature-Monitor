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
                    Temperature = sensors[0].Reading.ToString("0.00"); //Sensor 0 is the internal temperature
                    
                    if (System.Convert.ToDouble(Temperature) > temperatureLimit)
                    {
                        Limit_Reached = true;
                        //System.Windows.MessageBox.Show(string.Format("{0}", UserAlias));
                    }
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

        public bool Limit_Reached
        {
            get;
            private set;
        }
    }
}
