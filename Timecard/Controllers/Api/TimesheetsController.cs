using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timecard.Models;
using Timecard.Utilities;

namespace Timecard.Controllers.Api {
	public class TimesheetsController : ApiController {
		TimecardDbContext db = new TimecardDbContext();

		[HttpGet]
		[Route("timesheets/signin/{pin}")]
		public JsonResponse Signin(string pin) {
			if (pin == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Pin cannot be null"
				};
			}
			if (!ModelState.IsValid) {
				return new JsonResponse {
					Result = "Failed",
					Message = "One of the attributes was invalid"
				};
			}
			var student = db.Students.SingleOrDefault(s => s.Pin == pin);
			if (student == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "That student does not exist"
				};
			}
			student.Signedinalready = true;
			var timesheet = new Timesheet {
				Signin = DateTime.Now,
				Signout = null,
				StudentId = student.Id
			};
			db.Timesheets.Add(timesheet);
			db.SaveChanges();
			return new JsonResponse {
				Message = "Successfully signed in!",
				Data = timesheet
			};
		}

		[HttpGet]
		[Route("timesheets/signout/{pin}")]
		public JsonResponse Signout(string pin) {
			if (pin == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Student cannot be null"
				};
			}
			if (!ModelState.IsValid) {
				return new JsonResponse {
					Result = "Failed",
					Message = "One of the attributes was invalid"
				};
			}
			var student = db.Students.SingleOrDefault(s => s.Pin == pin);
			if (student == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "That student does not exist"
				};
			}
			if (student.Signedinalready == false) {
				// set default for signin time if the user didnt signin properly
				var timesheet = new Timesheet {
					Signin = null,
					Signout = DateTime.Now,
					StudentId = student.Id
				};
				db.Timesheets.Add(timesheet);
				db.SaveChanges();
				return new JsonResponse {
					Message = "Successfully signed out!",
					Data = timesheet
				};
			}
			student.Signedinalready = false;
			var studenttimesheets = db.Timesheets.Where(t => t.StudentId == student.Id && t.Signout == null).OrderByDescending(t => t.Signin).ToList();
			if (studenttimesheets.Count == 0) {
				return new JsonResponse {
					Result = "Failed",
					Message = "You've already signed out"
				};
			}
			var timesheetforsignout = studenttimesheets.First();
			timesheetforsignout.Signout = DateTime.Now;
			db.Entry(timesheetforsignout).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();
			return new JsonResponse {
				Message = "Successfully signed out!",
				Data = timesheetforsignout
			};
		}

		[HttpGet]
		[Route("timesheets/signout/{pin}")]
		public JsonResponse TotalHours(string pin) {
			if (pin == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Student cannot be null"
				};
			}
			if (!ModelState.IsValid) {
				return new JsonResponse {
					Result = "Failed",
					Message = "One of the attributes was invalid"
				};
			}
			var student = db.Students.SingleOrDefault(s => s.Pin == pin);
			if (student == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "That student does not exist"
				};
			}
			var timesheets = db.Timesheets.Where(t => t.StudentId == student.Id).ToList();
			double totalhours = 0;
			timesheets.ForEach(t => {
				if (t.Signout != null || t.Signin != null) {
					var timespan = (t.Signin - t.Signout);
					totalhours += timespan.Value.TotalHours;
				}
			});
			return new JsonResponse {
				Data = totalhours
			};
		}
	}
}
