using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApplication
{
    // Classes
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuizId { get; set; }
        public bool IsMultipleChoice { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }

    public class QuizResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public DateTime DateTaken { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
    }

    // Interfaces
    public interface IUserRepository
    {
        User GetByLogin(string login);
        User GetById(int id);  
        void Add(User user);
        void Update(User user);
    }

    public interface IQuizRepository
    {
        IEnumerable<Quiz> GetAll();
        Quiz GetById(int id);
        IEnumerable<Question> GetQuestions(int quizId);
        IEnumerable<Answer> GetAnswers(int questionId);
    }

    public interface IQuizResultRepository
    {
        void Add(QuizResult result);
        IEnumerable<QuizResult> GetUserResults(int userId);
        IEnumerable<QuizResult> GetTopResults(int quizId, int count);
    }

    // Repositories
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;

        public User GetByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public void Update(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Login = user.Login;
                existingUser.Password = user.Password;
                existingUser.BirthDate = user.BirthDate;
            }
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }

    public class QuizRepository : IQuizRepository
    {
        private readonly List<Quiz> _quizzes;
        private readonly List<Question> _questions;
        private readonly List<Answer> _answers;

        public QuizRepository()
        {
            // Test data
            _quizzes = new List<Quiz>
            {
                new Quiz { Id = 1, Title = "History of Ukraine", Category = "History" },
                new Quiz { Id = 2, Title = "World geography", Category = "Geography" },
                new Quiz { Id = 3, Title = "Basics of biology", Category = "Biology" }
            };

            _questions = new List<Question>
            {
                // History questions
                new Question { Id = 1, QuizId = 1, Text = "In which year did WW2 start?", IsMultipleChoice = false },
                new Question { Id = 2, QuizId = 1, Text = "Who was the first president of Ukraine?", IsMultipleChoice = false },
                new Question { Id = 3, QuizId = 1, Text = "Which of these events happened in the XX cent.?", IsMultipleChoice = true },
                
                // Geography questions
                new Question { Id = 4, QuizId = 2, Text = "What`s the longest river in the world?", IsMultipleChoice = false },
                new Question { Id = 5, QuizId = 2, Text = "Capital of Australia?", IsMultipleChoice = false },
                
                // Biology questions
                new Question { Id = 6, QuizId = 3, Text = "How much chromosomes does a man have?", IsMultipleChoice = false },
                new Question { Id = 7, QuizId = 3, Text = "Which of these organisms are procariotes?", IsMultipleChoice = true }
            };

            _answers = new List<Answer>
            {
                // History answers
                new Answer { Id = 1, QuestionId = 1, Text = "1939", IsCorrect = true },
                new Answer { Id = 2, QuestionId = 1, Text = "1941", IsCorrect = false },
                new Answer { Id = 3, QuestionId = 1, Text = "1914", IsCorrect = false },

                new Answer { Id = 4, QuestionId = 2, Text = "Leonid Kravchuk", IsCorrect = true },
                new Answer { Id = 5, QuestionId = 2, Text = "Leonid Kuchma", IsCorrect = false },
                new Answer { Id = 6, QuestionId = 2, Text = "Donald Obama", IsCorrect = false },

                new Answer { Id = 7, QuestionId = 3, Text = "Fall of USSR", IsCorrect = true },
                new Answer { Id = 8, QuestionId = 3, Text = "WW2", IsCorrect = true },
                new Answer { Id = 9, QuestionId = 3, Text = "Abolition of serfdom", IsCorrect = false },
                
                // Geography answers
                new Answer { Id = 10, QuestionId = 4, Text = "Nile", IsCorrect = true },
                new Answer { Id = 11, QuestionId = 4, Text = "Amazon", IsCorrect = false },
                new Answer { Id = 12, QuestionId = 4, Text = "Yanzhi", IsCorrect = false },

                new Answer { Id = 13, QuestionId = 5, Text = "Kanberra", IsCorrect = true },
                new Answer { Id = 14, QuestionId = 5, Text = "Sidney", IsCorrect = false },
                new Answer { Id = 15, QuestionId = 5, Text = "Melburn", IsCorrect = false },
                
                // Biology answers
                new Answer { Id = 16, QuestionId = 6, Text = "46", IsCorrect = true },
                new Answer { Id = 17, QuestionId = 6, Text = "23", IsCorrect = false },
                new Answer { Id = 18, QuestionId = 6, Text = "64", IsCorrect = false },

                new Answer { Id = 19, QuestionId = 7, Text = "Bacterias", IsCorrect = true },
                new Answer { Id = 20, QuestionId = 7, Text = "Archeas", IsCorrect = true },
                new Answer { Id = 21, QuestionId = 7, Text = "Mushrooms", IsCorrect = false }
            };
        }

        public IEnumerable<Quiz> GetAll()
        {
            return _quizzes;
        }

        public Quiz GetById(int id)
        {
            return _quizzes.FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Question> GetQuestions(int quizId)
        {
            return _questions.Where(q => q.QuizId == quizId);
        }

        public IEnumerable<Answer> GetAnswers(int questionId)
        {
            return _answers.Where(a => a.QuestionId == questionId);
        }
    }

    public class QuizResultRepository : IQuizResultRepository
    {
        private readonly List<QuizResult> _results = new List<QuizResult>();
        private int _nextId = 1;

        public void Add(QuizResult result)
        {
            result.Id = _nextId++;
            _results.Add(result);
        }

        public IEnumerable<QuizResult> GetUserResults(int userId)
        {
            return _results.Where(r => r.UserId == userId);
        }

        public IEnumerable<QuizResult> GetTopResults(int quizId, int count)
        {
            var query = _results.AsQueryable();

            if (quizId != -1)
            {
                query = query.Where(r => r.QuizId == quizId);
            }

            return query.OrderByDescending(r => r.Score)
                       .ThenBy(r => r.DateTaken)
                       .Take(count);
        }
    }

    // Services
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string login, string password)
        {
            var user = _userRepository.GetByLogin(login);
            if (user == null || user.Password != password)
                return null;

            return user;
        }

        public bool Register(User newUser)
        {
            if (_userRepository.GetByLogin(newUser.Login) != null)
                return false;

            _userRepository.Add(newUser);
            return true;
        }
    }

    // Quiz service

    public class QuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizResultRepository _resultRepository;
        private readonly IUserRepository _userRepository;
        private readonly Random _random = new Random();

        public QuizService(IQuizRepository quizRepository, IQuizResultRepository resultRepository, IUserRepository userRepository)
        {
            _quizRepository = quizRepository;
            _resultRepository = resultRepository;
            _userRepository = userRepository;
        }

        public QuizResult TakeQuiz(int userId, int quizId)
        {
           
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found!");
                Console.ReadLine();
                return null;
            }

            var quiz = quizId == -1
                ? new Quiz { Id = -1, Title = "Mixed quiz", Category = "Mixed" }
                : _quizRepository.GetById(quizId);

            if (quiz == null && quizId != -1)
            {
                Console.WriteLine("Quiz not found!");
                Console.ReadLine();
                return null;
            }

            var allQuestions = quizId == -1
                ? _quizRepository.GetAll().SelectMany(q => _quizRepository.GetQuestions(q.Id)).ToList()
                : _quizRepository.GetQuestions(quizId).ToList();

            if (allQuestions.Count == 0)
            {
                Console.WriteLine("This quiz has no questions yet!");
                Console.ReadLine();
                return null;
            }

            var selectedQuestions = allQuestions
                .OrderBy(q => _random.Next())
                .Take(Math.Min(20, allQuestions.Count))
                .ToList();

            int score = 0;
            int questionNumber = 1;

            foreach (var question in selectedQuestions)
            {
                Console.Clear();
                Console.WriteLine($"Question {questionNumber}/{selectedQuestions.Count}");
                Console.WriteLine(question.Text);

                var answers = _quizRepository.GetAnswers(question.Id).OrderBy(a => _random.Next()).ToList();

                for (int i = 0; i < answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {answers[i].Text}");
                }

                Console.WriteLine(question.IsMultipleChoice ?
                    "Choose the right answer numbers through a coma:" :
                    "Choose a number for the right answer:");

                var userInput = Console.ReadLine();

                if (CheckAnswer(question, answers, userInput))
                {
                    score++;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Not really correct!");
                    var correctAnswers = answers.Where(a => a.IsCorrect).Select(a => a.Text);
                    Console.WriteLine($"Right answers: {string.Join(", ", correctAnswers)}");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                questionNumber++;
            }

            var result = new QuizResult
            {
                UserId = userId,
                UserLogin = user.Login,
                QuizId = quiz?.Id ?? -1,
                QuizTitle = quiz?.Title ?? "Mixed quiz",
                DateTaken = DateTime.Now,
                Score = score,
                TotalQuestions = selectedQuestions.Count
            };

            _resultRepository.Add(result);

            ShowQuizResults(result);

            return result;
        }

        private bool CheckAnswer(Question question, List<Answer> answers, string userInput)
        {
            try
            {
                if (question.IsMultipleChoice)
                {
                    var selectedIndices = userInput.Split(',')
                        .Select(s => int.Parse(s.Trim()) - 1)
                        .ToList();

                    var selectedAnswers = selectedIndices
                        .Where(i => i >= 0 && i < answers.Count)
                        .Select(i => answers[i]);

                    var correctAnswers = answers.Where(a => a.IsCorrect).ToList();

                    return selectedAnswers.Count() == correctAnswers.Count &&
                           selectedAnswers.All(a => a.IsCorrect);
                }
                else
                {
                    int selectedIndex = int.Parse(userInput.Trim()) - 1;
                    return selectedIndex >= 0 &&
                           selectedIndex < answers.Count &&
                           answers[selectedIndex].IsCorrect;
                }
            }
            catch
            {
                return false;
            }
        }

        private void ShowQuizResults(QuizResult result)
        {
            Console.Clear();
            Console.WriteLine("Quiz complete!");
            Console.WriteLine($"Your result: {result.Score} from {result.TotalQuestions}");

            var topResults = _resultRepository.GetTopResults(result.QuizId, 20).ToList();
            var userPosition = topResults.FindIndex(r => r.Id == result.Id) + 1;

            if (userPosition > 0)
            {
                Console.WriteLine($"Your place in the top: {userPosition}");
            }

            Console.WriteLine("\nTop-20 players:");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("| Place | Login | Result | Data |");
            Console.WriteLine("-------------------------------------------------------");

            for (int i = 0; i < Math.Min(topResults.Count, 20); i++)
            {
                var topResult = topResults[i];
                Console.WriteLine($"| {i + 1,5} | {topResult.UserLogin,-15} | {topResult.Score,3}/{topResult.TotalQuestions,-3} | {topResult.DateTaken:dd.MM.yyyy} |");
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("\n Press Enter to exit to menu...");
            Console.ReadLine();
        }

        public IEnumerable<QuizResult> GetUserResults(int userId)
        {
            return _resultRepository.GetUserResults(userId);
        }

        public IEnumerable<QuizResult> GetTopResults(int quizId)
        {
            return _resultRepository.GetTopResults(quizId, 20);
        }
    }

    // Main program
    class Program
    {
        private static IUserRepository _userRepository;
        private static IQuizRepository _quizRepository;
        private static IQuizResultRepository _quizResultRepository;
        private static AuthService _authService;
        private static QuizService _quizService;

        static void Main()
        {
            InitializeServices();

            Console.WriteLine("Welcome!");
            Console.WriteLine("----------------------------");

            while (true)
            {
                Console.WriteLine("\nMain menu:");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Registration");
                Console.WriteLine("3. Exit");
                Console.Write("Choose your action: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LoginUser();
                        break;
                    case "2":
                        RegisterUser();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Uncorrect!.");
                        break;
                }
            }
        }

        static void InitializeServices()
        {
            _userRepository = new UserRepository();
            _quizRepository = new QuizRepository();
            _quizResultRepository = new QuizResultRepository();

            _authService = new AuthService(_userRepository);
            _quizService = new QuizService(_quizRepository, _quizResultRepository, _userRepository);
        }

        static void LoginUser()
        {
            Console.Write("\nEnter login: ");
            var login = Console.ReadLine();

            Console.Write("Enter password: ");
            var password = Console.ReadLine();

            var user = _authService.Login(login, password);

            if (user != null)
            {
                Console.WriteLine($"\nWelcome, {user.Login}!");
                ShowUserMenu(user);
            }
            else
            {
                Console.WriteLine("Either login or password isn`t right!.");
            }
        }

        static void RegisterUser()
        {
            Console.Write("\nMake up a login: ");
            var login = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Login can`t be empty!");
                return;
            }

            Console.Write("Придумайте пароль: ");
            var password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password can`t be empty!");
                return;
            }

            Console.Write("Enter your birth date (mm.dd.yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var birthDate))
            {
                Console.WriteLine("Uncorrect data format!");
                return;
            }

            var newUser = new User
            {
                Login = login,
                Password = password,
                BirthDate = birthDate,
                RegistrationDate = DateTime.Now
            };

            if (_authService.Register(newUser))
            {
                Console.WriteLine("Registration complete! Now you shall pass.");
            }
            else
            {
                Console.WriteLine("User with such a login already exists!");
            }
        }

        static void ShowUserMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("\nUser menu:");
                Console.WriteLine("1. Start a new quiz");
                Console.WriteLine("2. Check my results");
                Console.WriteLine("3. Check top-20");
                Console.WriteLine("4. Settings");
                Console.WriteLine("5. Exit");
                Console.Write("Choose your action: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartNewQuiz(user);
                        break;
                    case "2":
                        ShowUserResults(user);
                        break;
                    case "3":
                        ShowTopResults();
                        break;
                    case "4":
                        ShowSettings(user);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Uncorrect!");
                        break;
                }
            }
        }

        static void StartNewQuiz(User user)
        {
            Console.WriteLine("\nChoose your category:");
            Console.WriteLine("0. Mixed quiz");

            var quizzes = _quizRepository.GetAll().ToList();
            for (int i = 0; i < quizzes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quizzes[i].Title} ({quizzes[i].Category})");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= quizzes.Count)
            {
                int quizId = choice == 0 ? -1 : quizzes[choice - 1].Id;
                _quizService.TakeQuiz(user.Id, quizId);
            }
            else
            {
                Console.WriteLine("Uncorrect!.");
            }
        }

        static void ShowUserResults(User user)
        {
            var results = _quizService.GetUserResults(user.Id).ToList();

            if (!results.Any())
            {
                Console.WriteLine("\n No quiz results yet...");
                Console.WriteLine("Press enter to continie...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nYour results:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| Date | Quiz| Result|");
            Console.WriteLine("--------------------------------------------------");

            foreach (var result in results.OrderByDescending(r => r.DateTaken))
            {
                Console.WriteLine($"| {result.DateTaken:dd.MM.yyyy} | {result.QuizTitle,-18} | {result.Score,3}/{result.TotalQuestions,-3} |");
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("\nEnter to continue...");
            Console.ReadLine();
        }

        static void ShowTopResults()
        {
            var quizzes = _quizRepository.GetAll().ToList();

            Console.WriteLine("\nChoose a quiz to see it`s top players:");
            Console.WriteLine("0. Common rating");

            for (int i = 0; i < quizzes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quizzes[i].Title}");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= quizzes.Count)
            {
                int quizId = choice == 0 ? -1 : quizzes[choice - 1].Id;
                var topResults = _quizService.GetTopResults(quizId).ToList();

                string title = quizId == -1 ? "Common rating" : $"Top-20: {quizzes[choice - 1].Title}";

                Console.WriteLine($"\n{title}");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("| Place | Login | Result | Data |");
                Console.WriteLine("-------------------------------------------------------");

                for (int i = 0; i < topResults.Count; i++)
                {
                    var result = topResults[i];
                    Console.WriteLine($"| {i + 1,5} | {result.UserLogin,-15} | {result.Score,3}/{result.TotalQuestions,-3} | {result.DateTaken:dd.MM.yyyy} |");
                }

                Console.WriteLine("-------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("Uncorrect!");
            }

            Console.WriteLine("\nPress Enter...");
            Console.ReadLine();
        }

        static void ShowSettings(User user)
        {
            while (true)
            {
                Console.WriteLine("\n User settings:");
                Console.WriteLine($"1. Change password (current: {user.Password})");
                Console.WriteLine($"2. Change birth date (current: {user.BirthDate:dd.MM.yyyy})");
                Console.WriteLine("3. Return to user menu");
                Console.Write("Choose your action: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ChangePassword(user);
                        break;
                    case "2":
                        ChangeBirthDate(user);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Uncorrect!");
                        break;
                }
            }
        }

        static void ChangePassword(User user)
        {
            Console.Write("\nEnter new password: ");
            var newPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.WriteLine("Password can`t be empty!");
                return;
            }

            user.Password = newPassword;
            _userRepository.Update(user);
            Console.WriteLine("Password changed!");
        }

        static void ChangeBirthDate(User user)
        {
            Console.Write("\nEnter new birth date: ");
            if (DateTime.TryParse(Console.ReadLine(), out var newBirthDate))
            {
                user.BirthDate = newBirthDate;
                _userRepository.Update(user);
                Console.WriteLine("Birth date changed!");
            }
            else
            {
                Console.WriteLine("Uncorrect date format!");
            }
        }
    }
}
