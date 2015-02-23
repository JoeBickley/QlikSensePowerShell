using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ComponentModel;

namespace QlikSensePS
{
	[RunInstaller(true)]
	public class QlikSensePSSnapIn : CustomPSSnapIn
	{
		private Collection<CmdletConfigurationEntry> _cmdlets;
		/// <summary>
		/// Gets description of powershell snap-in.
		/// </summary>
		public override string Description
		{
            get { return "Qlik Sense cmdlet by JBC@QLIK.com"; }
		}

		/// <summary>
		/// Gets name of power shell snap-in
		/// </summary>
		public override string Name
		{
			get { return "QlikSensePSSnapIn"; }
		}

		/// <summary>
		/// Gets name of the vendor
		/// </summary>
		public override string Vendor
		{
			get { return "JBC@QLIK.com"; }
		}

		/// <summary>
		/// This method gets called during install time to get list of
		/// Cmdlets that need to be registered.
		/// </summary>
		public override Collection<CmdletConfigurationEntry> Cmdlets
		{
			get
			{
				if (null == _cmdlets)
				{
					_cmdlets = new Collection<CmdletConfigurationEntry>();
					_cmdlets.Add(new CmdletConfigurationEntry("QS-Get-Task", typeof(GetTaskCommand), "QlikSensePS.dll-Help.xml"));
                    _cmdlets.Add(new CmdletConfigurationEntry("QS-Start-Task", typeof(ExecuteTaskCommand), "QlikSensePS.dll-Help.xml"));
                    _cmdlets.Add(new CmdletConfigurationEntry("QS-Check-Task", typeof(CheckTaskCommand), "QlikSensePS.dll-Help.xml"));
                    _cmdlets.Add(new CmdletConfigurationEntry("QS-Get-List", typeof(GetListCommand), "QlikSensePS.dll-Help.xml"));
				}
				return _cmdlets;
			}
		}
	}
}
