using System.Text.RegularExpressions;

//("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")

string password = "CSharp@1.";

//break down the reg into pattern to list
var patterns = new List<string> { 
"(?=^.{8,15}$)", "(?=.*?[A-Z])", "(?=.*?[a-z])",
 "(?=.*?[0-9])", "(?=.*?[#?!@$%^&*-])"};

//Error message of the pattern to list
var ErrorMessagse = new List<string>{
    "Password must at least have 8 -15 characters in length.",
    "Password must at least have one uppercase letter (A-Z).",
    "Password must at least have one lowercase letter (a-z).",
    "Password must at least have one digit (0-9).",
    "Password must at least have one special character (@,#,%,&,!,$,etc…).",
};

//looping through the pattern list match with the password
//if match is true continue, if match false display the error
for (var x = 0; x < patterns.Count; x++)
{
    var validate = new Regex(patterns[x]);

    if (!validate.IsMatch(password))
    {

        Console.WriteLine(ErrorMessagse[x]);
    }
    else
    {
        continue;
    }
}

Console.WriteLine($"======Now checking if password doesn't contain any blacklisted words");

var blacklisted = new List<string> {"123","password", "axium", "admin", "administrator" };

var validator = blacklisted
                .Select(x => new Regex($@"\w*{Regex.Escape(x)}\w*", RegexOptions.IgnoreCase))
                .ToList();
if (validator.Any(valid => valid.IsMatch(password)))
{
    Console.WriteLine($"Invalid Password, password contains blacklisted word: {password}");
}

Console.WriteLine($"=========checking for consecutive repeating characters");

for(var x = 0; x< password.Length -2; x++){
    if(password[x].Equals(password[x+1]) && password[x].Equals(password[x+2])){
        
        Console.WriteLine($"Password can not have consecutive repeating characters.");
        break;
    }
}
