using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Newtonsoft.Json;
using System.Diagnostics;

namespace QlikSensePS
{
	public class ExecuteTaskCommand : Cmdlet
	{
		#region Class Members

		private string _serverURL;
		private string _TaskID;
        private string _TaskName;
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

		[Parameter(Mandatory = false, HelpMessage = "Id of task to execute")]
		public string TaskID
		{
			get { return _TaskID; }
			set{ _TaskID = value;}
		}

        [Parameter(Mandatory = false, HelpMessage = "Name of task to execute")]
        public string TaskName
        {
            get { return _TaskName; }
            set { _TaskName = value; }
        }

		#endregion

		#region Override Methods
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			StartTask();
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
        private void StartTask()
        {

            taskstartresult startresult;
            Client = new QRSNTLMWebClient(_serverURL);

            if (_TaskID != null || _TaskName != null)
            {

                if (_TaskID != null)
                {
                    startresult = JsonConvert.DeserializeObject<taskstartresult>(Client.Post("/qrs/task/" + _TaskID + "/start/synchronous", ""));
                }
                else
                {
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add("filter", "name eq '" + _TaskName + "'");
                    List<task> searchtasks = JsonConvert.DeserializeObject<List<task>>(Client.Get("/qrs/task/", param));

                    if (searchtasks.Count == 0 || searchtasks.Count > 1)
                    {
                        throw new Exception("Either no task found or multiple tasks found");
                    }

                    startresult = JsonConvert.DeserializeObject<taskstartresult>(Client.Post("/qrs/task/" + searchtasks[0].ID + "/start/synchronous", ""));
                }

                Dictionary<string, string> filter = new Dictionary<string, string>();
                filter.Add("filter", "ExecutionID eq " + startresult.value);


                string checkexec;
                checkexec = Client.Get("/qrs/executionresult/full", filter);
                List<executionresult> execresults = JsonConvert.DeserializeObject<List<executionresult>>(checkexec);
                _taskresult = execresults[0];

                return;
            }

            throw new Exception("task id or task name not supplied");
        }

		#endregion
	}
}
