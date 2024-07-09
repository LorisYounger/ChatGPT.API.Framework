# ChatGPT.API.Framework
This is the only(before) .Net Framework ChatGPT API You Can use.
你(以前)唯一能在.Net Framework中的ChatGPT API [中文版本文档](#如何使用)

## How To Use

### Create Client

```C#
ChatGPTClient cgc = new ChatGPTClient("YOU-API-KEY");
```

### Just Ask with out System Message

```C#
cgc.Ask("Store-History-ID", "Hi ChatGPT").GetMessageContent();
```

### Create A Completions with System Message

```C#
cgc.CreateCompletions("Store-History-ID", "System Message");
cgc.Ask("Store-History-ID", "Hi ChatGPT");
```

### Return data using streaming

```C#
cgc.Ask_stream("Store-History-ID", "Hi ChatGPT", (x) =>
{
    if (!string.IsNullOrEmpty(x.GetDeltaContent()))
    {
        Console.Write(x.choices[0].delta.content);
    }
    else if (x.GetDelta()?.finish_reason != null)
    {
        Console.WriteLine("\n---" + x.choices[0].delta.finish_reason + "---\n");
    }
});
```

Also Can see Demo at `ChatGPT.API.Test`

## Save & Load History

```C#
ChatGPTClient cgc = new ChatGPTClient("YOU-API-KEY");
string json = cgc.Save();
cgc = ChatGPTClient.Load(Json);
```

## Other Config

See `Completions.cs` and `ChatGPTClient.cs`

```C#
cgc.Completions["Store-History-ID"].max_tokens = 20;
```



## 如何使用

### 创建客户端

```C#
ChatGPTClient cgc = new ChatGPTClient("你的ChatGPT的APIKEY");
```

### 直接问问题

```C#
//若库中无对话历史记录,会自动创建对话
cgc.Ask("储存历史用的id", "你好 ChatGPT").GetMessageContent();
```

### 创建一个对话并设置初始系统信息

```C#
cgc.CreateCompletions("储存历史用的id", "你是个可爱的桌宠,请用可爱的语气和我说话");
cgc.Ask("储存历史用的id", "你好,桌宠");
```

### 使用流式传输返回数据

```C#
cgc.Ask_stream("储存历史用的id", "你好,桌宠", (x) =>
{
    if (!string.IsNullOrEmpty(x.GetDeltaContent()))
    {
        Console.Write(x.GetDeltaContent());
    }
    else if (x.GetDelta()?.finish_reason != null)
    {
        Console.WriteLine("\n---" + x.GetDelta().delta.finish_reason + "---\n");
    }
});
```



具体案例可以参见 `ChatGPT.API.Test`

## 储存和加载信息

```C#
ChatGPTClient cgc = new ChatGPTClient("YOU-API-KEY");
string json = cgc.Save();
cgc = ChatGPTClient.Load(Json);
```

## 其他设置

具体见 `Completions.cs` 和`ChatGPTClient.cs`

```C#
cgc.Completions["Store-History-ID"].max_tokens = 20;
```
