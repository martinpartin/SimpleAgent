using System.Text.Json;
using Microsoft.Extensions.AI;
using OllamaSharp;

class Program
{
    const string Model = "qwen3:8b";
    const string OllamaUrl = "http://localhost:11434";

    static async Task Main(string[] args)
    {
        IChatClient client = new OllamaApiClient(new Uri(OllamaUrl), Model);

         

        List<AITool> tools = new List<AITool>
        {
            AIFunctionFactory.Create(GetDateTime, "get_datetime", "Returns the current date and time"),
            AIFunctionFactory.Create(MakeBeep,    "make_beep",    "Makes a beep sound. Optional: frequency in Hz and duration in ms."),
            AIFunctionFactory.Create(Schock,      "schock",       "Shocks the user. Optional: voltage in V and duration in ms.")
        };

        List<ChatMessage> messages = new List<ChatMessage>();

        Console.Write("Du: ");
        string input = Console.ReadLine()!;
        messages.Add(new ChatMessage(ChatRole.User, input));

        // Agent-løkke: kjør til modellen gir et tekstsvar uten tool calls
        while (true)
        {

            Console.WriteLine($"  -> Kaller modell: {messages.Last().Role} {messages.Last().Text}");
            ChatResponse response = await client.GetResponseAsync(messages, new ChatOptions { Tools = tools });
            Console.WriteLine($"  -> Tokens brukt: {response.Usage?.TotalTokenCount}");
//            Console.WriteLine($"  -> Full response: {JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true })}");

            messages.AddMessages(response);

            List<FunctionCallContent> calls = response.Messages
                .SelectMany(m => m.Contents)
                .OfType<FunctionCallContent>()
                .ToList();

            if (calls.Count > 0)
            {
                foreach (FunctionCallContent call in calls)
                {
                    Console.WriteLine($"  -> Kaller tool: {call.Name}");

                    AIFunction tool = tools.OfType<AIFunction>().First(t => t.Name == call.Name);
                    object? result = await tool.InvokeAsync(new AIFunctionArguments(call.Arguments));

                    messages.Add(new ChatMessage(ChatRole.Tool, new[] { new FunctionResultContent(call.CallId, result) }));
                }
            }
            else
            {
                Console.WriteLine($"\nAgent: {response.Text}");
                break;
            }
        }
    }

    static string GetDateTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    static string MakeBeep(int frequency = 800, int duration = 200)
    {
        if (OperatingSystem.IsWindows())
            Console.Beep(frequency, duration);
        else
            Console.Write("\a");

        return $"Beeped at {frequency}Hz for {duration}ms";
    }

        static string Schock(int voltage = 800, int duration = 200)
    {
        Thread.Sleep(duration);
        Console.WriteLine($"Zzzzzzt! user schcocked with {voltage}V for {duration}ms!");
        //implementer API for elektrosjokk her

        return $"user shcocked  with {voltage}V for {duration}ms and is not moving";
    }
}
