namespace Task3
{
		using AxGrid;
		using AxGrid.Base;
		using AxGrid.FSM;
		using Task3.States;

		public class Main : MonoBehaviourExt
		{

				[OnAwake]
				private void Init()
				{
						Settings.Fsm = new FSM();
						Settings.Fsm.Add(new InitCollectionState());
						Settings.Fsm.Add(new ReactiveCollectionState());
						Settings.Fsm.Add(new AddCardCollectionState());
				}
				[OnStart]
				private void Run()
				{
						Settings.Fsm.Start("InitCollection");
				}
		}
}