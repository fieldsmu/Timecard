using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timecard.Utilities {
	public class JsonResponse {
		public string Result { get; set; } = "Ok";
		public string Message { get; set; }
		public object Data { get; set; }
	}
}