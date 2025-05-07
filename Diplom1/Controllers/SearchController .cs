using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class SearchController : Controller
    {
        // GET: /Search/Search?query=...
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Введите поисковый запрос.";
                return View();
            }

            // Здесь можно сделать запрос к базе данных или сервису
            // Пример заглушки:
            var results = new List<string>
            {
                "Результат 1 для " + query,
                "Результат 2 для " + query
            };

            ViewBag.Query = query;
            return View(results);
        }
    }
}
