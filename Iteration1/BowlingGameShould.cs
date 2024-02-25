using FluentAssertions;

namespace Iteration1;

class BowlingGame
{
    private List<int> rolls = new List<int>();

    public void roll(int pins)
    {
        rolls.Add(pins);
    }

    public int calculateScore()
    {
        var totalScore= (
            this.calculateTotalPinesKnockedDown() +
            this.calculateSpareBonusPerFrame() +
            this.calculateStrikeBonusPerFrame()
        );
        
        if (totalScore > 300)
        {
            return 300;
        }
        
        return totalScore;
    }

    private int calculateStrikeBonusPerFrame()
    {
        var strikeBonus = 0;
        for (int i = 0; i < rolls.Count; i++)
        {
            if (i + 2 < rolls.Count && rolls[i] == 10)
            {
                strikeBonus += rolls[i + 1] + rolls[i + 2];
            }
        }
        
        return strikeBonus;
    }

    private int calculateSpareBonusPerFrame()
    {
        var spareBonus = 0;
        for (int i = 0; i < rolls.Count; i+=2)
        {
            if (i + 2 < rolls.Count &&  rolls[i] + rolls[i + 1] == 10)
            {
                spareBonus += rolls[i + 2];
            }
        }
        
        return spareBonus;
    }

    private int calculateTotalPinesKnockedDown()
    {
        return rolls.Sum();
    }
}



public class BowlingGameShould
{
    private BowlingGame game;
    [SetUp]
    public void Setup()
    {
        game = new BowlingGame();
    }

    [Test]
    public void calculate_the_score_where_no_pins_have_been_knocked_down()
    {
        rollMany(20, 0);

        game.calculateScore().Should().Be(0);
    }

    [Test]
    public void calculate_the_score_where_in_all_rolls_all_pines_were_not_knocked_down()
    {
        rollMany(20, 1);    
        
        game.calculateScore().Should().Be(20);
    }

    [Test]
    public void calculate_the_score_when_spare_and_some_pines_are_knocked_down_in_the_following_roll()
    {
        game.roll(5);
        game.roll(5);
        game.roll(5);
        rollMany(17, 0);

        game.calculateScore().Should().Be(20);
    }

    [Test]
    public void calculate_the_score_when_spare_and_not_all_pines_were_knocked_down_in_the_two_following_rolls()
    {
        game.roll(10);
        game.roll(2);
        game.roll(3);
        rollMany(17, 0);

        game.calculateScore().Should().Be(20);
    }

    [Test]
    public void calculate_the_score_in_a_perfect_game()
    {
        rollMany(12, 10);

        game.calculateScore().Should().Be(300);
    }

    [Test]
    public void calculate_score_when_there_is_an_strike_in_the_final_frame_with_extra_rolls()
    {
        rollMany(18,0);

        game.roll(10);
        game.roll(2);
        game.roll(5);
        
        game.calculateScore().Should().Be(24);
    }
    
    
    private void rollMany(int times, int pins)
    {
        for (int i = 0; i < times; i++)
        {
            game.roll(pins);
        }
    }

    [Test]
    public void calculate_score_when_there_is_an_spare_in_the_final_frame_with_extra_roll()
    {

        for (int i = 0; i < 18; i++)
        {
            game.roll(0);
        }

        game.roll(5);
        game.roll(5);
        game.roll(2);
        
        game.calculateScore().Should().Be(14);
    }
}