using System.Collections;
using Modules.PokemonUnity.Backend.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.PokemonUnity.Frontend.UI
{
    public static class BattleAnimator
    {
        public static IEnumerator AnimatePokemon(Image pokemon, Sprite[] animation)
        {
            int frame = 0;
            while (animation != null)
            {
                if (animation.Length > 0)
                {
                    if (frame < animation.Length - 1)
                    {
                        frame += 1;
                    }
                    else
                    {
                        frame = 0;
                    }
                    pokemon.sprite = animation[frame];
                }
                yield return new WaitForSeconds(0.08f);
            }
        }

        public static IEnumerator AnimateOverlayer(RawImage overlay, Texture overlayTex, float verMovement, float hozMovement,
            float time, float fadeTime)
        {
            overlay.gameObject.SetActive(true);
            overlay.texture = overlayTex;
            float fadeStartIncrement = (time - fadeTime) / time;
            float initialAlpha = overlay.color.a;

            float increment = 0;
            while (increment < 1)
            {
                increment += (1 / time) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1;
                }

                overlay.uvRect = new Rect(hozMovement * increment, verMovement * increment, 1, 1);
                if (increment > fadeStartIncrement)
                {
                    float increment2 = (increment - fadeStartIncrement) / (1 - fadeStartIncrement);
                    var color = overlay.color;
                    color = new Color(color.r, color.g, color.b,
                        initialAlpha * (1 - increment2));
                    overlay.color = color;
                }
                yield return null;
            }

            var color1 = overlay.color;
            color1 = new Color(color1.r, color1.g, color1.b, initialAlpha);
            overlay.color = color1;
            overlay.gameObject.SetActive(false);
        }

        /// Slides the Pokemon's Platform across the screen. Takes 1.9f seconds.
        public static IEnumerator SlidePokemon(Image platform, Image pokemon, bool showPokemon, bool fromRight,
            Vector3 destinationPosition)
        {
            var rectTransform = platform.rectTransform;
            var sizeDelta = rectTransform.sizeDelta;
            var localScale = rectTransform.localScale;
            Vector3 startPosition = (fromRight)
                ? new Vector3(171 + (sizeDelta.x * localScale.x / 2f),
                    destinationPosition.y, 0)
                : new Vector3(-171 - (sizeDelta.x * localScale.x / 2f),
                    destinationPosition.y, 0);
            Vector3 distance = destinationPosition - startPosition;

            pokemon.color = new Color(0.25f, 0.25f, 0.25f, 1);

            pokemon.transform.parent.parent.gameObject.SetActive(showPokemon);

            float speed = 1.5f;
            float increment = 0f;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }
                platform.rectTransform.localPosition = startPosition + (distance * increment);
                yield return null;
            }

            speed = 0.4f;
            increment = 0f;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }
                pokemon.color = new Color(0.25f + (0.25f * increment), 0.25f + (0.25f * increment),
                    0.25f + (0.25f * increment), 1);
                yield return null;
            }
        }

        public static IEnumerator SlideTrainer(Image platform, Image trainer, bool isOpponent, bool slideOut)
        {
            Vector3 startPosition = trainer.rectTransform.localPosition;
            //assume !slide out for both
            float destinationPositionX = (isOpponent && !slideOut) ? platform.rectTransform.sizeDelta.x * 0.3f : 0;
            //if it actually was slide out, use the formula to find the hidden position
            if (slideOut)
            {
                destinationPositionX = (171 - Mathf.Abs(platform.rectTransform.localPosition.x)) /
                                       platform.rectTransform.localScale.x + trainer.rectTransform.sizeDelta.x / 2f;
            }
            //flip direction if is player
            if (!isOpponent)
            {
                destinationPositionX = -destinationPositionX;
            }

            Vector3 distance = new Vector3(destinationPositionX, startPosition.y, 0) - startPosition;

            float speed = 128f;
            float time = Mathf.Abs(distance.x) / speed;

            float increment = 0f;
            while (increment < 1)
            {
                increment += (1 / time) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }
                trainer.rectTransform.localPosition = startPosition + (distance * increment);
                yield return null;
            }
        }

        public static IEnumerator AnimatePlayerThrow(Image trainer, Sprite[] throwAnim, bool finishThrow)
        {
            var sprite = trainer.sprite;
            sprite = throwAnim[1];
            yield return new WaitForSeconds(0.4f);
            sprite = throwAnim[2];
            yield return new WaitForSeconds(0.05f);
            sprite = throwAnim[3];
            trainer.sprite = sprite;
            if (finishThrow)
            {
                yield return new WaitForSeconds(0.05f);
                trainer.sprite = throwAnim[4];
            }
        }

        public static IEnumerator ReleasePokemon(Image pokemon)
        {
            var rectTransform = pokemon.rectTransform;
            Vector2 normalSize = rectTransform.sizeDelta;
            rectTransform.sizeDelta = new Vector2(0, 0);
            pokemon.color = new Color(0.812f, 0.312f, 0.312f, 1);

            pokemon.transform.parent.parent.gameObject.SetActive(true);

            float speed = 0.3f;
            float increment = 0f;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }
                pokemon.rectTransform.sizeDelta = normalSize * increment;
                pokemon.color = new Color(0.812f - (0.312f * increment), 0.312f + (0.188f * increment),
                    0.312f + (0.188f * increment), 1);
                yield return null;
            }

            pokemon.rectTransform.sizeDelta = normalSize;
            pokemon.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        public static IEnumerator WithdrawPokemon(Image pokemon)
        {
            Vector2 normalSize = pokemon.rectTransform.sizeDelta;

            float speed = 0.3f;
            float increment = 0f;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }
                pokemon.rectTransform.sizeDelta = normalSize * (1 - increment);
                pokemon.color = new Color(0.5f + (0.312f * increment), 0.5f - (0.188f * increment),
                    0.5f - (0.188f * increment), 1);
                yield return null;
            }
            pokemon.transform.parent.parent.gameObject.SetActive(false);

            pokemon.rectTransform.sizeDelta = normalSize;
            pokemon.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        public static IEnumerator FaintPokemonAnimation(Image pokemon)
        {
            var rectTransform = pokemon.rectTransform;
            Vector3 startPosition = rectTransform.localPosition;
            Vector3 distance = new Vector3(0, rectTransform.sizeDelta.y, 0);

            pokemon.transform.parent.parent.gameObject.SetActive(true);

            float speed = 0.5f;
            float increment = 0f;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }

                //	pokemon.fillAmount = 1-increment;
                pokemon.rectTransform.localPosition = startPosition - (distance * increment);

                yield return null;
            }

            pokemon.transform.parent.parent.gameObject.SetActive(false);

            pokemon.fillAmount = 1f;
            pokemon.rectTransform.localPosition = startPosition;
        }

        public static IEnumerator FadeImages(Image[] images, float time)
        {
            float increment = 0f;
            while (increment < 1)
            {
                increment += (1 / time) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1f;
                }

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1 - increment);
                }
                yield return null;
            }
        }

        public static IEnumerator SlidePartyBarBall(Image ball, float startX, float destinationX, float speed)
        {
            var rectTransform = ball.rectTransform;
            rectTransform.localPosition = new Vector3(startX, rectTransform.localPosition.y, 0);

            float distanceX = destinationX - startX;

            float increment = 0;
            while (increment < 1)
            {
                increment += (1 / speed) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1;
                }

                var rectTransform1 = ball.rectTransform;
                rectTransform1.localPosition = new Vector3(startX + (distanceX * increment),
                    rectTransform1.localPosition.y, 0);
                yield return null;
            }
        }
        
        public static IEnumerator StretchBar(Image bar, float targetSize, float pixelsPerSec = 32f, bool isHP = false, Text hpText = null,
            Text hpTextShadow = null, int endValue = 0)
        {
            float increment = 0f;
            if (pixelsPerSec <= 0)
            {
                pixelsPerSec = 32;
            }
            float startSize = bar.rectTransform.sizeDelta.x;
            float distance = targetSize - startSize;
            float time = Mathf.Abs(distance) / pixelsPerSec;

            int startValue = (hpText != null) ? int.Parse(hpText.text) : 0;
            float valueDistance = endValue - startValue;

            while (increment < 1)
            {
                increment += (1 / time) * Time.deltaTime;
                if (increment > 1)
                {
                    increment = 1;
                }

                var rectTransform = bar.rectTransform;
                rectTransform.sizeDelta = new Vector2(startSize + (distance * increment), rectTransform.sizeDelta.y);

                if (isHP)
                {
                    BattleCalculation.SetHpBarColor(bar, 48f);
                }
                if (hpText != null)
                {
                    hpText.text = "" + (startValue + Mathf.FloorToInt(valueDistance * increment));
                    if (hpTextShadow != null)
                    {
                        hpTextShadow.text = hpText.text;
                    }
                }
                yield return null;
            }
        }

    }
}
