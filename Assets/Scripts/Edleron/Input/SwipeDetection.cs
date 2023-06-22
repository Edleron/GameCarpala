namespace Edleron.Input
{

    using System.Collections;
    using Edleron.Events;
    using UnityEngine;

    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField] private float mininumDistance = .2f;
        [SerializeField] private float maximumTime = 1f;
        [SerializeField, Range(0f, 1f)] private float directionThreshold = .9f;

        private InputManager inputManager;

        private Vector2 startPosition;
        private float startTime;
        private Vector2 endPosition;
        private float endTime;
        private bool locked = false; // Swipe algılandı mı?

        private void Awake()
        {
            inputManager = InputManager.Instance;
        }

        private void OnEnable()
        {
            inputManager.OnStartTouch += SwipeStart;
            inputManager.OnEndTouch += SwipeEnd;
            inputManager.OnPressTouch += Presseed;
        }

        private void OnDisable()
        {
            inputManager.OnStartTouch -= SwipeStart;
            inputManager.OnEndTouch -= SwipeEnd;
            inputManager.OnPressTouch -= Presseed;
        }

        private void Presseed(Vector2 position)
        {
            // EventManager.Fire_onTouch();
        }

        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;
            if (!locked)
            {
                locked = true;
                StartCoroutine(DelayedSwipeDetection());
            }

        }

        private IEnumerator DelayedSwipeDetection()
        {
            DetectSwipe();

            yield return new WaitForSeconds(2f); // Bekleme süresi (1 saniye)

            locked = false; // Swipe algılama sıfırla
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= mininumDistance && (endTime - startTime) <= maximumTime)
            {
                Debug.Log("Swipe Detected");
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

                Vector3 direction = endPosition - startPosition;
                Vector2 direction2D = new Vector3(direction.x, direction.y).normalized;
                SwipeDirection(direction2D);
            }
        }

        private void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Up");
                EventManager.Fire_onSwipeUp();
            }
            else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Down");
                EventManager.Fire_onSwipeDown();
            }
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Left");
            }
            else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                // Debug.Log("Swipe Right");
            }
            else
            {
                // Debug.Log("Swipe Error");
            }
        }
    }
}