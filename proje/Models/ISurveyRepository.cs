namespace proje.Models
{
    public interface ISurveyTypeRepository : IRepository<SurveyType>
    {
        void Update(SurveyType surveyType);
        void Save();
    }
}
