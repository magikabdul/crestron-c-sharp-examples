using System;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO; // For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.Diagnostics;		    	// For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.UI; // For Generic Device Support

namespace smart_graphics
{
    public class ControlSystem : CrestronControlSystem
    {
        private XpanelForSmartGraphics smartPanel;
        
       public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;
                
                smartPanel = new XpanelForSmartGraphics(0x03, this);
                
                //all types of signals except the smart object
                smartPanel.SigChange += SmartPanel_SigChange;
                
                //sgd
                var SGDFilePath = Path.Combine(Directory.GetApplicationDirectory(), "client/smart-graphics.sgd");
                
                if (smartPanel.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                {
                    ErrorLog.Error("Failure in smart panel registration {0)", smartPanel.RegistrationFailureReason);
                }
                else
                {
                    if (File.Exists(SGDFilePath))
                    {
                        smartPanel.LoadSmartObjects(SGDFilePath);
                    }
                    else
                    {
                        ErrorLog.Error("Invalid path {0}", SGDFilePath);
                    }
                }
                
                
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        private void SmartPanel_SigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            // throw new NotImplementedException();
        }

        public override void InitializeSystem()
        {
            try
            {

            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }
    }
}