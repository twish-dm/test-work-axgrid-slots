namespace Task3.States
{
		using AxGrid.FSM;
		using AxGrid.Model;
		using Task3.Cards;
		[State("ReactiveCollection")]
		public class ReactiveCollectionState : FSMState
		{
				
				[Enter]
				protected void Enter()
				{
						Model.Set("BtnAddCardEnable", true);
						
				}

				[Bind("OnCardSelectd")]
				private void CardHandler(string fieldName, Card card)
				{
						string targetField = "";
						switch(fieldName)
						{
								case "CardList_0":
										targetField = "CardList_1";
										break;
								case "CardList_1":
										targetField = "CardList_0";
										break;
						}
						SendCard(card, fieldName, targetField);
				}
				private void SendCard(Card card, string currentFieldList, string targetFieldList)
				{
						Model.GetList<Card>(currentFieldList).Remove(card);
						Model.GetList<Card>(targetFieldList).Insert(Model.GetList<Card>(targetFieldList).Count/2, card);
						Model.Refresh(currentFieldList);
						Model.Refresh(targetFieldList);
				}
				
				[Bind]
				public void OnBtn(string name)
				{
						switch (name)
						{
								case "AddCard":
										Parent.Change("AddCardCollection");
										break;
						}
				}

		}
}