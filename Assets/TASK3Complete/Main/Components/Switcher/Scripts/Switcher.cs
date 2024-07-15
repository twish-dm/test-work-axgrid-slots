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
				/// Имя переключателя (если пустое берется из имени объекта)
				/// </summary>
				[Tooltip("Имя переключателя (если пустое берется из имени объекта)")]
				[SerializeField] private string m_FieldName = "";

				/// <summary>
				/// Включен или выключен по умолчанию
				/// </summary>
				[Tooltip("Включен или выключен по умолчанию")]
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