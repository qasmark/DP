using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Valuator.Config;

namespace Valuator.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRedisStorage _redisStorage;
    
    public IndexModel(ILogger<IndexModel> logger, IRedisStorage storage)
    {
        _logger = logger;
        _redisStorage = storage;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost(string text)
    {
        _logger.LogDebug(text);

        string id = Guid.NewGuid().ToString();

        string textKey = "TEXT-" + id;
        //TODO: сохранить в БД text по ключу textKey [+]
        
        
        string rankKey = "RANK-" + id;
        //TODO: посчитать rank и сохранить в БД по ключу rankKey [+] 
        _redisStorage.Store(rankKey, GetRank(text).ToString());

        string similarityKey = "SIMILARITY-" + id;
        //TODO: посчитать similarity и сохранить в БД по ключу similarityKey [+]
        _redisStorage.Store(similarityKey, GetSimilarity(text, id).ToString());
        _redisStorage.Store(textKey, text);
        return Redirect($"summary?id={id}");
    }

    private int GetSimilarity(string text, string id)
    {
        var keys = _redisStorage.GetKeys();

        return keys.Any(item =>
            item.Substring(0, 5) == "TEXT-" && _redisStorage.Load(item) == text)
            ? 1
            : 0;
    }

    private static double GetRank(string text)
    {
        var notLetterCount = text.Count(ch => !char.IsLetter(ch));

        return (double) notLetterCount / text.Length;
    }
}
