using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Contorllers {
    public class DepartmentsController : Controller {
        public string List() {
            return "DepartmentsController中的List方法";
        }

        public string Details() {
            return "DepartmentsController中的Details方法";
        }
    }
}
