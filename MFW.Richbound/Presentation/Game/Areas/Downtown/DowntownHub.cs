using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Services.Interfaces;

namespace MFW.Richbound.Presentation.Game.Areas.Downtown;

/// <summary>
/// Responsible for providing activity options for the downtown area.
/// </summary>
public class DowntownHub(IGameState gameState, ICharacterService characterService) : Prompt
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
        Console.WriteLine("2. Perform an activity for 1 hour (placeholder)");
        Console.WriteLine("3. Perform an activity for 8 hours (placeholder)");
        Console.WriteLine("4. Sleep (placeholder)");
        Console.WriteLine("5. Eat food (placeholder)");
        Console.WriteLine("6. Heal yourself (placeholder)");
        Console.WriteLine("7. Hurt yourself (placeholder)");
        Console.WriteLine("Select an option [1-3]:");

        while (true)
        {
            int? input;

            Console.Write(DisplayText.InputPrompt);

            try
            {
                input = PromptHelper.ReadInt(false, 1, 7);
            }
            catch (Exception)
            {
                Console.WriteLine(DisplayText.TooltipInvalidMenuOption);

                continue;
            }

            bool completed;

            switch (input)
            {
                case 1:
                    return PromptType.CharacterMenu;
                case 2:
                    completed = characterService.HandleActivity(1, 4);

                    if (!completed)
                    {
                        Console.WriteLine("Not enough energy to complete activity.");
                    }
                    else
                    {
                        DisplayPostActivityStatus(gameState, 1);
                    }

                    return PromptType.DowntownHub;
                case 3:
                    completed = characterService.HandleActivity(8, 32);

                    if (!completed)
                    {
                        Console.WriteLine("Not enough energy to complete activity.");
                    }
                    else
                    {
                        DisplayPostActivityStatus(gameState, 8);
                    }

                    return PromptType.DowntownHub;
                case 4:
                    completed = characterService.HandleSleep(BedQuality.Good);

                    if (!completed)
                    {
                        Console.WriteLine("You are already fully rested.");
                    }
                    else
                    {
                        DisplayPostActivityStatus(gameState, 8);
                    }

                    ContinuePrompt();

                    return PromptType.DowntownHub;
                case 5:
                    Console.WriteLine("Eating...");
                    Console.WriteLine("Restored hunger by 40%.");

                    gameState.UpdateHunger(40);

                    ContinuePrompt();

                    return PromptType.DowntownHub;
                case 6:
                    if (gameState.Health == 100)
                    {
                        Console.WriteLine("You are already at full health.");

                        ContinuePrompt();

                        return PromptType.DowntownHub;
                    }

                    Console.WriteLine("Healing...");
                    Console.WriteLine($"Restored 40% of your health. You are now at {gameState.Health} health.");

                    gameState.UpdateHealth(40);

                    ContinuePrompt();

                    return PromptType.DowntownHub;
                case 7:
                    Console.WriteLine("You hurt yourself in your confusion...");
                    Console.WriteLine($"You lost 20% of your health. You are now at {gameState.Health} health.");

                    // TODO: Also a reason to move this to a separate service to handle getting wasted?
                    gameState.UpdateHealth(-30);

                    ContinuePrompt();

                    return PromptType.DowntownHub;
            }
        }
    }
}
