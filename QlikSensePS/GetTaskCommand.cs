using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Newtonsoft.Json;


namespace QlikSensePS
{
	public class GetTaskCommand : Cmdlet
	{
		#region Class Members

		private string _serverURL;
		private string _searchTerm;

		private QRSNTLMWebClient Client;
		private List<task> _Tasks;

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

		[Parameter(Mandatory = false, HelpMessage = "Filter to use for search")]
		public string SearchTerm
		{
			get { return _searchTerm; }
			set{ _searchTerm = value;}
		}


		#endregion

		#region Override Methods
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			GetTasks();
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			foreach (task task in _Tasks)
			{
				WriteObject(task);
			}
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
		private void GetTasks()
		{
			Client = new QRSNTLMWebClient(_serverURL);

            if (_searchTerm != null)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("filter", "name eq '" + _searchTerm + "'");

                _Tasks = JsonConvert.DeserializeObject<List<task>>(Client.Get("/qrs/task",param));
            }
            else
            {
                _Tasks = JsonConvert.DeserializeObject<List<task>>(Client.Get("/qrs/task"));
            }

		}

		#endregion
	}
}
