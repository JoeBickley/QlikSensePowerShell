using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikSensePS
{

    public class stream
    {
        public string ID { get; set; }
        public string name { get; set; }
    }

    public class LoginLicenceType
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string assignedTokens { get; set; }
    }

    public class dataconnection
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string connectionstring { get; set; }
        public string type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class systemrule
    {
        public string name { get; set; }
        public string rule { get; set; }
        public string resourceFilter { get; set; }
        public string actions { get; set; }
        public string comment { get; set; }
        public string category { get; set; }
    }

    public class tag
    {
        public string ID { get; set; }
        public string name { get; set; }
    }

    public class CustomProperty
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string valueType { get; set; }
        public string[] objectTypes { get; set; }
        public string[] choiceValues { get; set; }
    }

    public class user
    {
        public string Id { get; set; }
        public string userId { get; set; }
        public string userDirectory { get; set; }
        public string name { get; set; }
        public List<userattribute> attributes { get; set; }
        public List<string> roles { get; set; }
        public List<CustomPropertyApplied> customProperties { get; set; }
    }

    public class CustomPropertyApplied
    {
        public CustomProperty definition { get; set; }
        public string value { get; set; }
    }

    public class userattribute
    {
        public string attributeType { get; set; }
        public string attributeValue { get; set; }
        public string externalId { get; set; }
    }
    public class app
    {
        public string ID { get; set; }
        public string name { get; set; }
        public List<tag> tags { get; set; }
        public List<CustomPropertyApplied> customproperties { get; set; }
        public string modifiedDate { get; set; }
    }

    public class appobject
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string objectType { get; set; }
        public string approved { get; set; }
        public app app { get; set; }
        public user owner { get; set; }
    }

    public class ReloadObject
    {
        public task task { get; set; }
        public compositeEvent[] compositeEvents { get; set; }
    }

    public class task
    {
        public string ID { get; set; }
        public string name { get; set; }
        public int taskType { get; set; }
        public bool enabled { get; set; }
        public app app { get; set; }

    }

    public class compositeEvent
    {
        public string ID { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public int eventType { get; set; }
        public timeConstraint timeConstraint { get; set; }
        public task reloadTask { get; set; }
        public compositeRule[] compositeRules { get; set; }
    }

    public class compositeRule
    {
        public string ruleState { get; set; }
        public task reloadTask { get; set; }
    }

    public class timeConstraint
    {
        public int days { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
    }

	class taskstartresult
    {
        public string value { get; set; }
    }

    public class executionresult
    {
        public string taskID { get; set; }
        public string executionID { get; set; }
        public string appID { get; set; }
        public string executingNodeID { get; set; }
        public int status { get; set; }
        public string startTime { get; set; }
        public string stopTime { get; set; }
        public int duration { get; set; }
    }
	
    public class endpoint
    {
        public string method { get; set; }
        public string fullpath { get; set; }
        public string path { get; set; }
        public string options { get; set; }
        public string primaryobject { get; set; }
        public string jsontemplate { get; set; }
    }

   public class reloadtask
    {
        public string id { get; set; }
        public string name { get; set; }
        public reloadtaskoperational operational { get; set; }

    }

    public class reloadtaskoperational
    {
        public string id { get; set; }
        public reloadtaskoperationallastexecution lastExecutionResult { get; set; }

    }

    public class reloadtaskoperationallastexecution
    {
        public string id { get; set; }     

    }
	
    public class JSONObject
    {
        public string JSON { get; set; }
        public string fullpath { get; set; }
    }

    public class Proxy
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string modifiedDate { get; set; }
        public ProxySettings Settings { get; set; }
    }

    public class ProxySettings
    {
        public List<VirtualProxy> virtualProxyConfigurations { get; set; }
    }

    public class VirtualProxy
    {
        public string prefix { get; set; }
        public string authenticationModuleRedirectUri { get; set; }
        public string sessionCookieHeaderName { get; set; }
        public bool defaultVirtualProxy { get; set; }
    }


}
