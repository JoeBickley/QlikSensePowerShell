using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Diagnostics;
using Newtonsoft.Json;


namespace QlikSensePS
{
	public class GetListCommand : Cmdlet
	{
		#region Class Members

		private string _serverURL;
		private string _searchTerm;
        private string _objecttype;

		private QRSNTLMWebClient Client;
		private List<object> ObjectList;

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

        [Parameter(Mandatory = false, HelpMessage = "Type of object to find")]
        public string ObjectType
        {
            get { return _objecttype; }
            set { _objecttype = value; }
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
			GetObjectList();
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			foreach (object item in ObjectList)
			{
				WriteObject(item);
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
		private void GetObjectList()
		{
			Client = new QRSNTLMWebClient(_serverURL);

            System.Diagnostics.Debugger.Launch();

            if (_searchTerm != null)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("filter", "name eq '" + _searchTerm + "'");

                ObjectList = JsonConvert.DeserializeObject<List<object>>(Client.Get("/qrs/"+_objecttype,param));
            }
            else
            {
                ObjectList = JsonConvert.DeserializeObject<List<object>>(Client.Get("/qrs/"+ _objecttype));
            }

		}

		#endregion
	}
}
