using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // added for TextMeshPro

public class Login : MonoBehaviour
{
	public TMP_InputField usernameField;
	public TMP_InputField passwordField;
	[Tooltip("Scene name to load on successful login")]
	public string sceneToLoad;

	public string correctUsername = "admin";
	public string correctPassword = "password";

	// Optional UI feedback text (TextMeshPro)
	public TMP_Text feedbackText;

	// Call this from a Button OnClick in the Inspector
	public void TryLogin()
	{
		if (usernameField == null || passwordField == null)
		{
			Debug.LogWarning("Username or Password TMP_InputField not assigned in inspector.");
			return;
		}

		string enteredUser = usernameField.text;
		string enteredPass = passwordField.text;

		if (enteredUser == correctUsername && enteredPass == correctPassword)
		{
			if (!string.IsNullOrEmpty(sceneToLoad))
			{
				SceneManager.LoadScene(sceneToLoad);
			}
			else
			{
				Debug.Log("Login successful but sceneToLoad is empty.");
			}
		}
		else
		{
			if (feedbackText != null) feedbackText.text = "Invalid username or password";
			Debug.Log("Invalid username or password");
		}
	}
}
