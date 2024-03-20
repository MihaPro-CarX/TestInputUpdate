using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
	public InputActionReference actionRef;

	private Vector2 m_input1;
	private Vector2 m_input2;
	private Vector2 m_input3;
	private Vector2 m_diff2_1;
	private Vector2 m_diff3_2;
	private bool m_trySync;

	private void Start()
	{
		actionRef.action.Enable();
	}

	private Vector2 GetInput(bool trySyncMouse)
	{
		if (trySyncMouse)
		{
			InputSystem.TrySyncDevice(Mouse.current);
		}
		InputSystem.Update();
		return actionRef.action.ReadValue<Vector2>();
	}

	private void Update()
	{
		m_input1 = GetInput(false);

		System.Threading.Thread.Sleep(30);

		m_input2 = GetInput(m_trySync);
		m_diff2_1 = m_input2 - m_input1;
	}

	private void LateUpdate()
	{
		System.Threading.Thread.Sleep(30);

		m_input3 = GetInput(m_trySync);
		m_diff3_2 = m_input3 - m_input2;
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 2, Screen.height / 2));


		GUILayout.Label("Please move mouse");
		
		GUILayout.Space(20);

		GUILayout.Label($"Input1 {m_input1} (Update 1)");

		GUILayout.Label($"   diff {m_diff2_1} - {m_diff2_1.magnitude}");

		GUILayout.Label($"Input2 {m_input2} (Update 2)");

		GUILayout.Label($"   diff {m_diff3_2} - {m_diff3_2.magnitude}");

		GUILayout.Label($"Input3 {m_input3} (LateUpdate)");
		
		GUILayout.Space(20);
		
		m_trySync = GUILayout.Toggle(m_trySync, "TrySync before Update");

		GUILayout.EndArea();
	}

}
