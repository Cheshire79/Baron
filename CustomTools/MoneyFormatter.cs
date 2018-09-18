using System;
using System.Text;

namespace Poker.Tools
{
    public static class MoneyFormatter
    {
        private const char DollarSymbol = '$';
        private const char DecimalSeparator = '.';
        private const char ThousandsSeparator = ',';
        private const int DigitsInterval = 3;
        private static readonly char[] DegreeSymbols = {'K', 'M', 'B', 'T', 'Q'};
        private static readonly StringBuilder _buffer = new StringBuilder(20);

        public static string Format(long value, int digitsLimit)
        {
            try
            {
                lock (_buffer)
                {
                    if (digitsLimit > DigitsInterval)
                    {
                        return ToLongForm(value, digitsLimit);
                    }
                    return ToShortForm(value);
                }
            }
            catch (Exception e)
            {
                //Logger.LogWarning(string.Format("MoneyFormatter.format({0}, {1}). {2}", value, digitsLimit, e));
                return "$?";
            }
        }

        /**
	 * Formats money amount into string using as many as possible digits,
	 * but not more than <code>digitsLimit</code>.
	 * <br>For example:
	 * <ul>
	 * <li>toLongForm( 1L , 5 ) = "$1"</li>
	 * <li>toLongForm( 1234L , 5 ) = "$1,234"</li>
	 * <li>toLongForm( 12345678L , 5 ) = "$12,346K"</li>
	 * <li>toLongForm( 999999L , 5 ) = "$1M"</li>
	 * <li>toLongForm( 100000000L , 5 ) = "$100M"</li>
	 * <li>toLongForm( 1000590L , 5 ) = "$1,001K"</li>
	 * </ul>
	 * @param value
	 * @param digitsLimit (should be greater than 2)
	 * @return
	 */

        private static string ToLongForm(long value, int digitsLimit)
        {
            PutIntoBuffer(value);
            int thousandDegree = cutIntoBuffer(value, digitsLimit, 1000);
            thousandDegree += cutExtraThousandsFromBuffer();
            addThousandSeparatorsIntoBuffer();
            addThousandDegreeSymbol(thousandDegree);
            addDollarSymbol();
            return _buffer.ToString();
        }

        /**
	 * Formats money amount into 3-digits form.
	 * <br>For example:
	 * <ul>
	 * <li>$1 -> "$1"</li>
	 * <li>$634 -> "$634"</li>
	 * <li>$1,234 -> "$1.23K"</li>
	 * <li>$12,345 -> "$12.3K"</li>
	 * <li>$123,456,789 -> "$123M"</li>
	 * <li>$10,037,400 -> "$10M"</li>
	 * <li>$999,500 -> "$1M"</li>
	 * </ul>
	 * @param value
	 * @return formated string
	 */

        private static string ToShortForm(long value)
        {
            int tenDegree = cutIntoBuffer(value, DigitsInterval, 10);
            int floatDigits = (DigitsInterval - tenDegree%DigitsInterval)%DigitsInterval;
            while (floatDigits > 0 && _buffer[_buffer.Length - 1] == '0')
            {
                _buffer.Length = _buffer.Length - 1;
                floatDigits--;
            }
            if (floatDigits != 0)
            {
                _buffer.Insert(_buffer.Length - floatDigits, DecimalSeparator);
            }
            addThousandDegreeSymbol((tenDegree + DigitsInterval - 1)/DigitsInterval);
            addDollarSymbol();
            return _buffer.ToString();
        }

        private static void PutIntoBuffer(long value)
        {
            _buffer.Length = 0;
            _buffer.Append(value);
        }

        private static int cutIntoBuffer(long value, int digitsLimit, int @base)
        {
            long dividor = 1;
            int degree = 0;
            PutIntoBuffer(value);
            while (_buffer.Length > digitsLimit)
            {
                degree++;
                dividor *= @base;
                var cutValue = (long) Math.Round((float) value/dividor);
                PutIntoBuffer(cutValue);
            }
            return degree;
        }

        private static int cutExtraThousandsFromBuffer()
        {
            int extraThousandsCount = 0;
            bool canReduce;
            do
            {
                canReduce = _buffer.Length > DigitsInterval;
                for (int i = 0; i < DigitsInterval && canReduce; i++)
                    canReduce &= _buffer[_buffer.Length - 1 - i] == '0';
                if (canReduce)
                {
                    _buffer.Length = _buffer.Length - DigitsInterval;
                    extraThousandsCount++;
                }
            } while (canReduce);
            return extraThousandsCount;
        }

        private static void addThousandSeparatorsIntoBuffer()
        {
            int i = _buffer.Length;
            while (i > DigitsInterval)
            {
                i -= DigitsInterval;
                _buffer.Insert(i, ThousandsSeparator);
            }
        }

        private static void addThousandDegreeSymbol(int degreeSymbolIndex)
        {
            if (degreeSymbolIndex > 0)
            {
                _buffer.Append(DegreeSymbols[degreeSymbolIndex - 1]);
            }
        }

        private static void addDollarSymbol()
        {
            _buffer.Insert(0, DollarSymbol);
        }
    }
}