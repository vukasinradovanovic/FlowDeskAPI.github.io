using HandlebarsDotNet;
using Implementation.Emails.Enums;

namespace Implementation.Emails
{
    public class EmailTemplateComposer
    {
        public string GetTemplateContent(EmailTemplate template, object model)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var templateFile = $"{template.ToString().ToLower()}.html";

            var filePath = Path.Combine(dir, "Emails", "Templates", templateFile);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Email template file not found at: {filePath}");
            }

            var html = File.ReadAllText(filePath);
            var compiledTemplate = Handlebars.Compile(html);

            return compiledTemplate(model);
        }
    }
}
