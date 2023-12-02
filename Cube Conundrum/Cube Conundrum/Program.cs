// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;


// Test Data

string strGame1 = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
string strGame2 = "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue";
string strGame3 = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";
string strGame4 = "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red";
string strGame5 = "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";


// In game 1, three sets of cubes are revealed from the bag (and then put back again).
// The first set is 3 blue cubes and 4 red cubes;
// the second set is 1 red cube, 2 green cubes, and 6 blue cubes;
// the third set is only 2 green cubes.

int iResult1 = Helper.isValidGame(strGame1); // return 1
int iResult2 = Helper.isValidGame(strGame2); // return 2
int iResult3 = Helper.isValidGame(strGame3); // reutrn 0 - not possible
int iResult4 = Helper.isValidGame(strGame4); // return 0 - not possible
int iResult5 = Helper.isValidGame(strGame5); // return 5

int iSum = iResult1 + iResult2 + iResult3 + iResult4 + iResult5;


// Specify the path to the game test file
string filePath = "GameData.txt";
iSum = 0;

try
{
    // Read all lines from the file
    string[] lines = File.ReadAllLines(filePath);

    // Display the content of the file
    Console.WriteLine("File Content:");
    foreach (string line in lines)
    {
        int iResult = Helper.isValidGame(line);

        iSum += iResult;
    }

    Console.WriteLine("Sum of valid game numbers is: " + iSum);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}


public static class Helper
{
    #region isValidGame

    public static int isValidGame(string strLine)
    {
        //SPEC
        // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        // 12 red cubes, 13 green cubes, and 14 blue cubes

        int iGameNumber = 0;

        // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        string[] astrSplit = strLine.Split(':');

        // Use regular expression to extract the numeric part
        string strNumericPart = Regex.Match(astrSplit[0], @"\d+").Value;

        // Convert the extracted string to an integer
        if (int.TryParse(strNumericPart, out int result))
        {
            iGameNumber = result;
        }
        else
        {
            Console.WriteLine("Conversion failed for string " + astrSplit[0]);
        }


        // Data Part
        string[] astrDraws = astrSplit[1].Split(';');

        foreach (string strDraw in astrDraws)
        {
            int iRedCubes = 0;
            int iGreenCubes = 0;
            int iBlueCubes = 0;

            string[] astrDrawItems = strDraw.Split(',');

            foreach (string strData in astrDrawItems)
            {
                string strDataTrimmed = strData.Trim();

                string[] astrParts = strDataTrimmed.Split(' ');

                string strNumberCubes = Regex.Match(astrParts[0], @"\d+").Value;

                // Convert the extracted string to an integer
                if (int.TryParse(strNumberCubes, out int iNumberOfCubes))
                {
                    string strBallColour = astrParts[1].Trim().ToLower();

                    switch (strBallColour)
                    {
                        case "red":
                            iRedCubes += iNumberOfCubes;
                            break;
                        case "green":
                            iGreenCubes += iNumberOfCubes;
                            break;
                        case "blue":
                            iBlueCubes += iNumberOfCubes;
                            break;
                        default:
                            Console.WriteLine("Error with ball colour!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Conversion failed for string " + strNumberCubes);
                }
            }

            // Check that not too many cubes are used!
            // 12 red cubes, 13 green cubes, and 14 blue cubes
            if (iRedCubes > 12)
            {
                return 0;
            }

            if (iGreenCubes > 13)
            {
                return 0;
            }

            if (iBlueCubes > 14)
            {
                return 0;
            }
        }

        // Game is valid, return game number!
        return iGameNumber;
    }

    #endregion
}

