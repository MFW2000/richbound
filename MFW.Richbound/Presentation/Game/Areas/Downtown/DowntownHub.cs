using MFW.Richbound.Domain.Interfaces;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Helpers;
using MFW.Richbound.Infrastructure.Interfaces;
using MFW.Richbound.Services.Interfaces;

namespace MFW.Richbound.Presentation.Game.Areas.Downtown;

/// <summary>
/// Responsible for providing activity options for the downtown area.
/// </summary>
public class DowntownHub(IGameState gameState, ICharacterService characterService, IConsoleLogger logger) : Prompt
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
        Console.WriteLine("2. Perform an activity for 1 hour (placeholder).");
        Console.WriteLine("3. Perform an activity for 8 hours (placeholder).");
        Console.WriteLine("4. Sleep (placeholder).");
        Console.WriteLine("5. Eat food (placeholder).");
        Console.WriteLine("6. Heal yourself (placeholder).");
        Console.WriteLine("7. Hurt yourself (placeholder).");
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

            switch (input)
            {
                case 1:
                    return PromptType.CharacterMenu;
                case 2:
                    return Activity(1, 5);
                case 3:
                    return Activity(8, 40);
                case 4:
                    return Sleep(Comfort.Good);
                case 5:
                    return Eat(40);
                case 6:
                    return Heal(40);
                case 7:
                    return DealDamage(20);
            }
        }
    }

    private PromptType Activity(int hoursToComplete, int energyToComplete)
    {
        bool success;

        try
        {
            success = characterService.HandleActivity(hoursToComplete, energyToComplete);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);

            Console.WriteLine(DisplayText.TooltipActionCouldNotBeCompleted);
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        if (!success)
        {
            Console.WriteLine("You do not have enough energy to complete the activity.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        DisplayPostActivityStatus(gameState, hoursToComplete);

        return PromptType.DowntownHub;
    }

    private PromptType Sleep(Comfort comfortLevel)
    {
        bool success;

        try
        {
            success = characterService.HandleSleep(comfortLevel);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);

            Console.WriteLine(DisplayText.TooltipActionCouldNotBeCompleted);
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        if (!success)
        {
            Console.WriteLine("You are not tired right now.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        Console.WriteLine("Sleeping...");

        DisplayPostActivityStatus(gameState, 8);

        return PromptType.DowntownHub;
    }

    private PromptType Eat(int foodPoints)
    {
        bool success;

        try
        {
            success = characterService.HandleEat(foodPoints);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);

            Console.WriteLine(DisplayText.TooltipActionCouldNotBeCompleted);
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        if (!success)
        {
            Console.WriteLine("You are not hungry right now.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        Console.WriteLine("Eating...");

        DisplayPostActivityStatus(gameState, $"Restored hunger by {DisplayPercentage(foodPoints)}.");

        return PromptType.DowntownHub;
    }

    private PromptType Heal(int hitPoints)
    {
        bool success;

        try
        {
            success = characterService.HandleHealing(hitPoints);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);

            Console.WriteLine(DisplayText.TooltipActionCouldNotBeCompleted);
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        if (!success)
        {
            Console.WriteLine("You are already at full health.");
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        Console.WriteLine("Healing...");

        DisplayPostActivityStatus(gameState, $"Restored health by {DisplayPercentage(hitPoints)}.");

        return PromptType.DowntownHub;
    }

    private PromptType DealDamage(int hitPoints)
    {
        try
        {
            characterService.HandleDamage(hitPoints);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);

            Console.WriteLine(DisplayText.TooltipActionCouldNotBeCompleted);
            Console.WriteLine();

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        // TODO: Move this logic to a hospital location.
        if (gameState.Health == 0)
        {
            var hospitalBill = gameState.NetWorth / Constants.HospitalBillPercentage;

            if (hospitalBill > Constants.HospitalBillCap)
            {
                hospitalBill = Constants.HospitalBillCap;
            }

            characterService.HandleDeath(hospitalBill);

            Console.WriteLine("WASTED");
            Console.WriteLine();
            Console.WriteLine("You were found barely alive and taken to hospital.");

            DisplayPostActivityStatus(
                gameState,
                $"{Constants.HospitalStayDurationHours} hours have passed and you lost {DisplayCurrency(hospitalBill)}.");

            ContinuePrompt();

            return PromptType.DowntownHub;
        }

        DisplayPostActivityStatus(gameState, $"You lost {DisplayHitPoints(hitPoints)}.");

        return PromptType.DowntownHub;
    }
}
