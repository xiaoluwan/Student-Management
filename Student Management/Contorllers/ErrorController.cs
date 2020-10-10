using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Contorllers {
    public class ErrorController:Controller {
        [Route("Error/{statusCode}")] //属性路由 ,内容自定义,接受
        public IActionResult HttpStatusCodeHandler(int StatusCode) {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (StatusCode) {
                case 404:
                    ViewBag.ErrorMessge = "抱歉,您访问的页面不存在";
                    ViewBag.Path = statusCodeResult.OriginalPath;//获取地址
                    ViewBag.QuerySer = statusCodeResult.OriginalQueryString;//获取连接字符串
                    ViewBag.BasePath = statusCodeResult.OriginalPathBase;
                    break;
            }
            return View("NotFound");
        }
    }
}
