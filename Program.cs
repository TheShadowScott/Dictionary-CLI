using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace DictionaryAPI;
class Program
{
    async static Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please enter a word to search for.");
            return;
        }
        
        string endpoint = $"/api/v2/entries/en-gb/{args[0]}";

        var LoginData = Login.GetLogin();

        Dictionary<string, string> headers = new()
        {
            { "app_id", LoginData?.AppId! },
            { "app_key", LoginData?.AppKey! }
        };

        HttpClient client = new()
        {
            BaseAddress = new Uri("https://od-api.oxforddictionaries.com:443")
        };


        client.DefaultRequestHeaders.Clear();

        foreach (var header in headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
        try
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = await client.GetFromJsonAsync<Root>(endpoint);

            if (data is not null)
                Console.WriteLine(EntryBuilder.BuildStringFromLexicalEntryList(
                    EntryBuilder.BuildLexicalEntries(data)
                ));
            else
                Console.WriteLine("No entries found.");
        }
        catch (HttpRequestException except)
        {
            if (except.Message.Contains("404 (NOT FOUND)"))
                Console.WriteLine("No entries found for that word.");
            else
                throw;
        }
    }
}