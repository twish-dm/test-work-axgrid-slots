namespace Task3.States
{
		using AxGrid.FSM;
		using SmartFormat;
		using Task3.Cards;
		[State("AddCardCollection")]
		internal class AddCardCollectionState : FSMState
		{
				private int m_CardCount = 0;
				[Enter]
				public void Enter()
				{
						Model.Set("BtnAddCardEnable", false);
						AddCard();
						Parent.Change("ReactiveCollection");
				}
				private void AddCard()
				{
						Model.GetList<Card>("CardList_0").Add(new Card(UnityEngine.Random.Range(0, int.MaxValue), Smart.Format("Card {0}", m_CardCount), Smart.Format("icon_{0}", m_CardCount % 5)));
						Model.Refresh("CardList_0");
						m_CardCount++;
				}
		}
}