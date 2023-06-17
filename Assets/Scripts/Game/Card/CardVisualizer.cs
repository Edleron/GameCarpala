namespace Game.Card
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Game.SOLevel;
    using Game.Field;
    using TMPro;

    public class CardVisualizer : MonoBehaviour
    {
        private Animator animator;
        private CardShake cardShake;

        public List<GameObject> stamp = new List<GameObject>();

        [HideInInspector] public float rotationSpeed = 1.0f;
        [HideInInspector] public bool rotationControl = false;

        private void Awake()
        {
            rotationSpeed = 1.0f;
            rotationControl = false;
            animator = GetComponent<Animator>();
            cardShake = GetComponent<CardShake>();
        }

        private void Update()
        {
            if (rotationControl)
            {
                CardRotating();
            }
        }

        public void Shake()
        {
            // Card Shake Effects
            cardShake.StartShake();
        }

        public void SetRotationSpeed()
        {
            // Set Rotation
            rotationSpeed = LevelManager.Instance.GetRotationSpeed();
            Debug.Log(rotationSpeed);
        }

        public void CardInit()
        {
            // Set Stamp
            foreach (var item in stamp)
            {
                item.SetActive(false);
            }

            // Active Card Anim
            CardAnimActiveTrue(); // TODO
        }

        public IEnumerator CardGenerate(int wait)
        {
            yield return new WaitForSeconds(wait);

            var arr = LevelManager.Instance.GetPrepareField();

            var values = LevelManager.Instance.GetFieldValue();

            for (int i = 0; i < arr.Length; i++)
            {
                stamp[arr[i]].SetActive(true);

                // TODO
                Transform stampObje = stamp[arr[i]].transform.GetChild(3);
                TextMeshPro textObje = stampObje.GetComponent<TextMeshPro>();
                FieldRotate fieldRotate = stampObje.GetComponent<FieldRotate>();

                textObje.text = values[i].ToString();
                fieldRotate.FieldRotating(rotationSpeed, rotationControl);
            }
        }

        public IEnumerator CardRotate(float wait, bool value)
        {
            yield return new WaitForSeconds(wait);

            rotationControl = value;
        }

        private void CardRotating()
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }
        // Animation Events
        public void CardAnimActiveTrue()
        {
            animator.SetBool("active", true);
        }
        private void CardAnimActiveFalse()
        {
            animator.SetBool("active", false);
        }
        public void CardAnimPassiveTrue()
        {
            animator.SetBool("passive", true);
        }
        private void CardAnimPassiveFalse()
        {
            animator.SetBool("passive", false);
        }
    }
}