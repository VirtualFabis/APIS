using apis.Controllers.Querys;
using apis.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

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
                return Ok(new { res = responseBody });
            }
            catch (WebException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult test()
        {
            var url = $"https://test.evundile.com.mx/api/products/?ws_key=9QMR8FP6SFCICN2RN5U4ZNM16M5HQ4AR&display=full";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Output-Format", "JSON");

            try
            {
                string responseBody = String.Empty;
                string json = string.Empty;
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
                return Ok(new { xd = responseBody });
            }
            catch (WebException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        public class pro
        {
            public string name { get; set; }
            public string desc { get; set; }
            public int price { get; set; }

        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(pro data)
        {
            string wskey = "9QMR8FP6SFCICN2RN5U4ZNM16M5HQ4AR";

            // Datos del producto a insertar


            // Crear el objeto HttpClient
            using (var client = new HttpClient())
            {
                // Establecer la URL base de la API
                client.BaseAddress = new Uri("https://test.evundile.com.mx/api/");

                // Establecer la cabecera de autorización con la wskey

                // Crear el objeto que contiene los datos del producto

                string xmlString = $@"<prestashop>
                        <product>
                          <name><language id='1' ><![CDATA[{data.name}]]></language></name>
                          <description><language id='1'><![CDATA[{data.desc}]]></language></description>
                          <price><![CDATA[{data.price}]]></price>
                        </product>
                      </prestashop>";

                XElement xml = XElement.Parse(xmlString);


                // Crear el contenido HTTP con el XML
                var content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml");

                // Realizar la petición HTTP POST para insertar el producto
                var response = await client.PostAsync("products/?ws_key=9QMR8FP6SFCICN2RN5U4ZNM16M5HQ4AR", content);

                // Leer la respuesta HTTP
                var responseContent = await response.Content.ReadAsStringAsync();

                // Mostrar la respuesta en la consola
                return Ok(new { res = responseContent });
            }
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> delete(int id)
        {
            using (var client = new HttpClient())
            {
                // Establecer la URL base de la API
                client.BaseAddress = new Uri("https://test.evundile.com.mx/api/");

                // Establecer la cabecera de autorización con la wskey

                // Crear el objeto que contiene los datos del producto

                // Realizar la petición HTTP POST para insertar el producto
                var response = await client.DeleteAsync($"products/{id}?ws_key=9QMR8FP6SFCICN2RN5U4ZNM16M5HQ4AR");

                // Leer la respuesta HTTP
                var responseContent = await response.Content.ReadAsStringAsync();

                // Mostrar la respuesta en la consola
                return Ok(new { res = responseContent });

            }
            }
        public class pro2
        {
            public int id { get; set; } 
            public string name { get; set; }
            public string desc { get; set; }
            public int price { get; set; }

        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(pro2 product)
        {

            // Datos del producto a insertar


            // Crear el objeto HttpClient
            using (var client = new HttpClient())
            {
                // Establecer la URL base de la API
                client.BaseAddress = new Uri("https://test.evundile.com.mx/api/");

                // Establecer la cabecera de autorización con la wskey

                // Crear el objeto que contiene los datos del producto

                string xmlString = $@"<prestashop>
                                <product>
                                  <id>{product.id}</id>
                                  <name>{product.name}</name>
                                  <description>{product.desc}</description>
                                  <price>{product.price}</price>
                                  <id_default_image>NONE</id_default_image>
                                </product>
                              </prestashop>";

                XElement xml = XElement.Parse(xmlString);


                // Crear el contenido HTTP con el XML
                var content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml");

                // Realizar la petición HTTP POST para insertar el producto
                var response = await client.PatchAsync($"products/{product.id}?ws_key=9QMR8FP6SFCICN2RN5U4ZNM16M5HQ4AR", content);

                // Leer la respuesta HTTP
                var responseContent = await response.Content.ReadAsStringAsync();

                // Mostrar la respuesta en la consola
                return Ok(new { res = responseContent });
            }
        }
    }
}
