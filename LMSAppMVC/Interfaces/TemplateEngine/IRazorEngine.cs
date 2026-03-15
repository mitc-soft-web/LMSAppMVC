namespace LMSAppMVC.Interfaces.TemplateEngine
{
    public interface IRazorEngine
    {
        Task<string> ParseAsync<TModel>(string viewName, TModel model);
    }
}
