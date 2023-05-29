using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class CoctelController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Coctel coctel = new ML.Coctel();
            coctel.Cocteles = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/");

                var responseTask = client.GetAsync("json/v1/1/search.php?s=");
                responseTask.Wait();

                var resultUsuario = responseTask.Result;

                if (resultUsuario.IsSuccessStatusCode)
                {
                    var readTask = resultUsuario.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.drinks)
                    {
                        ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem.ToString());

                        coctel.Cocteles.Add(resultItemList);
                    }
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta";
                    return View("Modal");
                }

            }
            return View(coctel);

        }


        [HttpPost]
        public ActionResult GetAll(ML.Coctel coctel)
        {
            coctel.Cocteles = new List<object>();
            if (coctel.Ingrediente == null && coctel.strDrink == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/");

                    var responseTask = client.GetAsync("json/v1/1/search.php?s=");
                    responseTask.Wait();

                    var resultUsuario = responseTask.Result;

                    if (resultUsuario.IsSuccessStatusCode)
                    {
                        var readTask = resultUsuario.Content.ReadAsAsync<dynamic>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.drinks)
                        {
                            ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem.ToString());

                            coctel.Cocteles.Add(resultItemList);
                        }
                    }
                }
            }
            else
            {
                if (coctel.strDrink != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/");

                        var responseTask = client.GetAsync("json/v1/1/search.php?s=" + coctel.strDrink);
                        responseTask.Wait();

                        var resultUsuario = responseTask.Result;

                        if (resultUsuario.IsSuccessStatusCode)
                        {
                            var readTask = resultUsuario.Content.ReadAsAsync<dynamic>();
                            readTask.Wait();

                            foreach (var resultItem in readTask.Result.drinks)
                            {
                                ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem.ToString());

                                coctel.Cocteles.Add(resultItemList);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al hacer la consulta";
                            return View("Modal");
                        }

                    }

                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/");

                        var responseTask = client.GetAsync("json/v1/1/filter.php?i=" + coctel.Ingrediente);
                        responseTask.Wait();

                        var resultUsuario = responseTask.Result;

                        if (resultUsuario.IsSuccessStatusCode)
                        {
                            var readTask = resultUsuario.Content.ReadAsAsync<dynamic>();
                            readTask.Wait();

                            foreach (var resultItem in readTask.Result.drinks)
                            {
                                ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem.ToString());

                                coctel.Cocteles.Add(resultItemList);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al hacer la consulta";
                            return View("Modal");
                        }

                    }
                }
            }
            
            return View(coctel);

        }
    }
}
