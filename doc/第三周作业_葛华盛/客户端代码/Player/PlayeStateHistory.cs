using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家移动位置的历史信息
public class PlayerMoveStateHistory
{
    // state_arr用于存储前面 size 帧的状态，目前存5帧,[0]为最新的一帧数据
    private Queue<MoveState> state_history_queue;
    private int cur_size = 0;
    private int max_size = 5;

    // 存下玩家由服务器传来的两个逻辑帧的历史状态
    public PlayerMoveStateHistory() {
        state_history_queue = new Queue<MoveState>();
    }

    // 加入一帧到历史记录
    public void add_to_history(float x, float y, float z, float ry) {
        if (cur_size < max_size)
        {
            state_history_queue.Enqueue(new MoveState(x, y, z, ry));
        } 
        else
        {
            state_history_queue.Dequeue();
            state_history_queue.Enqueue(new MoveState(x, y, z, ry));
        }
    }

    public bool isPredictable() {
        return cur_size >= max_size;
    }

    // 影子跟随(插值实现) ===========================================================================================
    //// Transforms to act as start and end markers for the journey.
    //public Transform startMarker;
    //public Transform endMarker;
    //// Movement speed in units/sec.
    //public float speed = 1.0F;
    //// Time when the movement started.
    //private float startTime;
    //// Total distance between the markers.
    //private float journeyLength;

    //void Start()
    //{
    //    // Keep a note of the time the movement started.
    //    startTime = Time.time;
    //    // Calculate the journey length.
    //    journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    //}

    // Follows the target position like with a spring
    //void Update()
    //{
    //    // Distance moved = time * speed.
    //    float distCovered = (Time.time - startTime) * speed;
    //    // Fraction of journey completed = current distance divided by total distance.
    //    float fracJourney = distCovered / journeyLength;
    //    // Set our position as a fraction of the distance between the markers.
    //    transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    //}
    //===========================================================================================


    // 获取方向，位置的预测信息，float数组中分别是x,y,z与ry等信息的预测变化量 比如x方向走0.1
    public MoveState getMoveDeltaPrediction(float x, float y, float z, float ry) 
    {
        if (isPredictable())
        {
            MoveState[] move_state_arr = state_history_queue.ToArray();
            // 加权平均计算，最近的一帧权值最大
            int total_weight = 0;
            MoveState total_delta_weight_product = new MoveState();
            for (int i = 0; i < move_state_arr.Length - 1; i++)
            {
                total_weight += i + 1;
                total_delta_weight_product += (move_state_arr[i + 1] - move_state_arr[i]) * (i + 1);
            }
            Debug.LogError(move_state_arr.Length);
            Debug.LogError(move_state_arr.Length - 1);

            return new MoveState(x,y,z,ry) + total_delta_weight_product / total_weight;
        }
        Debug.LogError("is not predictable");
        return null;
    }

    // 插值
    public static float Lerp(float from, float to, float t)
    {
        // 变化率最大为100%
        if (t > 1)
            t = 1;
        else if (t < 0)
            t = 0;

        float delta = to - from;
        return from + delta * t;
    }
}

// 玩家位置信息
public class MoveState
{
    public MoveState(float x, float y, float z, float ry)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.player_direc = ry;
    }

    public MoveState()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
        this.player_direc = 0;
    }

    public static MoveState operator-(MoveState a, MoveState b) {
        MoveState ms = new MoveState();
        ms.x = a.x - b.x;
        ms.y = a.y - b.y;
        ms.z = a.z - a.z;
        ms.player_direc = a.player_direc - b.player_direc;
        return ms;
    }

    public static MoveState operator +(MoveState a, MoveState b)
    {
        MoveState ms = new MoveState();
        ms.x = a.x + b.x;
        ms.y = a.y + b.y;
        ms.z = a.z + a.z;
        ms.player_direc = a.player_direc + b.player_direc;
        return ms;
    }

    public static MoveState operator *(MoveState a, float b)
    {
        MoveState ms = new MoveState();
        ms.x = a.x * b;
        ms.y = a.y * b;
        ms.z = a.z * b;
        ms.player_direc = a.player_direc * b;
        return ms;
    }

    public static MoveState operator /(MoveState a, float b)
    {
        MoveState ms = new MoveState();
        ms.x = a.x / b;
        ms.y = a.y / b;
        ms.z = a.z / b;
        ms.player_direc = a.player_direc / b;
        return ms;
    }

    public Vector3 get_pos() {
        return new Vector3(x, y, z);
    }

    public float get_facing_direc() {
        return player_direc;
    }

    public float x;
    public float y;
    public float z;
    public float player_direc;
    //public float time_stamp;
}

