char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

Console.WriteLine("Welcome to my Pig Latin translator!");

//Main program loop
do
{
    string word = GetInput();

    string newLine = "";

    string[] words = word.Split(" ");
    string[] wordCase = new string[words.Length];
    string[] frontMarks = new string[words.Length];
    string[] endMarks = new string[words.Length];

    for(int i = 0; i <  words.Length; i++)
    {
        frontMarks[i] = GetFrontMark(words[i]);
        endMarks[i] = GetEndMark(words[i]);
        words[i] = RemoveFrontMark(words[i]);
        words[i] = RemoveEndMark(words[i]);

        wordCase[i] = GetCase(words[i]);

        if (!NumbersAndSymbolsChecker(words[i])) //converts word to lowercase unless it has numbers or symbols, in which case it's not getting translated
        {
            words[i] = words[i].ToLower();
        }

        words[i] = frontMarks[i] + AdjustCase(GetTranslatedWord(words[i], vowels), wordCase[i]) + endMarks[i];

        newLine += words[i];

        if (i < words.Length - 1)
        {
            newLine += " ";
        }
    }

    Console.WriteLine(newLine);

    Console.WriteLine("Would you like to translate another word?  Enter yes or y to continue, enter anything else to exit");
    string input = Console.ReadLine();
    if (input.ToLower() != "y" && input.ToLower() != "yes") break;
} while (true);

//if word ends with punctuation, return the punctuation, otherwise returns empty string
static string GetEndMark(string str)
{
    if(String.IsNullOrEmpty(str))
    {
        return str;
    }
    else if (Char.IsPunctuation(str[str.Length - 1]))
    {
        return Char.ToString(str[str.Length - 1]);
    }
    else
    {
        return "";
    }
}

//if word starts with punctuation, return the punctuation, otherwise returns empty string
static string GetFrontMark(string str)
{
    if(String.IsNullOrEmpty(str))
    {
        return str;
    }
    else if (Char.IsPunctuation(str[0]))
    {
        return Char.ToString(str[0]);
    }
    else
    {
        return "";
    }
}

//if word ends with punctuation, returns word with last character removed, otherwise returns the same string
static string RemoveEndMark(string str)
{
    if (String.IsNullOrEmpty(str))
    {
        return str;
    }
    else if (Char.IsPunctuation(str[str.Length - 1]))
    {
        return str.Substring(0, str.Length - 1);
    }
    else
    {
        return str;
    }
}

//if word starts with punctuation, returns word with first character removed, otherwise returns the same string
static string RemoveFrontMark(string str)
{
    if (String.IsNullOrEmpty(str))
    {
        return str;
    }
    else if (Char.IsPunctuation(str[0]))
    {
        return str.Substring(1);
    }
    else
    {
        return str;
    }
}

//gets input from the user, looping to make sure they actually input something
static string GetInput()
{
    string str;

    do
    {
        Console.WriteLine("Please enter a word to be translated");
        str = Console.ReadLine();

        if (String.IsNullOrEmpty(str))
        {
            Console.WriteLine("Nothing was input");
        }

    } while (String.IsNullOrEmpty(str));

    return str;
}

//returns case of word if it can be determined, otherwise returns space
static string GetCase(string str)
{
    if (IsAllUpper(str))
    {
        return "UPPER";
    }
    else if (IsAllLower(str))
    {
        return "lower";
    }
    else if (IsTitleCase(str))
    {
        return "Title";
    }
    else
    {
        return " ";
    }
}

//checks to see if the first character of a string is a vowel
static bool VowelChecker(string str, char[] vowels)
{
    for (int i = 0; i < vowels.Length; i++)
    {
        if(String.IsNullOrEmpty(str))
        {
            break;
        }
        else if (vowels[i] == Char.ToLower(str[0]))
        {
            return true;
        }
    }
    return false;
}

//checks to see if a string contains something other than letters or '
static bool NumbersAndSymbolsChecker(string str)
{
    for (int i = 0; i < str.Length; i++)
    {
        if (!Char.IsLetter(str[i]) && str[i] != '\'')
        {
            return true;
        }
    }
    return false;
}

//returns word translated to pig latin
static string GetTranslatedWord(string str, char[] vowels)
{
    if (!NumbersAndSymbolsChecker(str))
    {
        if (VowelChecker(str, vowels))
        {
            return str + "way";
        }
        else
        {
            string startingLetters = "";
            //gets starting letters if not a vowel
            int count = 0;
            do
            {
                if (count == str.Length)
                {
                    startingLetters = sometimesY(startingLetters);
                    break;
                }
                else if ((String.IsNullOrEmpty(str) || str.Length == 1) || vowels.Contains(str[count]))
                {
                    break;
                }
                else
                {
                    startingLetters += Char.ToString(str[count]);
                    count++;
                }

            } while (true);

            if(String.IsNullOrEmpty(str))
            {
                return "";
            }


            return str.Substring(count) + startingLetters + "ay";
        }
    }
    else
    {
        return str;
    }
}

//if a string ends in a y this returns it with the y moved to the front.
//This should only be called to avoid the program breaking if a word has no vowels other than y
static string sometimesY(string str)
{
    if (String.IsNullOrEmpty(str))
    {
        return str;
    }
    else if (str[str.Length - 1] == 'y')
    {
        return "y" + str.Substring(0, str.Length - 1);
    }
    else
    {
        return str;
    }
}

//Checks to see if a string is all uppercase letters
static bool IsAllUpper(string str)
{
    foreach(char c in str)
    {
        if (!Char.IsUpper(c) && Char.IsLetter(c)) return false;
    }
    return true;
}

//Checks to see if a string is all lowercase letters
static bool IsAllLower(string str)
{
    foreach (char c in str)
    {
        if (!Char.IsLower(c) && Char.IsLetter(c)) return false;
    }
    return true;
}

//checks to see if a string is titlecase
static bool IsTitleCase(string str)
{
    if (!Char.IsUpper(str[0]) && Char.IsLetter(str[0])) return false;

    for(int i = 1; i < str.Length; i++)
    {
        if (!Char.IsLower(str[i]) && Char.IsLetter(str[i])) return false;
    }
    return true;
}

//Takes a string and returns it titlecase
static string MakeTitleCase(string str)
{
    str = str.ToLower();
    char firstChar = Char.ToUpper(str[0]);
    return Char.ToString(firstChar) + str.Substring(1);
}

//Adjusts the case of a string based on the case given
static string AdjustCase(string str, string strCase)
{
    //This was visual studio's idea
    return strCase switch
    {
        "UPPER" => str.ToUpper(),
        "lower" => str.ToLower(),
        "Title" => MakeTitleCase(str),
        _ => str,
    };
}