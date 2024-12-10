using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using proje.Models;
using proje.Utility;

namespace proje.Controllers
{
    [Authorize]
    public class SurveyHomeController : Controller
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyTypeRepository _surveyTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public SurveyHomeController(ISurveyRepository surveyRepository, ISurveyTypeRepository surveyTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _surveyRepository = surveyRepository;
            _surveyTypeRepository = surveyTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Survey> objSurveyList = _surveyRepository.GetAll().ToList();
            List<Survey> objSurveyList = _surveyRepository.GetAll(includeProps: "SurveyType").ToList();
            return View(objSurveyList);

        }

        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> SurveyTypeLİst = _surveyRepository.GetAll()
               .Select(k => new SelectListItem
               {
                   Text = k.Title,
                   Value = k.Id.ToString()
               });
            ViewBag.SurveyTypeList = SurveyTypeLİst;

            if (id == null || id == 0)
            {
                // ekle
                return View();
            }
            else
            {
                // güncelleme
                Survey? surveyVt = _surveyRepository.Get(u => u.Id == id); // Expression<Func<T, bool>> filter
                if (surveyVt == null)
                {
                    return NotFound();
                }
                return View(surveyVt);
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Survey survey, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string surveyPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {

                    using (var fileStream = new FileStream(Path.Combine(surveyPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    survey.ResimUrl = @"\img\" + file.FileName;
                }

                if (survey.Id == 0)
                {
                    _surveyRepository.Add(survey);
                    TempData["basarili"] = "Yeni Anket başarıyla oluşturuldu!";
                }
                else
                {
                    _surveyRepository.Update(survey);
                    TempData["basarili"] = "Anket güncelleme başarılı!";

                }
                _surveyRepository.Save(); // SaveChanges yazpmazsam veriler veritabanına eklenmez!
                TempData["basarili"] = "Yeni Anket Türü başarıyla oluşturuldu!";
                return RedirectToAction("Index", "Survey");
            }
            return View();
        }

        /*
        public IActionResult Update(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            Survey? surveyVt = _surveyRepository.Get(u=>u.Id==id); // Expression<Func<T, bool>> filter
            if (surveyVt==null) 
            { 
                return NotFound(); 
            }
            return View(surveyVt);
        }
        */


        /*
        [HttpPost]
        public IActionResult Update(Survey survey)
        {

            if (ModelState.IsValid)
            {
                _surveyRepository.Update(survey);
                _surveyRepository.Save(); // SaveChanges yazpmazsam veriler veritabanına eklenmez!
                TempData["basarili"] = "Anket Türü başarıyla güncellendi!";
                return RedirectToAction("Index", "Survey");
            }
            return View();
        }
        */


        // GET ACTION
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Survey? surveyVt = _surveyRepository.Get(u => u.Id == id);
            if (surveyVt == null)
            {
                return NotFound();
            }
            return View(surveyVt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Survey? survey = _surveyRepository.Get(u => u.Id == id);
            if (survey == null)
            {
                return NotFound();
            }
            _surveyRepository.Delete(survey);
            _surveyRepository.Save();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "Survey");
        }
    }
}
