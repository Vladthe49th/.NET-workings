using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DictionaryApp
{
    // Dict. type
    public enum DictionaryType
    {
        EngRus,
        RusEng,
        
    }

    //Dyct. class

    public class Dictionary
    {
        public DictionaryType Type { get; set; }
        public string Name { get; set; }
        public Dictionary<string, List<string>> Words { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary(DictionaryType type, string name)
        {
            Type = type;
            Name = name;
        }
    }


    //Dyct. manager
    public class DictionaryManager
    {
        private List<Dictionary> dictionaries = new List<Dictionary>();
        private const string DictionariesFolder = "Dictionaries";

        public DictionaryManager()
        {
            LoadDictionaries();
        }


        //Dyct. create
        public void CreateDictionary(DictionaryType type, string name)
        {
            if (dictionaries.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Dictionary with that name already exists!");
                return;
            }

            var dictionary = new Dictionary(type, name);
            dictionaries.Add(dictionary);
            SaveDictionary(dictionary);
            Console.WriteLine($"Dictionary '{name}' created successfully.");
        }

        //Add a word
        public void AddWord(string dictionaryName, string word, string translation)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (dictionary.Words.ContainsKey(word))
            {
                dictionary.Words[word].Add(translation);
            }
            else
            {
                dictionary.Words[word] = new List<string> { translation };
            }

            SaveDictionary(dictionary);
            Console.WriteLine($"Word '{word}' with translation '{translation}' added to dictionary '{dictionaryName}'.");
        }


        // Replace word
        public void ReplaceWord(string dictionaryName, string oldWord, string newWord)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (!dictionary.Words.ContainsKey(oldWord))
            {
                Console.WriteLine("Word not found in the dictionary!");
                return;
            }

            var translations = dictionary.Words[oldWord];
            dictionary.Words.Remove(oldWord);
            dictionary.Words[newWord] = translations;

            SaveDictionary(dictionary);
            Console.WriteLine($"Word '{oldWord}' replaced by '{newWord}'.");
        }

        //Translation replace

        public void ReplaceTranslation(string dictionaryName, string word, string oldTranslation, string newTranslation)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (!dictionary.Words.ContainsKey(word))
            {
                Console.WriteLine("Word not found!");
                return;
            }

            var translations = dictionary.Words[word];
            if (!translations.Contains(oldTranslation))
            {
                Console.WriteLine("Translation not found!");
                return;
            }

            translations.Remove(oldTranslation);
            translations.Add(newTranslation);

            SaveDictionary(dictionary);
            Console.WriteLine($"Translation '{oldTranslation}' replaced with '{newTranslation}'.");
        }


        // Word remove
        public void RemoveWord(string dictionaryName, string word)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (!dictionary.Words.ContainsKey(word))
            {
                Console.WriteLine("Word not found!");
                return;
            }

            dictionary.Words.Remove(word);
            SaveDictionary(dictionary);
            Console.WriteLine($"Word '{word}' and it`s translations deleted from '{dictionaryName}'.");
        }


        // Translation remove
        public void RemoveTranslation(string dictionaryName, string word, string translation)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (!dictionary.Words.ContainsKey(word))
            {
                Console.WriteLine("Word not found!");
                return;
            }

            var translations = dictionary.Words[word];
            if (translations.Count <= 1)
            {
                Console.WriteLine("Can`t remove the translation!");
                return;
            }

            if (!translations.Contains(translation))
            {
                Console.WriteLine("Translation not found!");
                return;
            }

            translations.Remove(translation);
            SaveDictionary(dictionary);
            Console.WriteLine($"Translation '{translation}' deleted for '{word}'.");
        }

        // Translation search
        public void SearchTranslation(string dictionaryName, string word)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (!dictionary.Words.ContainsKey(word))
            {
                Console.WriteLine("Word not found!");
                return;
            }

            Console.WriteLine($"Translation(s) of the word '{word}':");
            foreach (var translation in dictionary.Words[word])
            {
                Console.WriteLine($"- {translation}");
            }
        }


        // Export a word
        public void ExportWord(string dictionaryName, string word, string exportFilePath)
        {
            var dictionary = dictionaries.FirstOrDefault(d => d.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase));
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not found!");
                return;
            }

            if (!dictionary.Words.ContainsKey(word))
            {
                Console.WriteLine("Word not found!");
                return;
            }

            var exportData = new
            {
                Word = word,
                Translations = dictionary.Words[word],
                Dictionary = dictionary.Name,
                Type = dictionary.Type.ToString()
            };

            var json = JsonSerializer.Serialize(exportData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(exportFilePath, json);
            Console.WriteLine($"Word '{word}' and it`s translations exported to '{exportFilePath}'.");
        }


        // List
        public void ListDictionaries()
        {
            Console.WriteLine("Available dictionaries:");
            foreach (var dict in dictionaries)
            {
                Console.WriteLine($"- {dict.Name} ({dict.Type}), number of words: {dict.Words.Count}");
            }
        }

        private void LoadDictionaries()
        {
            if (!Directory.Exists(DictionariesFolder))
            {
                Directory.CreateDirectory(DictionariesFolder);
                return;
            }

            foreach (var file in Directory.GetFiles(DictionariesFolder, "*.json"))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var dictionary = JsonSerializer.Deserialize<Dictionary>(json);
                    dictionaries.Add(dictionary);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to load dictionary from {file}: {ex.Message}");
                }
            }
        }


        // Dictionary save
        private void SaveDictionary(Dictionary dictionary)
        {
            if (!Directory.Exists(DictionariesFolder))
            {
                Directory.CreateDirectory(DictionariesFolder);
            }

            var filePath = Path.Combine(DictionariesFolder, $"{dictionary.Name}.json");
            var json = JsonSerializer.Serialize(dictionary, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }



    // The program in action
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new DictionaryManager();
            ShowMainMenu(manager);
        }

        static void ShowMainMenu(DictionaryManager manager)
        {
            while (true)
            {
                Console.WriteLine("\nThe main menu:");
                Console.WriteLine("1. Create dictionary");
                Console.WriteLine("2. Work with a dictionary");
                Console.WriteLine("3. Dictionary list");
                Console.WriteLine("4. Exit");

                Console.Write("Choose your action: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateDictionary(manager);
                        break;
                    case "2":
                        WorkWithDictionary(manager);
                        break;
                    case "3":
                        manager.ListDictionaries();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Wrong choice...");
                        break;
                }
            }
        }

        static void CreateDictionary(DictionaryManager manager)
        {
            Console.WriteLine("\nCreate a dictionary:");
            Console.Write("Dictionary name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Dictionary type:");
            Console.WriteLine("1. End-rus");
            Console.WriteLine("2. Rus-eng");
            Console.Write("Your choice: ");
            var typeChoice = Console.ReadLine();

            DictionaryType type;
            switch (typeChoice)
            {
                case "1":
                    type = DictionaryType.EngRus;
                    break;
                case "2":
                    type = DictionaryType.RusEng;
                    break;
                default:
                    Console.WriteLine("Wrong choice... Gonna use Eng-rus as default");
                    type = DictionaryType.EngRus;
                    break;
            }

            manager.CreateDictionary(type, name);
        }

        static void WorkWithDictionary(DictionaryManager manager)
        {
            Console.Write("\nDictionary name: ");
            var name = Console.ReadLine();

            var dictionary = manager.GetType().GetMethod("ListDictionaries").Invoke(manager, null);


            while (true)
            {
                Console.WriteLine("\nDictionary action menu:");
                Console.WriteLine("1. Add word and translation");
                Console.WriteLine("2. Replace word");
                Console.WriteLine("3. Replace translation");
                Console.WriteLine("4. Delete a word");
                Console.WriteLine("5. Delete a translation");
                Console.WriteLine("6. Find a translation");
                Console.WriteLine("7. Export words to file");
                Console.WriteLine("8. Return to main menu");

                Console.Write("Your action: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter a word: ");
                        var word = Console.ReadLine();
                        Console.Write("Enter it`s translation: ");
                        var translation = Console.ReadLine();
                        manager.AddWord(name, word, translation);
                        break;
                    case "2":
                        Console.Write("Enter a word to replace: ");
                        var oldWord = Console.ReadLine();
                        Console.Write("Enter a new word: ");
                        var newWord = Console.ReadLine();
                        manager.ReplaceWord(name, oldWord, newWord);
                        break;
                    case "3":
                        Console.Write("Enter word: ");
                        var wordToReplace = Console.ReadLine();
                        Console.Write("Enter the translation to replace: ");
                        var oldTranslation = Console.ReadLine();
                        Console.Write("Enter new translation: ");
                        var newTranslation = Console.ReadLine();
                        manager.ReplaceTranslation(name, wordToReplace, oldTranslation, newTranslation);
                        break;
                    case "4":
                        Console.Write("Enter word for deletion: ");
                        var wordToDelete = Console.ReadLine();
                        manager.RemoveWord(name, wordToDelete);
                        break;
                    case "5":
                        Console.Write("Enter the word: ");
                        var wordForTranslation = Console.ReadLine();
                        Console.Write("Enter a translation to delete: ");
                        var translationToDelete = Console.ReadLine();
                        manager.RemoveTranslation(name, wordForTranslation, translationToDelete);
                        break;
                    case "6":
                        Console.Write("Enter a translation to delete: ");
                        var wordToSearch = Console.ReadLine();
                        manager.SearchTranslation(name, wordToSearch);
                        break;
                    case "7":
                        Console.Write("Enter a word to export: ");
                        var wordToExport = Console.ReadLine();
                        Console.Write("Show me the way for export: ");
                        var exportPath = Console.ReadLine();
                        manager.ExportWord(name, wordToExport, exportPath);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Wrong choice, mate!");
                        break;
                }
            }
        }
    }
}
