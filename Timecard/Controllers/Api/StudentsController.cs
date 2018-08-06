using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timecard.Models;
using Timecard.Utilities;

namespace Timecard.Controllers.Api {
	public class StudentsController : ApiController {
		TimecardDbContext db = new TimecardDbContext();

		[HttpGet]
		[ActionName("List")]
		public JsonResponse List() {
			return new JsonResponse {
				Data = db.Students.ToList()
			};
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse Get(int? id) {
			if (id == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "id cannot be null"
				};
			}
			var student = db.Students.Find(id);
			if (student == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Student cannot be null"
				};
			}
			return new JsonResponse {
				Data = student
			};
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse Create(Student student) {
			if (student == null) {
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
			db.Students.Add(student);
			db.SaveChanges();
			return new JsonResponse {
				Data = student
			};
		}
	}
}
