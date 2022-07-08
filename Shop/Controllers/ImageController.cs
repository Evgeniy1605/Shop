using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile ImageFile) // SecondMethod "Index"
        {
            var saviimg = Path.Combine(_environment.WebRootPath, "Images", ImageFile.FileName);
            string ImgExt = Path.GetExtension(ImageFile.FileName); // Extention
            if (ImgExt == ".png")
            {
                using (var UpLoadign = new FileStream(saviimg, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(UpLoadign);
                    ViewData["Message"] = "The Selected File" + ImageFile.FileName + "Is saved Succcessfully !";
                }
            }
            else
            {
                ViewData["Message"] = "Only the image file type .png are allowed";
            }
            return View("Index");
        }
    }
}
