using apis.Controllers.Querys;
using apis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using Ubiety.Dns.Core.Common;

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
        [HttpGet]
        public IActionResult VerMail(string email)
        {
            var url = $"https://verifier.meetchopra.com/verify/{email}?token=2b1e810090b21cab8a8753ec6bd1f091c66b567b1b893db1209f36999975e99f93bc75c0ab6fa2b35bdfee4b8ac61df6";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                string responseBody = String.Empty;
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return BadRequest();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                             responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
                return Ok(new { res = responseBody   });
            }
            catch (WebException ex)
            {
                return BadRequest(new { error = ex.Message});
            }
        }
    }
}
