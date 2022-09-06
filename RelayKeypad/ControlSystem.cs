using System;
using Crestron.SimplSharp; // For Basic SIMPL# Classes
using Crestron.SimplSharpPro; // For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread; // For Threading
using Crestron.SimplSharpPro.Diagnostics; // For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro.Keypads;
using Crestron.SimplSharpPro.UI; // For Generic Device Support

namespace RelayKeypad
{
    public class ControlSystem : CrestronControlSystem
    {
        private C2niCb keypad;
        private XpanelForSmartGraphics panel;

        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                if (this.SupportsCresnet)
                {
                    keypad = new C2niCb(0x30, this);
                    keypad.ButtonStateChange += new ButtonEventHandler(KeypadButtonStateChange);
                    keypad.Register();
                }

                panel = new XpanelForSmartGraphics(0x03, this);
                panel.SigChange += PanelSigChange;
                panel.Register();

                if (this.SupportsRelay)
                {
                    this.RelayPorts[1].Register();
                    this.RelayPorts[1].StateChange += new RelayEventHandler(RelayStateChange);
                    
                    this.RelayPorts[2].Register();
                    this.RelayPorts[2].StateChange += new RelayEventHandler(RelayStateChange);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        private void RelayStateChange(Relay relay, RelayEventArgs args)
        {
            
        }

        private void KeypadButtonStateChange(GenericBase device, ButtonEventArgs args)
        {
        }

        private void PanelSigChange(BasicTriList currentDevice, SigEventArgs args)
        {
            switch (args.Sig.Number)
            {
                case 7:
                    RelayPorts[1].Open();
                    break;
                case 8: 
                    RelayPorts[1].Close();
                    break;
                default:
                    break;
            }
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