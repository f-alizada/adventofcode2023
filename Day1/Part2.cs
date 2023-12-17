using System.Text;

public class Part2
{
    public static void Main(string[] args)
    {
        GetCalibrationValue("7pqrstsixteen");
    }

    private static Dictionary<string, char> numberData = new Dictionary<string, char>()
    {
        { "one", '1' },
        { "two", '2' },
        { "three", '3' },
        { "four", '4' },
        { "five", '5' },
        { "six", '6' },
        { "seven", '7' },
        { "eight", '8' },
        { "nine", '9' },
    };

    public static int GetCalibrationValue(string line)
    {
        var consoleHelper = new ConsoleHelper(line, 100);
        int firstDigitIndex = -1, secondDigitIndex = -1;
        int firstDigitLength = -1, secondDigitLength = -1;

        consoleHelper.DrawInputLine(firstDigitIndex, firstDigitLength, secondDigitIndex, secondDigitLength);

        for (var i = 0; i < line.Length; i++)
        {
            if (line[i] >= 48 && line[i] <= 57)
            {
                if (firstDigitIndex < 0)
                {
                    firstDigitIndex = i;
                    firstDigitLength = 1;
                }
                secondDigitIndex = i;
                secondDigitLength = 1;
            }
            else
            {
                foreach (var wordData in numberData.Keys)
                {
                    if (line.Substring(i).StartsWith(wordData))
                    {
                        if (firstDigitIndex < 0)
                        {
                            firstDigitIndex = i;
                            firstDigitLength = wordData.Length;
                        }
                        secondDigitIndex = i;
                        secondDigitLength = wordData.Length;
                        break;
                    }
                }
            }
            consoleHelper.DrawCurrentCursor(i);
            consoleHelper.DrawInputLine(firstDigitIndex, firstDigitLength, secondDigitIndex, secondDigitLength);
            consoleHelper.DrawCurrentAnswer(firstDigitIndex, firstDigitLength, secondDigitIndex, secondDigitLength);
        }

        var calibrationValue = new StringBuilder();
        calibrationValue.Append(line[firstDigitIndex]);
        calibrationValue.Append(line[secondDigitIndex]);
        if (int.TryParse(calibrationValue.ToString(), out var result))
        {
            return result;
        }
        return -1;
    }
}

internal class ConsoleHelper
{
    private int inputCurosrTop;
    private int stepCursorTop;
    private int answerCursorTop;
    private int sleepTime;
    private string inputLine;

    public ConsoleHelper(string input, int sleepTime = 1000)
    {
        inputLine = input;
        inputCurosrTop = Console.CursorTop;
        stepCursorTop = Console.CursorTop + 1;
        answerCursorTop = Console.CursorTop + 2;
        this.sleepTime = sleepTime;

        Console.CursorVisible = false;
    }

    public void Sleep()
    {
        Thread.Sleep(sleepTime);
    }

    public void DrawInputLine(int firstDigitIndex, int firstDigitLength, int secondDigitIndex, int secondDigitLength)
    {
        Console.SetCursorPosition(0, inputCurosrTop);
        Console.CursorLeft = 0;

        for (var i = 0; i < inputLine.Length; i++)
        {
            var currentForegroundColor = Console.ForegroundColor;
            if (i >= firstDigitIndex && i < firstDigitIndex+firstDigitLength ||
                i >= secondDigitIndex && i < secondDigitIndex + secondDigitLength)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write(inputLine[i]);
            Console.ForegroundColor = currentForegroundColor;
        }
        Sleep();
    }

    public void DrawCurrentCursor(int skip)
    {
        Console.SetCursorPosition(0, stepCursorTop);
        Console.CursorLeft = 0;
        for (var i = 0; i < skip; i++)
        {
            Console.Write(' ');
        }
        Console.Write('^');
        Sleep();
    }

    public void DrawCurrentAnswer(int firstDigitIndex, int firstDigitLength, int secondDigitIndex, int secondDigitLength)
    {
        if (firstDigitIndex < 0 || secondDigitIndex < 0)
        {
            return;
        }
        Console.SetCursorPosition(0, answerCursorTop);
        Console.CursorLeft = 0;

        Console.Write("Answer:");
        Console.Write(inputLine.Substring(firstDigitIndex,firstDigitLength));
        Console.Write(inputLine.Substring(secondDigitIndex, secondDigitLength));
        Sleep();
    }
}