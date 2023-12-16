using System.Text;

public class Part1
{
    public static void Main(string[] args)
    {
        GetCalibrationValuePart1("a1b2c3d4e5f");
    }

    public static int GetCalibrationValuePart1(string line)
    {
        var consoleHelper = new ConsoleHelper(line, 100);
        int firstDigitIndex = -1, secondDigitIndex = -1;

        consoleHelper.DrawInputLine(firstDigitIndex, secondDigitIndex);

        for (var i = 0; i < line.Length; i++)
        {
            if (line[i] >= 48 && line[i] <= 57)
            {
                if (firstDigitIndex < 0)
                {
                    firstDigitIndex = i;
                }
                secondDigitIndex = i;
            }
            consoleHelper.DrawCurrentCursor(i);
            consoleHelper.DrawInputLine(firstDigitIndex, secondDigitIndex);
            consoleHelper.DrawCurrentAnswer(firstDigitIndex, secondDigitIndex);
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

public class ConsoleHelper
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

    public void DrawInputLine(int firstDigitIndex, int secondDigitIndex)
    {
        Console.SetCursorPosition(0, inputCurosrTop);
        Console.CursorLeft = 0;

        for (var i = 0; i < inputLine.Length; i++)
        {
            var currentForegroundColor = Console.ForegroundColor;
            if (firstDigitIndex == i || secondDigitIndex == i)
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

    public void DrawCurrentAnswer(int firstDigitIndex, int secondDigitIndex)
    {
        if (firstDigitIndex < 0 || secondDigitIndex < 0)
        {
            return;
        }
        Console.SetCursorPosition(0, answerCursorTop);
        Console.CursorLeft = 0;

        Console.Write("Answer:");
        Console.Write(inputLine[firstDigitIndex]);
        Console.Write(inputLine[secondDigitIndex]);
        Sleep();
    }
}