using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

public class JXmlManager
{
	#region Properties
	private StreamReader _streamReader = null;
	private StreamWriter _streamWriter = null;
	private string _lastLine = "";
	#endregion

	#region Class Methods
	internal JXmlManager()
	{
		_streamReader = null;
		_streamWriter = null;
		_lastLine = "";
	}
	#endregion

	#region XML File Methods
	internal bool OpenWriteFile(string a_filePath)
	{
		if (_streamWriter != null)
		{
			//TODO DebugMsg
			return false;
		}
		
		_streamWriter = new StreamWriter (a_filePath);
		return true;
	}
	
	internal void CloseWriteFile()
	{
        
        _streamWriter.Close ();
		_streamWriter = null;
	}

	internal bool OpenReadFile(string a_filePath)
	{
		if (_streamReader != null)
		{
			//TODO DebugMsg
			return false;
		}

		_streamReader = new StreamReader (a_filePath);
		_lastLine = "";
		return true;
	}

	internal void CloseReadFile()
	{
		_streamReader.Close ();
		_streamReader = null;
	}
	#endregion

	#region XML Write Methods
	internal void WriteOpenTag(string a_elementName)
	{
        string value = Encrypt("<" + a_elementName + ">");
        _streamWriter.WriteLine (value);
	}
	
	internal void WriteCloseTag(string a_elementName)
	{
        string value = Encrypt("</" + a_elementName + ">");
        _streamWriter.WriteLine (value);
	}

	internal void Write(string a_elementName, bool a_value)
	{
        string value = Encrypt("<" + a_elementName + ">" + a_value.ToString() + "</" + a_elementName + ">");
        _streamWriter.WriteLine (value);
	}

	internal void Write(string a_elementName, int a_value)
	{
        string value = Encrypt("<" + a_elementName + ">" + a_value.ToString() + "</" + a_elementName + ">");
        _streamWriter.WriteLine (value);
	}

	internal void Write(string a_elementName, string a_value)
	{
        string value = Encrypt("<" + a_elementName + ">" + a_value + "</" + a_elementName + ">");
        _streamWriter.WriteLine (value);
	}
	#endregion

	#region XML Read Methods
	internal bool ReadOpenTag(string a_elementName)
	{
		string line = "";
		if (_lastLine != "")
		{
			line = _lastLine;
		}
		else
		{
			line = _streamReader.ReadLine ();
			_lastLine = line;
		}

		if(line != null)
		{
            line = Decrypt(line);
			if(line.Equals("<" + a_elementName + ">"))
			{
				_lastLine = "";
				return true;
			}
			else
			{
				//TODO DebugMsg
				return false;
			}
		}
		else
		{
			//TODO DebugMsg
			return false;
		}
	}
	
	internal bool ReadCloseTag(string a_elementName)
	{
		string line = "";
		if (_lastLine != "")
		{
			line = _lastLine;
		}
		else
		{
			line = _streamReader.ReadLine ();
			_lastLine = line;
		}

		if(line != null)
		{
            line = Decrypt(line);
            if (line.Equals("</" + a_elementName + ">"))
			{
				_lastLine = "";
				return true;
			}
			else
			{
				//TODO DebugMsg
				return false;
			}
		}
		else
		{
			//TODO DebugMsg
			return false;
		}
	}

	internal bool ReadBool(string a_elementName, out bool a_value)
	{
		a_value = false;

		string line = "";
		if (_lastLine != "")
		{
			line = _lastLine;
		}
		else
		{
			line = _streamReader.ReadLine ();
			_lastLine = line;
		}

		if(line != null)
		{
            line = Decrypt(line);
            if (line.StartsWith("<" + a_elementName + ">"))
			{
				int tagLength = ("<" + a_elementName + ">").Length;
				line = line.Substring(tagLength, line.Length-(tagLength*2)-1);
				if(bool.TryParse(line, out a_value))
				{
					_lastLine = "";
					return true;
				}
				else
				{
					//TODO DebugMsg
					return false;
				}
			}
			else
			{
				//TODO DebugMsg
				return false;
			}
		}
		else
		{
			//TODO DebugMsg
			return false;
		}
	}
	
	internal bool ReadInt(string a_elementName, out int a_value)
	{
		a_value = -1;

		string line = "";
		if (_lastLine != "")
		{
			line = _lastLine;
		}
		else
		{
			line = _streamReader.ReadLine ();
			_lastLine = line;
		}

		if(line != null)
		{
            line = Decrypt(line);
            if (line.StartsWith("<" + a_elementName + ">"))
			{
				int tagLength = ("<" + a_elementName + ">").Length;
				line = line.Substring(tagLength, line.Length-(tagLength*2)-1);
				if(int.TryParse(line, out a_value))
				{
					_lastLine = "";
					return true;
				}
				else
				{
					//TODO DebugMsg
					return false;
				}
			}
			else
			{
				//TODO DebugMsg
				return false;
			}
		}
		else
		{
			//TODO DebugMsg
			return false;
		}
	}

	internal bool ReadString(string a_elementName, out string a_value)
	{
		a_value = "";

		string line = "";
		if (_lastLine != "")
		{
			line = _lastLine;
		}
		else
		{
			line = _streamReader.ReadLine ();
			_lastLine = line;
		}

		if(line != null)
		{
            line = Decrypt(line);
            if (line.StartsWith("<" + a_elementName + ">"))
			{
				int tagLength = ("<" + a_elementName + ">").Length;
				line = line.Substring(tagLength, line.Length-(tagLength*2)-1);
				a_value = line;
				_lastLine = "";
				return true;
			}
			else
			{
				//TODO DebugMsg
				return false;
			}
		}
		else
		{
			//TODO DebugMsg
			return false;
		}
	}
    #endregion

    #region Cryptography Methods
    private string Encrypt(string toEncrypt)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("3P0pV7R52FTceta2m2B175K4IZri2ecl");
        // 256-AES key
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
        rDel.Padding = PaddingMode.PKCS7;
        // better lang support
        ICryptoTransform cTransform = rDel.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    private string Decrypt(string toDecrypt)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes("3P0pV7R52FTceta2m2B175K4IZri2ecl");
        // AES-256 key
        byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        // http://msdn.microsoft.com/en-us/library/system.security.cryptography.ciphermode.aspx
        rDel.Padding = PaddingMode.PKCS7;
        // better lang support
        ICryptoTransform cTransform = rDel.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
    #endregion
}
