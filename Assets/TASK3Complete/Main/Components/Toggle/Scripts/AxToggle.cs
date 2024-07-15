namespace AxGame.Components
{
		using AxGrid.Base;

		using System;
		using System.Collections;
		using System.Collections.Generic;

		using UnityEngine;
		using UnityEngine.UI;

		[RequireComponent(typeof(Toggle))]
		public class AxToggle : MonoBehaviourExt
		{
				/// <summary>
				/// ��� ������������� (���� ������ ������� �� ����� �������)
				/// </summary>
				[Tooltip("��� ������������� (���� ������ ������� �� ����� �������)")]
				[SerializeField] private string m_FieldName = "";
				public string FieldName => m_FieldName;

				/// <summary>
				/// ���� ��� ���������/���������� ���������� ����� ������, ��������� Toggle{m_FieldName}Enable
				/// </summary>
				[Tooltip(" ���� ��� ���������/���������� ���������� ����� ������, ��������� Toggle{m_FieldName}Enable")]
				[SerializeField] private string m_EnableField = "";

				/// <summary>
				/// ����� ���� ������ � ������ ��� ��������, ���� ��������� �� �����, �������� ������
				/// </summary>
				[Tooltip("����� ���� ������ � ������ ��� ��������, ���� ��������� �� �����, �������� ������")]
				[SerializeField] private string m_TargetFieldName = "";

				/// <summary>
				/// ���� ���������� ������ ��������������(>1) ��������� ���� ��� �� ��� ������. �������� ������, ���� �� �����
				/// </summary>
				[Tooltip("���� ���������� ������ ��������������(>1) ��������� ���� ��� �� ��� ������. �������� ������, ���� �� �����")]
				[SerializeField] private string m_GroupToggleField = "";
				public string GroupToggleField => m_GroupToggleField;

				/// <summary>
				/// ������� ��� �������� �� ���������
				/// </summary>
				[Tooltip("������� ��� �������� �� ���������")]
				[field: SerializeField] public bool defaultEnable { get; protected set; } = true;

				private Toggle m_Toggle;
				public Toggle LegacyToggle => m_Toggle;
				public bool IsOn
				{
						get => m_Toggle.isOn;
						set => m_Toggle.isOn = value;
				}
				[OnAwake]
				private void Init()
				{
						m_Toggle = GetComponent<Toggle>();
						m_FieldName = string.IsNullOrEmpty(m_FieldName) ? name : m_FieldName;
						m_EnableField = string.IsNullOrEmpty(m_EnableField) ? $"Toggle{m_FieldName}Enable" : m_EnableField;
						m_Toggle.onValueChanged.AddListener(InternalToggleHandler);
				}



				[OnStart]
				private void Run()
				{
						Model.EventManager.AddAction($"On{m_EnableField}Changed", EnableHandler);
						Model.EventManager.AddAction($"On{m_FieldName}Changed", ModelChangedHandler);
						ModelChangedHandler();
						EnableHandler();
				}
				[OnDestroy]
				private void Destroy()
				{
						Model.EventManager.RemoveAction($"On{m_EnableField}Changed", EnableHandler);
						Model.EventManager.RemoveAction($"On{m_FieldName}Changed", ModelChangedHandler);
						m_Toggle.onValueChanged.RemoveListener(InternalToggleHandler);
				}
				private void EnableHandler()
				{
						if (m_Toggle.interactable != Model.GetBool(m_EnableField, defaultEnable))
								m_Toggle.interactable = Model.GetBool(m_EnableField, defaultEnable);
				}

				private void ModelChangedHandler()
				{
						m_Toggle.onValueChanged.RemoveListener(InternalToggleHandler);
						m_Toggle.isOn = Model.GetBool(m_FieldName, defaultEnable);
						Model.Set(m_TargetFieldName, m_Toggle.isOn);
						m_Toggle.onValueChanged.AddListener(InternalToggleHandler);
				}
				private void InternalToggleHandler(bool value)
				{
						Model.Set(m_FieldName, value);
				}
		}
}