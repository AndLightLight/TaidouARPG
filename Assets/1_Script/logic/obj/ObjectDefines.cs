

using UnityEngine;
using System;


public class ObjectDefines
{
    // ���ﶯ������hash
	public static int HASH_IDLE = Animator.StringToHash("idle");
	public static int HASH_DEAD = Animator.StringToHash("die");
	public static int HASH_RUN = Animator.StringToHash("run");
	public static int HASH_MOVE = Animator.StringToHash("move");

    // ���ֹͣ���룬�����ж�����������ĳ��
    public static float maxStopDistance = 0.15f;

}