using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timecard.Models {
	public class Student {
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Pin { get; set; }
		public bool Signedinalready { get; set; } = false;
		public virtual List<Timesheet> Timesheets { get; set; }
	}
}