using proje.Utility;

namespace proje.Models
{
    public class SurveyTypeRepository : Repository<SurveyType>, ISurveyTypeRepository
    {
        private AppDbContext _appDbContext;
        public SurveyTypeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(SurveyType surveyType)
        {
            _appDbContext.Update(surveyType);
        }
    }
}
