using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;

namespace MFW.Richbound.Presentation.Game.Areas.Downtown;

/// <summary>
/// Responsible for providing activity options for the downtown area.
/// </summary>
public class DowntownHub : Prompt
{
    /// <inheritdoc/>
    public override PromptType? DisplayMainPrompt()
    {
        Console.WriteLine($"=== Downtown {DisplayText.CityName} ===");
        Console.WriteLine($"The business and commercial district of {DisplayText.CityName}.");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine();
        Console.WriteLine("--- Actions ---");
        Console.WriteLine("1. Open Menu");
        Console.WriteLine("Select an option [1-1]:");

        while (true)
        {
            int? input;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 1);
            }
            catch (Exception)
            {
                Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                continue;
            }

            switch (input)
            {
                case 1:
                    return PromptType.CharacterMenu;
            }
        }
    }
}
