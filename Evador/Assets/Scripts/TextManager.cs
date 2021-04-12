using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] List<Text> intro;

    string[] intro_rus = new string[] { "Однажды Маленький Лучик Света узнал, что мир бывает не только чёрным и белым...",
        "Великий Старый Шестиугольник сказал, что у Лучика есть много братьев, которые живут в собственных мирах и имеют собственные личности...",
        "Лучик даже представить такого не мог, так что он решил найти всех своих братьев. Чего бы это не стоило!",
        "ПОНЯЛ!"};
    string[] intro_eng = new string[] { "Once The Little Ray of Light found out that the world is not only black and white...",
        "The Great Old Hexagon told him that Ray had many brothers who lived in their own worlds and had own personality...",
        "Ray couldn't even imagine that, so he decided to find all his brothers. Whatever it takes!",
        "GOT IT!"};


    [SerializeField] Text stats;

    [SerializeField] List<Text> settings;
    string[] settings_rus = new string[] { "Изменить язык", "Сброс настроек", "Нет", "Да" };
    string[] settings_eng = new string[] { "Change the language", "Clear data", "No", "Yes" };

    [SerializeField] Text AdButtonText;


    public static string[] locations_eng = { "abstract space", "candy land", "deep swamp", "my office" };
    public static string[] locations_rus = { "абстрактность", "конфетный мир", "глубокое болото", "мой офис" };

    [SerializeField] List<Text> final;
    string[] final_rus = new string[] { "Лучик...",
        "Ты отыскал всех своих братьев...",
        "Теперь ты видишь, что вместе они создают что-то новое...",
        "Они создают что-то светлое и открытое, что-то, от чего ты не можешь уклониться...",
    "Ты герой, Лучик..."};

    string[] final_eng = new string[] { "Ray...",
        "You have found all your brothers...",
        "Now you can see that all of them create something new...",
        "They create something light and open, something that you can't evade...",
    "You are a hero, Ray..."};


    [SerializeField] List<Text> credits;

    public void SetMenuText()
    {
        int i = 0;

        if (Stats.lng == "rus") // Меняем все на русский
        {
            foreach (Text t in intro)
                t.text = intro_rus[i++];
        }
        else
        {
            foreach (Text t in intro)
                t.text = intro_eng[i++];
        }

        SetSettingsText();

        i = 0;
        if (Stats.lng == "rus") // Меняем все на русский
        {
            foreach (Text t in final)
                t.text = final_rus[i++];
        }
        else
        {
            foreach (Text t in final)
                t.text = final_eng[i++];
        }

        i = 0;
        if (Stats.lng == "rus") // Меняем все на русский
        {
            foreach (Text t in credits)
                t.text = credits_rus[i++];
        }
        else
        {
            foreach (Text t in credits)
                t.text = credits_eng[i++];
        }
    }

    public void SetSettingsText()
    {
        int i = 0;

        if (Stats.lng == "rus")
        {
            stats.text = $"Evador v{Stats.version}\r\nУровней пройдено: {Stats.maxLevel}\r\nСмертей: {Stats.numOfDeaths}\r\n" +
            $"Осколков собрано: {Stats.numOfShards}";

            foreach (Text t in settings)
                t.text = settings_rus[i++];
        }
        else if (Stats.lng == "eng")
        {
            stats.text = $"Evador v{Stats.version}\r\nLevels completed: {Stats.maxLevel}\r\nNumber of deaths: {Stats.numOfDeaths}\r\n" +
            $"Shards collected: {Stats.numOfShards}";

            foreach (Text t in settings)
                t.text = settings_eng[i++];
        }

        if (Stats.AdsRemoved)
            AdButtonText.text = RemovedAdText();
        else
            AdButtonText.text = NonRemovedAdText();
    }

    public string RemovedAdText()
    {
        switch (Stats.lng)
        {
            case "rus":
                return "Бро, да ты герой ^-^!";
            default:
                return "You are literally a hero ^-^!";
        }
    }

    public string NonRemovedAdText()
    {
        switch (Stats.lng)
        {
            case "rus":
                return "Поддержать автора - 0.99$.\r\n(Реклама отключится)";
            default:
                return "SUPPORT THE DEVELOPER - 0.99$\r\n(ADs disabling included)";
        }
    }

    public static string[] credits_rus = { "Evador\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nКреативная команда:\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nКоманда программистов:\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nМузыка:\r\n\r\nBroke For Free - Calm The F*ck Down\r\nLonely Punk - Left The Building\r\n\r\n\r\n\r\n\r\n\r\n\r\nПарень, который мне помог:\r\n\r\nДаниил Казанцев\r\n\r\nПервый, кто прошёл эту игру:\r\n\r\nАнтон Буслаев\r\n\r\nОсобое спасибо тем,\r\nкто помог с тестированием\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nИ спасибо тебе!",
        "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nГенеральный дизайнер\r\n\r\n\r\nДизайнер уровней\r\n\r\n\r\nУлучшитель картинок\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nПисатель скриптов\r\n\r\n\r\nГлавный инженер",
        "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nМаксим Гудзикевич\r\n\r\n\r\nМаксим Гудзикевич\r\n\r\n\r\nМаксим Гудзикевич\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nМаксим Гудзикевич\r\n\r\n\r\n(Угадай, кто)" };


    public static string[] credits_eng = { "Evador\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nCreative team:\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nProgramming team:\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nMusic:\r\n\r\nBroke For Free - Calm The F*ck Down\r\nLonely Punk - Left The Building\r\n\r\n\r\n\r\n\r\n\r\n\r\nThe guy who helped me a lot:\r\n\r\nDanil Kazantsev\r\n\r\nThe first guy who has completed this game:\r\n\r\nAnton Buslaev\r\n\r\nSpecial thanks to the people who\r\nhelped me with testing\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nAnd thank you!",
        "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nGlobal Designer\r\n\r\n\r\nLevel Designer\r\n\r\n\r\nSprites Improver\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nScripts writer\r\n\r\n\r\nMain engineer",
        "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nMaxim Gudzikevich\r\n\r\n\r\nMaxim Gudzikevich\r\n\r\n\r\nMaxim Gudzikevich\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nMaxim Gudzikevich\r\n\r\n\r\n(Guess who)"};
}
