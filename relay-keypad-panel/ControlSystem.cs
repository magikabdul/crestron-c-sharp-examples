using System;
using Crestron.SimplSharp; // For Basic SIMPL# Classes
using Crestron.SimplSharpPro; // For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread; // For Threading
using Crestron.SimplSharpPro.Diagnostics; // For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.Keypads;
using Crestron.SimplSharpPro.UI; // For Generic Device Support

namespace relay_keypad_panel
{
    public class ControlSystem : CrestronControlSystem
    {
        private C2niCb keypad;
        private Xpanel panel;

        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                if (this.SupportsCresnet)
                {
                    keypad = new C2niCb(0x30, this);
                    keypad.ButtonStateChange += new ButtonEventHandler(keypad_ButtonStateChange);
                }

                if (this.SupportsEthernet)
                {
                    panel = new Xpanel(0x03, this);
                    panel.SigChange += PanelSigChange;
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        private void keypad_ButtonStateChange(GenericBase device, ButtonEventArgs args)
        {
            
        }

        private void PanelSigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            
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