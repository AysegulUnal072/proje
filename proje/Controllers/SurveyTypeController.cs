using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proje.Models;
using proje.Utility;

namespace proje.Controllers
{
    [Authorize]
    public class SurveyTypeController : Controller
    {
        private readonly ISurveyTypeRepository _surveyTypeRepository;

        public SurveyTypeController(ISurveyTypeRepository context)
        {
            _surveyTypeRepository = context;
        }
        public IActionResult Index()
        {
            List<SurveyType> objSurveyTypesList = _surveyTypeRepository.GetAll().ToList();
            return View(objSurveyTypesList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SurveyType surveyType)
        { 
        
            if(ModelState.IsValid)
            {
                _surveyTypeRepository.Add(surveyType);
                _surveyTypeRepository.Save(); // SaveChanges yazpmazsam veriler veritabanına eklenmez!
                TempData["basarili"] = "Yeni Anket Türü başarıyla oluşturuldu!";
                return RedirectToAction("Index", "SurveyType");
            }
            return View();        
        }


        public IActionResult Update(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            SurveyType? surveyTypeVt = _surveyTypeRepository.Get(u=>u.Id==id); // Expression<Func<T, bool>> filter
            if (surveyTypeVt==null) 
            { 
                return NotFound(); 
            }
            return View(surveyTypeVt);
        }

        [HttpPost]
        public IActionResult Update(SurveyType surveyType)
        {

            if (ModelState.IsValid)
            {
                _surveyTypeRepository.Update(surveyType);
                _surveyTypeRepository.Save(); // SaveChanges yazpmazsam veriler veritabanına eklenmez!
                TempData["basarili"] = "Anket Türü başarıyla güncellendi!";
                return RedirectToAction("Index", "SurveyType");
            }
            return View();
        }

        // ET ACTION
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            SurveyType? surveyTypeVt = _surveyTypeRepository.Get(u => u.Id == id);
            if (surveyTypeVt == null)
            {
                return NotFound();
            }
            return View(surveyTypeVt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            SurveyType? surveyType = _surveyTypeRepository.Get(u => u.Id == id);
            if (surveyType == null)
            {
                return NotFound();
            }
            _surveyTypeRepository.Delete(surveyType);
            _surveyTypeRepository.Save();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "SurveyType");
        }
    }
}
