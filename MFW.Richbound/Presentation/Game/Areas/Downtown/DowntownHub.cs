using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Services.Interfaces;

namespace MFW.Richbound.Presentation.Game.Areas.Downtown;

/// <summary>
/// Responsible for providing activity options for the downtown area.
/// </summary>
public class DowntownHub(ITimeService timeService) : Prompt
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
        Console.WriteLine("2. Increment time by 1 hour (placeholder).");
        Console.WriteLine("3. Increment time by 8 hours (placeholder).");
        Console.WriteLine("Select an option [1-3]:");

        while (true)
        {
            int? input;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 3);
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
                case 2:
                    timeService.PassTime(1);

                    return PromptType.DowntownHub;
                case 3:
                    timeService.PassTime(8);

                    return PromptType.DowntownHub;
            }
        }
    }
}
