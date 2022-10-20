using labtaskAssociation.db;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace labtaskAssociation.Controllers
{
    public class registrationController : Controller
    {
        // GET: registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult showStudents()
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();

            return View(db.students.ToList());
        }

        // INSERT STUDENTS
        [HttpGet]
        public ActionResult Insert()
        {
            //StudentsDBEntities2 db = new StudentsDBEntities2();
            return View();
        }

        [HttpPost]
        public ActionResult Insert(student s)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            db.students.Add(s);

            db.SaveChanges();

            return RedirectToAction("showStudents");

        }

        // EDIT STUDENTS
        [HttpGet]
        public ActionResult StudentEdit(int id)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();

            var s = (from i in db.students
                     where i.id == id
                     select i).SingleOrDefault();

            return View(s);
        }

        [HttpPost]
        public ActionResult StudentEdit(student s)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            var item = (from p in db.students
                        where p.id == s.id
                        select p).SingleOrDefault();

            item.name = s.name;
            item.cgpa = s.cgpa;

            db.SaveChanges();

            return RedirectToAction("showStudents");
        }

        // DELETE STUDENTS
        public ActionResult StudentDelete(int id)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            var item = (from p in db.students
                        where p.id == id
                        select p).SingleOrDefault();

            db.students.Remove(item);

            var i = (from p in db.courseStudents
                     where p.studentid == id
                     select p).ToList();
            if (i == null)
            {
                db.SaveChanges();

                return RedirectToAction("showStudents");
            }

            foreach (var j in i)
            {
                db.courseStudents.Remove(j);
            }

            db.SaveChanges();

            return RedirectToAction("showStudents");
        }


        // Show courses

        [HttpGet]
        public ActionResult showCourse()
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();



            return View(db.courses.ToList());
        }
        // INSERT COURSES
        [HttpGet]
        public ActionResult InsertCourse()
        {
            //StudentsDBEntities2 db = new StudentsDBEntities2();
            return View();
        }

        [HttpPost]
        public ActionResult InsertCourse(cours s)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            db.courses.Add(s);

            db.SaveChanges();

            return RedirectToAction("InsertCourse");

        }

        [HttpGet]
        public ActionResult CourseEdit(int id)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();

            var s = (from i in db.courses
                     where i.id == id
                     select i).SingleOrDefault();

            return View(s);

        }

        [HttpPost]
        public ActionResult CourseEdit(cours s)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            var item = (from p in db.courses
                        where p.id == s.id
                        select p).SingleOrDefault();

            item.name = s.name;
            item.PreReq = s.PreReq;

            db.SaveChanges();

            return RedirectToAction("showCourse");
        }
        // DELETE STUDENTS
        public ActionResult CourseDelete(int id)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            var item = (from p in db.courses
                        where p.id == id
                        select p).SingleOrDefault();

            db.courses.Remove(item);

            var i = (from p in db.courseStudents
                     where p.courseid == id
                     select p).ToList();
            if (i == null)
            {
                db.SaveChanges();

                return RedirectToAction("showCourse");
            }

            foreach (var j in i)
            {
                db.courseStudents.Remove(j);
            }

            db.SaveChanges();

            return RedirectToAction("showCourse");
        }

        //PRE-REGISTRATION
        [HttpGet]
        public ActionResult preReg(int id)
        {
            ViewBag.sid = id;
            StudentsDBEntities2 db = new StudentsDBEntities2();
            var courses = db.courses.ToList();
            //var cs = db.courseStudents.ToList();
            var list = new List<cours>();

            var i = (from c in db.courseStudents
                     where c.studentid == id
                     select c).ToList();

            for (int a = 0; a < courses.Count; a++)
            {
                foreach (var q in i)
                {

                    if (q.grade == null || q.marks == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (q.courseid == courses[a].PreReq && (!q.grade.Equals("W") || q.marks >= 60))
                        {
                            if (!list.Contains(courses[a]))
                            {
                                list.Add(courses[a]);
                            }
                        }

                        else if (q.grade.Equals("W"))
                        {
                            if (q.courseid == courses[a].id)
                            {
                                if (!list.Contains(courses[a]))
                                {
                                    list.Add(courses[a]);
                                }
                            }

                        }


                        else if (q.marks < 60)
                        {
                            if (q.courseid == courses[a].id)
                            {
                                if (!list.Contains(courses[a]))
                                {
                                    list.Add(courses[a]);
                                }
                            }

                        }
                    }

                }
            }

            return View(list);

        }
        [HttpPost]
        public ActionResult preReg(int[] courses, int id)
        {
            StudentsDBEntities2 db = new StudentsDBEntities2();
            //int len = courses.Length;

            if (courses != null)
            {
                if (courses.Length > 5 )
                {
                    TempData["error"] = "Please select 1 to 5 courses";
                    return RedirectToAction("showStudents");

                }
                foreach (var i in courses)
                {
                    var l = (from a in db.courseStudents
                             where a.studentid == id && a.courseid == i
                             select a).ToList();
                    if (l == null)
                    {
                        db.courseStudents.Add(new courseStudent()
                        {
                            courseid = i,
                            studentid = id

                        });
                    }
                    else
                    {
                        //TempData["error"] = "You have already taken this course";
                        break;
                    }
                }
            }
            else
            {
                TempData["error"] = "Please select 1 to 5 courses";
            }
            db.SaveChanges();

            return RedirectToAction("showStudents");
        }
    }
}