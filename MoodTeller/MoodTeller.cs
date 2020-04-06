using System;

// ==============================================================================================================
// Mood Teller
// ==============================================================================================================
// Author:           Trevor Richardson
// Date Completed:   4/6/2020
// Purpose:          To predict the user's mood based on the user's reponse to the programs only question
// ==============================================================================================================

namespace MoodTeller
{
    class Program
    {
        /* The MoodTeller program will:
         * - Use an array of good-moods and bad-moods to predict the user's mood
         * - Use the user input to search for matches between the input and mood words
         * - If a good-mood word was found, the feeling score increases, if a bad-mood word was found, the feeling score decreases
         * - If an opposite word is found, the score reverses (explained later)
         * - If an opposite word is found, the confusion level increases. The "bot" will later inform the user of its uncertainty
         * - The "bot" will use the feeling score to predict the user's feeling
         */

        static void Main(string[] args)
        {
            int numWords = 1;
            int feelingScore = 0;
            int confusionLevel = 0;
            int currWord = 0;
            String thisWord = "";
            String input;

            // String of many good-mood words
            String[] goodMoods = {"absolutely", "accepted", "acclaimed", "accomplish", "accomplishment", "achievement", "action", "active", "admire,", "adorable", "adventure", "affirmative", "affluent", "agree", "agreeable", "amazing", "angelic",
                                "appealing", "aprove", "aptitude", "attractive", "awesome", "beaming", "beautiful", "believe", "beneficial", "bliss", "bountiful", "bounty", "brave", "bravo", "brilliant", "bubbly", "calm", "celebrated", "certain",
                                "champ", "champion", "charming", "cheery", "choice", "classic", "classical", "clean", "commend", "composed", "constant", "cool", "courageous", "creative", "cute", "dazzling", "delight", "delightful", "distinguished",
                                "divine", "earnest", "easy", "ecstatic", "effective", "effervescent", "efficient", "effortless", "electrifying", "elegant", "enchanting", "encouraging", "endorsed", "energetic", "energized", "engaging", "enthusiastic",
                                "essential", "esteemed", "ethical", "excellent", "exciting", "exquisite", "fabulous", "fair", "familiar", "famous", "fantastic", "favorable", "fetching", "fine", "fitting", "flourishing", "fortunate", "free", "fresh",
                                "friendly", "fun", "funny", "generous", "genius", "genuine", "giving", "glamorous", "glowing", "good", "gorgeous", "graceful", "great", "green", "grin", "growing", "handsome", "happy", "harmonious", "healing", "healthy",
                                "hearty", "heavenly", "honest", "honorable", "honored", "hug", "idea", "ideal", "imaginative", "imagine", "impressive", "independent", "innovate", "innovative", "instant", "instantaneous", "instinctive", "intellectual",
                                "intelligent", "intuitive", "inventive", "jovial", "joy", "jubilant", "keen", "kind", "knowing", "knowledgeable", "laugh", "learned", "legendary", "light", "lively", "lovely", "lucid", "lucky", "luminous", "marvelous",
                                "masterful", "meaningful", "merit", "meritorious", "miraculous", "motivating", "moving", "natural", "nice", "novel", "now", "nurturing", "nutritious", "okay", "one", "one-hundred percent", "open", "optimistic", "paradise",
                                "perfect", "phenomenal", "pleasant", "pleasurable", "plentiful", "poised", "polished", "popular", "positive", "powerful", "prepared", "pretty", "principled", "productive", "progress", "prominent", "protected", "proud",
                                "quality", "quick", "quiet", "ready", "reassuring", "refined", "refreshing", "rejoice", "reliable", "remarkable", "resounding", "respected", "restored", "reward", "rewarding", "right", "robust", "safe", "satisfactory",
                                "secure", "seemly", "simple", "skilled", "skillful", "smile", "soulful", "sparkling", "special", "spirited", "spiritual", "stirring", "stunning", "stupendous", "success", "successful", "sunny", "super", "superb", "supporting",
                                "surprising", "terrific", "thorough", "thrilling", "thriving", "tops", "tranquil", "transformative", "transforming", "trusting", "truthful", "unreal", "unwavering", "up", "upbeat", "upright", "upstanding", "valued",
                                "vibrant", "victorious", "victory", "vigorous", "virtuous", "vital", "vivacious", "wealthy", "welcome", "well", "whole", "wholesome", "willing", "wonderful", "wondrous", "worthy", "wow", "yes", "yummy", "zeal", "zealous"
                                };

            // String of many bad-mood words
            String[] badMoods = {"abysmal", "adverseal", "arming", "angry", "annoy", "anxious", "apathy", "appalling", "atrocious", "awful", "bad", "banal", "barbed", "belligerent", "bemoan", "beneath", "boring", "broken", "callouscan", "clumsy",
                                 "coarse", "cold", "cold-hearted", "collapse", "confused", "contradictory", "contrary", "corrosive", "corrupt", "crazy", "creepy", "criminal", "cruel", "cry", "cutting", "damage", "damaging", "dastardly", "dead", "decaying",
                                 "deformed", "deny", "deplorable", "depressed", "deprived", "despicable", "detrimental", "dirty", "disease", "disgusting", "disheveled", "dishonest", "dishonorable", "dismal", "distress", "don", "dreadful", "dreary", "enraged",
                                 "eroding", "evil", "fail", "faulty", "fear", "feeble", "fight", "filthy", "foul", "frighten", "frightful", "gawky", "ghastly", "grave", "greed", "grim", "grimace", "gross", "grotesque", "gruesome", "guilty", "haggard",
                                 "hard", "hard-hearted", "harmful", "hate", "hideous", "homely", "horrendous", "horrible", "hostile", "hurt", "hurtful", "icky", "ignorant", "ignore", "ill", "immature", "imperfect", "impossible", "inane", "inelegant", "infernal",
                                 "injure", "injurious", "insane", "insidious", "insipid", "jealous", "junky", "lose", "lousy", "lumpy", "malicious", "mean", "menacing", "messy", "misshapen", "missing", "misunderstood", "moan", "moldy", "monstrous", "naive",
                                 "nasty", "naughty", "negate", "negative", "nondescript", "nonsense", "noxious", "objectionable", "odious", "offensive", "old", "oppressive", "pain", "perturb", "pessimistic", "petty", "plain",
                                 "poisonous", "poor", "prejudice", "questionable", "quirky", "quit", "reject", "renege", "repellant", "reptilian", "repugnant", "repulsive", "revenge", "revolting", "rocky", "rotten", "rude", "ruthless", "sad", "savage", "scare",
                                 "scary", "scream", "severe", "shocking", "shoddy", "sick", "sickening", "sinister", "slimy", "smelly", "sobbing", "sorry", "spiteful", "sticky", "stinky", "stormy", "stressful", "stuck", "stupid", "sub", "standard", "suspect",
                                 "suspicious", "tense", "terrible", "terrifying", "threatening", "ugly", "undermine", "unfair", "unfavorable", "unhappy", "unhealthy", "unjust", "unlucky", "unpleasant", "unsatisfactory", "unsightly", "untoward", "unwanted",
                                 "unwelcome", "unwholesome", "unwieldy", "unwise", "upset", "vice", "vicious", "vile", "villainous", "vindictive", "wary", "weary", "wicked", "woeful", "worthless", "wound", "yell", "yucky", "zero"
                                 };

            // String of oppositing words (explained later)
            String[] oppositeWords = { "not", "doesn't", "doesnt", "never", "nevermore", "nothing", "nobody" };

            Console.WriteLine("How are you feeling today? You can type in a sentence and I can try to guess your mood.");
            input = Console.ReadLine();
            Console.WriteLine("\nHmm, let me think.");

            // for loop to find the number of words in the input
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    numWords++;
                }
            }

            // Create a new string array of number of words from input
            String[] arrayOfWords = new string[numWords];
            // add a space after the last word so the program can detect where the last word ends
            input += " ";

            // for loop to construct the array of words from the input
            for (int i = 0; i < input.Length; i++)
            {
                // if a space has been found, we have reached the end of a word. Add the word to the array and start over
                if (input[i] == ' ')
                {
                    arrayOfWords[currWord] = thisWord.ToLower();
                    currWord++;
                    thisWord = "";
                }
                // if there was not a space, continue to construct the word
                else
                {
                    thisWord += input[i];
                }
            }

            // for loop will find all the good-mood words
            for (int i = 0; i < goodMoods.Length; i++)
            {
                // for loop will search through the array of words to see if there is a match with the current good-mood word
                for (int j = 0; j < arrayOfWords.Length; j++)
                {
                    // if there is a match, add to the feeling score
                    if (arrayOfWords[j].Contains(goodMoods[i]))
                    {
                        feelingScore++;
                    }
                }
            }

            // for loop will find all the bad-mood words
            for (int i = 0; i < badMoods.Length; i++)
            {
                // for loop will search through the array of words to see if there is a match with the current bad-mood word
                for (int j = 0; j < arrayOfWords.Length; j++)
                {
                    // if there is a match, subtract from the feeling score
                    if (arrayOfWords[j].Contains(badMoods[i]))
                    {
                        feelingScore--;
                    }
                }
            }

            // for loop will find all the opposite words
            // Ex: "not happy"... "happy" will make the score 1, "not" will reverse the score to "-1". This makes sense becasue "not happy" is a bad mood
            for (int i = 0; i < oppositeWords.Length; i++)
            {
                // for loop will search through the array of words to see if there is a match with the current opposite word
                for (int j = 0; j < arrayOfWords.Length; j++)
                {
                    // if there is a match, reverse the feeling score and increase the bot's confusion level
                    if (arrayOfWords[j].Contains(oppositeWords[i]))
                    {
                        feelingScore *= -1;
                        confusionLevel++;
                    }
                }
            }

            // if there were opposite words found, print a statement based on the level of confusion
            if (confusionLevel == 1)
            {
                Console.WriteLine("I'm a little confused... But I think...");
            }
            else if (confusionLevel > 1)
            {
                Console.WriteLine("I'm VERY confused... But I think...");
            }

            // Using the feeling score, predict the user's mood (feeling)
            if (feelingScore == 0)
            {
                Console.WriteLine("Hmm, it's hard for me to tell what mood you're in. Could you be a little more clear?");
            }
            else if (feelingScore > 0)
            {
                Console.WriteLine("Sounds to me like you're in a good mood!");
            }
            else
            {
                Console.WriteLine("Sounds to me like you're in a bad mood :'(");
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
