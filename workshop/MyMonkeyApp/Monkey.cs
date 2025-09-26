namespace MyMonkeyApp;

/// <summary>
/// 원숭이 정보를 나타내는 모델 클래스입니다.
/// </summary>
public class Monkey
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int Population { get; set; }
    public string AsciiArt { get; set; }

    public Monkey(string name, string location, int population, string asciiArt)
    {
        Name = name;
        Location = location;
        Population = population;
        AsciiArt = asciiArt;
    }
}
