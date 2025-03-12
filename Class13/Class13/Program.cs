namespace Class13
{
    public class CreditCard
    {

        public string CardNumber { get; private set; }
        public string OwnerName { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        private int _pin;
        public decimal CreditLimit { get; private set; }
        private decimal _balance;

        // Events
        public event Action<decimal> OnDeposit;          // Deposit to card
        public event Action<decimal> OnWithdraw;        // Withdraw from card
        public event Action OnCreditUsageStarted;       // Credit start
        public event Action<decimal> OnTargetAmountReached; // Reach the target amount
        public event Action OnPinChanged;               // Pin change

        // Constructor
        public CreditCard(string cardNumber, string ownerName, DateTime expiryDate, int pin, decimal creditLimit)
        {
            CardNumber = cardNumber;
            OwnerName = ownerName;
            ExpiryDate = expiryDate;
            _pin = pin;
            CreditLimit = creditLimit;
            _balance = 0;
        }

        // Deposit
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Sum of depozit must be more than zero, right?");

            _balance += amount;
            OnDeposit?.Invoke(amount); // Invoke deposit
        }

        // Withdraw
        public void Withdraw(decimal amount, int pin)
        {
            if (pin != _pin)
                throw new ArgumentException("Wrong pin!");

            if (amount <= 0)
                throw new ArgumentException("Sum of withdraw must be more than zero!");

            if (_balance + CreditLimit < amount)
                throw new InvalidOperationException("You`re out of money.");

            _balance -= amount;
            OnWithdraw?.Invoke(amount); // Invoke withdraw

            if (_balance < 0)
                OnCreditUsageStarted?.Invoke(); // Invoke credit start
        }

        // Pin change
        public void ChangePin(int oldPin, int newPin)
        {
            if (oldPin != _pin)
                throw new ArgumentException("This is already your pin!");

            _pin = newPin;
            OnPinChanged?.Invoke(); // Change pin invoke
        }

        // Check if target sum has been reached
        public void CheckTargetAmount(decimal targetAmount)
        {
            if (_balance >= targetAmount)
                OnTargetAmountReached?.Invoke(_balance); // Method invoke
        }

        // Receive your current balance
        public decimal GetBalance()
        {
            return _balance;
        }
    }


    internal class Program
    {

       

        static void Main()
        {
            // Credit card
            var card = new CreditCard("1234 5678 9012 3456", "Selvestre Petrov", new DateTime(2025, 12, 31), 1234, 50000);

            // Events subscription
            card.OnDeposit += amount => Console.WriteLine($"You made a deposit of {amount} $. Current balance: {card.GetBalance()} $.");
            card.OnWithdraw += amount => Console.WriteLine($"Withdrawed {amount} $. Currenrt balance: {card.GetBalance()} $.");
            card.OnCreditUsageStarted += () => Console.WriteLine("You are now using your credit card.");
            card.OnTargetAmountReached += balance => Console.WriteLine($"Reached the target amount! Current balance: {balance} $.");
            card.OnPinChanged += () => Console.WriteLine("PIN-code changed.");

            // Deposit
            card.Deposit(10000);

            // Withdraw
            card.Withdraw(5000, 1234);

            // Target check
            card.CheckTargetAmount(15000);

            // Change pin
            card.ChangePin(1234, 4321);

            // Withdraw more than target amount and start using credits
            card.Withdraw(20000, 4321);

            // And balance is:
            Console.WriteLine($"Current balance: {card.GetBalance()} $.");
        
    }

    }
}
