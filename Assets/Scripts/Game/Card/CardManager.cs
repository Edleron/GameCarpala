namespace Game.Card
{
    using UnityEngine;

    public class CardManager : MonoBehaviour
    {
        public static CardManager Instance { get; private set; }

        private CardController cardController;

        private void Awake()
        {
            Instance = this;
            cardController = GetComponent<CardController>();
        }

        public void StartCarding()
        {
            cardController.InitialCard();
        }
    }
}
