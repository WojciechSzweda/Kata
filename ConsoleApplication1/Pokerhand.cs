using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public enum Result
    {
        Win,
        Loss,
        Tie
    }
    public struct Card
    {
        public char Value { get; set; }
        public char Suit { get; set; }
    }
    public class PokerHand
    {
        static string OrderedValues = "23456789TJQKA";
        static string StraigthOrder = "A23456789TJQK";
        static string MaxStraigth = "TJQKA";
        public static int RoyalFlush(IEnumerable<Card> hand) => (MaxStraigth.Where(v => hand.Select(o => o.Value).Contains(v)).Count() == 5) ? 100000 : 0;
        public static int Flush(IEnumerable<Card> hand)
        {
            var kickers = hand.OrderBy(x => OrderedValues.IndexOf(x.Value)).Select(x => x.Value);
            var g = hand.GroupBy(x => x.Suit).Select(x => x.Count());
            if (g.Count() == 1)
            {
                var kickersSum = 0;
                for (int i = 0; i < 5; i++)
                {
                    kickersSum += 15 * i + OrderedValues.IndexOf(kickers.ElementAt(i));
                }
                return (40000 + RoyalFlush(hand) + Straigth(hand)) + kickersSum;
            }
            return FullHouse(hand);
        }
        static int Straigth(IEnumerable<Card> hand)
        {
            var ordered = hand.OrderBy(x => StraigthOrder.IndexOf(x.Value));
            if (MaxStraigth.Where(x => hand.Select(o => o.Value).Contains(x)).Count() == 5)
            {
                var maxOrdered = hand.OrderBy(x => MaxStraigth.IndexOf(x.Value));
                if ((MaxStraigth.IndexOf(maxOrdered.Last().Value) - MaxStraigth.IndexOf(maxOrdered.First().Value)) == 4)
                    return 30000 + MaxStraigth.IndexOf(maxOrdered.Last().Value) + 9;
            }
            if (ordered.Select(x => x.Value).Distinct().Count() != 5) return ThreeOfKind(hand);

            if ((StraigthOrder.IndexOf(ordered.Last().Value) - StraigthOrder.IndexOf(ordered.First().Value)) == 4)
                return 30000 + StraigthOrder.IndexOf(ordered.Last().Value);
            return ThreeOfKind(hand);
        }
        static int FourOfAKind(IEnumerable<Card> hand)
        {
            var g = hand.GroupBy(x => x.Value).Where(x => x.Count() >= 4);
            if (g.Count() == 1)
                return 60000 + 15 * OrderedValues.IndexOf(g.FirstOrDefault().Key) + OrderedValues.IndexOf(hand.GroupBy(x => x.Value).Where(x => x.Count() == 1).FirstOrDefault().Key);
            return Flush(hand);
        }
        static int FullHouse(IEnumerable<Card> hand)
        {
            var g = hand.GroupBy(x => x.Value).OrderByDescending(x => x.Count());
            if (g.Count() == 2)
                return 50000 + 100 * OrderedValues.IndexOf(g.FirstOrDefault().Key) + OrderedValues.IndexOf(g.LastOrDefault().Key);
            return Straigth(hand);
        }
        static int ThreeOfKind(IEnumerable<Card> hand)
        {
            var kickers = hand.GroupBy(x => x.Value).Where(x => x.Count() == 1).OrderByDescending(x => OrderedValues.IndexOf(x.Key));
            var g = hand.GroupBy(x => x.Value).Where(x => x.Count() >= 3);
            if (g.Count() == 1)
                return 20000 + 15 * OrderedValues.IndexOf(g.FirstOrDefault().Key) + 3 * OrderedValues.IndexOf(kickers.ElementAt(0).Key)
                            + OrderedValues.IndexOf(kickers.ElementAt(1).Key);
            return TwoPairs(hand);
        }
        static int TwoPairs(IEnumerable<Card> hand)
        {
            var kicker = hand.GroupBy(x => x.Value).Where(x => x.Count() == 1).OrderBy(x => OrderedValues.IndexOf(x.Key)).LastOrDefault().Key;
            var g = hand.GroupBy(x => x.Value).Where(x => x.Count() == 2).OrderBy(x => OrderedValues.IndexOf(x.Key));
            if (g.Count() == 2)
                return 10000 + 50 * OrderedValues.IndexOf(g.FirstOrDefault().Key) + 2 * OrderedValues.IndexOf(g.LastOrDefault().Key) + OrderedValues.IndexOf(kicker);
            return Pair(hand);
        }
        static int Pair(IEnumerable<Card> hand)
        {
            var kickers = hand.GroupBy(x => x.Value).Where(x => x.Count() == 1).OrderByDescending(x => OrderedValues.IndexOf(x.Key));
            var g = hand.GroupBy(x => x.Value).Where(x => x.Count() == 2);
            if (g.Count() == 1)
                return 5000 + 60 * OrderedValues.IndexOf(g.FirstOrDefault().Key) + 15 * OrderedValues.IndexOf(kickers.ElementAt(0).Key)
                            + 3 * OrderedValues.IndexOf(kickers.ElementAt(1).Key) + OrderedValues.IndexOf(kickers.ElementAt(2).Key);
            return 0;
        }
        static Result Kicker(IEnumerable<Card> thisHand, IEnumerable<Card> enemyHand)
        {
            var thisH = thisHand.Select(x => OrderedValues.IndexOf(x.Value)).OrderByDescending(x => x).ToArray();
            var enemyH = enemyHand.Select(x => OrderedValues.IndexOf(x.Value)).OrderByDescending(x => x).ToArray();
            for (int i = 0; i < thisH.Count(); i++)
            {
                if (thisH[i] > enemyH[i])
                    return Result.Win;
                else if (thisH[i] < enemyH[i])
                    return Result.Loss;
            }
            return Result.Tie;
        }

        static int Evaluate(IEnumerable<Card> hand) => FourOfAKind(hand);

        IEnumerable<Card> Hand { get; set; }
        public PokerHand(string hand)
        {
            Hand = hand.Split(' ').Select(x => new Card { Value = x[0], Suit = x[1] });
        }

        public Result CompareWith(PokerHand hand)
        {
            int thisScore = 0;
            int enemyScore = 0;
            thisScore = Evaluate(this.Hand);
            enemyScore = Evaluate(hand.Hand);
            if (thisScore == 0 && enemyScore == 0)
                return Kicker(this.Hand, hand.Hand);
            return thisScore > enemyScore ? Result.Win : enemyScore > thisScore ? Result.Loss : Result.Tie;
        }
    }
}
