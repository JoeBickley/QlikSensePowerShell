using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Newtonsoft.Json;
using System.Diagnostics;

namespace QlikSensePS
{
	public class CheckTaskCommand : Cmdlet
	{
		#region Class Members

		private string _serverURL;
		private string _TaskID;
        private string _ExecutionID;
        private executionresult _taskresult;

		private QRSNTLMWebClient Client;


		#endregion

		#region Parameters
		/// <summary>
		/// 
		/// </summary>
		[Parameter(Mandatory = true, HelpMessage="Specify server URL")]
		[ValidateNotNullOrEmpty]
        public string ServerURL
		{
            get { return _serverURL; }
            set { _serverURL = value; }
		}

		[Parameter(Mandatory = false, HelpMessage = "Id of task, to check last execution of the task")]
		public string TaskID
		{
			get { return _TaskID; }
			set{ _TaskID = value;}
		}

        [Parameter(Mandatory = false, HelpMessage = "Id of a specific exection of a task")]
        public string ExecutionID
        {
            get { return _ExecutionID; }
            set { _ExecutionID = value; }
        }

		#endregion

		#region Override Methods
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckTask();
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
            WriteObject(_taskresult);
		}

		protected override void EndProcessing()
		{
			base.EndProcessing();
		}

		protected override void StopProcessing()
		{
			base.StopProcessing();
		}
		#endregion

		#region Data Access
        private void CheckTask()
        {

            System.Diagnostics.Debugger.Launch();

            Client = new QRSNTLMWebClient(_serverURL);
            
            if (TaskID != null)
            {
                string findtask;
                findtask = Client.Get("/qrs/reloadtask/" + TaskID);
                reloadtask task = JsonConvert.DeserializeObject<reloadtask>(findtask);

                Dictionary<string, string> filter = new Dictionary<string, string>();
                filter.Add("filter", "ExecutionID eq " + task.operational.lastExecutionResult.id);

                string checkexec;
                checkexec = Client.Get("/qrs/executionresult/full", filter);
                List<executionresult> execresults = JsonConvert.DeserializeObject<List<executionresult>>(checkexec);
                _taskresult = execresults[0];

                return;
            }

            if (ExecutionID != null)
            {
                Dictionary<string, string> filter = new Dictionary<string, string>();
                filter.Add("filter", "ExecutionID eq " + ExecutionID);

                string checkexec;
                checkexec = Client.Get("/qrs/executionresult/full", filter);
                List<executionresult> execresults = JsonConvert.DeserializeObject<List<executionresult>>(checkexec);
                _taskresult = execresults[0];
                return;
            }

            throw new Exception("TaskID or ExecutionID not supplied"); 

        }

		#endregion
	}
}
