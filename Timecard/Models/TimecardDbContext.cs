using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Timecard.Models {
	public class TimecardDbContext : DbContext {
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Timesheet> Timesheets { get; set; }
	}
}