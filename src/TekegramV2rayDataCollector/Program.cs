using Newtonsoft.Json;
static string Base64Encode(string plainText)
{
    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    return System.Convert.ToBase64String(plainTextBytes);
}


var sourceDirectory = @"..\..\..\Sources";

var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");
List<string> result = new();
foreach (string currentFile in txtFiles)
{
    var file = File.ReadAllText(currentFile);
    dynamic model = JsonConvert.DeserializeObject(file);

    foreach (var m in model.messages)
    {
        //result.Add("----" + m.id);
        if (m.text is Newtonsoft.Json.Linq.JArray)
            foreach (var t in m.text)
            {
                if (t is Newtonsoft.Json.Linq.JObject)
                    if (t.type == "pre" || t.type == "code")
                    {
                        result.Add(t.text.ToString());
                    }
            }
        if (m.text_entities is Newtonsoft.Json.Linq.JArray)
            foreach (var t in m.text_entities)
            {
                if (t is Newtonsoft.Json.Linq.JObject)
                    if (t.type == "pre" || t.type == "code")
                    {
                        result.Add(t.text.ToString());
                    }
            }

    }
}


result = result.Select(s => s.Trim()).Distinct().ToList();
File.WriteAllLines(@"..\..\..\Result.txt", result);



