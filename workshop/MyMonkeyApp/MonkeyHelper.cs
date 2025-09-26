
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyMonkeyApp;

/// <summary>
/// 원숭이 데이터 관리 및 조회를 위한 정적 헬퍼 클래스입니다.
/// MCP 서버에서 데이터를 가져오며, 랜덤 접근 카운트도 추적합니다.
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey> monkeys = new();
    private static int randomAccessCount = 0;
    private static bool isLoaded = false;

    /// <summary>
    /// MCP 서버에서 원숭이 데이터를 비동기로 로드합니다.
    /// </summary>
    public static async Task LoadMonkeysFromServerAsync(string serverUrl)
    {
        using var client = new HttpClient();
        var result = await client.GetFromJsonAsync<List<Monkey>>(serverUrl);
        if (result != null)
        {
            monkeys = result;
            isLoaded = true;
        }
    }

    /// <summary>
    /// 모든 원숭이 목록을 반환합니다.
    /// </summary>
    public static List<Monkey> GetMonkeys()
    {
        EnsureLoaded();
        return monkeys;
    }

    /// <summary>
    /// 이름으로 원숭이 정보를 반환합니다.
    /// </summary>
    public static Monkey? GetMonkeyByName(string name)
    {
        EnsureLoaded();
        return monkeys.Find(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 랜덤 원숭이 정보를 반환하며, 접근 카운트를 증가시킵니다.
    /// </summary>
    public static Monkey GetRandomMonkey()
    {
        EnsureLoaded();
        randomAccessCount++;
        var rand = new Random();
        return monkeys[rand.Next(monkeys.Count)];
    }

    /// <summary>
    /// 랜덤 원숭이 접근 카운트를 반환합니다.
    /// </summary>
    public static int GetRandomAccessCount() => randomAccessCount;

    private static void EnsureLoaded()
    {
        if (!isLoaded)
            throw new InvalidOperationException("Monkey 데이터가 아직 로드되지 않았습니다. LoadMonkeysFromServerAsync를 먼저 호출하세요.");
    }
}
