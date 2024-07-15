namespace AxGame.Components.Lists
{
		using AxGrid.Base;
		using UnityEngine;
		using UnityEngine.UI;

		public class ListItemRenderer : MonoBehaviourExt
		{
				[SerializeField] protected Image m_Icon;
				[SerializeField] protected int m_Id;
				public RectTransform rectTransform => (RectTransform)transform;
				private ListItemData m_Data;
				virtual public void SetData(ListItemData data)
				{
						m_Data = data;
						m_Icon.sprite = data.Icon;
						m_Id = data.Id;
				}
		}
}