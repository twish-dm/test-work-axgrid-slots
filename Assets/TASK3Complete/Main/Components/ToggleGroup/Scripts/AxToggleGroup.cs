namespace AxGame.Components
{
		using AxGrid.Base;
		using UniRx;
		using System;
		using System.Collections;
		using System.Collections.Generic;
		using System.Linq;
		using UnityEngine;
		using UnityEngine.UI;

		[RequireComponent(typeof(ToggleGroup))]
		public class AxToggleGroup : MonoBehaviourExt
		{
				/// <summary>
				/// ”казываем поле в модели, дл€ группы Toggle, по умолчанию Toggle{name}Group
				/// </summary>
				[Tooltip("”казываем поле в модели, дл€ группы Toggle")]
				[SerializeField] private string m_GroupToggleField = "";
				private List<AxToggle> m_TogglesList;
				[OnAwake]
				private void Init()
				{
						m_GroupToggleField = string.IsNullOrEmpty(m_GroupToggleField) ? $"Toggle{name}Group" : m_GroupToggleField;
				}
				[OnStart]
				private void Run()
				{
						m_TogglesList = FindObjectsOfType<AxToggle>().Where(x => x.GroupToggleField == m_GroupToggleField).ToList();
						m_TogglesList.ForEach(x => x.LegacyToggle.OnValueChangedAsObservable().Subscribe(isOn =>
						{
								if (isOn)
										ToggleChangeHandler(x);
						}).AddTo(this));

						Model.EventManager.AddAction($"On{m_GroupToggleField}Changed", GroupToggleChangeHandler);
				}

				private void GroupToggleChangeHandler()
				{
						AxToggle changedToggle = m_TogglesList.FirstOrDefault(x => x.FieldName == Model.GetString(m_GroupToggleField));
						if (!changedToggle.IsOn)
								changedToggle.IsOn = true;
				}

				private void ToggleChangeHandler(AxToggle toggle)
				{
						m_TogglesList.ForEach(x =>
						{
								if (x != toggle)
										x.IsOn = false;
						});
						Model.Set(m_GroupToggleField, toggle.FieldName);
				}
		}
}