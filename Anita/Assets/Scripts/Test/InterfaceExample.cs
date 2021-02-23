using Anita;
using UnityEngine;

/**
 * 例子
 * 重写相应接口
 * 然后将其挂载在AVGFrame同一物体，AVGFrame会自动注入
 */
public class InterfaceExample : MonoBehaviour, AnitaInput
{
    public bool Input_Next()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X);
    }
}
