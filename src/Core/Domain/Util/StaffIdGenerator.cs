namespace OpsManagerAPI.Domain.Util;

public class StaffIdGenerator
{
    private static readonly Random Random = new();

    public static string GenerateUniqueId(string firstName, string lastName)
    {
        // Generate a random number with 4 digits
        int randomNumber = Random.Next(1000, 10000);

        // Return the unique ID combining initials and random number
        return $"{GetInitials(firstName, lastName)}{randomNumber}";
    }

    private static string GetInitials(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("First name and last name must be provided.");
        }

        return $"{firstName[0]}{lastName[0]}".ToUpper();
    }
}
