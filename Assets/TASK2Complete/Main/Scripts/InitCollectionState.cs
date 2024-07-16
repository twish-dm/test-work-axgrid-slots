using AxGrid.FSM;
using System.Collections.Generic;
using Task3.Cards;

namespace Task3.States
{
		[State("InitCollection")]
		public class InitCollectionState : FSMState
		{
				[Enter]
				private void Enter()
				{
						Model.Set("CardList_0", new List<Card>() { new Card(0, "First card", "icon_0")});
						Model.Set("CardList_1", new List<Card>());
						Model.Set("BtnAddCardEnable", false);

						Parent.Change("ReactiveCollection");
				}
		}
}