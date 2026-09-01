using HandlebarsDotNet;
using Implementation.Emails.Enums;

namespace Implementation.Emails
{
    public class EmailTemplateComposer
    {
        public string GetTemplateContent(EmailTemplate template, object model)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(dir, "Emails", "Templates");
            var templateFile = template.ToString().ToLower() + ".html";

            filePath = Path.Combine(filePath, templateFile);

            var html = File.ReadAllText(filePath);

            var compiledTemplate = Handlebars.Compile(html);

            return compiledTemplate(model);
        }
    }
}
