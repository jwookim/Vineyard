using Assets.HeroEditor4D.Common.CharacterScripts;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.ExampleScripts
{
	/// <summary>
	/// An example of how to handle input and make actions.
	/// </summary>
	public class CharacterControls : MonoBehaviour
	{
		public CharacterAnimation CharacterAnimation;

		public void Start()
		{
			CharacterAnimation.Move();
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				CharacterAnimation.Attack();
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				CharacterAnimation.Stand();
			}

			if (Input.GetKeyDown(KeyCode.M))
			{
				CharacterAnimation.Move();
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				TurnLeft();
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				TurnRight();
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				TurnUp();
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				TurnDown();
			}
		}

		public void TurnLeft()
		{
			GetComponent<Character4D>().SetDirection(Vector2.left);
		}

		public void TurnRight()
		{
			GetComponent<Character4D>().SetDirection(Vector2.right);
		}

		public void TurnUp()
		{
			GetComponent<Character4D>().SetDirection(Vector2.up);
		}

		public void TurnDown()
		{
			GetComponent<Character4D>().SetDirection(Vector2.down);
		}
	}
}