using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.CalibrationAudit
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
                try
                {
                    Model = productResource.ProductName;
                    SerialNumber = productResource.SerialNumber;

                    if (productResource.SupportsInternalCalibration)
                    {
                        IntLastCalDate = productResource.InternalCalibrationDate.ToString("MM-dd-yyyy");
                        IntLastCalTemp = productResource.InternalCalibrationTemperature.ToString();
                    }
                    else
                    {
                        IntLastCalDate = "";
                    }

                    if (productResource.SupportsExternalCalibration)
                    {
                        ExtLastCalDate = productResource.ExternalCalibrationDate.ToString("MM-dd-yyyy");
                        ExtLastCalTemp = productResource.ExternalCalibrationTemperature.ToString();
                        RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString("MM-dd-yyyy"); 

                        if (System.DateTime.Compare(productResource.ExternalCalibrationDueDate, System.DateTime.Now) < 0)
                        {
                            CalibrationOverdue = true;
                        }
                        else
                        {
                            CalibrationOverdue = false;
                        }

                    }
                    else
                    {
                        ExtLastCalDate = "";
                        ExtLastCalTemp = "";
                        RecommendedNextCal = "";
                    }

                    TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);

                    try
                    {
                        Temperature = sensors[0].Reading.ToString(); //Sensor 0 is the internal temperature
                    }
                    catch
                    {
                        Temperature = "";
                    }

                }
                catch (SystemConfigurationException ex)
                {
                    Error = ex.ErrorCode.ToString();
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

        public string Model
        {
            get;
            private set;
        }

        public string SerialNumber
        {
            get;
            private set;
        }

        public string IntLastCalDate
        {
            get;
            private set;
        }

        public string IntLastCalTemp
        {
            get;
            private set;
        }

        public string ExtLastCalDate
        {
            get;
            private set;
        }

        public string ExtLastCalTemp
        {
            get;
            private set;
        }

        public string RecommendedNextCal
        {
            get;
            private set;
        }

        public string Temperature
        {
            get;
            private set;
        }

        public string Error
        {
            get;
            private set;
        }

        public bool CalibrationOverdue
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
