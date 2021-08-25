using Assets.GameManagement;
using System.Collections;
using UnityEngine;

namespace Assets.MapObject.TriggerHandling
{
    public class Appearing : MonoBehaviour, ITriggerHandler, ITriggerReleasedHandler
    {
        public enum InitState
        {
            Visible,
            Invisible
        }
        public InitState initialState;

        private enum State
        {
            Visible,
            Disappearing,
            Invisible,
            Appearing
        }
        private State state;

        public float animationTime = 0.2f;
        private float animationProgress = 0;
        private bool frozen;

        private new Rigidbody2D rigidbody;
        private SpriteRenderer sprite;
        
        // TODO: crashes if there is no rigidbody or spriteRenderer component
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();

            switch (initialState)
            {
                case InitState.Visible:
                    state = State.Visible;
                    rigidbody.simulated = true;
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
                    break;
                case InitState.Invisible:
                    state = State.Invisible;
                    rigidbody.simulated = false;
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .0f);
                    break;
            }

            frozen = false;
            GameplayController.Stop += Stop;
        }

        private void OnDestroy()
        {
            GameplayController.Stop -= Stop;
        }

        public void Trigger(MonoBehaviour detector, Collider2D triggeredBy)
        {
            if (triggeredBy.gameObject.tag == "Player") 
            {
                if (initialState == InitState.Visible)
                    StartCoroutine("Disappear");
                else
                    StartCoroutine("Appear");
            }
        }

        public void TriggerReleased(MonoBehaviour detector, Collider2D triggeredBy)
        {
            if (triggeredBy.gameObject.tag == "Player")
            {
                if (initialState == InitState.Visible)
                    StartCoroutine("Appear");
                else
                    StartCoroutine("Disappear");
            }
        }

        private void Stop()
        {
            frozen = true;
        }

        private IEnumerator Appear()
        {
            // Init animation
            switch (state)
            {
                case State.Invisible:
                    state = State.Appearing;
                    animationProgress = .0f;
                    break;
                case State.Disappearing:
                    state = State.Appearing;
                    animationProgress = animationTime - animationProgress;
                    break;
                default:
                    yield break;
            }

            // Animation progress
            while(state == State.Appearing)
            {
                if (!frozen)
                {
                    animationProgress += Time.deltaTime;
                    if (animationProgress >= animationTime)
                    {
                        state = State.Visible;
                        break;
                    }
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, animationProgress / animationTime);
                }
                yield return null;
            }

            // interrupted
            if (state != State.Visible)
                yield break;

            // finish animation
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            animationProgress = 0;
            rigidbody.simulated = true;
            yield break;
        }

        private IEnumerator Disappear()
        {
            // Init animation
            switch (state)
            {
                case State.Visible:
                    state = State.Disappearing;
                    animationProgress = .0f;
                    break;
                case State.Appearing:
                    state = State.Disappearing;
                    animationProgress = animationTime - animationProgress;
                    break;
                default:
                    yield break;
            }

            // Animation progress
            while (state == State.Disappearing)
            {
                if (!frozen)
                {
                    animationProgress += Time.deltaTime;
                    if (animationProgress >= animationTime)
                    {
                        state = State.Invisible;
                        break;
                    }
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, (animationTime - animationProgress) / animationTime);
                }
                yield return null;
            }

            // interrupted
            if (state != State.Invisible)
                yield break;

            // finish animation
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            animationProgress = 0;
            rigidbody.simulated = false;
            yield break;
        }
    }
}