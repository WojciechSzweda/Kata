using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Reflection;
namespace ConsoleApplication1
{
    class Program
    {

        public static string ToCamelCase(string str)
        {
            var txt = str.ToList();
            for (int i = txt.Count - 1; i > 0; i--)
            {
                if (txt[i] == '-' || txt[i] == '_')
                {
                    txt[i + 1] = char.ToUpper(txt[i + 1]);
                    txt.RemoveAt(i);
                }
            }

            return new string(txt.ToArray());
        }

        public static int Persistence(long n)
        {
            var numbers = n.ToString().ToList();
            var counter = 0;
            int multi = 1;
            if (numbers.Count() > 1)
            {
                foreach (var item in numbers)
                {
                    multi *= (int)char.GetNumericValue(item);

                }
                counter++;
            }
            if (multi >= 10)
                counter += Persistence(multi);

            return counter;

        }

        public static int[] FoldArray(int[] array, int runs)
        {
            var len = array.Length / 2;
            for (int i = 0; i < len; i++)
            {
                array[i] = array[i] + array[(array.Length - 1) - i];
            }
            array = array.Take(len + array.Length % 2).ToArray();
            if (runs > 1)
                array = FoldArray(array, runs - 1);
            return array;
        }

        public static string ReverseWords(string str)
        {
            return string.Join(" ", str.Split(' ').Reverse().ToArray());
        }

        public static char FindMissingLetter(char[] array)
        {
            return Enumerable.Range(array.First(), array.Length).Select(x => (char)x).Except(array).First();
        }

        public static string print(int n)
        {
            var sb = new StringBuilder();

            for (int i = 1; i < n + 1; i += 2)
            {
                for (int j = 0; j < (n - i) / 2; j++)
                {
                    sb.Append(" ");
                }
                for (int k = 0; k < i; k++)
                {
                    sb.Append("*");
                }
                sb.Append("\n");
            }
            for (int i = n - 1; i > 1; i -= 2)
            {
                for (int j = 0; j < (n - i) / 2; j++)
                {
                    sb.Append(" ");
                }
                for (int k = 0; k < i; k++)
                {
                    sb.Append("*");
                }
                sb.Append("\n");

            }
            return sb.ToString();
        }

        public static int Test(string numbers)
        {
            var b = numbers.Replace(" ", "");
            var a = numbers.Split(' ').ToList().IndexOf(numbers.Split(' ').Select(x => int.Parse(x)).GroupBy(x => x % 2 == 0).OrderBy(x => x.Count()).First().First().ToString());


            return 0;
        }

        public static string MixedFraction(string s)
        {
            var numbers = s.Split('/').Select(int.Parse).ToArray();
            var denom = numbers[1];
            var a = numbers[0] / denom;
            var b = numbers[0] % denom;
            var multi = 0;
            for (int i = 1; i < denom; i++)
            {
                if (b % i == 0 && denom % i == 0)
                    multi = i;
            }
            if (multi != 0)
            {
                b /= multi;
                denom /= multi;
            }
            if (b == 0) return a.ToString();
            else if (a == 0) return $"{b * Math.Sign(denom)}/{Math.Abs(denom)}";
            else
                return $"{a} {Math.Abs(b)}/{Math.Abs(denom)}";
        }



        public static string FirstNonRepeatingLetter(string s)
        {
            var letter = s.GroupBy(x => char.ToString(x), StringComparer.InvariantCultureIgnoreCase).FirstOrDefault(x => x.Count() == 1);
            return letter == null ? "" : letter.Key;
        }

        public static string GoodVsEvil(string good, string evil)
        {
            var goodMulti = new List<int>() { 1, 2, 3, 3, 4, 10 };
            var evilMulti = new List<int>() { 1, 2, 2, 2, 3, 5, 10 };

            var goodSum = good.Split(' ').Select(int.Parse).ToList().Zip(goodMulti, (x, y) => new { x, y }).Sum(x => x.x * x.y);
            var evilSum = evil.Split(' ').Select(int.Parse).ToList().Zip(evilMulti, (x, y) => new { x, y }).Sum(x => x.x * x.y);

            if (evilSum > goodSum)
            {
                return "Battle Result: Evil eradicates all trace of Good";
            }
            else if (evilSum < goodSum)
            {
                return "Battle Result: Good triumphs over Evil";
            }
            else
                return "Battle Result: No victor on this battle field";
        }

        public static string GetReadableTime(int seconds)
        {
            var HH = seconds / 3600;
            var MM = (seconds / 60) % 60;
            var SS = (seconds - MM * 60) % 60;
            return $"{(HH < 99 ? HH : 99).ToString("00")}:{(MM < 59 ? MM : 59).ToString("00")}:{(SS < 59 ? SS : 59).ToString("00")}";
        }

        static List<T> Swap<T>(List<T> list, int a, int b)
        {
            var copy = new List<T>(list);
            var tmp = copy[a];
            copy.RemoveAt(a);
            copy.Insert(b, tmp);
            return copy;
        }

        public static long[] Smallest(long n)
        {
            var txt = n.ToString().Select(x => (int)char.GetNumericValue(x)).ToList();
            var len = txt.Count() / 2;
            var a = Enumerable.Range(0, txt.Count() - 1).SelectMany(x => Enumerable.Range(0, txt.Count() - 1), (x1, x2) => new int[] { x1, x2 }).ToArray().Distinct().Where(x => x[0] != x[1] || (x[0] == 0 && x[1] == 0)).Select(x => new long[] { long.Parse(string.Join("", Swap(txt, x[0], x[1]))), x[0], x[1] }).OrderBy(x => x[0]);
            return a.FirstOrDefault();
        }
        static Dictionary<int, string> roman = new Dictionary<int, string>()
        {
            { 1, "I" }, {5, "V" }, {10, "X" }, {50, "L" }, {100, "C" },
            {500, "D" }, {1000, "M" }
        };
        static string GetRomanLetters(int num, int index)
        {
            var romanNumber = roman.ElementAt(index).Key;
            var sb = new StringBuilder();
            var a = num - romanNumber;

            if (a >= 0)
            {
                if (num >= 9 && num < 10)
                {
                    sb.Append(roman.ElementAt(index - 1).Value);
                    sb.Append(roman.ElementAt(index + 1).Value);
                    sb.Append(GetRomanLetters(num - 9, index - 1));

                }
                else if (num >= 90 && num < 100)
                {
                    sb.Append(roman.ElementAt(index - 1).Value);
                    sb.Append(roman.ElementAt(index + 1).Value);
                    sb.Append(GetRomanLetters(num - 90, index - 1));

                }
                else if (num >= 900 && num < 1000)
                {
                    sb.Append(roman.ElementAt(index - 1).Value);
                    sb.Append(roman.ElementAt(index + 1).Value);
                    sb.Append(GetRomanLetters(num - 900, index - 1));

                }
                else if (num >= 400 && num < 500)
                {
                    sb.Append(roman.ElementAt(index).Value);
                    sb.Append(roman.ElementAt(index + 1).Value);
                    sb.Append(GetRomanLetters(num - 400, index - 1));


                }
                else if (num >= 40 && num < 50)
                {
                    sb.Append(roman.ElementAt(index).Value);
                    sb.Append(roman.ElementAt(index + 1).Value);
                    sb.Append(GetRomanLetters(num - 40, index - 1));


                }
                else if (num >= 4 && num < 5)
                {
                    sb.Append(roman.ElementAt(index).Value);
                    sb.Append(roman.ElementAt(index + 1).Value);



                }
                else
                {
                    sb.Append(roman[romanNumber]);
                    sb.Append(GetRomanLetters(a, index));
                }
            }
            else if (a < 0 && index > 0)
            {
                sb.Append(GetRomanLetters(num, index - 1));
            }

            return sb.ToString();
        }
        public static string Mix(string s1, string s2)
        {
            var s1g = s1.ToCharArray().Where(x => char.IsLower(x) && char.IsLetter(x)).GroupBy(x => x).Select(x => new { g = '1', x.Key, n = x.Count() }).ToList();
            var s2g = s2.ToCharArray().Where(x => char.IsLower(x) && char.IsLetter(x)).GroupBy(x => x).Select(x => new { g = '2', x.Key, n = x.Count() }).ToList();
            var s = s1g.Concat(s2g).OrderByDescending(x => x.n).ThenBy(x => x.Key).Where(x => x.n > 1).ToList();
            for (int i = s.Count - 1; i >= 0; i--)
            {
                var elem = s.FirstOrDefault(x => x.Key == s[i].Key && x.g != s[i].g);
                if (elem == null)
                    continue;
                if (s[i].n < elem.n)
                {
                    s.Remove(s[i]);
                }
                else if (s[i].n == elem.n)
                {
                    s.Remove(s[i]);
                    s.Remove(elem);
                    s.Insert(i - 1, new { g = '=', Key = elem.Key, n = elem.n });
                }
            }
            s = s.OrderByDescending(x => x.n).ThenByDescending(x => char.IsDigit(x.g)).ThenBy(x => x.Key).Where(x => x.n > 1).ToList();
            var sb = new StringBuilder();
            foreach (var item in s)
            {
                sb.Append($"{item.g}:");
                for (int i = 0; i < item.n; i++)
                {
                    sb.Append(item.Key);
                }
                if (item != s.Last())
                    sb.Append("/");
            }
            return sb.ToString();
        }
        static string Scan(Dictionary<char, List<string>> list)
        {
            for (char i = 'A'; i <= list.Last().Key; i++)
            {
                var counter1 = 0; var counter2 = 0; var counter3 = 0; var counter4 = 0;
                for (int j = 0; j < list[i].Count; j++)
                {
                    for (int k = 1; k < 4; k++)
                    {
                        if (j + k >= list[i].Count)
                        {
                            counter1 = 0;
                            break;
                        }
                        if (list[i][j + k] == null || list[i][j] != list[i][j + k])
                        {
                            counter1 = 0;
                            break;
                        }
                        counter1++;
                    }
                    if (counter1 == 3)
                        return list[i][j];

                    for (int k = 1; k < 4; k++)
                    {
                        if ((char)(i + k) > list.Last().Key || list[(char)(i + k)].Count < 1 || j >= list[(char)(i + k)].Count)
                        {
                            counter2 = 0;
                            break;
                        }
                        if (list[(char)(i + k)][j] == null || list[i][j] != list[(char)(i + k)][j])
                        {
                            counter2 = 0;
                            break;
                        }
                        counter2++;

                    }
                    if (counter2 == 3)
                        return list[i][j];
                    for (int k = 1; k < 4; k++)
                    {
                        if ((char)(i + k) > list.Last().Key)
                        {
                            counter3 = 0;
                            break;
                        }
                        if (j + k >= list[(char)(i + k)].Count || list[(char)(i + k)].Count < 1)
                        {
                            counter3 = 0;
                            break;
                        }
                        if (list[(char)(i + k)][j + k] == null || list[i][j] != list[(char)(i + k)][j + k])
                        {
                            counter3 = 0;
                            break;
                        }
                        counter3++;
                    }
                    if (counter3 == 3)
                        return list[i][j];
                    for (int k = 1; k < 4; k++)
                    {
                        if ((char)(i - k) < 'A')
                        {
                            counter4 = 0;
                            break;
                        }
                        if (j + k >= list[(char)(i - k)].Count)
                        {
                            counter4 = 0;
                            break;
                        }
                        if (list[(char)(i - k)].Count < 1)
                        {
                            counter4 = 0;
                            break;
                        }
                        if (list[(char)(i - k)][j + k] == null || list[i][j] != list[(char)(i - k)][j + k])
                        {
                            counter4 = 0;
                            break;
                        }
                        counter4++;
                    }
                    if (counter4 == 3)
                        return list[i][j];
                }
            }
            return "Draw";
        }

        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            var list = new Dictionary<char, List<string>>();
            for (char i = 'A'; i <= 'G'; i++)
            {
                list.Add(i, new List<string>());
            }
            foreach (var item in piecesPositionList)
            {
                list[item[0]].Add(new string(item.Where((x, y) => y > 1).ToArray()));
                var result = Scan(list);
                if (result != "Draw")
                    return result;
            }

            return "Draw";
        }

        public static string HighAndLow(string numbers)
        {
            Console.WriteLine(numbers);
            var array = numbers.Split(' ');
            return $"{array.Max()} {array.Min()}";
        }

        public static string ToJadenCase(string phrase)
        {
            return string.Join(" ", phrase.Split(' ').Select(x => char.ToString(char.ToUpper(x[0])) + x.Substring(1)));
        }

        public static long digPow(int n, int p)
        {
            var sum = n.ToString().Select(char.GetNumericValue).Select((x, y) => Math.Pow(x, p + y)).Sum();
            var k = sum / n;
            return k % 1 == 0 ? (long)k : -1;
        }

        public static string[] dirReduc2(String[] arr)
        {
            var dirs = arr.GroupBy(x => x).ToDictionary(x => x.Key, y => y.Count());
            var result = new List<string>();

            var sn = dirs["NORTH"] - dirs["SOUTH"];
            var ew = dirs["EAST"] - dirs["WEST"];
            if (sn > 0) result.AddRange(Enumerable.Repeat("NORTH", sn));
            else result.AddRange(Enumerable.Repeat("SOUTH", -sn));
            if (ew > 0) result.AddRange(Enumerable.Repeat("EAST", ew));
            else result.AddRange(Enumerable.Repeat("WEST", -ew));
            return result.ToArray();
        }

        public static int FindEvenIndex(int[] arr)
        {
            var listL = new List<int>();
            var listR = new List<int>();
            var m = 0;
            var n = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                m += arr[i];
                listL.Add(m);
                n += arr[arr.Length - 1 - i];
                listR.Add(n);
            }
            listR.Reverse();
            var list = listL.Zip(listR, (x, y) => new { x, y }).ToList();
            return list.IndexOf(list.First(e => e.x == e.y));
        }
        public static string Disemvowel(string str)
        {
            Console.WriteLine(str);
            var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            return string.Join("", str.ToCharArray().Where(x => !vowels.Contains(x) && char.IsLower(x)));
        }
        public static long rowSumOddNumbers(long n)
        {
            var a = Enumerable.Range((int)(n * n - 1), (int)n * 2).Where(x => x % 2 != 0).Sum();
            return a;
        }
        public static string[] dirReduc(string[] arr)
        {
            var dirs = new Dictionary<string, string>();
            var list = arr.ToList();
            dirs.Add("NORTH", "SOUTH");
            dirs.Add("SOUTH", "NORTH");
            dirs.Add("WEST", "EAST");
            dirs.Add("EAST", "WEST");
            //for (int i = list.Count - 2; i >= 0; i--)
            //{
            //    if (list.Contains(dirs[list[i]]))
            //    {
            //        var tmp = list[i];
            //        //list[list.IndexOf(dirs[tmp])] = "x";
            //        list.Remove(dirs[tmp]);
            //        list.Remove(tmp);
            //        i--;
            //    }
            //}
            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (list.Count <= 1) break;
                if (i + 1 >= list.Count) continue;
                if (list[i] == dirs[list[i + 1]])
                {
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i);
                }
            }

            return list.ToArray();
        }

        public static string orderWeight(string strng)
        {
            var a = string.Join(" ", strng.Split(' ').Select(x => new { weigth = x.ToCharArray().Select(char.GetNumericValue).Sum(), x }).OrderBy(x => x.weigth).ThenBy(x => x.x).Select(x => x.x).ToArray());
            return "";

        }

        public static string WhoIsNext(string[] names, long n)
        {
            int q = names.Length;
            if (n <= q) return names[n - 1];
            while (n > q)
            {
                n -= q;
                q *= 2;
            }
            return names[n / (q / 5)];
        }
        public static bool isMerge(string s, string part1, string part2)
        {
            if (s == string.Empty && part1 != string.Empty && part2 != string.Empty) return false;
            var p1 = part1.ToList();
            var p2 = part2.ToList();
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == p2.FirstOrDefault() && s[i] == p1.FirstOrDefault())
                {
                    if (p2.Count > 1)
                    {
                        if (s[i + 1] == p2[1]) p2.RemoveAt(0);
                        continue;
                    }
                    if (p1.Count > 1)
                    {
                        if (s[i + 1] == p1[1]) p1.RemoveAt(0);
                    }
                }
                else if (s[i] == p2.FirstOrDefault()) p2.RemoveAt(0);
                else if (s[i] == p1.FirstOrDefault()) p1.RemoveAt(0);
                else return false;
            }
            if (p1.Count() > 0 || p2.Count() > 0) return false;
            return true;
        }

        public static string seriesSum(int n)
        {
            double sum = 0;
            for (var i = 1; i <= n; i++)
                sum += (1 / (3 * (n - 1) + 1.0));
            return sum.ToString("F");
        }


        public static int DontGiveMeFive(int start, int end)
        {
            return Enumerable.Range(start, (end - start) + 1).Where(x => x % 5 != 0).Count();
        }

        public static string PigIt(string str)
        {

            var lists = str.Split(' ').Select(x => x.ToCharArray().ToList()).ToList();
            lists.ForEach(x => x.Add(x.FirstOrDefault()));
            lists.ForEach(x => x.AddRange("ay"));
            lists.ForEach(x => x.Remove(x.FirstOrDefault()));
            return string.Join(" ", lists.Select(x => new string(x.ToArray())).ToArray());
        }

        static private IEnumerable<string> words = new List<string> { "cherry", "pineapple", "melon", "strawberry", "raspberry" };

        static int GetSimilarityRate(List<char> s, List<char> t)
        {
            var counter = 0;
            for (int i = s.Count - 1; i >= 0; i--)
            {
                if (t.Contains(s[i]))
                {
                    t.Remove(s[i]);
                    s.Remove(s[i]);
                    counter++;
                }
            }
            return counter;
        }

        static public string FindMostSimilar(string term)
        {
            // TODO: this is your task ;)
            return words.Select(x => new { x, score = GetSimilarityRate(term.ToList(), x.ToList()) }).OrderByDescending(x => x.score).FirstOrDefault().x;
        }
        struct RomanNumerals
        {
            public char C { get; set; }
            public int Value { get; set; }
            public int Index { get; set; }
        }
        public static int Solution(string roman)
        {
            var romanNumerals = new List<RomanNumerals> {   new RomanNumerals { C = 'I', Value = 1, Index = 0},
                                                            new RomanNumerals { C = 'V', Value = 5, Index = 1 },
                                                            new RomanNumerals { C = 'X', Value = 10, Index = 2},
                                                            new RomanNumerals { C = 'L', Value = 50, Index = 3},
                                                            new RomanNumerals { C = 'C', Value = 100, Index = 4},
                                                            new RomanNumerals { C = 'D', Value = 500, Index = 5},
                                                            new RomanNumerals { C = 'M', Value = 1000, Index = 6}};
            var number = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                var c = roman[i];
                var letter = romanNumerals.FirstOrDefault(x => x.C == c);

                if (i + 1 < roman.Length)
                {
                    if (letter.Index < romanNumerals.Count - 2)
                    {
                        var nextLetter = romanNumerals[letter.Index + 1];
                        if (roman[i + 1] == nextLetter.C)
                        {
                            number += (nextLetter.Value - letter.Value);
                            i++;
                            continue;
                        }

                        var secondLetter = romanNumerals[letter.Index + 2];
                        if (roman[i + 1] == secondLetter.C)
                        {
                            number += (secondLetter.Value - letter.Value);
                            i++;
                            continue;
                        }
                    }
                }
                number += letter.Value;
            }
            return number;
        }

        public static bool is_valid_IP(string IpAddres)
        {
            var arr = IpAddres.Split('.');
            var a = arr.Select(x => x.Where(c => char.IsDigit(c))).Where(x => x.Count() > 0);
            if (a.Count() != 4)
                return false;
            var b = arr.Where(x => int.Parse(x) > 255 && int.Parse(x) >= 0);
            var c1 = arr.Where(x => x.Contains('0') && x.Length > 1).Count() == 0;
            var c2 = arr.Where(x => x.Contains(' '));
            return arr.Count() == 4;
        }
        public int DigitalRoot(long n)
        {
            if (n < 10)
                return (int)n;
            else
                return DigitalRoot((long)n.ToString().Select(char.GetNumericValue).Sum());

        }
        static Dictionary<char, char> bracesDict = new Dictionary<char, char>()
        {
            {'(',')' }, {'[',']' }, {'{','}' }
        };
        public static bool Check(string input)
        {
            var txt = input;
            while (txt.Length > 0)
            {
                var id1 = txt.IndexOf(txt[0]) + 1;
                if (!bracesDict.ContainsKey(txt[0]))
                    return false;
                var id2 = txt.IndexOf(bracesDict[txt[0]]);
                if (id2 + 1 < txt.Length && id2 != -1)
                    if (txt[id2] == txt[id2 + 1])
                        id2++;
                if (id1 == -1 || id2 == -1)
                    return false;
                var rest = txt.Substring(id2 + 1, txt.Length - (id2 + 1));
                if (rest.Length > 0)
                    if (!Check(rest))
                        return false;
                txt = txt.Substring(id1, id2 - id1);
            }
            return true;
        }

        public static bool validBraces2(String str)
        {
            string prev = "";
            while (str.Length != prev.Length)
            {
                prev = str;
                str = str
                    .Replace("()", String.Empty)
                    .Replace("[]", String.Empty)
                    .Replace("{}", String.Empty);
            }
            return (str.Length == 0);
        }
        public static int DblLinear(int n)
        {
            var list = new List<int>() { 1 };
            int x = 0;
            while (list.Count <= n + 5)
            {
                var y = 2 * list[x] + 1;
                if (!list.Contains(y))
                    list.Add(2 * list[x] + 1);
                var z = 3 * list[x] + 1;
                if (!list.Contains(z))
                    list.Add(z);
                //list = list.Distinct().ToList();
                x++;
            }
            return list.OrderBy(s => s).ElementAt(n);
        }

        public static void GetTypes(List<Tuple<object, Type>> objectTypes)
        {
            for (int i = 0; i < objectTypes.Count; i++)
            {
                objectTypes[i] = new Tuple<object, Type>(objectTypes[i].Item1, objectTypes[i].Item1.GetType());
            }
        }
        public static string[] WordSearch(string query, string[] seq)
        {
            return seq.Where(x => x.Contains(query)).ToArray();
        }
        public static string[] GetMethodNames(object TestObject)
        {
            var t = TestObject.GetType();
            return t.GetMethods().Select(x => x.Name).ToArray();
        }
        public static string ConcatStringMembers(object TestObject)
        {
            var methods = TestObject.GetType().GetMethods().Where(x => x.ReturnType == typeof(string) && x.Name != "ToString");
            var result = "";
            foreach (var method in methods)
            {
                Console.WriteLine(string.Join(" ", method.GetParameters().Select(x => x.ParameterType)));
                result += method.Invoke(Activator.CreateInstance(TestObject.GetType()), null);
            }
            return result;
        }
        public class testClass
        {
            public string Output1()
            {
                return "Output";
            }

            public string Output2()
            {
                return "It";
            }
        }

        public static object[] Unflatten(int[] flatArray)
        {
            var list = new List<object>();
            var stack = new Stack<int>(flatArray.Reverse());
            while (stack.Count > 0)
            {
                var number = stack.First();
                if (number < 3)
                {
                    list.Add(number);
                    stack.Pop();
                }
                else
                {
                    if (number > stack.Count)
                        number = stack.Count;
                    list.Add(stack.Take(number).ToArray());
                    for (int i = 0; i < number; i++)
                    {
                        stack.Pop();
                    }
                }
            }

            return list.ToArray();
        }

        static double Factorial(int n)
        {
            if (n <= 0)
                return 1;
            return n * Factorial(n - 1);
        }

        static double PascalElement(int row, int col)
        {
            return Factorial(row) / (Factorial(col) * Factorial(row - col));
        }

        public static List<int> PascalsTriangle(int n)
        {
            var counter = 0;
            var list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    list.Add((int)Math.Round(PascalElement(i, j)));
                    counter++;
                }
            }

            return list;
        }
        public static long NextBiggerNumber(long n)
        {
            var list = n.ToString().Select(c => (int)char.GetNumericValue(c)).ToList();
            var x = list.Count - 1;
            for (int i = list.Count - 2; i >= 0; i--)
            {
                if (list[i] < list[i + 1])
                {
                    x = i;
                    break;
                }
            }
            var y = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[x] < list[i])
                {
                    y = i;
                    break;
                }
            }
            var tmp = list[x];
            list[x] = list[y];
            list[y] = tmp;
            if (list.Count - (x + 1) == 0) return -1;
            list.Reverse(x + 1, list.Count - (x + 1));
            return long.Parse(string.Join("", list.Select(c => c.ToString())));
        }


        static List<int> primes = new List<int>() { 2, 3, 5, 7, 11, 13, 17 };
        public static string sumOfDivided(int[] lst)
        {
            var list = new List<int>();
            var sb = new StringBuilder();
            foreach (var prime in primes.TakeWhile(x => x <= lst.Max()))
            {
                var sum = lst.Where(x => x % prime == 0).Sum();
                if (sum != 0)
                {
                    sb.Append($"({prime} {sum})");
                }

            }
            return sb.ToString();
        }
        public static int[] DeleteNth(int[] arr, int x)
        {
            var list = new List<int>();
            foreach (var item in arr)
            {
                if (list.Where(c => c == item).Count() < x)
                    list.Add(item);
            }
            return list.ToArray();
        }
        static IEnumerable<char> GetPermutation(List<char> word, int middle, int counter)
        {
            var index = (middle - Factorial(counter)) > 0 ? middle / (middle - Factorial(counter)) : 0;
            var result = new List<char>();
            result.Add(word.ElementAt((int)index));
            word.RemoveAt((int)index);
            if (counter == 1) return result;
            return result.Concat(GetPermutation(word, middle, counter - 1));

        }
        private IDictionary<string, string> presetColors;
        public class RGB
        {
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
            public RGB(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }
        }

        public void HtmlColorParser(IDictionary<string, string> presetColors)
        {
            this.presetColors = presetColors;
        }
        static public RGB Parse(string color)
        {
            if (color.Length == 7)
                return new RGB(Convert.ToByte(color.Substring(1, 2), 16), Convert.ToByte(color.Substring(3, 2), 16), Convert.ToByte(color.Substring(5, 2), 16));
            if (color.Length == 4)
                return new RGB(Convert.ToByte(new string(new char[] { color[1], color[1] }), 16), Convert.ToByte(new string(new char[] { color[2], color[2] }), 16),
                                Convert.ToByte(new string(new char[] { color[3], color[3] }), 16));

            return new RGB(0, 0, 0);
        }
        public static int Find(int[] integers)
        {
            return integers.GroupBy(x => x % 2).FirstOrDefault(x => x.Count() == 1).FirstOrDefault();
        }
        public static int[] SortArray(int[] array)
        {
            if (array.Length == 0) return array;
            var even = new Dictionary<int, int>();
            var sorted = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0)
                    even.Add(i, array[i]);
                else
                    sorted.Add(array[i]);
            }
            sorted = sorted.OrderBy(x => x).ToList();
            foreach (var item in even)
            {
                sorted.Insert(item.Key, item.Value);
            }
            return sorted.ToArray();
        }
        public static int[] TakeWhile(int[] arr, Func<int, bool> pred)
        {
            return arr.TakeWhile(pred).ToArray();

        }
        static public int BestMatch(int[] goals1, int[] goals2)
        {
            return goals1.Zip(goals2, (x, y) => new { diff = x - y, y }).Select((x, i) => new { diff = x.diff, y = x.y, index = i }).OrderBy(x => x.diff).ThenByDescending(x => x.y).FirstOrDefault().index;

        }
        static double map(double n, double start1, double stop1, double start2, double stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
        public static string Unlock(string str)
        {
            var a = str.ToLower().Select(n => char.Parse(((int)(2 * (((n - 97.0) / (115.0 - 97.0)) * (4.0 - 1.0) + 1.0))).ToString())).ToArray();
            var list = new List<char>();
            foreach (var item in str.ToLower())
            {
                var c = ((int)(2 * (((item - 97.0) / (115.0 - 97.0)) * (4.0 - 1.0) + 1.0)));
                if (c > 8)
                    list.Add(char.Parse(8.ToString()));
                else
                    list.Add(char.Parse(c.ToString()));
            }
            return new string(list.ToArray());
        }

        public static String LongestConsec(string[] strarr, int k)
        {
            var list = new List<string>();
            for (int i = 0; i < strarr.Length - k; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < k; j++)
                {
                    sb.Append(strarr[i + j]);
                }
                list.Add(sb.ToString());
            }
            return list.OrderByDescending(x => x.Length).FirstOrDefault();
        }
        public static string[] Solution2(string str)
        {
            var list = new List<string>();
            for (int i = 0; i < str.Length; i += 2)
            {
                if (i + 1 >= str.Length)
                    list.Add(str[i] + "_");
                else
                    list.Add(str.Substring(i, 2));
            }
            return list.ToArray();
        }
        public static double binaryArrayToNumber(int[] BinaryArray)
        {
            return BinaryArray.Reverse().
                Select((x, y) => new { value = x, index = y }).
                Sum(x => x.value * Math.Pow(2, x.index));
        }
        public static string ToBase64(string s)
        {
            var a = s.Select(x => Convert.ToString(x, 2));
            var sb = new StringBuilder();
            foreach (var item in s)
            {
                var bin = Convert.ToString(item, 2);
                if (bin.Length < 8)
                    sb.Append("0" + bin);
                else
                    sb.Append(bin);
            }
            return "";
        }
        static int Counter = 0;
        public static int palindromeChainLength(int n)
        {
            var reversed = string.Join("", n.ToString().Reverse());
            if (n.ToString() == reversed)
                return Counter;
            else
            {
                Counter++;
                return palindromeChainLength(n + Convert.ToInt32(reversed));
            }
        }
        public static bool Scramble(string str1, string str2)
        {
            var list1 = str1.ToList();
            foreach (var item in str2)
            {
                if (!list1.Contains(item))
                    return false;
                else
                    list1.Remove(item);
            }
            return true;
        }

        public class PlayPass
        {

            public static string playPass(string s, int n)
            {
                var chars = s.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    if (char.IsDigit(chars[i]))
                    {
                        chars[i] = char.Parse((9 - char.GetNumericValue(chars[i])).ToString());
                    }
                    else if (char.IsLetter(chars[i]))
                    {
                        var a = (int)chars[i] + n;
                        if (a > 90)
                            a = a - 25;
                        chars[i] = (char)(a);
                    }
                    if (i % 2 == 0)
                    {
                        chars[i] = char.Parse(chars[i].ToString().ToUpper());
                    }
                    else
                    {
                        chars[i] = char.Parse(chars[i].ToString().ToLower());
                    }
                }
                return new string(chars.Reverse().ToArray());

            }
        }


        static void Main(string[] args)
        {
            var poker = new PokerHand("7C 9S AH 9C QD");
            var result = poker.CompareWith(new PokerHand("AS TS 9C 9H 2C"));
        }
    }
}
