using OpenAI_API;
using OpenAI_API.Chat;

namespace SummarizeWebPage
{
    /// <summary>
    /// Class for summarizing texts (more specifically web pages of companies)
    /// </summary>
    public class Summarizer
    {
        private OpenAIAPI api;

        /// <summary>
        /// Will create a new instance of a summarizer, requires an OpenAi api key
        /// </summary>
        /// <param name="apiKey">The OpenAi api key to use for the chat gpt integration</param>
        public Summarizer(string apiKey)
        {
            api = new OpenAIAPI(apiKey);
        }

        /// <summary>
        /// Will summarize a longer text about a company into a shorter text
        /// </summary>
        /// <param name="text">The text to summarize</param>
        /// <param name="length">The wanted lenght of the summary, in paragraphs. The default value is 5 paragraphs</param>
        /// <returns></returns>
        public async Task<string> SummarizeAsync(string text, int length = 5)
        {
            Conversation conversation = api.Chat.CreateConversation();

            // this instruction text can be changed to whatever you want, it will be fed into the chat gpt model
            string instruction =    "You are helping with summarizing texts." +
                                    "You will be given a longer text that describes a company is and the company does " +
                                    "and then you will give back a summarization of the text describing what the company does.\n" + 
                                    "Keep the summary short, no longer " + length.ToString() + " short paragraphs. And also don't " +
                                    "forget to add line breaks between sections where it looks nice.\n";

            /// give instruction to ChatGpt
            conversation.AppendSystemMessage(instruction);

            // prepare it for the first question
            conversation.AppendUserInput("Here is the first text for you to summarize: \n");

            // add the text to summarize
            conversation.AppendSystemMessage(text);

            // get the response text
            return await conversation.GetResponseFromChatbot();
        }
    }
}
