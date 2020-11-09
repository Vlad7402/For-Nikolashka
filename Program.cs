using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace For_Nikolashka
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] alphabet = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '@', '#', '$', '%', '^', '&', '*', '<', '>', '(', ')', '|', '~' };
            string[] lines = new string[5];
            lines[0] = "Перевод числа в различные системы счисления(с поддержкой вещественных)";
            lines[1] = "Перевод числа в римскую систему счисления";
            lines[2] = "Объяснение сложения столбиком";
            lines[3] = "Объяснение умножения столбиком";
            lines[4] = "Выход";
            while (true)
            {
                int chose = Menu(lines);
                if (chose == 0) TranslateInAny();
                if (chose == 1) PrinValInRim();
                if (chose == 2) SumForFive();
                if (chose == 3) MultiForFive();
                if (chose == 4 ) break;
            }
        }
        static void TranslateInAny()
        {
            char[] alphabet = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '@', '#', '$', '%', '^', '&', '*', '<', '>', '(', ')', '|', '~' };
            string[] numInfo = GetInputNum(alphabet);
            Console.Clear();
            bool isMinusFirst = IsMinusFirst(numInfo[0]);
            decimal valInTen = GetValInTen(numInfo[0], numInfo[1], alphabet);
            int newBasis = GetBasis("для нового числа.");
            Console.Clear();
            string vallInany = GetValInAny(valInTen, newBasis, alphabet);
            PrinValInAny(vallInany, isMinusFirst);
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }
        static void PrinValInAny(string input, bool minus)
        {
            string minusStr = "";
            if (minus) minusStr = "-";
            Console.WriteLine("Значение в искомой системе счистления равно:");
            Console.WriteLine(minusStr + input);
        }
        static int[] GetDotNum(string input)
        {
            int[] result = new int[2];
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    result[0]++;
                    result[1] = i;
                }
            }
            return result;
        }
        static bool IsInputCorrect(string input, char[] alphabet, int basis)
        {
            bool result = false;
            int[] dotNum = GetDotNum(input);
            if (!(input.Length > 17 || input.Length < 1 || (input.Length == 1 && IsMinusFirst(input))))
            {
                if (IsMinusFirst(input))
                {
                    if (dotNum[0] == 1)
                    {
                        if (IsItInAlphabet(1, dotNum[1], basis, input, alphabet) &&
                            IsItInAlphabet((dotNum[1] + 1), input.Length, basis, input, alphabet))
                        {
                            result = true;
                        }
                    }
                    else if (dotNum[0] == 0)
                    {
                        if (IsItInAlphabet(1, input.Length, basis, input, alphabet)) result = true;
                    }
                }
                else
                {
                    if (dotNum[0] == 1)
                    {
                        if (IsItInAlphabet(0, dotNum[1], basis, input, alphabet) &&
                            IsItInAlphabet((dotNum[1] + 1), input.Length, basis, input, alphabet))
                        {
                            result = true;
                        }
                    }
                    else if (dotNum[0] == 0)
                    {
                        if (IsItInAlphabet(0, input.Length, basis, input, alphabet)) result = true;
                    }
                }
            }
            return result;
        }
        static string[] GetInputNum(char[] alphabet)
        {
            string[] result = new string[2];
            result[0] = "";
            result[1] = "";
            Console.WriteLine("Пожалуйста, введите значение для перевода.");
            string val = Console.ReadLine();
            Console.Clear();
            val = val.ToUpper();
            result[0] = val;
            int basis = GetBasis("для данного числа.");
            Console.Clear();
            result[1] = basis.ToString();
            while (!IsInputCorrect(val, alphabet, basis))
            {
                Console.WriteLine("Возможно, вы ошиблись.");
                Console.WriteLine("Символы необходимо вводить в соответствии с алфавитом, длинна числа не должна превышать 17 символов.");
                Console.WriteLine("Пожалуйста, введите значение для перевода.");
                val = Console.ReadLine();
                Console.Clear();
                val = val.ToUpper();
                result[0] = val;
                basis = GetBasis("для данного числа.");
                Console.Clear();
                result[1] = basis.ToString();
            }
            return result;
        }
        static int GetRightVal(string input)
        {
            int result = 0;
            while (!int.TryParse(input, out result))
            {
                Console.WriteLine("Возможно, вы ошиблись. Пожалуйста, введите значение ещё раз.");
                input = Console.ReadLine();
                Console.Clear();
            }
            return result;
        }
        static int GetBasis(string add)
        {
            int result = 0;
            Console.WriteLine("Пожалуйста, введите значение основания " + add);
            result = GetRightVal(Console.ReadLine());
            Console.Clear();
            if (result < 2 || result > 50)
            {
                while (true)
                {
                    Console.WriteLine("Обратите внимание, основание не может быть меньше 2 и больше 50.");
                    Console.WriteLine("Пожалуйста, введите значение основания.");
                    result = GetRightVal(Console.ReadLine());
                    Console.Clear();
                    if (!(result < 2 || result > 50)) break;
                }
            }
            return result;
        }
        static bool IsItInAlphabet(int lineStart, int lineEnd, int basis, string input, char[] alphabet) 
        {
            bool result = false;
            int counter = 0;
            for (int i = lineStart; i < lineEnd; i++)
            {
                for (int j = 0; j < basis; j++)
                {
                    if (input[i] == alphabet[j])
                    {
                        counter++;
                        break;
                    }
                }
            }
            if (counter == (lineEnd - lineStart)) result = true;
            return result;
        }
        static decimal GetValInTen (string input, string basisStr, char[] alphabet)
        {
            int basis = int.Parse(basisStr);
            decimal result = 0;
            int[] dotNum = GetDotNum(input);
            decimal[] beforDot = new decimal[17];
            decimal[] afterDot = new decimal[20];
            if (IsMinusFirst(input))
            {
                if (dotNum[0] == 1)
                {
                    for (int i = 1; i < dotNum[1]; i++)
                    {
                        for (int j = 0; j < basis; j++)
                        {
                            if (input[i] == alphabet[j]) beforDot[i] = (decimal)j * (decimal)Math.Pow((double)basis, (double)(input.Length - i - 2));
                        }
                    }
                    for (int i = dotNum[1] + 1; i < input.Length; i++)
                    {
                        for (int j = 0; j < basis; j++)
                        {
                            if (input[i] == alphabet[j]) afterDot[(i - (dotNum[1] + 1))] = j;
                        }
                    }
                }
                else if (dotNum[0] == 0)
                {
                    for (int i = 1; i < input.Length; i++)
                    {
                        for (int j = 0; j < basis; j++)
                        {
                            if (input[i] == alphabet[j]) beforDot[i] = (decimal)j * (decimal)Math.Pow((double)basis, (double)(input.Length - i - 1));
                        }
                    }
                }
            }
            else
            {
                if (dotNum[0] == 1)
                {
                    for (int i = 0; i < dotNum[1]; i++)
                    {
                        for (int j = 0; j < basis; j++)
                        {
                            if (input[i] == alphabet[j]) beforDot[i] = (decimal)j * (decimal)Math.Pow((double)basis, (double)(dotNum[1] - i - 1));
                        }
                    }
                    for (int i = dotNum[1] + 1; i < input.Length; i++)
                    {
                        for (int j = 0; j < basis; j++)
                        {
                            if (input[i] == alphabet[j]) afterDot[(i -(dotNum[1] + 1))] = j;
                        }
                    }
                }
                else if (dotNum[0] == 0)
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        for (int j = 0; j < basis; j++)
                        {
                            if (input[i] == alphabet[j]) beforDot[i] = (decimal)j * (decimal)Math.Pow((double)basis, (double)(input.Length - i - 1));
                        }
                    }
                }
            }
            decimal afterDotVel = 0;
            for (int i = 0; i < afterDot.Length; i++)
            {
                
                afterDotVel += afterDot[i] * (decimal)Math.Pow(basis, (-i - 1));
            }
            decimal beforDotVel = 0;
            for (int i = 0; i < beforDot.Length; i++)
            {
                beforDotVel += beforDot[i];
            }
            result = (decimal)beforDotVel + afterDotVel;
            return result;
        }
        static bool IsMinusFirst(string input)
        {
            bool result = false;
            if (input[0] == '-') result = true;
            return result;
        }
        static int GetIntLenght (int input)
        {
            int result = 0;
            string inputStr = input.ToString();
            result = inputStr.Length;
            return result;
        }
        static string GetValInAny(decimal input, int basis, char[] alphabet)
        {
            string result = "";
            decimal beforDot = (decimal)Math.Truncate(input);
            decimal afterDot = input - beforDot;
            int[] afterDotArr = new int[23];
            char[] afterDotArrChar = new char[23];
            string beforDotVel = "";
            string beforDotVelRevers = "";
            if (beforDot  >= basis)
            {
                decimal last = beforDot;
                int counter = 0;
                while (last >= basis)
                {
                    int temp = 0;
                    temp = (int)(last % basis);
                    last = last / basis;
                    beforDotVelRevers += alphabet[temp];
                    counter++;
                }
                beforDotVelRevers += alphabet[(int)Math.Truncate(last)];
                for (int i = beforDotVelRevers.Length - 1; i >= 0; i--)
                {
                    beforDotVel += beforDotVelRevers[i];
                }
            }
            else { beforDotVel += alphabet[(int)beforDot]; }
            bool isAfterdotNull = afterDot == 0;
            if (!isAfterdotNull)
            {
                for (int i = 0; i < 23; i++)
                {
                    decimal afterDotTempVel = afterDot * basis;
                    afterDotArr[i] = (int)Math.Truncate(afterDotTempVel);
                    afterDot = afterDotTempVel - (int)Math.Truncate(afterDotTempVel);
                }
                for (int i = 0; i < 23; i++)
                {
                    afterDotArrChar[i] = alphabet[afterDotArr[i]];
                }
                string afterDotVel = "";
                for (int i = 0; i < afterDotArrChar.Length; i++)
                {
                    afterDotVel += afterDotArrChar[i];
                }
                result = beforDotVel + "." + afterDotVel;
            }
            else
            {
                result = beforDotVel;
            }

            return result;
        }
        static int GetValForRim()
        {
            int result = 0;
            Console.WriteLine("Пожалуйста, введите зачение от 1 до 5000 для перевода в римскую систему счисления");
            result = GetRightVal(Console.ReadLine());
            Console.Clear();
            if (result < 1 || result > 5000)
            {
                while (true)
                {
                    Console.WriteLine("Вы допустили ошибку. Попробуйте ещё раз.");
                    Console.WriteLine("Пожалуйста, введите зачение от 1 до 5000 для перевода в римскую систему счисления");
                    result = GetRightVal(Console.ReadLine());
                    Console.Clear();
                    if (!(result < 1 || result > 5000)) break;
                }
            }
            return result;
        }
        static string TranslateInRim (int inputReversInt)
        {
            string inputRevers = inputReversInt.ToString();
            string input = "";
            string result = "";
            string resultRev = "";
            int lenght = GetIntLenght(inputReversInt);
            for (int i = GetIntLenght(inputReversInt) - 1; i >= 0; i--)
            {
                input += inputRevers[i];
            }
            if (input[0] == '1') result += "I";
            if (input[0] == '2') result += "II";
            if (input[0] == '3') result += "III";
            if (input[0] == '4') result += "VI";
            if (input[0] == '5') result += "V";
            if (input[0] == '6') result += "IV";
            if (input[0] == '7') result += "IIV";
            if (input[0] == '8') result += "IIIV";
            if (input[0] == '9') result += "XI";
            if (lenght > 1)
            {
                if (input[1] == '1') result += "X";
                if (input[1] == '2') result += "XX";
                if (input[1] == '3') result += "XXX";
                if (input[1] == '4') result += "LX";
                if (input[1] == '5') result += "L";
                if (input[1] == '6') result += "XL";
                if (input[1] == '7') result += "XXL";
                if (input[1] == '8') result += "XXXL";
                if (input[1] == '9') result += "CX";
            }
            if (lenght > 2)
            {
                if (input[2] == '1') result += "C";
                if (input[2] == '2') result += "CC";
                if (input[2] == '3') result += "CCC";
                if (input[2] == '4') result += "DC";
                if (input[2] == '5') result += "D";
                if (input[2] == '6') result += "CD";
                if (input[2] == '7') result += "CCD";
                if (input[2] == '8') result += "CCCD";
                if (input[2] == '9') result += "MC";
            }
            if (lenght > 3)
            {
                if (input[3] == '1') result += "M";
                if (input[3] == '2') result += "MM";
                if (input[3] == '3') result += "MMM";
                if (input[3] == '4') result += "M XM";
                if (input[3] == '5') result += "M X";
            }
            for (int i = result.Length - 1; i >= 0; i--)
            {
                resultRev += result[i];
            }
            return resultRev;
        }
        static void PrinValInRim()
        {
            int valForRim = GetValForRim();
            Console.Clear();
            string valRim = TranslateInRim(valForRim);
            Console.WriteLine("Число " + valForRim + " в римской систеие счисления равно " + valRim);
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
        }
        static int Menu(string[] lines)
        {
            Console.Clear();
            int chose = 0;
            Console.SetCursorPosition(0, 0);
            LinePrinting(lines);
            Recolour(chose, lines[chose], lines.Length);
            while (true)
            {
                int[] delta = GetMoove(chose, lines.Length);
                if (delta[0] != 0)
                {
                    chose += delta[0];
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    LinePrinting(lines);
                    Recolour(chose, lines[chose], lines.Length);
                }
                Console.SetCursorPosition(0, lines.Length);
                if (delta[1] == 1) break;
            }
            Console.Clear();
            return chose;
        }
        static void Recolour(int position, string line, int menuLenght)
        {
            Console.SetCursorPosition(0, position);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, (menuLenght + 1));
        }
        static int[] GetMoove(int position, int menuLenght)
        {
            int[] delta = new int[2];
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo buttonPresed = Console.ReadKey();
                if (buttonPresed.Key == ConsoleKey.UpArrow) delta[0] = -1;
                if (buttonPresed.Key == ConsoleKey.DownArrow) delta[0] = 1;
                if (buttonPresed.Key == ConsoleKey.Enter) delta[1] = 1;
            }
            System.Threading.Thread.Sleep(25);
            if (!IsMoveAvailable(position, menuLenght, delta[0])) delta[0] = 0;
            return delta;
        }
        static bool IsMoveAvailable(int position, int menuLenght, int move)
        {
            bool result = false;
            if (position + move < menuLenght &&
                position + move >= 0)
            { result = true; }
            return result;
        }
        static void LinePrinting(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
        static string Sumator(string firstNum, string secNum, int basis, char[] alphabet)
        {
            string resultStr = string.Empty;
            int temp = 0;
            firstNum = GetStringWith0First(firstNum);
            secNum = GetStringWith0First(secNum);
            int longestString = GetLongestString(firstNum, secNum) - 2;
            while (longestString > firstNum.Length)
            {
                firstNum = GetStringWith0First(firstNum);
            }
            while (firstNum.Length > secNum.Length)
            {
                secNum = GetStringWith0First(secNum);
            }
            List<char> result = new List<char>();
            for (int i = 0; i < firstNum.Length; i++) result.Add('0');
            for (int i = 0; i < firstNum.Length; i++)
            {
                int firstNumInTen = (int)GetValInTen(firstNum[firstNum.Length - 1 - i].ToString(), basis.ToString(), alphabet);
                int secNumInTen = (int)GetValInTen(secNum[secNum.Length - 1 - i].ToString(), basis.ToString(), alphabet);
                string sumResult = GetValInAny((decimal)(firstNumInTen + secNumInTen + temp), basis, alphabet);
                if (sumResult.Length == 1) sumResult = GetStringWith0First(sumResult);
                string pripiska1 = "";
                if (temp != 0) pripiska1 = " и осататок " + GetValInAny((decimal)temp, basis, alphabet);
                temp = 0;
                temp = (int)GetValInTen(sumResult[0].ToString(), basis.ToString(), alphabet);
                string pripiska = "";
                if (temp != 0) pripiska = " Осататок " + sumResult[0] + " от сумы переносится вперёд.";

                result.RemoveAt(firstNum.Length - 1 - i);
                result.Insert(firstNum.Length - 1 - i, sumResult[1]);
                Console.SetCursorPosition(1, 0);
                GoreisontPassing(GetLongestString(firstNum, secNum) - firstNum.Length);
                Console.WriteLine(firstNum);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("+");
                Console.SetCursorPosition(1, 2);
                GoreisontPassing(GetLongestString(firstNum, secNum) - secNum.Length);
                Console.WriteLine(secNum);
                Console.SetCursorPosition(1, 3);
                for (int j = 0; j < firstNum.Length; j++) Console.Write('─');
                string itog = "";
                for (int j = 0; j < result.Count; j++) itog += result[j];
                Console.SetCursorPosition(1, 4);
                Console.WriteLine(itog);
                Console.WriteLine("Складываем числа " + (i + 1) + " порядка" + pripiska1 + ". Результат равен " + sumResult + ". " + pripiska + "Вниз записываем " + sumResult[1] + ".");
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
                Console.Clear();
            }
            for (int i = 0; i < result.Count; i++) resultStr += result[i];
            return resultStr;
        }
        static string GetStringWith0First(string input) 
        {
            return "0" + input;
        }
        static int GetLongestString(string str1, string str2)
        {
            if (str1.Length >= str2.Length) return str1.Length;
            else return str2.Length;
        }
        static void GoreisontPassing(int pass)
        {
            for (int i = 0; i < pass; i++) Console.Write(" ");
        }
        static void SumForFive() 
        {
            char[] alphabet = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '@', '#', '$', '%', '^', '&', '*', '<', '>', '(', ')', '|', '~' };
            string[] numInfo1 = GetInputNum(alphabet);
            string[] numInfo2 = GetInputNum(alphabet);
            while (numInfo1[1] != numInfo2[1])
            {
                Console.WriteLine("Системы счисления должны совпадать.");
                Console.WriteLine("Пожалуйста, попробуйте eщё раз.");
                Console.ReadKey();
                Console.Clear();
                numInfo1 = GetInputNum(alphabet);
                while (IsMinusOrDotInside(numInfo1[0]))
                {
                    Console.WriteLine("Сумма может осуществляться только для натуральных чисел.");
                    Console.WriteLine("Пожалуйста, попробуйте eщё раз.");
                    Console.ReadKey();
                    Console.Clear();
                    numInfo1 = GetInputNum(alphabet);
                }
                numInfo2 = GetInputNum(alphabet);
                while (IsMinusOrDotInside(numInfo2[0]))
                {
                    Console.WriteLine("Сумма может осуществляться только для натуральных чисел.");
                    Console.WriteLine("Пожалуйста, попробуйте eщё раз.");
                    Console.ReadKey();
                    Console.Clear();
                    numInfo2 = GetInputNum(alphabet);
                }
                Console.Clear();
            }
            Sumator(numInfo1[0], numInfo2[0], int.Parse(numInfo1[1]), alphabet);
            Console.Clear();
        }
        static bool IsMinusOrDotInside(string input)
        {
            bool result = false;
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == '-' || input[i] == '.') 
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        static void Multiplicator(string firstNum, string secNum, int basis, char[] alphabet)
        {
            int temp = 0;
            firstNum = GetStringWith0First(firstNum);
            int longestString = GetLongestString(firstNum, secNum) - 2;
            while (longestString > firstNum.Length)
            {
                firstNum = GetStringWith0First(firstNum);
            }
            List<char> result = new List<char>();
            Stack<long> forSumming = new Stack<long>();
            Console.WriteLine("Выполняем умноение чисел по разрядам.");
            Console.WriteLine("Результат каждого записываем отдельно для дальнейшего сложения.");
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
            Console.Clear();
            for (int k = 0; k < secNum.Length; k++)
            {
                for (int i = 0; i < firstNum.Length; i++) result.Add('0');
                for (int i = 0; i < firstNum.Length; i++)
                {
                    int firstNumInTen = (int)GetValInTen(firstNum[firstNum.Length - 1 - i].ToString(), basis.ToString(), alphabet);
                    int secNumInTen = (int)GetValInTen(secNum[secNum.Length - 1 - k].ToString(), basis.ToString(), alphabet);
                    string sumResult = GetValInAny((decimal)(firstNumInTen * secNumInTen + temp), basis, alphabet);
                    if (sumResult.Length == 1) sumResult = GetStringWith0First(sumResult);
                    string pripiska1 = "";
                    if (temp != 0) pripiska1 = " и прибавляем осататок " + GetValInAny((decimal)temp, basis, alphabet);
                    temp = 0;
                    temp = (int)GetValInTen(sumResult[0].ToString(), basis.ToString(), alphabet);
                    string pripiska = "";
                    if (temp != 0) pripiska = " Осататок " + sumResult[0] + " от произведения переносится вперёд.";

                    result.RemoveAt(firstNum.Length - 1 - i);
                    result.Insert(firstNum.Length - 1 - i, sumResult[1]);
                    Console.SetCursorPosition(1, 0);
                    GoreisontPassing(GetLongestString(firstNum, secNum) - firstNum.Length);
                    Console.WriteLine(firstNum);
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("*");
                    Console.SetCursorPosition(1, 2);
                    GoreisontPassing(GetLongestString(firstNum, secNum) - secNum.Length);
                    Console.WriteLine(secNum);
                    Console.SetCursorPosition(1, 3);
                    for (int j = 0; j < firstNum.Length; j++) Console.Write('─');
                    string itog = "";
                    for (int j = 0; j < result.Count; j++) itog += result[j];
                    Console.SetCursorPosition(1, 4);
                    Console.WriteLine(itog);
                    Console.WriteLine("Умножаем число " + (i + 1) + " порядка" + " на " + secNum[secNum.Length - 1 - k].ToString() + pripiska1 + ". Результат равен " + sumResult + ". " + pripiska + "Вниз записываем " + sumResult[1] + ".");
                    Console.WriteLine("Для продолжения нажмите любую клавишу.");
                    Console.ReadKey();
                    Console.Clear();
                }
                string resultStr = string.Empty;
                for (int i = 0; i < result.Count; i++) resultStr += result[i];
                Console.WriteLine("Так как мы умножали на число " + (k + 1) + " порядка, то к "  + resultStr + " приписываем " + k + " \"0\".");
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
                Console.Clear();
                for (int i = 0; i < k; i++) resultStr += '0';
                forSumming.Push((long)GetValInTen(resultStr, basis.ToString(), alphabet));
                result.Clear();
                temp = 0;
            }
            while (forSumming.Count != 1)
            {
                string firsNumForSumming = GetValInAny((decimal)forSumming.Pop(), basis, alphabet);
                string secNumForSumming = GetValInAny((decimal)forSumming.Pop(), basis, alphabet);
                Console.WriteLine("Теперь складываем " + firsNumForSumming + " и " + secNumForSumming + " .");
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
                Console.Clear();
                string sumResult = Sumator(firsNumForSumming, secNumForSumming, basis, alphabet);
                forSumming.Push((long)(GetValInTen(sumResult, basis.ToString(), alphabet)));
                Console.Clear();
            }
            Console.WriteLine("Результат умножения равен " + forSumming.Pop() + " .");
            Console.WriteLine("Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
            Console.Clear();
        }
        static void MultiForFive()
        {
            char[] alphabet = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '!', '@', '#', '$', '%', '^', '&', '*', '<', '>', '(', ')', '|', '~' };
            string[] numInfo1 = GetInputNum(alphabet);
            string[] numInfo2 = GetInputNum(alphabet);
            while (numInfo1[1] != numInfo2[1])
            {
                Console.WriteLine("Системы счисления должны совпадать.");
                Console.WriteLine("Пожалуйста, попробуйте eщё раз.");
                Console.ReadKey();
                Console.Clear();
                numInfo1 = GetInputNum(alphabet);
                while (IsMinusOrDotInside(numInfo1[0]) || numInfo1.Length > 5)
                {
                    Console.WriteLine("Произведение может осуществляться только для натуральных чисел, длинна которых не превышает 5 символов.");
                    Console.WriteLine("Пожалуйста, попробуйте eщё раз.");
                    Console.ReadKey();
                    Console.Clear();
                    numInfo1 = GetInputNum(alphabet);
                }
                numInfo2 = GetInputNum(alphabet);
                while (IsMinusOrDotInside(numInfo2[0]) || numInfo2.Length > 5)
                {
                    Console.WriteLine("Произведение может осуществляться только для натуральных чисел, длинна которых не превышает 5 символов.");
                    Console.WriteLine("Пожалуйста, попробуйте eщё раз.");
                    Console.ReadKey();
                    Console.Clear();
                    numInfo2 = GetInputNum(alphabet);
                }
                Console.Clear();
            }
            Multiplicator(numInfo1[0], numInfo2[0], int.Parse(numInfo1[1]), alphabet);
            Console.Clear();
        }
    }
}
