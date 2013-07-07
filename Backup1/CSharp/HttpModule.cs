using System.Collections.Generic;
using CruiseControl.Models;
using Nancy;
using Nancy.IO;
using Newtonsoft.Json;

namespace CruiseControl
{
	public class HttpModule : NancyModule
	{
		public HttpModule()
		{
			Get["/"] = x => "Nancy";
			Post["/Command"] = x =>
				{
					var postData = ParseRequestBody(Request.Body);
					return ProcessCommand(postData);
				};
		}

		public string ParseRequestBody(RequestStream requestBody)
		{
			var length = int.Parse(requestBody.Length.ToString());
			var bytes = new byte[length];
			requestBody.Read(bytes, 0, length);

			return System.Text.Encoding.UTF8.GetString(bytes);
		}

		public string ProcessCommand(string parameters)
		{
			// Process the status

			// Create commands to do
			var cmds = new List<Command>();
			cmds.Add(new Command { vesselid = 1, action = "move:north" });
			cmds.Add(new Command { vesselid = 2, action = "move:west" });
			cmds.Add(new Command { vesselid = 3, action = "move:south" });

			return JsonConvert.SerializeObject(cmds);
		}
	}
}