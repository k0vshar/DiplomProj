using Microsoft.AspNetCore.Mvc;
using Diplom.Service.Interfaces;
using Diplom.Domain.ViewModels.Good;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Diplom.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class GoodController : Controller
    {
        public IActionResult Good()
        {
            return View();
        }
        //private readonly IGoodService _goodService;

        //public GoodController(IGoodService goodService)
        //{
        //    _goodService = goodService;
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(GoodViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        byte[] imageData = null;

        //        if (model.Avatar != null)
        //        {
        //            using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
        //            {
        //                imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
        //            }
        //        }

        //        var response = await _goodService.Create(model, imageData);

        //        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //        {
        //            return RedirectToAction("GetGoods", "Good");
        //        }

        //        ModelState.AddModelError("", response.Description);
        //    }

        //    return View(model);
        //}

        //[HttpGet]
        //public IActionResult GetGoods()
        //{
        //    var response = _goodService.GetGoods();
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View(response.Data);
        //    }

        //    return RedirectToAction("Error");
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetGood(long id)
        //{
        //    var response = await _goodService.GetGood(id);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View(response.Data);
        //    }

        //    return RedirectToAction("Error");
        //}

        //[HttpGet]
        //public async Task<IActionResult> Edit(long id)
        //{
        //    var response = await _goodService.GetGood(id);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View(response.Data);
        //    }

        //    return RedirectToAction("Error");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(long id, GoodViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _goodService.Edit(id, model);
        //        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //        {
        //            return RedirectToAction("GetGoods", "Good");
        //        }

        //        ModelState.AddModelError("", response.Description);
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(long id)
        //{
        //    var response = await _goodService.DeleteGood(id);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return RedirectToAction("GetGoods");
        //    }

        //    return RedirectToAction("Error");
        //}
    }
}
