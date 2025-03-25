using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace Home17
{
    class Note
    {
        public string Title { get; set; }
       public  string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public Note (string title, string text)
        {
            Title = title; Text = text; CreatedAt = DateTime.Now;
        }

    }

    class NoteManager
    { 
      
        private List<Note> notes = new List<Note> ();
        private const string FileName = "notes.json";

        public void AddNote()
        {
            Console.WriteLine("Enter title of the note: ");
            string title = Console.ReadLine ();
            Console.WriteLine("Write text for the note: ");
            string text = Console.ReadLine ();

            notes.Add (new Note (title, text));
            Console.WriteLine("Note added!");
        }

        public void DeleteNote()
        {
            Console.WriteLine("Enter title of the note: ");
            string title = Console.ReadLine ();
            notes.RemoveAll(n => n.Title == title);
            Console.WriteLine("Note deleted!");
        }

        public void EditNote()
        {
            Console.WriteLine("Enter title of the note to edit: ");
            string title = Console.ReadLine ();
            Note note = notes.Find (n => n.Title == title);

            if (note != null)
            {
                Console.Write("Enter new text: ");
                note.Text = Console.ReadLine();
                Console.WriteLine("Note updated!\n");
            }
            else
            {
                Console.WriteLine("Заметка не найдена.\n");
            }
        }

        public void SaveToFile()
        {
            File.WriteAllText(FileName, JsonSerializer.Serialize(notes));
            Console.WriteLine("Notes saved to file!\n");
        }

        public void LoadFromFile()
        {
            if (File.Exists(FileName))
            {
                notes = JsonSerializer.Deserialize<List<Note>>(File.ReadAllText(FileName)) ?? new List<Note>();
                Console.WriteLine("Notes loaded from file!\n");
            }
        }

        public void ListNotes()
        {
            foreach (var note in notes)
            {
                Console.WriteLine($"[{note.CreatedAt}] {note.Title}: {note.Text}\n");
            }
        }
    }


    internal class Program
    {





        static void Main(string[] args)
        {
            Console.WriteLine("Main task: \n");


            NoteManager manager = new NoteManager();
            manager.LoadFromFile();

            while (true)
            {
                Console.WriteLine("1. Add a note.\n2. Delete from file.\n3. Edit note.\n4. Show notes.\n5. Save to file. \n6. Load from file. \n7. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": manager.AddNote(); break;
                    case "2": manager.DeleteNote(); break;
                    case "3": manager.EditNote(); break;
                    case "4": manager.ListNotes(); break;
                    case "5": manager.SaveToFile(); break;
                    case "6": manager.LoadFromFile();break;
                    case "7": return;
                    default: Console.WriteLine("Wrong choice!"); break;
                }
            }


            ///////////////////////////
            Console.WriteLine("\n Sub-task - Filestream sum ");

            string filePath = "numbers.txt";

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);

                    string contents = Encoding.UTF8.GetString(buffer);
                    string[] numbers = contents.Split(',');

                    int sum = 0;
                    foreach (string num in numbers)
                    {
                        if (int.TryParse(num.Trim(), out int value))
                        {
                            sum += value;
                        }
                    }

                    Console.WriteLine($"Sum on nums: {sum}");
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Err: {ex.Message}");
            }



            ///////////////////////////
            Console.WriteLine("\n Sub-task - File statistics ");

            Console.WriteLine("Enter path to file: ");
            string path = Console.ReadLine();

            if(!File.Exists(path))
            {
                Console.WriteLine("File not found!");
                return;
            }

            string content = File.ReadAllText(path);


            int sentenceCount = Regex.Matches(content, "[.!?]").Count;
            int upperCaseCount = content.Count(char.IsUpper);
            int lowerCaseCount = content.Count(char.IsLower);
            int vowelCount = content.Count(c => "AEIOUY".Contains(c));
            int consonantCount = content.Count(c => "BCDFGHJKLMNPQRSTVWXZ".Contains(c));
            int digitCount = content.Count(char.IsDigit);

            Console.WriteLine("File statistics:");
            Console.WriteLine($" Sentence count: {sentenceCount}");
            Console.WriteLine($" Upper case count: {upperCaseCount}");
            Console.WriteLine($"Lower case count: {lowerCaseCount}");
            Console.WriteLine($"Vowel count: {vowelCount}");
            Console.WriteLine($"Consonant count: {consonantCount}");
            Console.WriteLine($" Digit count: {digitCount}");

        }
    }
    
}
