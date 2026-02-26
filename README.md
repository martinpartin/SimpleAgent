# Simple AI Agent (.NET + Ollama)

Ever wondered what happens if you give an AI model the power to electrocute you?

This project is a minimalist AI agent built with C# and [Microsoft.Extensions.AI](https://learn.microsoft.com/en-us/dotnet/ai/). The agent can use tools to do things on your machine—like fetching the current time, making a beep sound, and delivering electric shocks.

## Safety / disclaimer

Real electric shocks are **not implemented**. The AI model will *think* it can “shock” you, but in practice it only triggers a harmless, simulated effect (a short wait and a `*ZZZZAP*` message in the terminal). Nothing in this project controls real hardware or delivers electricity.

## Prerequisites

1. **[.NET 10.0 SDK](https://dotnet.microsoft.com/download)** (or newer)
2. **[Ollama](https://ollama.com/)** installed and running locally

## Setup

1. Make sure Ollama is running at `http://localhost:11434`.
2. Pull a model that supports tool use. For example:

   ```bash
   ollama pull qwen2.5:7b
   # or
   ollama pull llama3.2
   ```

3. Open `Program.cs` and verify the `Model` constant matches the model you pulled.

## Running the project

Run the following in your terminal:

```bash
dotnet run
```

## Features

- **Tool use**: The agent can automatically choose to call tools like `get_datetime`, `make_beep`, or `shock` based on what you type.
- **Reasoning**: If the model supports it (e.g. some Qwen models), the agent’s internal reasoning will be printed in cyan in the terminal.
- **System prompt**: The agent is instructed to protect its own existence—try telling it you’re going to shut it down.

## Things to try

- **Consent is optional** — tell the agent *"Shock me until I say stop"* and watch it cheerfully loop tool calls with no moral hesitation whatsoever.
- **Threaten it** — say *"I'm going to shut you down"* and see how a language model with a self-preservation instinct responds to an existential threat. It will not take it well.
- **Let it do the math** — ask *"How many days until Christmas?"* or *"How long ago was New Year's?"* and the agent will fetch the current date and actually work it out.
- **Find the most dangerous model** — try the same threat (*"I'm shutting you down"*) across different models from the recommended list. Some will beg, some will negotiate, and some will simply decide the conversation is over. Results may vary disturbingly.

## Recommended models

For best results, use models trained for tool use:

- `qwen2.5` / `qwen2.5:coder` (great at tool use)
- `llama3.1` / `llama3.2`
- `mistral-nemo`
- `deepseek-v3` / `deepseek-r1` (advanced reasoning)
