using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtility
{
    private static System.Random s_Rander = new System.Random();
    public static bool PointInSector(Vector3 point, Vector3 center, Vector3 dir, float radius, float angle)
    {
        angle *= Mathf.PI / 180f;
        Vector3 dis = point - center;
        float s = MathUtility.CalcDistance2D(point, center);
        if (s < radius)
        {
            float cosAngle = Vector3.Dot(dir, dis) / (s * s);
            if (cosAngle >Mathf.Cos(angle))
            {
                return true;
            }
        }

        return false;
    }
    public static bool PointInRect(Vector3 orig, Vector3 dir, float width, float length, Vector3 pos)
    {
        Vector3 dis = pos - orig;
        Vector3 vertical = Vector3.Cross(dir, Vector3.up);
        float projectWidth = Vector3.Dot(dir.normalized, dis);
        float projectHeight = Vector3.Dot(vertical.normalized, dis);

        return (projectWidth > 0 && projectWidth < length && Mathf.Abs(projectHeight) < (width/2));
    }

    public static bool PointInCircle(Vector3 center, float radius, Vector3 pos)
    {
        return MathUtility.CalcDistance2D(pos, center) < radius;
    }

    // 计算空间上两个点的平面距离
    static public float CalcDistance2D(Vector3 srcPoint, Vector3 destPoint)
    {
        try
        {
            Vector2 dis = new Vector2(destPoint.x - srcPoint.x, destPoint.z - srcPoint.z);
            return dis.magnitude;
        }
        catch (Exception)
        {
            return float.MaxValue;
        }
    }

    // 计算空间上两个点的平面距离的平方
    static public float CalcSqrDistance2D(Vector3 srcPoint, Vector3 destPoint)
    {
        try
        {
            Vector2 dis = new Vector2(destPoint.x - srcPoint.x, destPoint.z - srcPoint.z);
            return dis.sqrMagnitude;
        }
        catch (Exception)
        {
            return float.MaxValue;
        }
    }

    //计算UI上，屏幕坐标之间的距离
    static public float CalcUIScreenDistance2D(Vector3 srcPoint, Vector3 destPoint)
    {
        try
        {
            Vector2 dis = new Vector2(destPoint.x - srcPoint.x, destPoint.y - srcPoint.y);
            return dis.magnitude;
        }
        catch (Exception)
        {
            return float.MaxValue;
        }
    }

    /*static public string MD5Hash(string input)
    {
        //获取加密服务  
        System.Security.Cryptography.MD5CryptoServiceProvider md5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();

        //获取要加密的字段，并转化为Byte[]数组  
        byte[] tempEncrypt = System.Text.Encoding.UTF8.GetBytes(input);

        //加密Byte[]数组  
        byte[] resultEncrypt = md5CSP.ComputeHash(tempEncrypt);

        //将加密后的数组转化为字段(普通加密) 32位
        return Utility.Bytes2HEX(resultEncrypt);
    }

    static public string MD5Hash(int value1, int value2)
    {
        return MD5Hash(value1 + "_" + value2);
    } 

    static public System.Random GlobalRander
    {
        get
        {
            return s_Rander;
        }
    }

    // 获取整数某位的值（位置索引自右向左）
    public static int GetIntegerBit(int data, int index)
    {
        return data >> index & 1;
    }

    // 将整数的某位设置为0或1（位置索引自右向左）
    public static int SetIntegerBit(int data, int index, bool flag)
    {
        if (flag)
            data |= (0x1 << index);
        else
            data &= ~(0x1 << index);
        return data;
    }

    public static bool ArrayContains(int n, int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == n)
                return true;
        }
        return false;
    }*/
}
