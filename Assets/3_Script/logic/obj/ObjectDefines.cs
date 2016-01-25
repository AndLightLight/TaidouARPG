

using UnityEngine;
using System;


public class ObjectDefines
{
    // 人物动画变量hash
	public static int HASH_IDLE = Animator.StringToHash("idle");
	public static int HASH_DEAD = Animator.StringToHash("die");
	public static int HASH_RUN = Animator.StringToHash("run");
	public static int HASH_MOVE = Animator.StringToHash("move");

    // 最大停止距离，用来判断是算作到达某点
    public static float maxStopDistance = 0.15f;

}