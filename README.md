# ChatGPT.API.Framework
This is the only .Net Framework ChatGPT API You Can use.
你(目前)唯一能在.Net Framework中的ChatGPT API [中文版本文档](#如何使用)

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
