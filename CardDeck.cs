using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck
{
	internal class CardDeck
	{
		static void Main()
		{
			const string CommandAddCard = "1";
			const string CommandCountCardsInDeck = "2";
			const string CommandShowCardsInHand = "3";
			const string CommandExit = "4";

			Deck deck = new Deck();
			Player player = new Player();

			bool isProgramActive = true;

			while (isProgramActive)
			{
				Console.WriteLine("Список команд:\n" +
						$"{CommandAddCard} - вытянуть Карту\n" +
						$"{CommandCountCardsInDeck} - количество карт в колоде\n" +
						$"{CommandShowCardsInHand} - показать карты на руке\n" +
						$"{CommandExit} - выход из программы");
				Console.Write("Введите команду: ");
				string input = Console.ReadLine();

				switch (input)
				{
					case CommandAddCard:
						deck.GiveCard(player);
						break;

					case CommandCountCardsInDeck:
						deck.ShowCountCards();
						break;

					case CommandShowCardsInHand:
						player.ShowCardsInHand();
						break;

					case CommandExit:
						isProgramActive = false;
						break;

					default:
						Console.WriteLine("Неизвестная команда!");
						break;
				}

				Console.Write("Нажмите любую кнопку чтобы продолжить: ");
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}

class Card
{
	public Card(string suit, string rank)
	{
		Suit = suit;
		Rank = rank;
	}

	public string Suit { get; private set; }
	public string Rank { get; private set; }
}

class Deck
{
	private Stack<Card> _cards = new Stack<Card>();

	public Deck()
	{
		Initialize();
		Shuffle();
	}

	public void GiveCard(Player player)
	{
		if (_cards.Count > 0)
		{
			player.AddCardToHand(_cards.Pop());
		}
		else
		{
			Console.WriteLine("Карты в колоде закончились!");
		}
	}

	public void ShowCountCards()
	{
		Console.WriteLine(_cards.Count);
	}

	private void Initialize()
	{
		string[] suits = { "Diamonds", "Hearts", "Clubs", "Spades" };
		string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

		foreach (string suit in suits)
		{
			foreach (string rank in ranks)
			{
				_cards.Push(new Card(suit, rank));
			}
		}
	}

	private void Shuffle()
	{
		Random random = new Random();

		int countCardsInDeck = _cards.Count;

		List<Card> cardList = new List<Card>(_cards);

		_cards.Clear();

		for (int i = 0; i < countCardsInDeck; i++)
		{
			int movableCardIndex = random.Next(countCardsInDeck);
			Card movableCard = cardList[movableCardIndex];
			cardList[movableCardIndex] = cardList[i];
			cardList[i] = movableCard;
		}

		foreach (Card card in cardList)
		{
			_cards.Push(card);
		}
	}
}

class Player
{
	private Stack<Card> _hand;
	public Player()
	{
		_hand = new Stack<Card>();
	}

	public void AddCardToHand(Card card)
	{
		_hand.Push(card);

		Console.WriteLine($"Вытянутая карта: {card.Suit} {card.Rank}");
	}

	public void ShowCardsInHand()
	{
		if (_hand.Count > 0)
		{
			foreach (Card card in _hand)
				Console.WriteLine(card.Suit + " " + card.Rank);
		}
		else
		{
			Console.WriteLine("Карт на руке нет");
		}
	}
}