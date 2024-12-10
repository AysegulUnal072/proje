using proje.Utility;

namespace proje.Models
{
    public class SurveyRepository : Repository<Survey>, ISurveyRepository
    {
        private AppDbContext _appDbContext;
        public SurveyRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Survey survey)
        {
            _appDbContext.Update(survey);
        }
    }
}
