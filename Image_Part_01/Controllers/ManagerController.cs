using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Part_01.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IWebHostEnvironment _hostEnv;
        public ManagerController(IWebHostEnvironment _hostEnv)
        {
            this._hostEnv = _hostEnv;
        }
        public IActionResult Index()
        {
            ViewBag.msg = TempData["msg"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile fileToUpload)
        {
            string msg = "";
            if(fileToUpload !=null && fileToUpload.Length > 0)
            {
                string webroot = _hostEnv.WebRootPath;
                string folder = "UploadedFiles";
                string fileName = fileToUpload.FileName;
                string fileToWrite = Path.Combine(webroot, folder, fileName);

                using (var stream = new FileStream(fileToWrite, FileMode.Create))
                {
                    await fileToUpload.CopyToAsync(stream);
                    msg = "File[" + fileName + "] is uploaded successfull";
                }
            }
            else
            {
                msg = "Please select a valid file to upload%%%";
            }
            TempData["msg"] = msg;
            return RedirectToAction("Index");
        }
    }
}
