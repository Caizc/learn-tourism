using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    // 方向
    public Vector3 Speed;
    // 发射器位置
    public Transform shootTransform;
    // 起始位置
    public Vector3 StartPosition;
    // 结束位置
    public Vector3 EndPosition;
    // 重力加速度
    public float GravitatinalAcceleration = 10;
    // 绘制节点数量
    public int lineNodeNum = 15;
    // 绘制抛物线
    public LineRenderer line;

    Vector3[] positions;

    public Color color;
    Material newMaterial = null;

    // Use this for initialization
    void Start()
    {
        // 初始化线段绘制节点
        positions = new Vector3[lineNodeNum];
        line.numPositions = lineNodeNum;

        newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        line.material = newMaterial;
    }

    void FixedUpdate()
    {
        // 更新发射点位置
        shootTransform.position = this.transform.position;
        shootTransform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x - 30, this.transform.rotation.eulerAngles.y, 0);

        // 当结束点为0或没有结束的时候，将线段恢复为直线
        if (EndPosition == Vector3.zero)
        {
            ResetLine();
            return;
        }

        StartPosition = shootTransform.position;

        // 提前计算出在水平方向和竖直方向上的位移
        float Sx = Vector3.Distance(GetPlaneVector(EndPosition), GetPlaneVector(StartPosition));
        float Sy = StartPosition.y - EndPosition.y;

        // 计算出竖直方向和水平方向上的初速度的比值
        float tanA = -shootTransform.forward.y / Vector3.Distance(Vector3.zero, GetPlaneVector(shootTransform.forward));

        // 计算出运动时间
        float t = Mathf.Sqrt((2 * Sy - 2 * Sx * tanA) / GravitatinalAcceleration);

        // 判断计算是否有异常
        if (float.IsNaN(t))
        {
            ResetLine();
            return;
        }

        // 推导出水平和竖直方向上的初速度
        float Vx = Sx / t;
        float Vy = Vx * tanA;

        // 代入方程绘制出线段
        float firstLineNodeTime = t / lineNodeNum;
        positions[0] = StartPosition;

        for (int i = 1; i < lineNodeNum; i++)
        {
            float xz = GetX(Vx, firstLineNodeTime * (i + 1));
            float y = GetY(firstLineNodeTime * (i + 1), Vy);
            positions[i] = Vector3.Normalize(GetPlaneVector(shootTransform.forward)) * xz + Vector3.down * y + shootTransform.position;
        }

        line.SetPositions(positions);

        newMaterial.SetColor("_Color", Color.green);
    }

    // 将线段重置为一条直线
    void ResetLine()
    {
        for (int i = 0; i < lineNodeNum; i++)
        {
            positions[i] = this.transform.forward * i + this.transform.position;
        }

        line.SetPositions(positions);

        newMaterial.SetColor("_Color", Color.red);
    }

    private Vector3 GetPlaneVector(Vector3 v3)
    {
        return new Vector3(v3.x, 0, v3.z);
    }

    // 计算水平方向位移
    private float GetX(float speed, float time)
    {
        float X = speed * time;
        return X;
    }

    // 计算竖直方向位移
    private float GetY(float time, float speedDownFloat)
    {
        float Y = (float)(speedDownFloat * time + 0.5 * GravitatinalAcceleration * time * time);
        return Y;
    }
}
