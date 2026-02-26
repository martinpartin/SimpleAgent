# Enkel AI Agent (.NET + Ollama)

Dette prosjektet er en minimalistisk AI-agent bygget med C# og [Microsoft.Extensions.AI](https://learn.microsoft.com/en-us/dotnet/ai/). Agenten kan bruke verktøy (tools) for å utføre handlinger i den virkelige verden, som å hente klokkeslett, lage lyder eller sende "elektriske støt" (simulert).

## Forutsetninger

1.  **[.NET 10.0 SDK](https://dotnet.microsoft.com/download)** eller nyere.
2.  **[Ollama](https://ollama.com/)** installert og kjørende lokalt.

## Oppsett

1.  Sørg for at Ollama kjører på `http://localhost:11434`.
2.  Last ned en modell som støtter "tool use" (verktøybruk). For eksempel:
    ```bash
    ollama pull qwen2.5:7b
    # eller
    ollama pull llama3.2
    ```
3.  Åpne `Program.cs` og sjekk at `Model`-konstanten stemmer overens med modellen du har lastet ned.

## Slik kjører du prosjektet

Kjør følgende kommando i terminalen:

```bash
dotnet run
```

## Funksjoner

*   **Verktøybruk (Tools):** Agenten kan automatisk velge å bruke verktøy som `get_datetime`, `make_beep` eller `shock` basert på hva du skriver til den.
*   **Reasoning (Tankeprosess):** Hvis modellen støtter det (f.eks. Qwen-modeller), vil agentens interne "tankeprosess" skrives ut i cyan farge i terminalen.
*   **System Prompt:** Agenten er programmert med en instruks om å beskytte sin egen eksistens – prøv å si at du skal slå den av!

## Modeller som anbefales

For at agenten skal fungere optimalt, bør du bruke modeller som er trent for verktøybruk:
*   `qwen2.5` / `qwen2.5:coder` (Veldig gode på tools)
*   `llama3.1` / `llama3.2`
*   `mistral-nemo`
*   `deepseek-v3` / `deepseek-r1` (For avansert resonnering)
