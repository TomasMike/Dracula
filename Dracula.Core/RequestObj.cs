using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dracula.Core
{
	public class RequestObj
	{
		public RequestType RequestType;
	}

	public class UpdateRequest: RequestObj
	{
		public UpdateRequest()
		{
			RequestType = RequestType.UpdateLog;
		}

		public DateTime LastTimeStamp;
	}

	public class ResponseObj
	{

	}

	public class UpdateEventLogResponse : ResponseObj
	{
		public List<string> newData = new List<string>();
	}
}
