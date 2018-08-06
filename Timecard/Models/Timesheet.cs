using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timecard.Models {
	public class Timesheet {
		public int Id { get; set; }
		public DateTime? Signin { get; set; }
		public DateTime? Signout { get; set; }
		[Required]
		public int StudentId { get; set; }
	}
}