using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Audio_sampler.Player
{
    public class SamplePool
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo")] 
        private static readonly string[] DefaultPoolNames =
        {
            "Да", //1
            "Нет", //1
            "Не знаю", //1
            "За мной", //1
            "Стоп", //1
            "Ждать", //1
            "Назад", //1
            "Вперед", //1
            "Помоги мне", //1
            "Радость", //2
            "Огорчение", //2
            "Ярость", //2
            "Страх", //2
            "Удивление", //2
            "Вопрос", //2
            "Вздох", //2
            "Смех", //2
            "Боль", //2
            "Что?", //3
            "Где?", //3
            "Когда?", //3
            "Почему?", //3
            "Зачем?", //3
            "Согласие", //3
            "Любопытство", //3
            "Безразличие", //3
            "Отвращение", //3
            "Эй!", //4
            "Хочу", //4
            "Я помогу", //4
            "Быстрее", //4
            "Тише", //4
            "Облегчение", //4
            "Заглушка 1", //4
            "Заглушка 2", //4
            "Заглушка 3" //4
        };

        public readonly string Name;

        private List<Sample> Samples { get; } = new List<Sample>();

        public SamplePool(string path, int index)
        {
            var directories = Directory.GetDirectories(path);

            Name = directories.Length == 0 ? DefaultPoolNames[index - 1] : directories[0].Substring(directories[0].LastIndexOf('\\') + 1);

            var files = Directory.GetFiles(path);

            foreach (var file in files) Samples.Add(new Sample(file));
        }
        internal string GetSamplePath()
        {
            return Samples[App.Random.Next(Samples.Count)].Path;
        }
    }
}