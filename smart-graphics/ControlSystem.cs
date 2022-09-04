using System;
using System.Collections.Generic;
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

                        foreach (KeyValuePair<uint,SmartObject> pair in smartPanel.SmartObjects)
                        {
                            pair.Value.SigChange += SmartObjectSigChange;
                        }
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

       private void SmartObjectSigChange(GenericBase currentDevice, SmartObjectEventArgs args)
       {
           CrestronConsole.PrintLine("Smart Object used: {0}", args.SmartObjectArgs.ID);

           switch ((PanelSmartObjectIDs)args.SmartObjectArgs.ID)
           {
               case PanelSmartObjectIDs.smartDpad:
                   break;
               case PanelSmartObjectIDs.smartButtonList:
                   break;
               case PanelSmartObjectIDs.smartReferenceList:
                   break;

               default:
                   throw new ArgumentOutOfRangeException();
           }
       }
       
       private enum PanelSmartObjectIDs
       {
           smartDpad = 1,
           smartButtonList = 2,
           smartReferenceList = 3
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