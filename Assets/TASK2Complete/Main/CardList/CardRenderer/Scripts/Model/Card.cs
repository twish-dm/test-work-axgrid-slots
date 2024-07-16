namespace Task3.Cards
{
		using SmartFormat;
		public class Card
		{
				public int value;
				public string cardName;
				public string spriteName;

				public  Card(int value, string cardName, string spriteName)
				{
						this.value = value;
						this.cardName = cardName;
						this.spriteName = spriteName;
				}

				public override string ToString()
				{
						return Smart.Format("[value = {0}, card = {1}, sprite = {2}]", value, cardName, spriteName);
				}
		}

		
}