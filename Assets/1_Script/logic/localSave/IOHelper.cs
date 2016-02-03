using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace LocalSave
{

	public static class IOHelper
	{

		public static bool IsFileExists(string fileName)
		{
			return File.Exists(fileName);
		}
		
		public static bool IsDirectoryExists(string fileName)
		{
			return Directory.Exists(fileName);
		}
		
		public static void CreateFile(string fileName, string content)
		{
			StreamWriter streamWriter = File.CreateText(fileName);
			streamWriter.Write(content);
			streamWriter.Close();
		}
		
		public static void CreateDirectory(string fileName)
		{
			if (IsDirectoryExists(fileName))
				return;
			Directory.CreateDirectory(fileName);
		}

		public static void SetData(string fileName, object pObject)
		{
			string toSave = SerializeObject(pObject);
			toSave = RijndaelEncrypt(toSave, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
			StreamWriter streamWriter = File.CreateText(fileName);
			streamWriter.Write(toSave);
			streamWriter.Close();
		}

		public static object GetData(string fileName, Type pType)
		{
			StreamReader streamReader = File.OpenText(fileName);
			string data = streamReader.ReadToEnd();
			data = RijndaelDecrypt(data, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
			streamReader.Close();
			return DeserializeObject(data, pType);
		}

		///
		/// Rijndael�����㷨
		///
		/// �����ܵ�����
		/// ��Կ,���ȿ���Ϊ:64λ(byte[8]),128λ(byte[16]),192λ(byte[24]),256λ(byte[32])
		/// iv����,����Ϊ128(byte[16])
		///
		private static string RijndaelEncrypt(string pString, string pKey)
		{
			//��Կ
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
			//��������������
			byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);
			//Rijndael�����㷨
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = keyArray;
			rDel.Mode = CipherMode.ECB;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateEncryptor();
			//���ؼ��ܺ������
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
			return Convert.ToBase64String(resultArray, 0, resultArray.Length);
		}

		///
		/// ijndael�����㷨
		///
		/// �����ܵ�����
		/// ��Կ,���ȿ���Ϊ:64λ(byte[8]),128λ(byte[16]),192λ(byte[24]),256λ(byte[32])
		/// iv����,����Ϊ128(byte[16])
		///
		private static String RijndaelDecrypt(string pString, string pKey)
		{
			//������Կ
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
			//��������������
			byte[] toEncryptArray = Convert.FromBase64String(pString);
			//Rijndael�����㷨
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = keyArray;
			rDel.Mode = CipherMode.ECB;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateDecryptor();
			//���ؽ��ܺ������
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
			return UTF8Encoding.UTF8.GetString(resultArray);
		}

		///
		/// ��һ���������л�Ϊ�ַ���
		///
		/// The object.
		/// ����
		/// ��������
		private static string SerializeObject(object pObject)
		{
			//���л�����ַ���
			string serializedString = string.Empty;
			//ʹ��Json.Net�������л�
			serializedString = JsonConvert.SerializeObject(pObject);
			return serializedString;
		}

		///
		/// ��һ���ַ��������л�Ϊ����
		///
		/// The object.
		/// �ַ���
		/// ��������
		private static object DeserializeObject(string pString, Type pType)
		{
			object deserializedObject = null;
			deserializedObject = JsonConvert.DeserializeObject(pString, pType);
			return deserializedObject;
		}
	}
}
