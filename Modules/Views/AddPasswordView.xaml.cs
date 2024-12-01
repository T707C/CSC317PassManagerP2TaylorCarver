namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    private bool _generatedPassword = false; // was once bool generatedPassword(incase it breaks)

    public AddPasswordView()
    {
        InitializeComponent();
        //Stores the original color of the text boxes. Used to revert a text box back
        //to its original color if it was "highlighted" during input validation.
        baseColorHighlight = (Color)Application.Current.Resources["ErrorEntryHighlightBG"];

        //Determines if a password was generated at least once.
        //generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        //Called when the Cancel button is clicked.
        Navigation.PopAsync();
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
        //Called when the Submit button is clicked for a password manually
        //entered.  (i.e., existing password).

        if (string.IsNullOrWhiteSpace(entryPlatformName.Text) ||
            string.IsNullOrWhiteSpace(entryPlatformUsername.Text) ||
            string.IsNullOrWhiteSpace(entryPassword.Text))
        {
            DisplayAlert("Error!", "All fields must be filled out!", "OK");
            return;
        }

        // Add the password
        App.PasswordController.AddPassword(entryPlatformName.Text, entryPlatformUsername.Text, entryPassword.Text);
        Navigation.PopAsync();
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        //Called when the submit button for a Generated password is clicked.
        if (string.IsNullOrWhiteSpace(entryPlatformName.Text) ||
            string.IsNullOrWhiteSpace(entryPlatformUsername.Text) ||
            !_generatedPassword)
        {
            DisplayAlert("Error", "Platform name, username, and a password are required.", "OK");
            return;
        }
        App.PasswordController.AddPassword(entryPlatformName.Text, entryPlatformUsername.Text, entryPassword.Text);
        Navigation.PopAsync();
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        //Called when the Generate Password button is clicked.
        string generatedPassword = PasswordGeneration.BuildPassword(
            upper_letter: true,
            digit: true,
            req_symbols: "!@#$%",
            min_length: 12);

        // display the password

        entryPassword.Text = generatedPassword;
        _generatedPassword = true;
    }
}