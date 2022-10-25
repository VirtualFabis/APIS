using apis.Controllers.Querys;
using apis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace apis.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost]
        public dynamic Login(Users request)
        {
            object[] result = new EveryQuerys().Sql_Login(request);

            if (Convert.ToBoolean(result[0]))
            {
                Users obj = (Users)result[1];

                return Json(obj);
            }
            else
            {
                return Json(result[1].ToString());
            }
        }
        [HttpPost]
        public dynamic Register(Users request)
        {
            object[] result = new EveryQuerys().Sql_Register(request);

            if (Convert.ToBoolean(result[0]))
            {
                Users obj = (Users)result[1];

                return Json(obj);
            }
            else
            {
                return Json(result[1].ToString());
            }
        }
        [HttpPost]
        public dynamic Task(Task request)
        {
            object[] result = new EveryQuerys().Sql_InsertTask(request);

            if (Convert.ToBoolean(result[0]))
            {
                Task obj = (Task)result[1];

                return Json(obj);
            }
            else
            {
                return Json(result[1].ToString());
            }
        }
        [HttpPost]
        public dynamic ListTask(Task request)
        {
            object[] result = new EveryQuerys().SQL_GetTask(request);

            if (Convert.ToBoolean(result[0]))
            {
                List<Task> list = (List<Task>)result[1];

                return Json(list);
            }
            else
            {
                return Json(result[1].ToString());
            }
        }
        [HttpPost]
        public dynamic UpdateTask(Task request)
        {
            object[] result = new EveryQuerys().Sql_UpdateTask(request);

            if (Convert.ToBoolean(result[0]))
            {

                return result[0];
            }
            else
            {
                return Json(result[1].ToString());
            }
        }
        [HttpPost]
        public dynamic DeleteTask(Task request)
        {
            object[] result = new EveryQuerys().Sql_DeleteTask(request);

            if (Convert.ToBoolean(result[0]))
            {
                return result[0];

            }
            else
            {
                return Json(result[1].ToString());
            }
        }
    }
}
