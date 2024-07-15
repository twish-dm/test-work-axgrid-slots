namespace AxGame.Components
{
		using AxGrid.Base;
		using AxGrid.Model;

		using System.Collections;
		using System.Collections.Generic;

		using UnityEngine;

		public class Switcher : MonoBehaviourExt
		{
				/// <summary>
				/// ��� ������������� (���� ������ ������� �� ����� �������)
				/// </summary>
				[Tooltip("��� ������������� (���� ������ ������� �� ����� �������)")]
				[SerializeField] private string m_FieldName = "";

				/// <summary>
				/// ������� ��� �������� �� ���������
				/// </summary>
				[Tooltip("������� ��� �������� �� ���������")]
				[field: SerializeField] public bool defaultEnable { get; protected set; } = true;
				[OnAwake]
				private void Init()
				{
						m_FieldName = string.IsNullOrEmpty(m_FieldName)?name:m_FieldName;
						
				}
				[OnStart]
				private void Run()
				{
						Model.EventManager.AddAction($"On{m_FieldName}Changed", ChangedHandler);
						ChangedHandler();
				}
				[OnDestroy]
				private void Destroy()
				{
						Model.EventManager.RemoveAction($"On{m_FieldName}Changed", ChangedHandler);
				}
				private void ChangedHandler()
				{
						gameObject.SetActive(Model.GetBool(m_FieldName, defaultEnable));
				}
		}
}