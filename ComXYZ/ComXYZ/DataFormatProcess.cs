using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ComXYZ
{
    public class DataFormatProcess
    {

        #region///数据处理
        //将十六进制数转换为ASCII代码
        private byte HexToAsciiHigh(byte BVal)
        {
            byte h;
            h = (byte)(BVal & (byte)0xf0);
            h >>= 4;
            if (h >= 0 && h <= 9) h = (byte)((byte)0x30 + h);
            if (h >= 10 && h <= 15)
            {
                h = (byte)(h - (byte)0x0A);
                h = (byte)((byte)0x41 + h);
            }
            return h;
        }
        //将十六进制数转换为ASCII代码
        private byte HexToAsciiLow(byte BVal)
        {
            byte l;
            l = (byte)(BVal & (byte)0x0f);
            if (l >= 0 && l <= 9) l = (byte)((byte)0x30 + l);
            if (l >= 10 && l <= 15)
            {
                l = (byte)(l - (byte)0x0A);
                l = (byte)((byte)0x41 + l);
            }
            return l;
        }
        //将ASCII代码转换为十六进制数
        private byte AsciiToHex(byte Val)
        {
            byte temp = 0;
            temp = Val;
            if (temp >= 0x30 && temp <= 0x39) temp &= (byte)0x0f;
            else if (temp >= 0x41 && temp <= 0x46) temp -= (byte)0x37;//大写ABCDEF转换成16进制数值
            else if (temp >= 0x61 && temp <= 0x66) temp -= (byte)0x57;//小写abcdef转换成16进制数值
            else temp = 0;
            return temp;
        }
        //检测字符串是否包含非十六进制字符
        private string HexStringRemoveBlack(string inputStr)
        {
            string OutStr="";
            for (int i = 0; i < inputStr.Length; i++)
            {
                if (inputStr[i] != ' ' && inputStr[i] != '\r' && inputStr[i] != '\n')
                {
                    OutStr += inputStr[i].ToString();
                }
            }
            return OutStr;
        }
        //检测字符串是否包含非十六进制字符
        public bool IsHexStringContainAscii(string inputStr)
        {
            bool Result = false;
            String WarnMessage = "包含非十六进制字符:";
            for (int i = 0; i < inputStr.Length; i++)
            {
                if (inputStr[i] != ' ')
                { 
                    if (Uri.IsHexDigit(inputStr[i]) == false)
                    {
                        WarnMessage += inputStr[i].ToString();
                        MessageBox.Show(WarnMessage, "提示");
                        Result = true;
                        break;
                    }                
                }
            }
            return Result;
        }

        //从输入字符串中提取仅包含HEX数据的字符串(字母转换为大写)
        private String GetHexString(string InputStr)
        {
            string result = "failure";
            if (IsHexStringContainAscii(InputStr) == true) return result;
            else
                return InputStr.ToUpper();
        }
        //先输入的HEX字符串字符之间插入空格
        public String HexStringInsertBlank(string OnlyHexCharInputStr)
        {
            string strHexWithBlank = "";
            for (Int32 i = 0; i < OnlyHexCharInputStr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    strHexWithBlank += ' ';
                }
                strHexWithBlank += OnlyHexCharInputStr[i];
            }
            strHexWithBlank = strHexWithBlank.Trim();
            return strHexWithBlank;
        }

        private byte GetHexValFromChar(char InputChar)
        {
            byte ByteVal = 0;
            string temp = "";
            string str1 = "";
            byte CharVal = 0;
            temp = Uri.HexEscape(InputChar);
            for (int j = 1; j < 3; j++) str1 += temp[j];
            CharVal = Convert.ToByte(str1);
            if (CharVal >= 30 && CharVal <= 39) ByteVal = (byte)(CharVal - 30);
            if (CharVal >= 41 && CharVal <= 46) ByteVal = (byte)(CharVal - 31);
            return ByteVal;
        }

        private byte ConverByteToChar(byte dat)
        {
            byte bTemp = 0;
            if (dat >= 0 && dat <= 9) bTemp = (byte)(30 + dat);
            if (dat >= 10 && dat <= 15) bTemp = (byte)(41 + dat - 10);
            return bTemp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputDataArr"></param>
        /// <returns></returns>
        /// 这个函数效率比较低
        public string ConvertHexToString2(byte[] InputDataArr)
        {
            string OutputStr = "";
            Int32 i = 0;
            byte CharVal1 = 0;
            byte bTemp1 = 0;
            string StrTemp = "%";
            int index = 0;
            for (i = 0; i < InputDataArr.Length; i++)
            {
                CharVal1 = InputDataArr[i];
                CharVal1 >>= 4;
                bTemp1 = ConverByteToChar(CharVal1);
                StrTemp = "%";
                StrTemp += bTemp1.ToString();
                index = 0;
                OutputStr += Uri.HexUnescape(StrTemp, ref index);

                CharVal1 = InputDataArr[i];
                CharVal1 &= (byte)0x0F;
                bTemp1 = ConverByteToChar(CharVal1);
                StrTemp = "%";
                StrTemp += bTemp1.ToString();
                index = 0;
                OutputStr += Uri.HexUnescape(StrTemp, ref index);
            }
            return OutputStr;
        }
        //改用这个效率更高
        public string ConvertHexToString(byte[] InputDataArr)
        {
            string OutputStr = "";
            Int32 i = 0;
            for (i = 0; i < InputDataArr.Length; i++)
            {
                OutputStr += InputDataArr[i].ToString("X2");
            }
            return OutputStr;
        }

        public string ConvertHexToString(byte[] InputDataArr,int Length)
        {
            string OutputStr = "";
            Int32 i = 0;
            for (i = 0; i < Length; i++)
            {
                OutputStr += InputDataArr[i].ToString("X2");
            }
            return OutputStr;
        }
        //转换字符串为HEX的数组
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputStr"></param>
        /// <returns></returns>
        public byte[] ConvertStringToHex(string InputStr)
        {
            string strHexchar = GetHexString(HexStringRemoveBlack(InputStr));
            if (strHexchar.Equals("failure") ==false)
            {
                if (strHexchar.Length % 2 != 0) strHexchar += "0";//单数个的字符时最后增加一个0
                byte[] bArr = new byte[strHexchar.Length / 2];
                Int32 ByteCnt = 0;
                byte ByteVal = 0;
                for (Int32 i = 0; i < strHexchar.Length; i++)
                {
                    ByteVal = GetHexValFromChar(strHexchar[i++]);
                    ByteVal <<= 4;
                    bArr[ByteCnt++] = (byte)(ByteVal + GetHexValFromChar(strHexchar[i]));
                }
                return bArr;
            }
            else
            {
                byte[] bArr = new byte[2];
                return bArr;
            }

        }
        //输入为16进制数据字符形式字符串
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceStr"></param>
        /// <param name="ByteNumber"></param>
        /// <returns></returns>
        public byte[] ConverStringToHex(string SourceStr, ref int ByteNumber)
        {
            byte[] byteBuff = new byte[1024];
            byte[] byteTemp = new byte[2];
            char temp = ' ';
            int j = 0, k = 0;
            for (int i = 0; i < SourceStr.Length; i++)
            {
                temp = SourceStr.ElementAtOrDefault<char>(i);
                if (temp != ' ')
                {
                    byteTemp[k++] = Convert.ToByte(temp);
                    if (k >= 2)
                    {
                        byteBuff[j++] = (byte)((AsciiToHex(byteTemp[0]) << (byte)4) + AsciiToHex(byteTemp[1]));
                        k = 0;
                        ByteNumber++;
                    }
                    else if (i == SourceStr.Length - 1)               //单数字符个数的时候处理最后一个并且作为第四位的值
                    {
                        byteBuff[j++] = AsciiToHex(byteTemp[0]);
                        k = 0;
                        ByteNumber++;
                    }
                }
            }
            return byteBuff;
        }
        #endregion
    }
}
