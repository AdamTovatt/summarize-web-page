using System.Security.Authentication;

namespace SummarizeWebPage
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the web page summarizer.");
            Console.WriteLine("Please enter the url of the page you want to summarize:");

            string? url = null;

            while (string.IsNullOrEmpty(url))
            {
                url = Console.ReadLine();

                if (!HtmlHelper.LooksLikeValidUrl(url))
                {
                    Console.WriteLine("Oops, that doesn't seem to be something that could be a url. Please try again: ");
                    url = null;
                }
            }

            Console.WriteLine("\nOkay, will try to summarize that page.");

            Summarizer summarizer = new Summarizer(KeyProvider.GetKey());

            Console.WriteLine("Downloading web page...");

            string webPageText = await HtmlHelper.GetWebPageTextAsync(url);

            Console.WriteLine("\nDownload completed ({0} characters)", webPageText.Length);
            Console.WriteLine("Summarizing web page...");

            try
            {
                string summary = await summarizer.SummarizeAsync(webPageText);

                Console.WriteLine("\nSummary completed, this is the summary: ");

                Console.WriteLine(summary);
            }
            catch(AuthenticationException) // there was an error with authentication
            {
                KeyProvider.DeleteStoredKey();
                Console.WriteLine("The key you provided was invalid, please try again with a valid key.");
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}