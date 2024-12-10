namespace proje.Models
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        void Update(Survey survey);
        void Save();
    }
}
