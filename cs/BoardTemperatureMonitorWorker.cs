using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.BoardTemperatureMonitor
{
    class BoardTemperatureMonitorWorker : INotifyPropertyChanged
    {
        private bool canBeginRunAudit;
        private bool canClickStop;
        private List<HardwareViewModel> allHardwareResources;

        public BoardTemperatureMonitorWorker()
        {
            CanBeginRunAudit = true;
            CanClickStop = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanBeginRunAudit
        {
            get { return canBeginRunAudit; }
            set
            {
                if (canBeginRunAudit != value)
                {
                    canBeginRunAudit = value;
                    NotifyPropertyChanged("CanBeginRunAudit");
                }
            }
        }
        
        public bool CanClickStop
        {
            get { return canClickStop; }
            set
            {
                if (canClickStop != value)
                {
                    canClickStop = value;
                    NotifyPropertyChanged("CanClickStop");
                }
            }
        }
        
        public string Target
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public bool StopMonitor
        {
            get;
            set;
        }

        public IEnumerable<HardwareViewModel> FilteredHardwareResources
        {
            get
            {
                if (AllHardwareResources == null)
                {
                    return Enumerable.Empty<HardwareViewModel>();
                }
                return from resource in AllHardwareResources
                       where !(resource.NumberOfExperts == 1 && resource.Expert0ProgrammaticName.Equals("network"))
                       select resource;
            }
        }

        private List<HardwareViewModel> AllHardwareResources
        {
            get { return allHardwareResources; }
            set
            {
                if (allHardwareResources != value)
                {
                    allHardwareResources = value;
                    NotifyPropertyChanged("FilteredHardwareResources");
                }
            }
        }

        public void StartRunAudit(string password)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    CanBeginRunAudit = false;
                    StopMonitor = false;
                    try
                    {
                        // Because the view does not allow modifying resources, there isn't a need to keep
                        // the raw HardwareResourceBase objects after creating the view models.
                        AllHardwareResources = null;
                        var session = new SystemConfiguration.SystemConfiguration(Target, Username, password);

                        SystemConfiguration.Filter filter = new SystemConfiguration.Filter(session); 
                        filter.IsDevice = true;
                        filter.SupportsCalibration = true;
                        filter.IsPresent = SystemConfiguration.IsPresentType.Present;
                        filter.IsSimulated = true; //********SET TO TRUE FOR TESTING 

                        ResourceCollection rawResources = session.FindHardware(filter);

                        CanClickStop = true;
                        //maybe iterate through list here?
                        while (StopMonitor == false)
                        {
                            AllHardwareResources =
                                (from resource in rawResources
                                 select new HardwareViewModel(resource)).ToList();
                            System.Threading.Thread.Sleep(100);
                        }
                        //or use this to find the devices, and then query temperature
                    }
                    catch (SystemConfigurationException ex)
                    {
                        if (ex.ErrorCode.ToString() != "-2147220623") //Do not report error if device does not support self calibration (-2147220623)
                        {
                            string errorMessage = string.Format("Find Hardware threw a System Configuration Exception.\n\nErrorCode: {0:X}\n{1}", ex.ErrorCode, ex.Message);
                            MessageBox.Show(errorMessage, "System Configuration Exception");
                        }
                    }
                    finally
                    {
                        CanBeginRunAudit = true;
                        CanClickStop = false;
                    }
                }
            );
            worker.RunWorkerAsync();
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
